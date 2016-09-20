using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSystem : MonoBehaviour {

    public string stageName = "STAGE_NAME";
    public GameObject player;
    public Transform playerRespawnPoint;

    public List<EnemySpawn> enemySpawnerList;
    private PlayerMovement pMovement;
    private PlayerUI pUI;

    public static GameSystem GetGameSystem()
    {
        return GameObject.Find(ConstMask.GAME_SYSTEM).GetComponent<GameSystem>();
    }

	// Use this for initialization
	void Start () {
        pUI = GameObject.Find("UI").GetComponent<PlayerUI>();
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
    }

    private void GetPlayerMovement()
    {
        pMovement = player.GetComponent<PlayerMovement>();
    }

}
