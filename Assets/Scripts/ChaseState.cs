using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    public ChaseState(GameObject _zombi, Animator _animator, Transform _player, NavMeshAgent _agent, EnemyManager _zombiManager) : base(_zombi, _animator, _player, _agent, _zombiManager)
    {
        name = STATE.CHASE;
    }
    
    public override void Enter()
    {
        animator.SetBool("isRunning", true);
        base.Enter();
    }

    public override void Update()
    {
        agent.destination = player.position;
        if ( IsLowHealth())
        {
            // nextState = new FakeDeadState(zombi,animator, player, agent, zombiManager);
            nextState = lowHealthState();
            stage = EVENT.EXIT;
        }else if ( CanAttackPlayer() )
        {
            nextState = new IdleState(zombi, animator, player, agent, zombiManager);
            stage = EVENT.EXIT;
        }else if ( !CanSeePlayer() )
        {
            nextState = new IdleState(zombi, animator, player, agent, zombiManager);
            stage = EVENT.EXIT;
        }
        
    }

    public override void Exit()
    {
        agent.destination = zombi.transform.position;
        animator.SetBool("isRunning", false);
        base.Exit();
    }
}
