using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    public bool isIdle { get; set; }
    public bool isFly{ get; set; }
    public bool isGrow{ get; set; }
    public bool isDie{ get; set; }
    public bool isMerge{ get; set; }
    public EnemyStateBase enemyStateIdle { get; set; }
    public EnemyStateBase enemyStateFly { get; set; }
    public EnemyStateBase enemyStateGrow { get; set; }
    public EnemyStateBase enemyStateDie { get; set; }
    public EnemyStateBase enemyStateMerge { get; set; }
    public EnemyStateMachine enemyStateMachine { get; set; }
}
