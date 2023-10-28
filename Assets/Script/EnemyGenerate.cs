using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    [Header("生成属性")] 
    public float maxGenRad;
    public float minGenRad;

    private Enemy enemyPerfab;
    private PlayerCon player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerCon>();
        //导入预制体
        //enemyPerfab = Resources.Load<Enemy>("Perfab/");
    }

    void Update()
    {
        
    }

    void Generate(Enemy prefabEnemy , bool isStatic , float speed , float atk , float size , int color)
    {
        float spawnDistance = Random.Range(minGenRad, maxGenRad);
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = player.transform.position + (Vector3)randomDirection * spawnDistance;
        
        Enemy enemy = Instantiate(prefabEnemy,spawnPosition,Quaternion.identity);
        enemy.transform.localScale = new Vector3 (size, size, size);
        enemy.m_player = player.gameObject;
        /*enemy.m_isFly = false;
        enemy.m_isStatic = isStatic;
        enemy.m_speed = speed;
        enemy.m_atk = atk;
        enemy.m_size = size;
        enemy.m_color = (1 << color);*/
        if(color < 0 || color > 2)
        {
            Debug.Log("wrong color");
        }
        
    }
}
