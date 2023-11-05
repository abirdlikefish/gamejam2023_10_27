using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{
    public static EnemyGeneration Instance ;
    float cameraWidth;
    float cameraHeight;

    

    // 3 - 1
    public float hard_1;
    protected float hard_1_factor;
    protected bool hard_1_flag = false;
    protected float hard_1_lastTime = 0;

    // 4 - 2
    public float hard_2;
    protected float hard_2_factor;
    protected bool hard_2_flag = false;
    protected float hard_2_lastTime = 0;
    void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }
        // 3 - 1
        hard_1 = 3;
        hard_1_factor = 0.990886678f;
        hard_1_flag = false;
        hard_1_lastTime = 0;

        // 4 - 2
        hard_2 = 4;
        hard_2_factor = 0.99615658722f;
        hard_2_flag = false;
        hard_2_lastTime = 0;

        cameraHeight = Camera.main.orthographicSize * 2;
        cameraWidth = Camera.main.aspect * cameraHeight * 2;
    }
    
    private void Start() 
    {
        EnemyBoss enemy = (ObjectPool.Instance.GetGameObject(ObjectPool.Instance.prefabEnemyBoss)).GetComponent<EnemyBoss>();
        enemy.Initialization_Boss(10);
        enemy.Initialization(Vector3.zero , 1f , 1f , 1f , 0 , 1 , 4 - hard_1 );
    }

    void Update()
    {

        if (Random.Range(0, 60) == 0)
        {
            if (!hard_1_flag)
            {
                hard_1 *= hard_1_factor;
                if (hard_1 < 1)
                {
                    hard_1 = 1;
                    hard_1_flag = true;
                }
            }

            if (!hard_2_flag)
            {
                hard_2 *= hard_2_factor;
                if (hard_2 < 2)
                {
                    hard_2 = 2;
                    hard_2_flag = true;
                }
            }
        }

        if (Time.time - hard_1_lastTime > hard_1 * Random.Range(0.9f, 1.1f))
        {
            Vector3 midPosition =new Vector3(Random.Range(-cameraWidth, cameraWidth), Random.Range(-cameraHeight, cameraHeight), 0) + Camera.main.transform.position;
            midPosition.z = Random.Range(-0.5f, 0.5f);
            int midColor = (1 << Random.Range(0,3) );
            int midSkill = 1;
            if(hard_1 < 2.5 && Random.Range(0,hard_1) < 1.0f)
            {
                midSkill |= (1 << Random.Range(1,4) );
            }

            EnemyBase enemy = (ObjectPool.Instance.GetGameObject(ObjectPool.Instance.prefabEnemy)).GetComponent<EnemyBase>();
            enemy.Initialization(midPosition , 0.1f , 4 - hard_1 , 1f , midColor , midSkill , 4 - hard_1 );
            Debug.Log("create Enemy");
            hard_1_lastTime = Time.time;
        }

        // if (Time.time - hard_2_lastTime > hard_2 * Random.Range(0.9f, 1.1f))
        // {
        //     Vector3 midPosition =new Vector3(Random.Range(-cameraWidth, cameraWidth), Random.Range(-cameraHeight, cameraHeight), 0) + Camera.main.transform.position;
        //     midPosition.z = 0;

        //     int midEnemyType = 0;
        //     // if(Random.Range(0, 20) == 0)
        //     if(midFlag)
        //     {
        //         // Debug.Log("create boss");
        //         midEnemyType |= (1 << 4);
        //         midFlag = false;
        //     }
        //     else if (Random.Range(0, 20) <= 3)
        //     {
        //         // Debug.Log("create colorenemy");
        //         midEnemyType |= 1;
        //     }
        //     EventManager.Instance.CreateEnemy(midEnemyType | 2 , midPosition,Random.Range(1.0f + (4 - hard_2) / 2, 1.0f + 4.0f - hard_2), 5 - hard_2, 0.5f, 5 - hard_2, -1, 1);
        //     hard_2_lastTime = Time.time;
        // }
    }
}
