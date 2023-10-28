using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;
using Unity.VisualScripting;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private PlayerCon player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCon>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            //敌人接口
            IEnemyBeHit enemyBeHit = other.GetComponent<IEnemyBeHit>();
            enemyBeHit.EnemyBeHit(player.enemySpeed*(other.transform.position-player.transform.position).normalized,player.value*player.maxHitForce,player.enemyFlyTime);
        }
    }
}
