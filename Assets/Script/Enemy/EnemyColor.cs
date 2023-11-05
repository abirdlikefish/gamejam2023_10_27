using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColor : EnemyBase
{
    public override void Initialization(Vector3 position, float enemyBeginSize, float enemyEndSize , float growTime, int enemyColor, int skillIndex, float skill_speed)
    {
        base.Initialization(position, enemyBeginSize, enemyEndSize , growTime, enemyColor, skillIndex, skill_speed);
        
        int spriteIndex =Random.Range(0,6);
        Sprite midSprite = ObjectPool.Instance.enemyBodyColor[spriteIndex];
        if(midSprite == null)
        {
            Debug.Log("sprite body " + spriteIndex.ToString() +" is null");
        }
        spriteRenderer.sprite =  midSprite;

        spriteIndex =Random.Range(0,6);
        midSprite = ObjectPool.Instance.enemyEyes[spriteIndex];
        if(midSprite == null)
        {
            Debug.Log("sprite eye " + spriteIndex.ToString() +" is null");
        }
        transform.Find("Eyes").GetComponent<SpriteRenderer>().sprite = midSprite;

        spriteIndex =Random.Range(0,6);
        midSprite = ObjectPool.Instance.enemyMouth[spriteIndex];
        if(midSprite == null)
        {
            Debug.Log("sprite mouth " + spriteIndex.ToString() +" is null");
        }
        transform.Find("Mouth").GetComponent<SpriteRenderer>().sprite = midSprite;
    }
}
