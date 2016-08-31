using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public GameObject weaponTarget;
	public Light weaponLight;
	public GameObject playerAimingArm;
	public StartAnimasi anim;

	float defaultAim = 300;
	float moveSpeed = 5f;
	float jumpHeight = 8f;
	Rigidbody2D rigid;
	bool right;
	bool left;
	IEnumerator shootingCoroutine;

	// Use this for initialization
	void Start ()
	{	
		rigid = GetComponent<Rigidbody2D> ();
		shootingCoroutine = AutomaticShooting ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	IEnumerator AutomaticShooting ()
	{
		do {
			ShootBullet ();
			yield return new WaitForSeconds (0.3f);
		} while(true);
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

	public void HoldTrigger ()
	{
		StartCoroutine (shootingCoroutine);
	}

	public void RemoveTrigger ()
	{
		StopCoroutine (shootingCoroutine);
	}

	public void UpdateAim (float angle)
	{
		playerAimingArm.transform.localEulerAngles = new Vector3 (0, 0, 300 + angle);
	}

	public void MoveRight (float multiplier)
	{
		CheckCollision ();
		rigid.velocity = new Vector2 (moveSpeed, rigid.velocity.y);
		anim.startanimation (1);
	}

	private void CheckCollision ()
	{
		
	}

	public void MoveLeft (float multiplier)
	{
		CheckCollision ();
		rigid.velocity = new Vector2 (-moveSpeed, rigid.velocity.y);
		anim.startanimation (1);
	}

	public void LookRight ()
	{
		transform.localScale = new Vector3 (1, 1, 1);
	}

	public void LookLeft ()
	{
		transform.localScale = new Vector3 (-1, 1, 1);
	}

	public void Jump ()
	{
		if (rigid.velocity.y == 0) {
			rigid.velocity = new Vector2 (rigid.velocity.x, jumpHeight);
		}
	}

    public void Duck()
    {

    }

	public void Stop ()
	{
		rigid.velocity = new Vector2 (0, rigid.velocity.y);
		anim.stopanimation (Vector2.zero);
	}

	private void ShootBullet ()
	{
		Vector2 target = weaponTarget.transform.right * transform.localScale.x;
		target += (Random.insideUnitCircle) * 0.05f;
		RaycastHit2D hit = Physics2D.Raycast (weaponTarget.transform.position, target, Mathf.Infinity);
		if (hit) {
			Debug.Log (hit.collider.gameObject.tag);
			Debug.DrawLine (weaponTarget.transform.position, hit.point, Color.yellow, 0.5f);
			StartCoroutine (ShowGunFire ());
		}
	}

}
