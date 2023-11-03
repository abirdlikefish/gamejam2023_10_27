using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IEnemyState, IEnemyMerge, IEnemyGrow, IColor, IEnemySkill
{
    // state
    public bool isIdle { get ; set ; }
    public bool isFly { get ; set ; }
    public bool isGrow { get ; set ; }
    public bool isDie { get ; set ; }
    public bool isMerge { get ; set ; }
    public EnemyStateBase enemyStateIdle { get ; set ; }
    public EnemyStateBase enemyStateFly { get ; set ; }
    public EnemyStateBase enemyStateGrow { get ; set ; }
    public EnemyStateBase enemyStateDie { get ; set ; }
    public EnemyStateBase enemyStateMerge { get ; set ; }
    public EnemyStateMachine enemyStateMachine { get ; set ; }

    //attribute
    public float enemySize { get ; set ; }
    public float merge_time { get ; set ; }
    public Collider2D enemyCollider { get ; set ; }

    public float grow_endSize { get ; set ; }
    public float grow_time { get ; set ; }

    public int colorIndex { get ; set ; }
    public SpriteRenderer spriteRenderer {get ; set ; }

    // skill
    public int skillIndex { get ; set ; }
    public float skill_speed { get ; set ; }
    public float skill_growSpeed { get ; set ; }
    public float skill_divisionSize { get ; set ; }
    public float skill_explosionRange { get ; set ; }


    public virtual void Initialization(Vector3 position , float enemyBeginSize , float enemyEndSize , int enemyColor , 
                        int skillIndex , float skill_speed , float skill_growSpeed , float skill_divisionSize , float skill_explosionRange)
    {
        isIdle = false;
        isFly = false;
        isGrow = true;
        isDie = false;
        isMerge = false;

        transform.position = position;
        
        ChangeEnemySize(enemyBeginSize);
        grow_endSize = enemyEndSize;

        colorIndex = enemyColor;
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        spriteRenderer.color = ColorTool.IndexToColor(enemyColor);

        this.skillIndex = skillIndex;
        this.skill_speed = skill_speed;
        this.skill_growSpeed = skill_growSpeed;
        this.skill_divisionSize = skill_divisionSize;
        this.skill_explosionRange = skill_explosionRange;

        enemyStateMachine = new EnemyStateMachine();
        enemyStateIdle = new EnemyStateBase(this );
        enemyStateFly = new EnemyStateBase(this);
        enemyStateGrow = new EnemyStateBase(this);
        enemyStateDie = new EnemyStateBase(this);
        enemyStateMerge = new EnemyStateBase(this);
        if(enemyColor == (1 << 3) - 1)
        {
            enemyStateMachine.initialization(enemyStateDie);
        }
        {
            enemyStateMachine.initialization(enemyStateGrow);
        }
    }

    private void Update() 
    {
        enemyStateMachine.Update();
    }

    private void FixedUpdate() 
    {
        enemyStateMachine.FixedUpdate();
    }

    public void ChangeEnemySize(float enemySize)
    {
        this.enemySize = enemySize;
        transform.localScale = new Vector3(enemySize , enemySize , enemySize);
    }

    public void skill_move()
    {
        throw new System.NotImplementedException();
    }

    public void skill_grow()
    {
        throw new System.NotImplementedException();
    }

    public void skill_division()
    {
        throw new System.NotImplementedException();
    }

    public void skill_explosion()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnTriggerEnter2D(Collider2D other) {
        
    }
}
