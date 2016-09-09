using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSystem : MonoBehaviour {

    public string stageName = "STAGE_NAME";
    public GameObject player;

    public List<EnemySpawn> enemySpawnerList;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public string GetStageName()
    {
        return stageName;
    }

    public void AddEnemySpawnLocation(EnemySpawn spawner)
    {
        if(enemySpawnerList == null)
        {
            enemySpawnerList = new List<EnemySpawn>();
        }
        enemySpawnerList.Add(spawner);
    }

    public void GameOver()
    {

    }

    public void RestartGame()
    {

    }

    public void StartSpawnEnemy()
    {
        foreach(EnemySpawn spawn in enemySpawnerList)
        {
            spawn.StartSpawnEnemy();
        }
    }
}
