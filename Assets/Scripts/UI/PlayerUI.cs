using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PlayerUI : MonoBehaviour, IPointerDownHandler
{
    public GameObject gameplayUI;
    public GameObject pauseUI;
    public GameObject winGame;

    public RectTransform playerHealthBar;
    public RectTransform bossHealthBar;
    public Text playerLive;
    public Text killCount;
    public Text mainLabel;
    public GameObject curtain;
    public Image changeWeapon;

    public int killCountInt;
    public int playerLiveInt = 3;
    public float playerHealth = 100;
    private float playerFullHealth;
    private bool isInvincible = false;
    private bool isGameStarted = false;
    private int equipedWeapon = 1;

    GameSystem gameSystem;

    // Use this for initialization
    void Start()
    {
        gameSystem = GameSystem.GetGameSystem();
        if (gameSystem == null)
        {
            throw new System.Exception("Fatal error: Game system is not found!!");
        }
        playerFullHealth = playerHealth;
        StartCoroutine(StartGameSquence());
    }

    IEnumerator StartGameSquence()
    {
        mainLabel.text = gameSystem.GetStageName() + "\nStart";
        yield return new WaitForSeconds(1f);
        curtain.SetActive(false);
        mainLabel.enabled = false;
        isGameStarted = true;
        gameSystem.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameSystem.isPaused && isGameStarted)
            {
                PauseGame();
            }
            else if(isGameStarted)
            {
                UnpauseGame();
            }
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && isGameStarted)
            PauseGame();
    }

    public void PauseGame()
    {
        Debug.Log("GameisPaused");
        gameSystem.isPaused = true;
        Time.timeScale = 0;
        gameplayUI.SetActive(false);
        pauseUI.SetActive(true);
    }

    public void UnpauseGame()
    {
        gameSystem.isPaused = false;
        Time.timeScale = 1;
        gameplayUI.SetActive(true);
        pauseUI.SetActive(false);
    }

    public void QuitToTitle()
    {
        gameSystem.QuitToTitle();
    }

    public void PlayerTakeDamage(int damage)
    {
        if (!isInvincible)
        {
            StartCoroutine(PostHitInvincibility());
            playerHealth -= damage;
            UpdatePlayerHealth();
            if (playerHealth <= 0)
            {
                PlayerDie();
            }
        }
    }

    IEnumerator PostHitInvincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(2);
        isInvincible = false;
    }

    public void UpdateBossHealth(float percentage)
    {
        bossHealthBar.sizeDelta = new Vector2(20, percentage * 220);
    }

    public void UpdatePlayerHealth()
    {
        playerHealthBar.sizeDelta = new Vector2(20, playerHealth / playerFullHealth * 220);
    }

    public void PlayerDie()
    {
        playerLiveInt--;
        if (playerLiveInt >= 0)
        {
            playerHealth = playerFullHealth;
            UpdatePlayerHealth();
            playerLive.text = "X" + playerLiveInt;
            gameSystem.Respawn();
        }
        else
        {
            mainLabel.enabled = true;
            mainLabel.text = "YOU DIE";
            gameSystem.GameOver();
        }
    }

    public void AddKillCount(int kill)
    {
        killCountInt += kill;
        killCount.text = "Kills: " + killCountInt;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null)
            if (eventData.pointerEnter.name == changeWeapon.gameObject.name)
            {
                equipedWeapon += 1;
                equipedWeapon = equipedWeapon % 2;
                if (equipedWeapon == 1)
                    gameSystem.ChangeWeapon(WeaponsList.GetPrimaryWeaponOnIndex(
            GameSession.GetSession().GetPlayer().GetPrimaryWeapon()));
                else
                    gameSystem.ChangeWeapon(WeaponsList.GetSecondaryWeaponOnIndex(
            GameSession.GetSession().GetPlayer().GetSecondaryWeapon()));
            }
    }

    public void GameWin()
    {
        winGame.SetActive(true);
        gameplayUI.SetActive(false);
        isGameStarted = false;
    }

}
