using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateBase 
{
    protected EnemyBase m_enemy ;
    protected IEnemyState m_enemyState;
    public EnemyStateBase(EnemyBase enemy)
    {
        m_enemy = enemy;
        m_enemyState = enemy as IEnemyState;
    }
    public virtual void EnterState()
    {

    }

    public virtual void Update()
    {
        // Debug.LogWarning("enemystatebase update");
    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void ExitState()
    {

    }
}
