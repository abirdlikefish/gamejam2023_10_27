using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyGrow
{
    public float enemySize{ get; set; }
    // public float grow_beginSize{ get; set; }
    public float grow_endSize{ get; set; }
    public float grow_time{ get; set; }
    public bool isGrow{get; set; }
    public Collider2D enemyCollider { get ; set ; }
    public void ChangeEnemySize(float enemySize);

}