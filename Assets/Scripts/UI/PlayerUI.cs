using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PlayerUI : MonoBehaviour, IPointerDownHandler
{

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
        gameSystem.StartGame();
        gameSystem.ChangeWeapon(WeaponsList.Assault());
    }

    // Update is called once per frame
    void Update()
    {

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
                    gameSystem.ChangeWeapon(WeaponsList.Pistol());
                else
                    gameSystem.ChangeWeapon(WeaponsList.Assault());
            }
    }

}
