using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase
{
    public override void Initialization(Vector3 position, float enemyBeginSize, float enemyEndSize , float growTime, int enemyColor, int skillIndex, float skill_speed)
    {
        base.Initialization(position, enemyBeginSize, enemyEndSize , growTime, enemyColor, skillIndex, skill_speed);
        int spriteIndex =Random.Range(0,6);
        Sprite midSprite = ObjectPool.Instance.enemyBody[spriteIndex];
        if(midSprite == null)
        {
            Debug.Log("sprite " + spriteIndex.ToString() +" is null");
        }
        spriteRenderer.sprite =  midSprite;
    }
}
