using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateGrow : EnemyStateBase
{
    protected IEnemyGrow m_enemyGrow;
    protected float m_growSpeed ;
    protected float m_beginTime ;
    public EnemyStateGrow(EnemyBase enemy) : base(enemy)
    {
        m_enemyGrow = enemy as IEnemyGrow;
    }
    public override void EnterState()
    {
        base.EnterState();
        m_enemyState.IsGrow = true;

        m_enemyGrow.enemyCollider.isTrigger = true;

        m_growSpeed = (m_enemyGrow.grow_endSize - m_enemyGrow.enemySize) / m_enemyGrow.grow_time;
        m_beginTime = Time.time;
    }
    public override void ExitState()
    {
        base.ExitState();
        m_enemyGrow.enemyCollider.isTrigger = false;
        m_enemyGrow.grow_endSize = m_enemyGrow.enemySize;
        m_enemyState.IsGrow = false;
    }
    public override void Update()
    {
        // Debug.LogWarning("grow update");
        base.Update();

        if(m_enemyState.IsMerge)
        {
            m_enemyState.enemyStateMachine.ChangeEnemyState(m_enemyState.enemyStateMerge);
            return;
        }
        if(m_enemyState.IsFly)
        {
            m_enemyState.enemyStateMachine.ChangeEnemyState(m_enemyState.enemyStateFly);
            return;
        }
        
        m_enemyGrow.enemySize += Time.deltaTime * m_growSpeed;
        m_enemyGrow.ChangeEnemySize(m_enemyGrow.enemySize);

        if(Time.time - m_beginTime > m_enemyGrow.grow_time)
        {
            m_enemyGrow.ChangeEnemySize(m_enemyGrow.grow_endSize);
            m_enemyState.enemyStateMachine.ChangeEnemyState(m_enemyState.enemyStateIdle);
            return;
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
