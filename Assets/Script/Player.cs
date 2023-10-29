using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IPlayerHurt
{
    protected float m_maxHP ;
    protected float m_HP ;
    protected float m_beAttackedTime_lef ;
    protected float m_speed ;
    protected Vector2 m_direction;
    protected float m_speedFactor ;
    protected float m_begForce ;
    protected float m_nowForce ;
    protected float m_maxForce ;
    protected float m_forceIncreaseSpeed ;
    protected float m_enemyFlyTime ;
    protected float m_enemyFlySpeed ;
    protected float m_attackTime ;
    [SerializeField]
    protected float m_attackTime_lef ;
    protected bool m_isAttack;
    protected bool m_isCharge ;

    protected Rigidbody2D m_rigidbody;
    protected SpriteRenderer m_renderer;
    protected GameObject m_weapon;
    protected Rigidbody2D m_rigidbody_weapon;

    void Awake()
    {
        m_maxHP = 400 ;
        m_HP = 400 ;
        m_beAttackedTime_lef = 0;
        m_speed = 5;
        m_direction = new Vector2(0,0);
        m_speedFactor = 0.5f;
        m_begForce = 1;//开始力
        m_nowForce = 1;
        m_maxForce = 4;//最大力
        m_forceIncreaseSpeed = 1.5f;
        m_enemyFlyTime = 2;
        m_enemyFlySpeed = 7;
        m_attackTime = 0.5f;
        m_attackTime_lef = 0;
        m_isAttack = false ;
        m_isCharge = false ;

        m_rigidbody = gameObject.GetComponent<Rigidbody2D>();
        m_renderer = gameObject.GetComponent<SpriteRenderer>();
        m_weapon = transform.Find("Weapon").gameObject;

        if(m_rigidbody == null)
        {
            Debug.Log("m_rigidbody is null");
        }
        if(m_renderer == null)
        {
            Debug.Log("m_renderer is null");
        }
        if(m_weapon == null)
        {
            Debug.Log("m_weapon is null");
        }

    }
    
    void Start()
    {
        EventManager.Instance.Level += UpLevel;
        EventManager.Instance.HPChange(m_HP , m_maxHP);
    }
    void Update()
    {
        m_direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if(m_direction.x > 0.01 )
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else if(m_direction.x < -0.01)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        m_rigidbody.velocity = m_direction * m_speed ;
        if(m_isCharge || m_isAttack)
        {
            m_rigidbody.velocity *= m_speedFactor ;
        }

        if(Input.GetKeyDown(KeyCode.Space) && m_isCharge == false && m_isAttack == false)
        {
            m_isCharge = true;
        }

        if (m_isCharge)
        {
            m_nowForce += Time.deltaTime * m_forceIncreaseSpeed ;
            if(m_nowForce >= m_maxForce)
            {
                m_nowForce = m_maxForce;
            }

            m_weapon.transform.localScale = new Vector3(m_nowForce , m_nowForce , m_nowForce );
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            m_isCharge = false ;
            Attack();
        }


        if(m_beAttackedTime_lef > 0.01)
        {
            m_renderer.color = new Color(1 , 0.5f , 0.5f , 1);
            m_beAttackedTime_lef -= Time.deltaTime;
        }
        else
        {
            m_renderer.color = new Color(1 , 1 , 1 , 1);
            m_beAttackedTime_lef = 0;
        }

    }

    public void UpLevel(int index)
    {
        switch(index)
        {
            case 0 :
                m_maxHP *= 1.3f;
                m_HP = m_maxHP;
                EventManager.Instance.HPChange(m_HP , m_maxHP);
                break;
            case 1 :
                m_speed *= 1.3f;
                break;
            case 2 :
                m_begForce *= 1.3f;
                break;
            case 3 :
                m_maxForce *= 1.3f;
                break;
            case 4 :
                m_forceIncreaseSpeed *= 1.3f;
                break;
            case 5 :
                m_enemyFlyTime *= 1.3f;
                break;
        }
    }

    protected void Attack()
    {
        m_isAttack = true ;
        m_weapon.GetComponent<Weapon>().Attack(m_enemyFlySpeed , m_nowForce , m_enemyFlyTime , m_attackTime);

        StartCoroutine(Attacking());
    }

    IEnumerator Attacking()
    {
        m_attackTime_lef = m_attackTime;

        while(m_attackTime_lef > 0.01)
        {
            m_attackTime_lef -= Time.deltaTime;
            yield return null;
        }
        
        m_nowForce = m_begForce;
        m_weapon.transform.localScale = new Vector3(m_nowForce , m_nowForce , m_nowForce );
        m_isAttack = false;
    }

    public void Hurt(float damage)
    {
        m_HP -= damage;
        m_beAttackedTime_lef = 0.1f;

        EventManager.Instance.HPChange(m_HP , m_maxHP);
    }

}
