using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class State
{
    public enum STATE
    {
        IDLE,
        CHASE,
        ATTACK,
        DEAD,
        FAKE_DEAD,
        CHASE_CORPSE,
        EAT_CORPSE,
        DEAMBULAR
    };

    public enum EVENT
    {
        ENTER,
        UPDATE,
        EXIT
    };

    public STATE name;
    protected EVENT stage;
    protected GameObject zombi;
    protected Animator animator;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;
    protected EnemyManager zombiManager;

    private float visionDistance = 50.0f;
    private float visionAngle = 60f;
    private float attackDistance = 7.0f;

    public State(GameObject _zombi, Animator _animator, Transform _player, NavMeshAgent _agent, EnemyManager _zombiManager)
    {
        zombi = _zombi;
        animator = _animator;
        player = _player;
        agent = _agent;
        zombiManager = _zombiManager;
    }

    public virtual void Enter()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Update()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Exit()
    {
        stage = EVENT.EXIT;
    }

    public State Process()
    {
        if (stage == EVENT.ENTER)
        {
            Enter();
        }

        if (stage == EVENT.UPDATE)
        {
            Update();
        }
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - zombi.transform.position;
        float angle = Vector3.Angle(direction, zombi.transform.forward);

        return (direction.magnitude < visionDistance && angle < visionAngle);
    }

    public bool CanAttackPlayer()
    {
        bool isNotOnCooldown = (zombiManager.lastAttackTime.Equals(null) || Math.Abs(zombiManager.lastAttackTime - Time.time) > zombiManager.delayBetweenAttacks);
        return (/*CanSeePlayer() && */zombiManager.playerInReach && isNotOnCooldown );
    }

    public void ChangeState(State _newState)
    {
        nextState = _newState;
        stage = EVENT.EXIT;
    }

    public bool IsLowHealth()
    {
        return ( zombiManager.health <= (zombiManager.maxHealth * 0.3f) ) && !zombiManager.alreadyFakedDead;
    }

    public State lowHealthState()
    {
        State newState;
        if (zombiManager.zombiWillFakeDead)
        {
            if (zombiManager.alreadyFakedDead)
            {
                newState = zombiManager.gameState;
            }
            else
            {
                newState = new FakeDeadState(zombi, animator, player, agent, zombiManager);
            }
        }
        else
        {
            newState = new ChaseCorpseState(zombi, animator, player, agent, zombiManager);
        }

        return newState;
    }
    
}
