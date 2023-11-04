using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMerge
{
    public float enemySize{ get; set; }
    public float merge_time{ get; set; }
    public bool IsMerge{get; set; }
    public bool BeMergeFlag{get; set; }
    public int Merge_Color{get; set; }
    public int Merge_Skill{get; set; }
    public Vector3 Merge_Position{get; set; }
    public float Merge_Size{get; set; }
    public Collider2D enemyCollider { get ; set ; }
    public Rigidbody2D rigidbody2D { get; set; }
    public void ChangeEnemySize(float enemySize);
}
