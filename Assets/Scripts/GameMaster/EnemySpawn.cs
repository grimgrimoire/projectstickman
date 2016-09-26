using UnityEngine;
using System.Collections;
using System;

public interface IEnemySpawn
{
    void AddEnemyNumber(EType type);
}

public enum EType
{
    Untaged, Archer, Knight, Mage, RedKnight, BlueKnight, Lancer1
}

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyListPrefab;
    public EType type;
    public int spawnNumber;
    public float spawnDelay;
    public int killRequirement;

    IEnumerator spawnNumerator;
    IEnemySpawn iSpawn;

    // Use this for initialization
    void Start()
    {
        spawnNumerator = SpawnEnemyWithInterval();
        GameSystem.GetGameSystem().AddEnemySpawnLocation(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetISpawn(IEnemySpawn gameSystem)
    {
        iSpawn = gameSystem;
    }

    public void StartSpawnEnemy()
    {
        StartCoroutine(spawnNumerator);
    }

    public void StopSpawnEnemy()
    {
        StopCoroutine(spawnNumerator);
    }

    private void UpdateUI()
    {

    }

    IEnumerator SpawnEnemyWithInterval()
    {
        while (spawnNumber != 0)
        {
            yield return new WaitForSeconds(spawnDelay);
            GameObject instance = Instantiate(enemyListPrefab);
            instance.GetComponent<BaseEnemy>().SetEType(type);
            instance.transform.position = new Vector2(transform.position.x, transform.position.y);
            spawnNumber--;
            iSpawn.AddEnemyNumber(type);
        }
    }
}
