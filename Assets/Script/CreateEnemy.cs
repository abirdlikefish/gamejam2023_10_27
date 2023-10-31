using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    float cameraWidth;
    float cameraHeight;

    protected float totalTime = 0;

    // 3 - 1
    protected float hard_1;
    protected float hard_1_factor;
    protected bool hard_1_flag = false;
    protected float hard_1_lastTime = 0;

    // 4 - 2
    protected float hard_2;
    protected float hard_2_factor;
    protected bool hard_2_flag = false;
    protected float hard_2_lastTime = 0;

    // 0 color
    // 1 move
    // 2 boom
    // 3 split
    // 4 black

    void Start()
    {
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
        //Debug.Log("hello");
        
    }

bool midFlag = true;
    void Update()
    {
// if(midFlag)
// {
//     midFlag = false;
// }
// EventManager.Instance.CreateEnemy( (1 << 4), new Vector3 (0,0,0),Random.Range(1.0f + (4 - hard_2) / 2, 1.0f + 4.0f - hard_2), 5 - hard_2, 0.5f, 5 - hard_2, 1, 1);

        totalTime += Time.deltaTime;

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

        if (totalTime - hard_1_lastTime > hard_1 * Random.Range(0.9f, 1.1f))
        {
            Vector3 midPosition =new Vector3(Random.Range(-cameraWidth, cameraWidth), Random.Range(-cameraHeight, cameraHeight), 0) + Camera.main.transform.position;
            midPosition.z = 0;
            EventManager.Instance.CreateEnemy( 0 , midPosition, 0 , 4 - hard_1, 0.5f, 4 - hard_1, -1, 1);
// EventManager.Instance.CreateEnemy( 0 , midPosition, 0 , 4 - hard_1, 0.5f, 4 - hard_1, 1, 1);
            hard_1_lastTime = totalTime;
        }

        if (totalTime - hard_2_lastTime > hard_2 * Random.Range(0.9f, 1.1f))
        {
            Vector3 midPosition =new Vector3(Random.Range(-cameraWidth, cameraWidth), Random.Range(-cameraHeight, cameraHeight), 0) + Camera.main.transform.position;
            midPosition.z = 0;

            int midEnemyType = 0;
            // if(Random.Range(0, 20) == 0)
            if(midFlag)
            {
                Debug.Log("create boss");
                midEnemyType |= (1 << 4);
                midFlag = false;
            }
            else if (Random.Range(0, 20) <= 3)
            {
                Debug.Log("create colorenemy");
                midEnemyType |= 1;
            }
// EventManager.Instance.CreateEnemy(midEnemyType | 2 , midPosition,Random.Range(1.0f + (4 - hard_2) / 2, 1.0f + 4.0f - hard_2), 5 - hard_2, 0.5f, 5 - hard_2, 1, 1);
            EventManager.Instance.CreateEnemy(midEnemyType | 2 , midPosition,Random.Range(1.0f + (4 - hard_2) / 2, 1.0f + 4.0f - hard_2), 5 - hard_2, 0.5f, 5 - hard_2, -1, 1);
            hard_2_lastTime = totalTime;
        }


        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Debug.Log("create enemy1");
        //    EventManager.Instance.CreateEnemy_1(new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), 1, 0, 1, -1,1);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    Debug.Log("create enemy2");
        //    EventManager.Instance.CreateEnemy_2(new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), 1, 1, 0, 1,
        //        -1, 1);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    Debug.Log("create enemy3");
        //    EventManager.Instance.CreateEnemy_1(gameObject.transform.position, 1, 0, 1, 1, 1);
        //}
    }
}