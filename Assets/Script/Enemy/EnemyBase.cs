using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IEnemyState, IEnemyMerge, IEnemyGrow, IColor, IEnemySkill, IEnemyFly
{
    [SerializeField]
    private bool isIdle ;
    public bool IsIdle{get { return isIdle; } set {isIdle = value;} }

    [SerializeField]
    private bool isFly ;
    public bool IsFly{get { return isFly; } set {isFly = value;} }
    [SerializeField]
    private bool isGrow ;
    public bool IsGrow{get { return isGrow; } set {isGrow = value;} }
    [SerializeField]
    private bool isDie ;
    public bool IsDie{get { return isDie; } set {isDie = value;} }
    [SerializeField]
    private bool isMerge ;
    public bool IsMerge{get { return isMerge; } set {isMerge = value;} }
    [SerializeField]
    private bool beMergeFlag;
    public bool BeMergeFlag{get { return beMergeFlag; } set {beMergeFlag = value;} }

    [SerializeField]
    private int merge_Color;
    public int Merge_Color{get { return merge_Color; } set {merge_Color = value;} }
    [SerializeField]
    private int merge_Skill;
    public int Merge_Skill{get { return merge_Skill; } set {merge_Skill = value;} }

    [SerializeField]
    private Vector3 merge_Position;
    public Vector3 Merge_Position{get { return merge_Position; } set {merge_Position = value;} }
    [SerializeField]
    private float merge_Size;
    public float Merge_Size{get { return merge_Size; } set {merge_Size = value;} }

    

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

    public float flySpeed { get ; set ; }
    public Rigidbody2D rigidbody2D { get; set; }

    private GameObject m_player;

    // skill
    public int skillIndex { get ; set ; }
    public float skill_speed { get ; set ; }

    public virtual void Initialization(Vector3 position , float enemyBeginSize , float enemyEndSize , float growTime , int enemyColor , 
                        int skillIndex , float skill_speed )
    {
        IsIdle = false;
        IsFly = false;
        IsGrow = false;
        IsDie = false;
        IsMerge = false;
        BeMergeFlag = false;

        transform.position = position;
        
        ChangeEnemySize(enemyBeginSize);
        grow_endSize = enemyEndSize;
        grow_time = growTime;

        Merge_Color = colorIndex = enemyColor;
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        spriteRenderer.color = ColorTool.IndexToColor(colorIndex);
        merge_Position = new Vector3(0,0,0);
        merge_Size = 0;

        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        enemyCollider = transform.GetComponent<Collider2D>();

        Merge_Skill = this.skillIndex = skillIndex;
        this.skill_speed = skill_speed;

        m_player = GameObject.FindWithTag("Player");
        if(m_player == null)
        {
            Debug.LogError("Player not found");
        }

        Initialization_State();

    }

    public virtual void Initialization_State()
    {
        enemyStateMachine = new EnemyStateMachine();
        enemyStateIdle = new EnemyStateIdle(this);
        enemyStateFly = new EnemyStateFly(this);
        enemyStateGrow = new EnemyStateGrow(this);
        enemyStateDie = new EnemyStateDie(this);
        enemyStateMerge = new EnemyStateMerge(this);
        if(colorIndex == (1 << 3) - 1)
        {
            enemyStateMachine.initialization(enemyStateDie);
        }
        else
        {
            enemyStateMachine.initialization(enemyStateGrow);
        }
    }

    public virtual void Update() 
    {
        enemyStateMachine.Update();

        if(((skillIndex >> 0) & 1) == 1 && !isFly)
        {
            skill_move();
        }

        if(((skillIndex >> 1) & 1) == 1 && !isFly)
        {
            skill_grow();
        }

        if(((skillIndex >> 2) & 1) == 1 && !isFly)
        {
            skill_division();
        }

        if(((skillIndex >> 3) & 1) == 1 && !isFly)
        {
            skill_explosion();
        }
    }



    public virtual void FixedUpdate() 
    {
        enemyStateMachine.FixedUpdate();
    }

    public virtual void ChangeEnemySize(float enemySize)
    {
        this.enemySize = enemySize;
        transform.localScale = new Vector3(enemySize , enemySize , enemySize);
    }

    public virtual void skill_move()
    {
        Vector3 midDirection = (m_player.transform.position - transform.position).normalized;
        rigidbody2D.velocity = midDirection * skill_speed;
    }

    public virtual void skill_grow()
    {
        ChangeEnemySize(enemySize + 0.001f);
    }

    public virtual void skill_division()
    {
        // throw new System.NotImplementedException();
    }

    public virtual void skill_explosion()
    {
        // throw new System.NotImplementedException();
    }

    public virtual void OnTriggerStay2D(Collider2D other) 
    {
        if(IsFly && !BeMergeFlag && other.CompareTag("Enemy"))
        {
            if(other.GetComponent<EnemyBase>() is EnemyBoss || other.GetComponent<EnemyBase>() is EnemyColor)
            {
                return ;
            }
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

            this.Merge_Color |= otherEnemy.Merge_Color;
            this.Merge_Skill |= otherEnemy.Merge_Skill;
            this.Merge_Position += otherEnemy.Merge_Position + other.transform.position * otherEnemy.enemySize;
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

    public virtual void OnTriggerEnter2D(Collider2D other) {
        
    }
}
