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

    GameSystem gameSystem;

	// Use this for initialization
	void Start () {
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
        if(gameSystem == null)
        {
            throw new System.Exception("Fatal error: Game system is not found!!");
        }
        StartCoroutine(StartGameSquence());
    }

    IEnumerator StartGameSquence()
    {
        mainLabel.text = gameSystem.GetStageName() + "\nStart";
        yield return new WaitForSeconds(1f);
        curtain.SetActive(false);
        mainLabel.enabled = false;
        gameSystem.StartSpawnEnemy();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayerTakeDamage(int damage)
    {
        playerHealth -= damage;
        UpdatePlayerHealth(damage);
        if(playerHealth <= 0)
        {
            PlayerDie();
        }
    }

    public void UpdatePlayerHealth(int damage)
    {
        playerHealthBar.sizeDelta = new Vector2(playerHealth * 3, 50);
    }

    public void PlayerDie()
    {

    }

}
