using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyFly
{
    public bool IsFly { get; set; }
    public float flySpeed { get; set; }
    public float enemySize { get; set; }
    public Rigidbody2D rigidbody2D { get; set; }
    public Collider2D enemyCollider { get; set; }
}
