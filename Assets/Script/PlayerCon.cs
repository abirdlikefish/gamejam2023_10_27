using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCon : MonoBehaviour,IPlayerHurt
{
    [Header("基础属性")]
    public float health=10;//生命值
    public float speed=5;//速度
    [Header("击飞敌人")] 
    public float maxHitForce=10;//最大击飞力
    public float maxHitTime=5;//最大蓄力值
    private float hitTimer = 0f;//蓄力计时器

    public float hitTime = 0.7f;//旋转时间
    private float timer = 0f;//旋转计时器
    private bool isHit=false;
    
    public float value;
    public float enemySpeed=5;
    public float enemyFlyTime=3;
    [Header("敌人")] 

    private Vector2 moveDir;// 移动方向


    private Rigidbody2D rb;
    private Collider2D cl;
    private Hand hand;
    private Collider2D handCl;
    private Animator handAnimator;
    private SpriteRenderer mySpr;

    public Slider slider;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cl = GetComponent<Collider2D>();
        mySpr = GetComponent<SpriteRenderer>();
        hand = GameObject.Find("Hand").GetComponent<Hand>();
        handCl = hand.GetComponent<Collider2D>();
        handAnimator = hand.GetComponent<Animator>();
        handAnimator.enabled = false;
        handCl.enabled = false;
    }
    void Update()
    {
        PlayerInput();
        Dead();
        slider.value = hitTimer / maxHitTime;
        if (isHit)
        {
            timer += Time.deltaTime;
            hand.transform.rotation=quaternion.Euler(0,0,timer/hitTime * 360);
        }
        if (timer >= hitTime)
        {
            timer = 0f;
            isHit = false;
            hand.transform.rotation=quaternion.Euler(0,0,0);
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerInput()
    {
        moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        
        if (Input.GetKey(KeyCode.Space))
        {
            hitTimer += Time.deltaTime;
            hitTimer = hitTimer < maxHitTime ? hitTimer : maxHitTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            //输出蓄力值
            HitEnemy();
            hitTimer = 0f;
        }
    }

    void PlayerMove()
    {
        rb.velocity = moveDir * speed;
        if (moveDir.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void HitEnemy()
    {
        //创建圆心形区域并击飞
        handCl.enabled = true;
        value = hitTimer / maxHitTime;
        //关闭圆形区域
        StartCoroutine(StartHit());
        isHit = true;
    }
    IEnumerator StartHit()
    {
        yield return new WaitForSeconds(hitTime);
        handCl.enabled = false;
    }
    public void Hurt(float damage)
    {
        health -= damage;
        StartCoroutine(StartHurt());
    }

    IEnumerator StartHurt()
    {
        mySpr.color=Color.red;
        yield return new WaitForSeconds(0.2f);
        mySpr.color=Color.white;
    }
    void Dead()
    {
        if (health <= 0)
        {
            //游戏结束
            
        }
    }
}
