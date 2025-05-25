using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{

    public GameObject player;
    public Animator enemyAnimator;
    public GameManager gameManager;
    
    public float damage = 20f;
    public float maxHealth = 100f;
    public float health;

    public Image HpBar;
    
    // Animacio i millora del xoc
    public bool playerInReach = false;
    public float attackDelayTimer = 0f;
    public float howMuchEarlierStartAttackAnimation = 1f; // inicialitzarem a 1f
    public float delayBetweenAttacks = 1.6f; // inicialitzarem a 0.6f
    public float lastAttackTime;
    public bool alreadyFakedDead = false;
    public State gameState;

    public bool zombiWillFakeDead = false;
    

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        gameState = new IdleState(this.gameObject, enemyAnimator, player.transform, transform.GetComponent<NavMeshAgent>(), this);
        lastAttackTime = Time.time;
        float randomNumber = Random.Range(0f, 1f);
        zombiWillFakeDead = randomNumber <= 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        gameState = gameState.Process();
    }
    
    // Detectar la col·lisió
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == player)
        {
            playerInReach = true;
        }else if (collision.gameObject.tag == "zombi" && health <= 0)
        {
            EnemyManager zombi = collision.gameObject.GetComponent<EnemyManager>();
            if (zombi.gameState.name == State.STATE.CHASE_CORPSE)
            {
                EatCorpseState newState = new EatCorpseState(zombi.gameObject, zombi.enemyAnimator, zombi.player.transform, zombi.gameObject.GetComponent<NavMeshAgent>(), zombi, this.gameObject);
                zombi.gameState.ChangeState(newState); 
            }
        }
    }

    /*private void OnCollisionStay(Collision other)
    {
        if (playerInReach)
        {
            attackDelayTimer += Time.deltaTime;
            if (attackDelayTimer >= delayBetweenAttacks - howMuchEarlierStartAttackAnimation && attackDelayTimer <= delayBetweenAttacks )
            {
                enemyAnimator.SetTrigger("isAttacking");
            }

            if (attackDelayTimer >= delayBetweenAttacks)
            {
                player.GetComponent<PlayerManager>().Hit(damage);
                attackDelayTimer = 0;
            }
        }
    }*/

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject == player)
        {
            playerInReach = false;
            attackDelayTimer = 0;
        }
    }

    public void Hit(float damage)
    {
        health -= damage;
        Debug.Log("Enemy got hit!");
        HpBar.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            // Destrium a l'enemic quan la seva salut arriba a zero
            // feim referència a ell amb la variable gameObject, que fa referència al GO
            // que conté el componentn EnemyManager
            gameManager.enemiesAlive--;
            // Destroy(gameObject);
            enemyAnimator.SetTrigger("isDead");
            enemyAnimator.SetBool("isRunning", false);
            Destroy(gameObject,30f);
            Destroy(GetComponent<NavMeshAgent>());
            Destroy(GetComponent<EnemyManager>());
            // Destroy(GetComponent<CapsuleCollider>());
            foreach (var capsuleCollider in gameObject.GetComponents<CapsuleCollider>())
            {
                Destroy(capsuleCollider);
            }
            

        }

    }

}
