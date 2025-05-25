using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class ChaseCorpseState : State
{
    public ChaseCorpseState(GameObject _zombi, Animator _animator, Transform _player, NavMeshAgent _agent, EnemyManager _zombiManager) : base(_zombi, _animator, _player, _agent, _zombiManager)
    {
        name = STATE.EAT_CORPSE;
    }
    
    public override void Enter()
    {
        animator.SetBool("isRunning", true);
        base.Enter();
    }

    public override void Update()
    {
        GameObject closestZombi = GetClosestCorpse();
        if (closestZombi != null)
        {
            agent.destination = closestZombi.transform.position;
            if (Vector3.Distance(zombi.transform.position, agent.destination) < 1.1f)
            {
                nextState = new EatCorpseState(zombi, animator, player, agent, zombiManager, closestZombi);
                stage = EVENT.EXIT;
            }
        }
        else
        {
            if (CanSeePlayer())
            {
                nextState = new ChaseState(zombi, animator, player, agent, zombiManager);
                stage = EVENT.EXIT;
            }
            else
            {
                nextState = new IdleState(zombi, animator, player, agent, zombiManager);
                stage = EVENT.EXIT;
            }
        }
        

        
    }

    public override void Exit()
    {
        animator.SetBool("isRunning", false);
        base.Exit();
    }

    [CanBeNull]
    public GameObject GetClosestCorpse()
    {
        List<GameObject> zombiList = new List<GameObject>();
        
        GameObject[] zombis = GameObject.FindGameObjectsWithTag("zombi");
        
        foreach (GameObject zombi in zombis)
        {
            
            if (zombi.GetComponent<EnemyManager>() == null)
            {
                zombiList.Add(zombi);
            }
        }

        
        float closestDistance = float.MaxValue;
        GameObject closestZombi = null;
        foreach (GameObject zombi in zombiList)
        {
            float distance = Vector3.Distance(zombi.transform.position, zombiManager.gameObject.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestZombi = zombi;
            }
        }
        
        return closestZombi;
    }
    
}
