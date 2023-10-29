using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    [Header("生成属性")] 
    public float maxGenRad=13;
    public float minGenRad=10;
    public float enemy1Pro=0.2f;
    public float createTime = 2f;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("create enemy1");
            EventManager.Instance.CreateEnemy_1(new Vector3(Random.Range(-5,5) , Random.Range(-5,5) , 0) , 1 , 0 , 1 , -1 , 1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("create enemy2");
            EventManager.Instance.CreateEnemy_2(new Vector3(Random.Range(-5,5) , Random.Range(-5,5) , 0) , 1 , 1 , 0 , 1 , -1 , 1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("create enemy3");
            EventManager.Instance.CreateEnemy_1(gameObject.transform.position , 1 , 0 , 1 , 1 , 1);
        }*/
        timer += Time.deltaTime;
        if (timer >= createTime)
        {
            Create();
            timer = 0f;
        }


    }
    void Create()
    {
        float spawnDistance = Random.Range(minGenRad, maxGenRad);
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = transform.position + (Vector3)randomDirection * spawnDistance;
        float i = Random.Range(0f, 1f);
        if (i<=enemy1Pro)
        {
            EventManager.Instance.CreateEnemy_1(spawnPosition, 1, 0, 1, -1, 1);
        }
        else
        {
            EventManager.Instance.CreateEnemy_2(spawnPosition , 1 , 1 , 0 , 1 , -1 , 1);
        }
    }
}
