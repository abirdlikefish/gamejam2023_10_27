using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
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
        }
    }
}
