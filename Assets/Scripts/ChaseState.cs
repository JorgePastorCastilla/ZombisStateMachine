using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    public ChaseState(GameObject _zombi, Animator _animator, Transform _player, NavMeshAgent _agent) : base(_zombi, _animator, _player, _agent)
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
        //Si detecta al jugador CAMBIAR
        if ( false /*idle condition*/)
        {
            nextState = new IdleState(zombi, animator, player, agent);
            stage = EVENT.EXIT;
        }
        base.Update();
    }

    public override void Exit()
    {
        animator.SetBool("isRunning", false);
        base.Exit();
    }
}
