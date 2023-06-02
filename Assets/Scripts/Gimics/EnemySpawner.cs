using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Stack<GameObject> enemies;
    [SerializeField] Transform player;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform[] spawntransforms;
    [SerializeField] int enemyCount, enemyMax;
    [SerializeField] float spawnDelay;

    void Awake()
    {
        enemies = new Stack<GameObject>();
        spawntransforms = GetComponentsInChildren<Transform>();
        for(int i = 0; i < 10; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.SetActive(false);
            enemy.GetComponent<Enemy>().spawner = this;
            enemy.transform.parent = transform;
            enemies.Push(enemy);
        }
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            if(enemyCount < enemyMax)
            {
                if (enemies.Count == 0)
                {
                    GameObject add = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                    add.SetActive(false);
                    add.GetComponent<Enemy>().spawner = this;
                    add.transform.parent = transform;
                    enemies.Push(add);
                }

                GameObject enemy = enemies.Pop();
                enemy.transform.position = spawntransforms[Random.Range(0, spawntransforms.Length)].position;
                enemy.GetComponent<Enemy>().Work(player);
                enemyCount++;
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void DestroyEnemy(GameObject enemy)
    {
        enemies.Push(enemy);
        enemyCount--;
    }
}
