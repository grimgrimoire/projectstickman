using UnityEngine;
using System.Collections;
using System;

public class EnemySpawn : MonoBehaviour
{

    public GameObject[] enemyListPrefab;
    public int[] spawnNumber;
    public float[] spawnDelay;

    // Use this for initialization
    void Start()
    {
        if (enemyListPrefab.Length != spawnNumber.Length && spawnDelay.Length != spawnNumber.Length)
        {
            throw new Exception("Enemy list, number & delay needs to match");
        }
        else
        {
            GameObject.Find("GameSystem").GetComponent<GameSystem>().AddEnemySpawnLocation(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartSpawnEnemy()
    {
        for (int i = 0; i < spawnNumber.Length; i++)
            StartCoroutine(SpawnEnemyWithInterval(i));
    }

    private void UpdateUI()
    {

    }

    IEnumerator SpawnEnemyWithInterval(int index)
    {
        while (spawnNumber[index] > 0)
        {
            yield return new WaitForSeconds(spawnDelay[index]);
            GameObject instance = Instantiate(enemyListPrefab[index]);
            instance.transform.position = transform.position;
            spawnNumber[index]--;
        }
    }
}
