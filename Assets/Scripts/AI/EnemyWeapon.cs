using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour {

    public int damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GameObject.Find("UI").GetComponent<PlayerUI>().PlayerTakeDamage(damage);
        }
    }

}
