using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMerge
{
    public float enemySize{ get; set; }
    public float merge_time{ get; set; }
    public bool isMerge{get; set; }
    public Collider2D enemyCollider { get ; set ; }
    public void ChangeEnemySize(float enemySize);
}
