using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    protected EnemyStateBase m_enemyState ;

    public void initialization(EnemyStateBase begEnemyState)
    {
        m_enemyState = begEnemyState;
        m_enemyState.EnterState();
    }

    public void ChangeEnemyState(EnemyStateBase enemyState)
    {
        m_enemyState.ExitState();
        m_enemyState = enemyState;
        m_enemyState.EnterState();
    }

    public void Update()
    {
        m_enemyState.Update();
    }

    public void FixedUpdate()
    {
        m_enemyState.FixedUpdate();
    }
}
