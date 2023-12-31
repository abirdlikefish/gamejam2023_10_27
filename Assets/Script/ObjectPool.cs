using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    
    public Sprite[] enemyBody = new Sprite[6];
    public Sprite[] enemyBodyColor = new Sprite[6];
    public Sprite[] enemyEyes = new Sprite[6];
    public Sprite[] enemyMouth = new Sprite[6];
    public Sprite enemyBoss;
    public GameObject prefabEnemy;
    public GameObject prefabEnemyBoss;
    public GameObject prefabEnemyColor;

    public Dictionary<string , Queue<GameObject> > objectPool ;

    public GameObject GetGameObject(GameObject obj)
    {
        if(obj == null)
        {
            Debug.Log("obj is null");
        }
        string name = obj.name;
        // Debug.LogWarning("GetGameObject name: " + name);
        GameObject target = null;
        if (objectPool.ContainsKey(name) && objectPool[name].Count > 0)
        {
            target = objectPool[name].Dequeue();
            target.SetActive(true);
        }
        else
        {
            target = Instantiate(obj);
        }
        return target;
    }

    public void ReturnGameObject(GameObject obj)
    {
        string name = obj.name;
        if(!name.EndsWith("(Clone)"))
        {
            Debug.Log("failed to return gameobject");
        }
        name = name.Replace("(Clone)" , "");
        // Debug.LogWarning("ReturnGameObject name: " + name);
        obj.SetActive(false);
        if (objectPool.ContainsKey(name))
        {
            objectPool[name].Enqueue(obj);
        }
        else
        {
            objectPool.Add(name, new Queue<GameObject>());
            objectPool[name].Enqueue(obj);
        }
    }

    private void Start()
    {
    }



    public static ObjectPool Instance{private set ; get;}
    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }
        for(int i = 1 ; i <= 6 ; i++)
        {
            enemyBody[i - 1] = Resources.Load("Pictures/Enemy/body_" + i.ToString() ,typeof(Sprite)) as Sprite;
            if(enemyBody[i - 1] == null)
            {
                Debug.Log("enemyBody" + i.ToString() + " is null");
            }
            enemyBodyColor[i - 1] = Resources.Load("Pictures/Enemy/body_color_" + i.ToString(),typeof(Sprite)) as Sprite;
            if(enemyBodyColor[i - 1] == null)
            {
                Debug.Log("enemyBodyColor" + i.ToString() + " is null");
            }
            enemyEyes[i - 1] = Resources.Load("Pictures/Enemy/eye_" + i.ToString() , typeof(Sprite) ) as Sprite ;
            if(enemyEyes[i - 1] == null)
            {
                Debug.Log("enemyEyes" + i.ToString() + " is null");
            }
            enemyMouth[i - 1] = Resources.Load("Pictures/Enemy/mouth_" + i.ToString(), typeof(Sprite)) as Sprite;
            if(enemyMouth[i - 1] == null)
            {
                Debug.Log("enemyMouth" + i.ToString() + " is null");
            }
        }
        enemyBoss = Resources.Load("Pictures/Enemy/body_boss", typeof(Sprite)) as Sprite;
        if(enemyBoss == null)
        {
            Debug.Log("enemyBoss is null");
        }

        prefabEnemy = Resources.Load("Prefabs/Enemy" , typeof(GameObject) ) as GameObject;
        if(prefabEnemy == null)
        {
            Debug.Log("prefabEnemy is null");
        }
        prefabEnemyBoss = Resources.Load("Prefabs/EnemyBoss", typeof(GameObject) ) as GameObject;
        if(prefabEnemyBoss == null)
        {
            Debug.Log("prefabEnemyBoss is null");
        }
        prefabEnemyColor = Resources.Load("Prefabs/EnemyColor", typeof(GameObject) ) as GameObject;
        if(prefabEnemyColor == null)
        {
            Debug.Log("prefabEnemyColor is null");
        }

        objectPool = new Dictionary<string, Queue<GameObject>>();
    }
}
