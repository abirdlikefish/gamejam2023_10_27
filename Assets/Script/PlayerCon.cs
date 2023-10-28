using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCon : MonoBehaviour,IPlayerHurt
{
    [Header("基础属性")]
    public float health;//生命值
    public float speed;//速度
    [Header("击飞敌人")] 
    public float maxHitForce;//最大击飞力
    public float maxHitTime;//最大蓄力值
    private float time = 0f;
    public float value;
    public float enemySpeed;
    public float enemyFlyTime;
    [Header("敌人")] 

    private Vector2 moveDir;// 移动方向
    private Vector2 mousePos;//鼠标位置
    private Camera playerCam;//摄像机


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
        playerCam=Camera.main;
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
        slider.value = time / maxHitTime;
    }

    private void FixedUpdate()
    {
        PlayerMove();
        HandMove();
    }

    void PlayerInput()
    {
        moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);

        
        if (Input.GetMouseButton(0))
        {
            time += Time.deltaTime;
            time = time < maxHitTime ? time : maxHitTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //输出蓄力值
            HitEnemy();
            time = 0f;
        }
    }

    void PlayerMove()
    {
        rb.velocity = moveDir * speed;
    }

    void HandMove()
    {
        // 计算手的朝向
        Vector2 direction = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (mousePos.x <transform.position.x )
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        // 旋转手的角度
        hand.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void HitEnemy()
    {
        //创建扇形区域并击飞
        handCl.enabled = true;
        value = time / maxHitTime;
        //关闭扇形区域并初始化enemyList
        StartCoroutine(StartHit());
    }
    IEnumerator StartHit()
    {
        handAnimator.enabled = true;
        yield return new WaitForSeconds(handAnimator.GetCurrentAnimatorStateInfo(0).length);
        handAnimator.enabled = false;
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
