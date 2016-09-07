using UnityEngine;
using System.Collections;
using System;

public class EnemySpawn : MonoBehaviour {

    public GameObject[] enemyListPrefab;
    public int[] spawnNumber;
    public float[] spawnDelay;

	// Use this for initialization
	void Start () {
	    if(enemyListPrefab.Length != spawnNumber.Length)
        {
            throw new Exception("Enemy prefab and spawn count needs to match");
        }
        else
        {
            for (int i=0; i< spawnNumber.Length; i++)
            {
                StartCoroutine(SpawnEnemyWithInterval(i));
            }
        }
	}

	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator SpawnEnemyWithInterval(int index)
    {
        while(spawnNumber[index] > 0)
        {
            yield return new WaitForSeconds(spawnDelay[index]);
            spawnNumber[index]--;
            GameObject instance = Instantiate(enemyListPrefab[index]);
            instance.transform.position = transform.position;
        }
    }
}
