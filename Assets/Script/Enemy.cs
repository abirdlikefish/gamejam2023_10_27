using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour , IEnemyBeHit
{
    protected bool m_isStatic = true;
    protected bool m_isFly = false;
    protected float m_size = 1;
    protected float m_speed = 1;
    protected float m_atk = 1;
    protected int m_color;
    protected float m_growTime;
    //public GameObject prefabEnemy;   // 敌人预制体，inspector窗口中获取

    public GameObject m_player;  // 后改为protected
    protected CircleCollider2D m_circleCollider;
    protected Rigidbody2D m_rigidbody;
    protected SpriteRenderer m_spriteRenderer;

    protected Coroutine m_flyCoroutine;
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
        m_camera = Camera.main;
        if(m_circleCollider == null)
        {
            Debug.Log("enemy无法获取collider");
        }
        if(m_rigidbody == null)
        {
            Debug.Log("enemy无法获取rigidbody");
        }
        if(m_spriteRenderer == null)
        {
            Debug.Log("enemy无法获取spriterender");
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy == null)
            {
                Debug.Log("无法获取敌人");
            }
            if (enemy.IsFly)
            {
                return;
            }
            if (m_flyCoroutine == null)
            {
                Debug.Log("m_flyCoroutine 为 null");
            }
            StopCoroutine(m_flyCoroutine);
            m_flyCoroutine = null;
            m_isFly = false;
            m_rigidbody.velocity = new Vector2(0, 0);
            m_circleCollider.isTrigger = false;


            // 生成敌人

            int midColor = m_color | enemy.m_color;
            Vector3 midPosition = (enemy.transform.position * enemy.m_size + transform.position * m_size) / (m_size + enemy.m_size);

            if(enemy.m_isStatic)
            {
                EventManager.Instance.CreateEnemy_1(midPosition, Math.Max(enemy.m_atk, m_atk), Math.Max(enemy.m_size, m_size), enemy.m_size + m_size, midColor, (enemy.m_growTime + m_growTime) / 2);
            }
            else
            {
                EventManager.Instance.CreateEnemy_2(midPosition, enemy.m_speed , Math.Max(enemy.m_atk, m_atk), Math.Max(enemy.m_size, m_size), enemy.m_size + m_size, midColor, (enemy.m_growTime + m_growTime) / 2);
            }

            enemy.KillEnemy(false);
            this.KillEnemy(false);
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)    //无法持续扣血
    {
        if ((!m_isFly) && collision.CompareTag("Player"))
        {
            //玩家受伤
            IPlayerHurt playerHurt = collision.GetComponent<IPlayerHurt>();
            playerHurt.Hurt(m_atk);

        }
    }

    protected virtual void FixedUpdate()
    {
        if((!m_isStatic) && (!m_isFly))
        {
            Vector2 midDirection = m_player.transform.position - gameObject.transform.position;
            midDirection.Normalize();
            m_rigidbody.velocity = m_speed * midDirection;
        }

        Vector2 viewportPosition = m_camera.WorldToViewportPoint(gameObject.transform.position);
        if(viewportPosition.x < 0 || viewportPosition.y < 0 || viewportPosition.x > 1 || viewportPosition.y > 1)
        {
            //超出范围
        }
    }


    //protected virtual void OnCollisionStay2D(Collision2D collision)
    //{
    //    if(collision.gameObject.CompareTag("Player"))
    //    {
    //        //玩家受伤
    //        IPlayerHurt playerHurt = collision.gameObject.GetComponent<IPlayerHurt>();
    //        playerHurt.Hurt(m_atk);
    //    }
    //}

    public virtual void EnemyBeHit(Vector2 velocity, float strength , float flyTime)
    {
        Debug.Log(strength);
        if((!m_isFly) && strength < m_size )
        {
            return;
        }
        Debug.Log("beattacked");
        m_isFly = true;
        m_circleCollider.isTrigger = true;
        m_rigidbody.velocity = velocity;
        m_flyCoroutine = StartCoroutine(Fly(flyTime));
    }

    IEnumerator Fly(float flyTime)
    {
        float leftTime = flyTime;
        Vector2 velocity = m_rigidbody.velocity;
        while(leftTime > 0)
        {
            leftTime -= Time.deltaTime;
            if(leftTime * 2 < flyTime)
            {
                m_rigidbody.velocity = velocity * leftTime * 2 / flyTime;
            }
            yield return null;
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
        m_circleCollider.enabled = true;
    }

    IEnumerator GrowToDead(float begSize, float finSize)
    {
        m_circleCollider.enabled = false;

        float passedTime = 0;
        while (passedTime < m_growTime)
        {
            passedTime += Time.deltaTime;
            float k = (finSize - begSize) * passedTime / m_growTime + begSize;
            gameObject.transform.localScale = new Vector3(k, k, k);
            m_spriteRenderer.color = new Color(m_spriteRenderer.color.r, m_spriteRenderer.color.g, m_spriteRenderer.color.b, 1 - k);
            yield return null;
        }
        KillEnemy(true);
    }


    //public virtual void CreateNewEnemy(GameObject player , GameObject prefabEnemy, Vector3 position , bool isStatic , float speed , float atk , float size , int color)
    //{
    //    GameObject newEnemy = Instantiate(prefabEnemy);
    //    newEnemy.transform.position = position;
    //    newEnemy.transform.localScale = new Vector3 (size, size, size);
    //    Enemy enemy = newEnemy.GetComponent<Enemy>();
    //    enemy.m_player = player;
    //    enemy.m_isFly = false;
    //    enemy.m_isStatic = isStatic;
    //    enemy.m_speed = speed;
    //    enemy.m_atk = atk;
    //    enemy.m_size = size;
    //    enemy.m_color = (1 << color);
    //    if(color < 0 || color > 2)
    //    {
    //        Debug.Log("wrong color");
    //    }
    //}

    public virtual void Initialization(bool isStatic , float speed, float atk, float begSize, float finSize, int color , float growTime , Sprite body)
    {
        m_spriteRenderer.sprite = body;
        m_speed = speed;
        m_atk = atk;
        m_color = color;
        m_growTime = growTime;

        m_isFly = false;
        m_isStatic = isStatic;

        if(m_color == (1 << 3) - 1)
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
            EventManager.Instance.GetScore?.Invoke(m_size);
        }

        if(m_flyCoroutine != null)
        {
            StopCoroutine(m_flyCoroutine);
            m_flyCoroutine = null;
        }
        Destroy(gameObject);
    }
}
