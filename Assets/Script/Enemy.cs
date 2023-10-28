using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour , IEnemyBeHit
{
    protected bool m_isStatic;
    [SerializeField]
    protected bool m_isFly;
    [SerializeField]
    protected float m_size ;
    protected float m_speed ;
    protected float m_atk = 1;
    [SerializeField]
    protected int m_color;
    protected float m_growTime;

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
        if((!m_isStatic) && (!m_isFly))
        {
            Vector2 midDirection = m_player.transform.position - gameObject.transform.position;
            midDirection.Normalize();
            m_rigidbody.velocity = m_speed * midDirection;
        }

        Vector2 viewportPosition = m_camera.WorldToViewportPoint(gameObject.transform.position);
        if(viewportPosition.x < 0 || viewportPosition.y < 0 || viewportPosition.x > 1 || viewportPosition.y > 1)
        {
            //out of the range of camera
        }
    }


    public virtual void EnemyBeHit(Vector2 velocity, float strength , float flyTime)
    {
        if((!m_isFly) && strength < m_size )
        {
            return;
        }
        m_isFly = true;
        m_circleCollider.isTrigger = true;
        m_rigidbody.velocity = velocity;
        StartCoroutine(Fly(flyTime));
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
                if(m_rigidbody == null)
                {
                    Debug.Log("can't find m_rigidbody in fly");
                }
                m_rigidbody.velocity = velocity * leftTime * 2 / flyTime;
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
        m_circleCollider.enabled = false;

        float passedTime = 0;
        while (passedTime < m_growTime)
        {
            passedTime += Time.deltaTime;
            float k = (finSize - begSize) * passedTime / m_growTime + begSize;
            gameObject.transform.localScale = new Vector3(k, k, k);
            m_spriteRenderer.color = new Color(m_spriteRenderer.color.r, m_spriteRenderer.color.g, m_spriteRenderer.color.b, 1.0f - passedTime / m_growTime);
            yield return null;
        }
        KillEnemy(true);
    }


    public virtual void Initialization(GameObject player , bool isStatic , float speed, float atk, float begSize, float finSize, int color , float growTime , Sprite body , Sprite mouth , Sprite eyes)
    {
        m_player = player ;
        m_isStatic = isStatic;
        m_speed = speed;
        m_atk = atk;
        m_color = color;
        m_growTime = growTime;

        m_isFly = false;
        m_spriteRenderer.sprite = body;
        m_spriteRenderer_mouth.sprite = mouth;
        m_spriteRenderer_eyes.sprite = eyes;

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
            EventManager.Instance.AddScore(m_size);
        }
        Destroy(gameObject);
    }
}
