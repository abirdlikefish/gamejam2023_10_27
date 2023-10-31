using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour , IEnemyBeHit
{
    //protected bool m_isStatic;
    protected int m_enemyType;
    [SerializeField]
    protected bool m_isFly;
    [SerializeField]
    protected float m_size ;
    protected float m_speed ;
    protected float m_atk = 1;
    [SerializeField]
    protected int m_color;
    protected float m_growTime;
    protected float m_lastTime_outCamera;

    protected GameObject m_player;
    protected CircleCollider2D m_circleCollider;
    protected Rigidbody2D m_rigidbody;
    protected SpriteRenderer m_spriteRenderer;
    protected SpriteRenderer m_spriteRenderer_mouth;
    protected SpriteRenderer m_spriteRenderer_eyes;

    protected Camera m_camera;

    public bool IsFly
    {
        get
        {
            return m_isFly;
        }
    }

    protected virtual void Awake()
    {
        m_circleCollider = GetComponent<CircleCollider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_spriteRenderer_mouth = transform.Find("Mouth").GetComponent<SpriteRenderer>();
        m_spriteRenderer_eyes = transform.Find("Eyes").GetComponent<SpriteRenderer>();

        m_camera = Camera.main;
        if(m_circleCollider == null)
        {
            Debug.Log("enemy collider is null");
        }
        if(m_rigidbody == null)
        {
            Debug.Log("enemy rigidbody is null");
        }
        if(m_spriteRenderer == null)
        {
            Debug.Log("enemy spriterender is null");
        }
        if(m_spriteRenderer_mouth == null)
        {
            Debug.Log("enemy mouth spriterender is null");
        }
        if(m_spriteRenderer_eyes == null)
        {
            Debug.Log("enemy eyes spriterender is null");
        }
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(m_isFly == false)
        {
            return ;
        }

        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy == null)
            {
                Debug.Log("enemy is null");
            }
            if (enemy.IsFly)
            {
                return;
            }
            
            m_isFly = false;
            m_rigidbody.velocity = new Vector2(0, 0);
            m_circleCollider.isTrigger = false;

            if(((enemy.m_enemyType >> 4) & 1) == 1)
            {
                enemy.m_size += m_size;
                this.KillEnemy(false);

                if(enemy.m_size > 7)
                {
                    enemy.StartCoroutine(enemy.GrowToDead(enemy.transform.localScale.x, enemy.m_size));
                }

                return;
            }
            else if((enemy.m_enemyType & 1) == 1)
            {
                enemy.KillEnemy(false);
                m_spriteRenderer.color = new Color(0.75f , 0.75f , 0.75f);
                //this.GrowToDead(m_size, m_size + enemy.m_size);
                StartCoroutine(GrowToDead(m_size, m_size + enemy.m_size));
                return;
            }
            else if ((m_enemyType & 1) == 1)
            {
                this.KillEnemy(false);
                enemy.m_spriteRenderer.color = new Color(0.75f, 0.75f, 0.75f);
                
                enemy.StartCoroutine(enemy.GrowToDead(enemy.m_size, m_size + enemy.m_size));
                return;
            }

            int midColor = m_color | enemy.m_color;
            //if((m_enemyType & 1) == 1)
            //{
            //    midColor = (1 << 3) - 1;
            //}

            Vector3 midPosition = (enemy.transform.position * enemy.m_size + transform.position * m_size) / (m_size + enemy.m_size);

            //EventManager.Instance.CreateEnemy(enemyType , midPosition , )
            EventManager.Instance.CreateEnemy(m_enemyType | enemy.m_enemyType , midPosition, enemy.m_speed, Math.Max(enemy.m_atk, m_atk), Math.Max(enemy.m_size, m_size), enemy.m_size + m_size, midColor, (enemy.m_growTime + m_growTime) / 2);
            //}
            //if(enemy.m_isStatic)
            //{
            //    EventManager.Instance.CreateEnemy_1(midPosition, Math.Max(enemy.m_atk, m_atk), Math.Max(enemy.m_size, m_size), enemy.m_size + m_size, midColor, (enemy.m_growTime + m_growTime) / 2);
            //}
            //else
            //{
            //    EventManager.Instance.CreateEnemy_2(midPosition, enemy.m_speed , Math.Max(enemy.m_atk, m_atk), Math.Max(enemy.m_size, m_size), enemy.m_size + m_size, midColor, (enemy.m_growTime + m_growTime) / 2);
            //}

            enemy.KillEnemy(false);
            this.KillEnemy(false);
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision) 
    {
        if ((!m_isFly) && collision.CompareTag("Player"))
        {
            IPlayerHurt playerHurt = collision.GetComponent<IPlayerHurt>();
            if(playerHurt == null)
            {
                Debug.Log("playerHurt is null");
            }
            playerHurt.Hurt(m_atk);

        }
    }

    protected virtual void FixedUpdate()
    {
        if(((m_enemyType >> 1) & 1 ) == 1 && (!m_isFly))
        {
            Vector2 midDirection = m_player.transform.position - gameObject.transform.position;
            midDirection.Normalize();
            m_rigidbody.velocity = m_speed * midDirection;
        }

        if(((m_enemyType >> 4) & 1) != 1)
        {
            Vector2 viewportPosition = m_camera.WorldToViewportPoint(gameObject.transform.position);
            if(viewportPosition.x < 0 || viewportPosition.y < 0 || viewportPosition.x > 1 || viewportPosition.y > 1)
            {
                if(m_lastTime_outCamera < -0.9)
                {
                    m_lastTime_outCamera = Time.time;
                }
                else if(Time.time - m_lastTime_outCamera > 20 )
                {
                    this.KillEnemy(false);
                }
            }
            else
            {
                m_lastTime_outCamera = -1;
            }
        }
    }



    public virtual void EnemyBeHit(Vector2 velocity, float strength , float flyTime)
    {
        if(m_isFly )
        {
            return;
        }
        if( strength < m_size || ((m_enemyType >> 4) & 1 ) == 1)
        {
            return;
        }

        m_isFly = true;
        m_circleCollider.isTrigger = true;
        m_rigidbody.velocity = velocity * strength / m_size;
        StartCoroutine(Fly(flyTime));
    }

    IEnumerator Fly(float flyTime)
    {
        float leftTime = flyTime;
        Vector2 velocity = m_rigidbody.velocity;
        while(leftTime > 0)
        {
            leftTime -= Time.deltaTime;
            if(leftTime * 3 < flyTime * 2)
            {
                if(m_rigidbody == null)
                {
                    Debug.Log("can't find m_rigidbody in fly");
                }
                m_rigidbody.velocity = velocity * leftTime * 3 / flyTime / 2;
            }
            yield return null;
        }

        if(m_rigidbody == null)
        {
            Debug.Log("can't find m_rigidbody in fly");
        }
        if(m_circleCollider == null)
        {
            Debug.Log("can't find m_circleCollider in fly");
        }
        m_isFly = false;
        m_rigidbody.velocity = new Vector2(0, 0);
        m_circleCollider.isTrigger = false;
    }


    IEnumerator Grow(float begSize , float finSize )
    {
        m_circleCollider.enabled = false;

        float passedTime = 0;
        while(passedTime < m_growTime)
        {
            passedTime += Time.deltaTime;
            float k = (finSize - begSize) * passedTime / m_growTime + begSize;
            gameObject.transform.localScale = new Vector3 (k, k, k);
            yield return null;
        }
        gameObject.transform.localScale = new Vector3(finSize, finSize, finSize);
        m_circleCollider.isTrigger = false ;
        m_circleCollider.enabled = true;
        m_size = finSize;
    }

    IEnumerator GrowToDead(float begSize, float finSize)
    {
        Debug.Log("begin dead");
        m_circleCollider.enabled = false;

        float passedTime = 0;
        while (passedTime < m_growTime)
        {
            passedTime += Time.deltaTime;
            float k = (finSize - begSize) * passedTime / m_growTime + begSize;
            gameObject.transform.localScale = new Vector3(k, k, k);
            if(((m_enemyType >> 4) & 1) != 1)
            {
                m_spriteRenderer.color = new Color(m_spriteRenderer.color.r, m_spriteRenderer.color.g, m_spriteRenderer.color.b, 1.0f - passedTime / m_growTime);
            }
            yield return null;
        }
        m_size = finSize;
        this.KillEnemy(true);
    }


    public virtual void Initialization(int enemyType , GameObject player , float speed, float atk, float begSize, float finSize, int color , float growTime , Sprite body , Sprite mouth , Sprite eyes)
    {
        m_enemyType = enemyType;
        m_player = player ;
        m_speed = speed;
        m_atk = atk;
        m_color = color;
        m_growTime = growTime;
        m_lastTime_outCamera = -1;

        m_isFly = false;
        m_spriteRenderer.sprite = body;
        m_spriteRenderer_mouth.sprite = mouth;
        m_spriteRenderer_eyes.sprite = eyes;
        m_spriteRenderer.color = new Color((m_color & 1) * 0.25f + 0.5f , ((m_color >> 1) & 1) * 0.25f + 0.5f, ((m_color >> 2) & 1) * 0.25f + 0.5f);

        if((m_enemyType & 1) == 1)
        {
            m_spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f);
            m_color = (1 << 3) - 1;
            StartCoroutine(Grow(begSize, finSize));
        }
        else if(((m_enemyType >> 4) & 1) == 1)
        {
            //m_spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f);
            m_spriteRenderer.color = new Color(0,0,0);
            m_size = finSize;
        }
        else if(m_color == (1 << 3) - 1)
        {
            StartCoroutine(GrowToDead(begSize, finSize));
        }
        else
        {
            StartCoroutine(Grow(begSize, finSize));
        }

    }


    public virtual void KillEnemy(bool isScore)
    {
        if(isScore)
        {
            EventManager.Instance.AddScore(m_size);
        }
        Destroy(gameObject);
    }
}
