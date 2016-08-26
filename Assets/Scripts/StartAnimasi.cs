using UnityEngine;
using System.Collections;

public class StartAnimasi : MonoBehaviour {

	Animator anim;
	//int walkHash = Animator.StringToHash("Walk");
	//int Stop = Animator.StringToHash("Stop");
	int speeding;

	public void startanimation (float x)
	{
		/*if (x < 0) {
			anim.Play (walkHash);
		} else if (x > 0)
			anim.Play (walkHash);*/
		if (x > 0) {
			speeding = 1;
		}else if (x < 0) {
			speeding = 1;
		}

	}

	public void stopanimation (Vector2 stop)
	{
		/*if (stop == Vector2.zero) {
			anim.Play (Stop);
		}*/
		if (stop == Vector2.zero) {
			speeding = 0;
		}
	}
		

	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		float pSpeed = speeding;
		anim.SetFloat("Speed", pSpeed);
	}
}
