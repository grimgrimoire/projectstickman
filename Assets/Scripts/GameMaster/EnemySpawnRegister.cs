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

    private int totalActive = 0;

    EnemySpawnSystem spawnSystem;
    IEnumerator spawnRoutine;

	// Use this for initialization
	void Start () {
        spawnSystem = GetComponent<EnemySpawnSystem>();
        spawnSystem.RegisterEnemyRegistry(this);
        spawnRoutine = SpawnEnemy();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartSpawnEnemy()
    {
        if (spawnDelay > 0)
            StartCoroutine(SpawnEnemy());
    }

    public void KillOneUnit()
    {
        totalActive--;
        
    }

    IEnumerator SpawnEnemy()
    {
        while (true && totalActive < maximumNumber && killRequirement <= spawnSystem.GetTotalKill()) {
            yield return new WaitForSeconds(spawnDelay);
            spawnSystem.Spawn(type, prefab);
            totalActive++;
        }
    }
}
