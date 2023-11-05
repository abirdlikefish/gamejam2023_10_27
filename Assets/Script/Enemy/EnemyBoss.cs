using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyBase
{
    private float boss_maxSize;
    public float Boss_MaxSize{ get { return boss_maxSize;} set{ boss_maxSize = value ;} }
    public virtual void Initialization_Boss(float maxSize)
    {
        Boss_MaxSize = maxSize;
    }
    public override void Initialization(Vector3 position, float enemyBeginSize, float enemyEndSize , float growTime, int enemyColor, int skillIndex, float skill_speed)
    {
        base.Initialization(position, enemyBeginSize, enemyEndSize , growTime, enemyColor, skillIndex, skill_speed);
        
        Sprite midSprite = ObjectPool.Instance.enemyBoss;
        if(midSprite == null)
        {
            Debug.Log("sprite body is null");
        }
        spriteRenderer.sprite =  midSprite;

        int spriteIndex =Random.Range(0,6);
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

        //--------------------
        spriteRenderer.color = ColorTool.IndexToColor(0);
        // enemyStateDie = new EnemState
    }

    public override void Initialization_State()
    {
        enemyStateMachine = new EnemyStateMachine();
        enemyStateIdle = new EnemyStateIdle(this);
        enemyStateFly = new EnemyStateFly(this);
        enemyStateGrow = new EnemyStateGrow(this);
        enemyStateDie = new EnemyStateDie_Boss(this);
        enemyStateMerge = new EnemyStateMerge_Boss(this);
        enemyStateMachine.initialization(enemyStateIdle);
    }

    public override void ChangeEnemySize(float enemySize)
    {
        this.enemySize = enemySize;
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
        if(!IsMerge && other.CompareTag("Enemy"))
        {
            IEnemyMerge otherEnemy = other.GetComponent<IEnemyMerge>();
            if(otherEnemy == null)
            {
                Debug.Log("otherEnemy is null");
            }
            if(otherEnemy.BeMergeFlag)
            {
                return ;
            }
            otherEnemy.IsMerge = this.IsMerge = true;
            otherEnemy.merge_time = merge_time = 1;

            this.skillIndex |= otherEnemy.Merge_Skill;
            this.Merge_Size += otherEnemy.Merge_Size + otherEnemy.enemySize;
            otherEnemy.BeMergeFlag = true;
        }
        
        if( other.CompareTag("Player"))
        {
            IPlayerHurt playerHurt = other.GetComponent<IPlayerHurt>();
            if(playerHurt == null)
            {
                Debug.Log("playerHurt is null");
            }
            playerHurt.Hurt(1);
            // Debug.LogWarning("playerHurt");
        }
    }


}
