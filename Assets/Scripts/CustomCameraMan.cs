using UnityEngine;
using System.Collections;

public class CustomCameraMan : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.transform.position.x, 0, -10);
	}

	void Awake(){
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}
}
