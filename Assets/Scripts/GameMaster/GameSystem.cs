using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameSystem : MonoBehaviour, IEnemySpawn
{

    public string stageName = "STAGE_NAME";
    public GameObject player;
    public Transform playerRespawnPoint;
    public int enemyLimit;
    public EType[] enemyType;
    public int[] enemyTypeLimit;
    public int killUntilBoss;

    private int[] enemyNumberByType;
    private List<EnemySpawn> enemySpawnerList;
    private PlayerMovement pMovement;
    private PlayerUI pUI;
    private int totalKill = 0;

    public static GameSystem GetGameSystem()
    {
        return GameObject.Find(ConstMask.GAME_SYSTEM).GetComponent<GameSystem>();
    }

    // Use this for initialization
    void Start()
    {
        pUI = GameObject.Find("UI").GetComponent<PlayerUI>();
        enemyNumberByType = new int[enemyType.Length];
        enemySpawnerList = new List<EnemySpawn>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetStageName()
    {
        return stageName;
    }

    public void AddEnemySpawnLocation(EnemySpawn spawner)
    {
        if (enemySpawnerList == null)
            enemySpawnerList = new List<EnemySpawn>();
        spawner.SetISpawn(this);
        enemySpawnerList.Add(spawner);
    }

    public void Respawn()
    {
        pMovement.SetAlive(true);
        player.transform.position = playerRespawnPoint.position;
    }

    public void GameOver()
    {
        pMovement.SetAlive(false);
    }

    public void RestartGame()
    {
    }

    public void StartGame()
    {
        pMovement = player.GetComponent<PlayerMovement>();
        pMovement.SetAlive(true);
        foreach (EnemySpawn spawn in enemySpawnerList)
        {
            if (spawn.killRequirement == 0)
                spawn.StartSpawnEnemy();
        }
    }

    public void ChangeWeapon(WeaponsPrefab weapon)
    {
        pMovement.ChangeWeapon(weapon);
    }

    public void AddKillCount(int kill)
    {
        pUI.AddKillCount(kill);
        totalKill++;
        if (totalKill >= killUntilBoss)
        {
            StopAllSpawn();
        }
        StartSpawnAtKillNumber();
    }

    public void KillEnemyOfType(EType type)
    {
        int index = 0;
        foreach (EType etype in enemyType)
        {
            if (etype == type)
            {
                enemyNumberByType[index]--;
                if (enemyNumberByType[index] < enemyTypeLimit[index])
                {
                    StartSpawnType(type);
                }
                break;
            }
            index += 1;
        }
    }
    private void GetPlayerMovement()
    {
        pMovement = player.GetComponent<PlayerMovement>();
    }

    public void AddEnemyNumber(EType type)
    {
        int index = 0;
        foreach (EType etype in enemyType)
        {
            if (etype == type)
            {
                enemyNumberByType[index]++;
                if (enemyNumberByType[index] >= enemyTypeLimit[index])
                {
                    StopSpawnType(type);
                }
                break;
            }
            index += 1;
        }
    }

    private void StopSpawnType(EType eType)
    {
        foreach (EnemySpawn spawn in enemySpawnerList)
        {
            if (spawn.type == eType)
            {
                spawn.StopSpawnEnemy();
            }
        }
    }

    private void StopAllSpawn()
    {
        foreach (EnemySpawn spawn in enemySpawnerList)
        {
            spawn.StopSpawnEnemy();
        }
    }

    private void StartSpawnAtKillNumber()
    {
        foreach (EnemySpawn spawn in enemySpawnerList)
        {
            if (spawn.killRequirement == totalKill)
            {
                spawn.StartSpawnEnemy();
            }
        }
    }

    private void StartSpawnType(EType eType)
    {
        List<EnemySpawn> temp = new List<EnemySpawn>();
        foreach (EnemySpawn spawn in enemySpawnerList)
        {
            if (spawn.type == eType && spawn.killRequirement <= totalKill && totalKill < killUntilBoss)
            {
                temp.Add(spawn);
            }
        }
        if (temp.Count > 0)
            temp[UnityEngine.Random.Range(0, temp.Count)].StartSpawnEnemy();
    }
}
