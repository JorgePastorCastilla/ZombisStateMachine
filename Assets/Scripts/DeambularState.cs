using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeambularState : State
{
    public DeambularState(GameObject _zombi, Animator _animator, Transform _player, NavMeshAgent _agent, EnemyManager _zombiManager) : base(_zombi, _animator, _player, _agent, _zombiManager)
    {
        name = STATE.DEAMBULAR;
    }
    private float stateTime = 5.0f;
    private float currentTime = 0.0f;
    
    private Vector3 destinationPoint;
    
    public override void Enter()
    {
        animator.SetBool("isRunning", true);

        SetRandomDestination();
        
        base.Enter();
    }

    public override void Update()
    {
        currentTime += Time.deltaTime;
        
        if ( IsLowHealth())
        {
            // nextState = new FakeDeadState(zombi,animator, player, agent, zombiManager);
            nextState = lowHealthState();
            stage = EVENT.EXIT;
        }else if (currentTime > stateTime)
        {
            if ( CanAttackPlayer() )
            {
                nextState = new IdleState(zombi, animator, player, agent, zombiManager);
                stage = EVENT.EXIT;
            }else if ( !CanSeePlayer() )
            {
                nextState = new IdleState(zombi, animator, player, agent, zombiManager);
                stage = EVENT.EXIT;
            }
        }else if (Vector3.Distance(zombi.transform.position, destinationPoint) < 1f || !agent.hasPath)
        {
            SetRandomDestination();
        }
               
    }

    public override void Exit()
    {
        agent.destination = zombi.transform.position;
        animator.SetBool("isRunning", false);
        base.Exit();
    }

    public void SetRandomDestination()
    {
        float x = Random.Range(-2f, 2f);
        float z = Random.Range(-2f, 2f);
        
        destinationPoint = new Vector3(zombi.transform.position.x + x, 0f, zombi.transform.position.z + z);
        agent.destination = destinationPoint;
    }
}
