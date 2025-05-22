using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FakeDeadState : State
{
    private float fakeTime = 5.0f;
    private float currentTime = 0.0f;
    // Start is called before the first frame update
    public FakeDeadState(GameObject _zombi, Animator _animator, Transform _player, NavMeshAgent _agent, EnemyManager _zombiManager) : base(_zombi, _animator, _player, _agent, _zombiManager)
    {
        name = STATE.FAKE_DEAD;
    }

    public override void Enter()
    {
        animator.SetTrigger("isDead");
        zombiManager.alreadyFakedDead = true;
        base.Enter();
    }

    public override void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= fakeTime)
        {
            //LOGICA PARA LOS DIFERENTES CAMBIOS DE ESTADO
            if (CanAttackPlayer())
            {
                nextState = new AttackState(zombi,animator, player, agent, zombiManager);
                stage = EVENT.EXIT;
            }else if (CanSeePlayer())
            {
                nextState = new ChaseState(zombi,animator, player, agent, zombiManager);
                stage = EVENT.EXIT;
            }
            else
            {
                nextState = new IdleState(zombi,animator, player, agent, zombiManager);
                stage = EVENT.EXIT;
            }
        }
        
    }

    public override void Exit()
    {
        animator.ResetTrigger("isDead");
        base.Exit();
    }
}
