using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSystem : MonoBehaviour {

    public string stageName = "STAGE_NAME";

    public List<EnemySpawn> enemySpawnerList;

	// Use this for initialization
	void Start () {
        enemySpawnerList = new List<EnemySpawn>();
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
        enemySpawnerList.Add(spawner);
    }
}
