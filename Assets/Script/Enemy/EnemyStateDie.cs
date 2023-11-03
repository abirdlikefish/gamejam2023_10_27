using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateDie : EnemyStateBase
{
    protected IEnemyGrow m_enemyGrow;
    protected IColor m_enemyColor;
    protected Color m_midColor;
    protected float m_growSpeed ;
    protected float m_alphaReduceSpeed ;
    protected float m_beginTime ;
    public EnemyStateDie(EnemyBase enemy) : base(enemy)
    {
        m_enemyGrow = enemy as IEnemyGrow;
        m_enemyColor = enemy as IColor;
    }
    public override void EnterState()
    {
        base.EnterState();
        m_enemyState.isDie = true;

        m_midColor = m_enemyColor.spriteRenderer.color;

        m_enemyGrow.enemyCollider.enabled = false;

        m_growSpeed = (m_enemyGrow.grow_endSize - m_enemyGrow.enemySize) / m_enemyGrow.grow_time;
        m_alphaReduceSpeed = m_midColor.a / m_enemyGrow.grow_time;
        m_beginTime = Time.time;
    }
    public override void ExitState()
    {
    }
    public override void Update()
    {
        base.Update();
        
        m_enemyGrow.enemySize += Time.deltaTime * m_growSpeed;
        m_enemyGrow.ChangeEnemySize(m_enemyGrow.enemySize);

        m_midColor.a -= Time.deltaTime * m_alphaReduceSpeed;
        m_enemyColor.spriteRenderer.color = m_midColor;

        if(Time.time - m_beginTime > m_enemyGrow.grow_time)
        {
            ObjectPool.Instance.ReturnGameObject(m_enemy.gameObject);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
