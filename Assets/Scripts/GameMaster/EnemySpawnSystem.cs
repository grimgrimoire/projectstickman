using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnSystem : MonoBehaviour {

    List<EnemySpawnNew> spawnerList = new List<EnemySpawnNew>();
    List<EnemySpawnRegister> spawnRegistry = new List<EnemySpawnRegister>();
    List<EnemySpawnNew> tempSpawnList = new List<EnemySpawnNew>();

    public GameObject bossPrefab;
    public Transform bossSpawnArea;

    public int killUntilBoss;
    private int totalKill;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
     
    }

    public int GetTotalKill()
    {
        return totalKill;
    }

    public bool IsTotalKillReached()
    {
        return totalKill >= killUntilBoss;
    }

    public void Spawn(EType type, GameObject prefab)
    {
        foreach(EnemySpawnNew spawn in spawnerList)
        {
            if (spawn.IsRegisteredType(type))
                tempSpawnList.Add(spawn);
        }
        if(tempSpawnList.Count > 0)
        {
            tempSpawnList[Random.Range(0, tempSpawnList.Count)].SpawnEnemy(prefab, type);
        }
        tempSpawnList.Clear();
    }

    public void RegisterEnemyRegistry(EnemySpawnRegister registry)
    {
        spawnRegistry.Add(registry);
    }

    public void RegisterSpawner(EnemySpawnNew spawner)
    {
        spawnerList.Add(spawner);
    }

    public void AddKillCount(EType type)
    {
        
        foreach(EnemySpawnRegister registry in spawnRegistry)
        {
            if (totalKill+1 == registry.killRequirement)
            {
                registry.StartSpawnEnemy();
            }

            if (registry.type == type)
            {
                registry.KillOneUnit();
                //break;
            }
        }

        totalKill++;
        if (IsObjectiveCleared())
        {
            SpawnBoss();
        }
    }

    private bool IsObjectiveCleared()
    {
        if (IsTotalKillReached())
        {
            foreach(EnemySpawnRegister registry in spawnRegistry)
            {
                if (!registry.IsAllKilled())
                    return false;
            }
            return true;
        }
        else
            return false;
    }

    private void SpawnBoss()
    {
        Debug.Log("SPAWN BOSS!!");
        if (bossPrefab != null) {
            GameObject instance = Instantiate(bossPrefab);
            bossPrefab.transform.position = bossSpawnArea.position;
        }
    }

    public void StartSpawn()
    {
        foreach (EnemySpawnRegister registry in spawnRegistry)
        {
            registry.StartSpawnEnemy();
        }
    }
}
