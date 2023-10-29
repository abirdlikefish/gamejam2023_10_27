using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    public Sprite[] enemyBody = new Sprite[6];
    public Sprite[] enemyBodyColor = new Sprite[6];
    public Sprite[] enemyEyes = new Sprite[6];
    public Sprite[] enemyMouth = new Sprite[6];
    public Sprite enemyBoss;

    public GameObject player;

    public GameObject prefabEnemy;





    // 1 static enemy
    // 2 normal enemy
    //public virtual void CreateNewEnemy_1(Vector3 position, float atk, float begSize, float finSize, int color, float growTime)
    //{
    //    GameObject newEnemy = Instantiate(prefabEnemy);
    //    newEnemy.transform.position = position;
    //    newEnemy.transform.localScale = new Vector3(begSize, begSize, begSize);
    //    Enemy enemy = newEnemy.GetComponent<Enemy>();


    //    if (color < -1 || color > 7)
    //    {
    //        Debug.Log("wrong color");
    //    }
    //    else if (color == -1)
    //    {
    //        color = Random.Range(0, 3);
    //        color = (1 << color);
    //    }

    //    Sprite spriteBody = enemyBody[Random.Range(0, 6)];
    //    Sprite spriteMouth = enemyMouth[Random.Range(0,6)];
    //    Sprite spriteEyes = enemyEyes[Random.Range(0,6)];

    //    enemy.Initialization(player , true, 0, atk, begSize, finSize, color, growTime, spriteBody , spriteMouth , spriteEyes);
    //}

    //public virtual void CreateNewEnemy_2(Vector3 position, float speed, float atk, float begSize, float finSize, int color, float growTime)
    //{
    //    GameObject newEnemy = Instantiate(prefabEnemy);
    //    newEnemy.transform.position = position;
    //    newEnemy.transform.localScale = new Vector3(begSize, begSize, begSize);
    //    Enemy enemy = newEnemy.GetComponent<Enemy>();


    //    if (color < -1 || color > 7)
    //    {
    //        Debug.Log("wrong color");
    //    }
    //    else if (color == -1)
    //    {
    //        color = Random.Range(0, 3);
    //        color = (1 << color);
    //    }

    //    Sprite spriteBody = enemyBody[Random.Range(0, 6)];
    //    Sprite spriteMouth = enemyMouth[Random.Range(0, 6)];
    //    Sprite spriteEyes = enemyEyes[Random.Range(0, 6)];

    //    enemy.Initialization(player, false, speed, atk, begSize, finSize, color, growTime, spriteBody, spriteMouth, spriteEyes);
    //}
    public virtual void CreateNewEnemy(int enemyType , Vector3 position, float speed, float atk, float begSize, float finSize, int color, float growTime)
    {
        GameObject newEnemy = Instantiate(prefabEnemy);
        newEnemy.transform.position = position;
        newEnemy.transform.localScale = new Vector3(begSize, begSize, begSize);
        Enemy enemy = newEnemy.GetComponent<Enemy>();


        if (color < -1 || color > 7)
        {
            Debug.Log("wrong color");
        }
        else if (color == -1 && (enemyType & 1) == 0 )
        {
            color = Random.Range(0, 3);
            color = (1 << color);
        }

        Sprite spriteMouth = enemyMouth[Random.Range(0, 6)];
        Sprite spriteEyes = enemyEyes[Random.Range(0, 6)];
        Sprite spriteBody = enemyBody[Random.Range(0, 6)];
        if ((enemyType & 1) == 1)
        {
            spriteBody = enemyBodyColor[Random.Range(0, 6)];
        }
        else if(((enemyType >> 4) & 1 ) == 1)
        {
            spriteBody = enemyBoss;
        }
        else
        {
            spriteBody = enemyBody[Random.Range(0, 6)];
        }

        enemy.Initialization(enemyType , player, speed, atk, begSize, finSize, color, growTime, spriteBody, spriteMouth, spriteEyes);
    }


    void Start()
    {
        //EventManager.Instance.Enemy_1 += CreateNewEnemy_1;
        //EventManager.Instance.Enemy_2 += CreateNewEnemy_2;
        EventManager.Instance.Enemy += CreateNewEnemy;
    }

}
