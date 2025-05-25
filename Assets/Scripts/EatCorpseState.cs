using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EatCorpseState : State
{
    GameObject corpse;
    public EatCorpseState(GameObject _zombi, Animator _animator, Transform _player, NavMeshAgent _agent, EnemyManager _zombiManager, GameObject _corpse) : base(_zombi, _animator, _player, _agent, _zombiManager)
    {
        corpse = _corpse;
        name = STATE.EAT_CORPSE;
    }
    
    public override void Enter()
    {
        animator.SetTrigger("isAttacking");
        base.Enter();
    }

    public override void Update()
    {
        // player.GetComponent<PlayerManager>().Hit(zombiManager.damage);
        zombiManager.lastAttackTime = Time.time;
        GameObject.Destroy(corpse, 2f);
        zombiManager.health += 50f;
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

    public override void Exit()
    {
        base.Exit();
    }
    
}
