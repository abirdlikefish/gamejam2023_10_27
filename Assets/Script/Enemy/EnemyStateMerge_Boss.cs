using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMerge_Boss : EnemyStateBase
{
    protected IEnemyMerge m_enemyMerge;
    public EnemyStateMerge_Boss(EnemyBase enemy) : base(enemy)
    {
        m_enemyMerge = enemy as IEnemyMerge;
    }

    public override void EnterState()
    {
        base.EnterState();
        m_enemyState.IsMerge = true;

        m_enemyMerge.enemyCollider.isTrigger = true;
        m_enemyMerge.rigidbody2D.velocity = Vector3.zero;

        // Debug.LogWarning("enemyState before: " + m_enemyMerge.enemySize);
        m_enemyMerge.enemySize += m_enemyMerge.Merge_Size;
        // Debug.LogWarning("enemyState after: " + m_enemyMerge.enemySize);
    }
    
    public override void Update()
    {
        if(m_enemyMerge.enemySize > (m_enemy as EnemyBoss).Boss_MaxSize )
        {
            // Debug.LogWarning( "BossSize:" + m_enemyMerge.enemySize + " Boss_MaxSize:" + (m_enemy as EnemyBoss).Boss_MaxSize.ToString());
            m_enemyState.enemyStateMachine.ChangeEnemyState(m_enemyState.enemyStateDie);
        }
        else
        {
            m_enemyState.enemyStateMachine.ChangeEnemyState(m_enemyState.enemyStateIdle);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void ExitState()
    {
        m_enemyState.IsMerge = false;
        m_enemyMerge.enemyCollider.isTrigger = false;
        base.ExitState();
    }

}
