using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public RectTransform playerHealthBar;
    public RectTransform bossHealthBar;
    public Text playerLive;
    public Text waveNumber;
    public Text mainLabel;
    public GameObject curtain;

    public int waveNumberInt;
    public int playerLiveInt = 3;
    public float playerHealth = 100;
    private float playerFullHealth;

    GameSystem gameSystem;

	// Use this for initialization
	void Start () {
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
        if(gameSystem == null)
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
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayerTakeDamage(int damage)
    {
        playerHealth -= damage;
        UpdatePlayerHealth();
        if(playerHealth <= 0)
        {
            PlayerDie();
        }
    }

    public void UpdatePlayerHealth()
    {
        playerHealthBar.sizeDelta = new Vector2(playerHealth/playerFullHealth * 300, 50);
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


}
