using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        IDLE,
        CHASE,
        ATTACK,
        DEAD,
        FAKE_DEAD,
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

    private float visionDistance = 10.0f;
    private float visionAngle = 30f;
    private float attackDistance = 7.0f;

    public State(GameObject _zombi, Animator _animator, Transform _player, NavMeshAgent _agent)
    {
        zombi = _zombi;
        animator = _animator;
        player = _player;
        agent = _agent;
    }

    public virtual void Enter()
    {
        stage = EVENT.ENTER;
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
    
}
