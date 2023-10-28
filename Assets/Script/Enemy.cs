using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour , IEnemyBeHit
{
    protected bool m_isStatic ;
    protected bool m_isFly = false;
    protected float m_size = 1;
    protected float m_speed = 1;
    protected float m_atk;
    protected int m_color;
    public GameObject m_player;  // 后改为protected
    public GameObject prefabEnemy;   // 敌人预制体，inspector窗口中获取
    protected CircleCollider2D m_circleCollider;
    protected Rigidbody2D m_rigidbody;

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
        m_camera = Camera.main;
        if(m_circleCollider == null)
        {
            Debug.Log("enemy无法获取collider");
        }
        if(m_rigidbody == null)
        {
            Debug.Log("enemy无法获取rigidbody");
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if(enemy == null)
            {
                Debug.Log("无法获取敌人");
            }
            if(enemy.IsFly)
            {
                return;
            }
            if(m_flyCoroutine == null)
            {
                Debug.Log("m_flyCoroutine 为 null");
            }
            StopCoroutine(m_flyCoroutine);
            m_flyCoroutine = null;
            m_isFly = false;
            m_rigidbody.velocity = new Vector2(0, 0);
            m_circleCollider.isTrigger = false;

            if(prefabEnemy == null)
            {
                Debug.Log("enemy无法获取Enemy预制体");
            }

            m_color |= enemy.m_color;
            if(m_color == (1 << 3) - 1)
            {
                //变黑，消灭敌人
                enemy.KillEnemy(true);
                this.KillEnemy(true);
                return;
            }
            
            transform.position = (enemy.transform.position * enemy.m_size + transform.position * m_size) / (m_size + enemy.m_size);
            m_isStatic = enemy.m_isStatic;
            m_size += enemy.m_size;
            transform.localScale = new Vector3(m_size, m_size, m_size);

            enemy.KillEnemy(false);
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


    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //玩家受伤
            IPlayerHurt playerHurt = collision.gameObject.GetComponent<IPlayerHurt>();
            playerHurt.Hurt(m_atk);
        }
    }

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

    public virtual void CreateNewEnemy(GameObject player , GameObject prefabEnemy, Vector3 position , bool isStatic , float speed , float atk , float size , int color)
    {
        GameObject newEnemy = Instantiate(prefabEnemy);
        newEnemy.transform.position = position;
        newEnemy.transform.localScale = new Vector3 (size, size, size);
        Enemy enemy = newEnemy.GetComponent<Enemy>();
        enemy.m_player = player;
        enemy.m_isFly = false;
        enemy.m_isStatic = isStatic;
        enemy.m_speed = speed;
        enemy.m_atk = atk;
        enemy.m_size = size;
        enemy.m_color = (1 << color);
        if(color < 0 || color > 2)
        {
            Debug.Log("wrong color");
        }
    }

    public virtual void KillEnemy(bool isScore)
    {
        //计分

        if(m_flyCoroutine != null)
        {
            StopCoroutine(m_flyCoroutine);
            m_flyCoroutine = null;
        }
        Destroy(gameObject);
    }
}
