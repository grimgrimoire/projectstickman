using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{

    public string stageName = "STAGE_NAME";
    public GameObject player;
    public Transform playerRespawnPoint;

    private PlayerMovement pMovement;
    private PlayerUI pUI;
    EnemySpawnSystem spawnSystem;

    public bool isPaused = false;

    public static GameSystem GetGameSystem()
    {
        return GameObject.Find(ConstMask.GAME_SYSTEM).GetComponent<GameSystem>();
    }

    // Use this for initialization
    void Start()
    {
        pUI = GameObject.Find("UI").GetComponent<PlayerUI>();
        spawnSystem = GetComponent<EnemySpawnSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuitToTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(ConstMask.SCENE_MAIN_MENU);
    }

    public string GetStageName()
    {
        return stageName;
    }

    public PlayerUI GetPlayerUI()
    {
        return pUI;
    }

    public void AddKillCount(EType type)
    {
        pUI.AddKillCount(1);
        spawnSystem.AddKillCount(type);
    }

    public EnemySpawnSystem GetSpawnSystem()
    {
        return spawnSystem;
    }

    public void Respawn()
    {
        pMovement.SetAlive(true);
        player.transform.position = playerRespawnPoint.position;
    }

    public void BossDead()
    {
        pUI.GameWin();
        GameSession.GetSession().SaveScore(ConstMask.NAME_STAGE_1, "100000");
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
        spawnSystem.StartSpawn();
    }

    public void ChangeWeapon(WeaponsPrefab weapon)
    {
        pMovement.ChangeWeapon(weapon);
    }

}
