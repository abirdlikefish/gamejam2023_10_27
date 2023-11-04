using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    public bool IsIdle { get; set; }
    public bool IsFly{ get; set; }
    public bool IsGrow{ get; set; }
    public bool IsDie{ get; set; }
    public bool IsMerge{ get; set; }
    public EnemyStateBase enemyStateIdle { get; set; }
    public EnemyStateBase enemyStateFly { get; set; }
    public EnemyStateBase enemyStateGrow { get; set; }
    public EnemyStateBase enemyStateDie { get; set; }
    public EnemyStateBase enemyStateMerge { get; set; }
    public EnemyStateMachine enemyStateMachine { get; set; }
}
