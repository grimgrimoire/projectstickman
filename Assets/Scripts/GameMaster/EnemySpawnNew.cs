using UnityEngine;
using System.Collections;

public enum EType
{
    Untaged = 1 << 1,
    Archer = 1 << 2,
    Knight = 1 << 3,
    Air = 1 << 4,
    RedKnight = 1 << 5,
    BlueKnight = 1 << 6
}

public class EnemySpawnNew : MonoBehaviour {

    public EType[] registeredType;
    private int registeredTypeMask;

	// Use this for initialization
	void Start () {
	    foreach(EType type in registeredType)
        {
            registeredTypeMask = registeredTypeMask | (int)type;
        }
        StartCoroutine(RegisterToSpawnSystem());
	}

    IEnumerator RegisterToSpawnSystem()
    {
        yield return new WaitForSeconds(1);
        GameSystem.GetGameSystem().GetSpawnSystem().RegisterSpawner(this);
    }
	
	// Update is called once per frame
	void Update () {
	}

    public bool IsRegisteredType(EType type)
    {
        return (registeredTypeMask & (int)type) == (int)type;
    }

    public void SpawnEnemy(GameObject enemy, EType type)
    {
        GameObject instance = Instantiate(enemy);
        instance.GetComponent<BaseEnemy>().SetEType(type);
        instance.transform.position = new Vector2(transform.position.x, transform.position.y);
    }
}