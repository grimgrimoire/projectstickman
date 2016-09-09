using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    Rigidbody2D[] allRigidbody;

	// Use this for initialization
	void Start () {
        allRigidbody = GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D rigid in allRigidbody)
        {
            rigid.AddForce(Random.insideUnitSphere * 2, ForceMode2D.Impulse);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
