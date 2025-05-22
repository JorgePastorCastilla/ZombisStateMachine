using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    public AttackState(GameObject _zombi, Animator _animator, Transform _player, NavMeshAgent _agent, EnemyManager _zombiManager) : base(_zombi, _animator, _player, _agent, _zombiManager)
    {
        name = STATE.ATTACK;
    }
    
    public override void Enter()
    {
        animator.SetTrigger("isAttacking");
        base.Enter();
    }

    public override void Update()
    {
        player.GetComponent<PlayerManager>().Hit(zombiManager.damage);
        zombiManager.lastAttackTime = Time.time;

        if ( IsLowHealth() )
        {
            nextState = new FakeDeadState(zombi,animator, player, agent, zombiManager);
            stage = EVENT.EXIT;
        }else if (CanSeePlayer())
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
