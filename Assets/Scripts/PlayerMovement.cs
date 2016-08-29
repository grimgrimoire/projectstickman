using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public GameObject weaponTarget;
	public Light weaponLight;
	public GameObject playerAimingArm;

	float defaultAim = 300;
	float moveSpeed = 5f;
	float jumpHeight = 8f;
	Rigidbody2D rigid;
	bool right;
	bool left;

	// Use this for initialization
	void Start ()
	{	
		rigid = GetComponent<Rigidbody2D> ();
//		StartCoroutine (Automatic ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	IEnumerator Automatic ()
	{
		do {
			ShootBullet (Vector3.zero);
			yield return new WaitForSeconds (0.1f);
		} while(true);
	}

	public void ShootBullet (Vector3 direction)
	{
		RaycastHit2D hit = Physics2D.Raycast (weaponTarget.transform.position, weaponTarget.transform.right * transform.localScale.x, Mathf.Infinity);
		if (hit) {
			Debug.Log (hit.collider.gameObject.tag);
			Debug.DrawLine (weaponTarget.transform.position, hit.point, Color.yellow, 0.5f);
			StartCoroutine (ShowGunFire ());
			if (hit.collider.gameObject.tag == "Hostile") {
				hit.rigidbody.AddForce (new Vector2 (255, 0));
				hit.rigidbody.AddTorque (22f);
			}
		}
	}

	IEnumerator ShowGunFire ()
	{
		if (weaponLight != null) {
			if (weaponLight.intensity == 0)
				weaponLight.intensity = 10f;
			yield return new WaitForSeconds (0.05f);
			weaponLight.intensity = 0;
		}
	}

	public void UpdateAim (float angle)
	{
		playerAimingArm.transform.localEulerAngles = new Vector3 (0, 0, 300 + angle);
	}

	public void MoveRight (float multiplier)
	{
		CheckCollision ();
		rigid.velocity = new Vector2 (moveSpeed, rigid.velocity.y);
		transform.localScale = new Vector3 (1, 1, 1);
		//anim.Play (walkHash);
	}

	private void CheckCollision ()
	{
		
	}

	public void MoveLeft (float multiplier)
	{
		CheckCollision ();
		rigid.velocity = new Vector2 (-moveSpeed, rigid.velocity.y);
		transform.localScale = new Vector3 (-1, 1, 1);
	}

	public void Jump ()
	{
		if (rigid.velocity.y == 0) {
			rigid.velocity = new Vector2 (rigid.velocity.x, jumpHeight);
		}
	}

	public void Stop ()
	{
		rigid.velocity = new Vector2 (0, rigid.velocity.y);
	}

}
