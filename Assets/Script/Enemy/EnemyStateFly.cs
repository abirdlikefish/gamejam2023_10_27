using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateFly : EnemyStateBase
{
    protected IEnemyFly m_enemyFly;
    public EnemyStateFly(EnemyBase enemy) : base(enemy)
    {
        m_enemyFly = enemy as IEnemyFly;
    }

    public override void EnterState()
    {
        base.EnterState();
        m_enemyState.IsFly = true;

        m_enemyFly.enemyCollider.isTrigger = true;

        GameObject player = GameObject.FindWithTag("Player");
        if(player == null)
        {
            Debug.Log("player is null");
        }
        m_enemyFly.rigidbody2D.velocity = m_enemyFly.flySpeed * (m_enemy.transform.position - player.transform.position) ;
    }
    public override void ExitState()
    {
        base.ExitState();
        m_enemyFly.enemyCollider.isTrigger = false;
        m_enemyFly.IsFly = false;
    }
    public override void Update()
    {
        base.Update();
        if(m_enemyFly.rigidbody2D.velocity.sqrMagnitude < 0.1)
        {
            m_enemyState.enemyStateMachine.ChangeEnemyState(m_enemyState.enemyStateIdle);
        }
        if(m_enemyState.IsMerge)
        {
            m_enemyState.enemyStateMachine.ChangeEnemyState(m_enemyState.enemyStateMerge);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}
