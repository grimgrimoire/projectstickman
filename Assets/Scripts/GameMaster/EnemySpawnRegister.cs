using UnityEngine;
using System.Collections;

public class EnemySpawnRegister : MonoBehaviour {

    public EType type;
    public GameObject prefab;
    public int maximumNumber;
    public float spawnDelay;
    public int killRequirement;
    public int increaseSpeedKill;
    public float increaseSpawnSpeed;
    public int TotalEnemy;

    private int totalActive = 0;

    EnemySpawnSystem spawnSystem;
    bool isCoroutineActive = false;
    

	// Use this for initialization
	void Start () {
        spawnSystem = GetComponent<EnemySpawnSystem>();
        spawnSystem.RegisterEnemyRegistry(this);
        
	}
	
	// Update is called once per frame
	void Update () {

    }

    public bool IsAllKilled()
    {
        return totalActive <= 0;
    }

    public void StartSpawnEnemy()
    {
        if (spawnDelay > 0)
            StartCoroutine(SpawnEnemy());
    }

    public void KillOneUnit()
    {
        totalActive--;
        if (!spawnSystem.IsTotalKillReached() && !isCoroutineActive)
            StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        isCoroutineActive = true;
        while (true) {
            yield return new WaitForSeconds(spawnDelay);
            if (totalActive < maximumNumber && killRequirement <= spawnSystem.GetTotalKill() && !spawnSystem.IsTotalKillReached() && (TotalEnemy>0 || TotalEnemy == 999))
            {
                spawnSystem.Spawn(type, prefab);
                totalActive++;
                if (TotalEnemy != 999)
                { TotalEnemy--; }
            }
            else
                break;

        }
        isCoroutineActive = false;
    }
}
