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
        m_enemyState.IsMerge = true;

        m_enemyMerge.enemyCollider.isTrigger = true;
        m_enemyMerge.rigidbody2D.velocity = Vector3.zero;

        m_shrinkSpeed = (m_enemyMerge.enemySize ) / m_enemyMerge.merge_time;
        m_beginTime = Time.time;

        if(!m_enemyMerge.BeMergeFlag)
        {
            Vector3 midPosition = m_enemyMerge.Merge_Position + m_enemy.transform.position * m_enemyMerge.enemySize;
            float midSize = m_enemyMerge.Merge_Size + m_enemyMerge.enemySize;
            midPosition /= midSize;

            if(ObjectPool.Instance.prefabEnemy == null)
            {
                Debug.Log("No enemy prefab selected");
            }
            EnemyBase enemy = (ObjectPool.Instance.GetGameObject(ObjectPool.Instance.prefabEnemy)).GetComponent<EnemyBase>();
            enemy.Initialization(midPosition , 0.1f , midSize , 1f , m_enemyMerge.Merge_Color , 0 , 1.0f );
            Debug.Log("create Enemy");
        }
    }
    public override void ExitState()
    {
        Debug.Log("enemy merge error");
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
