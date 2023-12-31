using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateIdle : EnemyStateBase
{
    public EnemyStateIdle(EnemyBase enemy) : base(enemy)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        m_enemyState.IsIdle = true;
    }
    public override void ExitState()
    {
        base.ExitState();
        m_enemyState.IsIdle = false;
    }
    public override void Update()
    {
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
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
