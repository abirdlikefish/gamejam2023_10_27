using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    protected float m_attackTime_lef ;
    protected float m_rotateSpeed ;
    protected float m_enemyFlySpeed;
    protected float m_force ;
    protected float m_flyTime ;

    protected Transform m_player ;
    protected BoxCollider2D m_weaponCollider;
    void Awake()
    {
        m_attackTime_lef = 0;
        m_rotateSpeed = 720;

        m_player = transform.parent;
        if(m_player == null)
        {
            Debug.Log("weapon's m_player is null");
        }
        m_weaponCollider = gameObject.GetComponent<BoxCollider2D>();
        if(m_weaponCollider == null)
        {
            Debug.Log("m_weaponCollider is null");
        }
        
    }

    public void Attack(float enemyFlySpeed , float force , float flyTime , float attackTime)
    {
        m_enemyFlySpeed = enemyFlySpeed;
        m_force = force ;
        m_flyTime = flyTime;

        StartCoroutine(Attacking(attackTime));
    }
    IEnumerator Attacking(float attackTime)
    {
        m_attackTime_lef = attackTime;
        m_weaponCollider.enabled = true;
        while(m_attackTime_lef > 0.01)
        {
            m_attackTime_lef -= Time.deltaTime;

            Vector3 currentRotation = transform.rotation.eulerAngles ;

            currentRotation.z += m_rotateSpeed * Time.deltaTime ;

            transform.rotation = Quaternion.Euler(currentRotation) ;

            yield return null;
        }
        
        m_weaponCollider.enabled = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            IEnemyBeHit enemyBeHit = collision.GetComponent<IEnemyBeHit>();
            if(enemyBeHit == null)
            {
                Debug.Log("enemyBeHit is null");
            }
            Vector2 midDirection = collision.transform.position - m_player.transform.position ;
            midDirection = midDirection.normalized;


            enemyBeHit.EnemyBeHit(m_enemyFlySpeed * midDirection , m_force ,m_flyTime);
        }
    }

}
