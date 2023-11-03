using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMerge : EnemyStateBase
{
    protected IEnemyMerge m_enemyMerge;
    protected float m_shrinkSpeed ;
    protected float m_beginTime ;
    public EnemyStateMerge(EnemyBase enemy) : base(enemy)
    {
        m_enemyMerge = enemy as IEnemyMerge;
    }
    public override void EnterState()
    {
        base.EnterState();
        m_enemyState.isMerge = true;

        m_enemyMerge.enemyCollider.isTrigger = true;
        // m_enemyGrow.ChangeEnemySize(m_enemyGrow.grow_beginSize);

        m_shrinkSpeed = (m_enemyMerge.enemySize ) / m_enemyMerge.merge_time;
        m_beginTime = Time.time;
    }
    public override void ExitState()
    {
        Debug.Log("enemy merge error");
        // base.ExitState();
        // m_enemyMerge.enemyCollider.isTrigger = false;
        // m_enemyMerge.ChangeEnemySize(0);
        // m_enemyState.isMerge = false;
    }
    public override void Update()
    {
        base.Update();
        
        m_enemyMerge.enemySize -= Time.deltaTime * m_shrinkSpeed;
        m_enemyMerge.ChangeEnemySize(m_enemyMerge.enemySize);

        if(Time.time - m_beginTime > m_enemyMerge.merge_time)
        {
            ObjectPool.Instance.ReturnGameObject(m_enemy.gameObject);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
