using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : State
{
    public IdleState(GameObject _zombi, Animator _animator, Transform _player, NavMeshAgent _agent, EnemyManager _zombiManager) : base(_zombi, _animator, _player, _agent, _zombiManager)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        animator.SetTrigger("isIdle");
        base.Enter();
    }

    public override void Update()
    {
        //LOGICA PARA LOS DIFERENTES CAMBIOS DE ESTADO
        if ( IsLowHealth())
        {
            // nextState = new FakeDeadState(zombi,animator, player, agent, zombiManager);
            nextState = lowHealthState();
            stage = EVENT.EXIT;
        }else if (CanAttackPlayer())
        {
            nextState = new AttackState(zombi,animator, player, agent, zombiManager);
            stage = EVENT.EXIT;
        }else if (CanSeePlayer())
        {
            nextState = new ChaseState(zombi,animator, player, agent, zombiManager);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("isIdle");
        base.Exit();
    }
}
