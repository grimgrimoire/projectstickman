using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour {

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
            
        }

        Debug.Log(collider.gameObject.tag);
    }

}
