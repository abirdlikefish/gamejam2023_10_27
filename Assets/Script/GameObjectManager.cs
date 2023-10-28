using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    public Sprite[] enemyBody = new Sprite[6];
    public Sprite[] enemyEye = new Sprite[6];
    public Sprite[] enemyMouth = new Sprite[6];

    public GameObject player;

    public GameObject prefabEnemy;





    // 1 静止敌人
    // 2 运动敌人
    public virtual void CreateNewEnemy_1(Vector3 position, float atk, float begSize, float finSize, int color, float growTime)
    {
        GameObject newEnemy = Instantiate(prefabEnemy);
        newEnemy.transform.position = position;
        newEnemy.transform.localScale = new Vector3(begSize, begSize, begSize);
        Enemy enemy = newEnemy.GetComponent<Enemy>();


        if (color < -1 || color > 7)
        {
            Debug.Log("wrong color");
        }
        else if (color == -1)
        {
            color = Random.Range(0, 3);
            color = (1 << color);
        }

        Sprite spriteBody = enemyBody[Random.Range(0, 6)];

        enemy.Initialization(true, 0, atk, begSize, finSize, color, growTime, spriteBody);
    }

    public virtual void CreateNewEnemy_2(Vector3 position, float speed, float atk, float begSize, float finSize, int color, float growTime)
    {
        GameObject newEnemy = Instantiate(prefabEnemy);
        newEnemy.transform.position = position;
        newEnemy.transform.localScale = new Vector3(begSize, begSize, begSize);
        Enemy enemy = newEnemy.GetComponent<Enemy>();


        if (color < -1 || color > 7)
        {
            Debug.Log("wrong color");
        }
        else if (color == -1)
        {
            color = Random.Range(0, 3);
            color = (1 << color);
        }

        Sprite spriteBody = enemyBody[Random.Range(0, 6)];

        enemy.Initialization(true, speed, atk, begSize, finSize, color, growTime, spriteBody);
    }


    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.Enemy_1 += CreateNewEnemy_1;
        EventManager.Instance.Enemy_2 += CreateNewEnemy_2;
    }
}
