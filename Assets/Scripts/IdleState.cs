using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : State
{
    public IdleState(GameObject _zombi, Animator _animator, Transform _player, NavMeshAgent _agent) : base(_zombi, _animator, _player, _agent)
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
        
    }

    public override void Exit()
    {
        animator.ResetTrigger("isIdle");
        base.Exit();
    }
}
