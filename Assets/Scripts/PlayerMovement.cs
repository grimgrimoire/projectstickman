using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	Animator anim;
	int walkHash = Animator.StringToHash("Walk");
	int IdleHash = Animator.StringToHash("Idle");
	float moveSpeed = 5f;
	float jumpHeight = 8f;
	Rigidbody2D rigid;
	bool right;
	bool left;

	// Use this for initialization
	void Start ()
	{
		rigid = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
		
	public void ShootBullet (Vector3 direction)
	{
		RaycastHit2D hit = Physics2D.Raycast (transform.position + new Vector3(1, 0, 0), Vector2.right * transform.localScale.x, Mathf.Infinity);
		Debug.Log (hit.collider.gameObject.tag);
		Debug.DrawLine (transform.position + Vector3.right * transform.localScale.x, hit.point, Color.red, 1f);
		if(hit.collider.gameObject.tag == "Hostile"){
			hit.rigidbody.AddForce (new Vector2(255, 0));
			hit.rigidbody.AddTorque (22f);
		}
	}

	public void MoveRight (float multiplier)
	{
		CheckCollision ();
		rigid.velocity = new Vector2 (moveSpeed * multiplier, rigid.velocity.y);
		transform.localScale = new Vector3 (1, 1, 1);
		PlayAnimation (walkHash);
	}

	private void CheckCollision ()
	{
		
	}

	public void MoveLeft (float multiplier)
	{
		CheckCollision ();
		rigid.velocity = new Vector2 (-moveSpeed * multiplier, rigid.velocity.y);
		transform.localScale = new Vector3 (-1, 1, 1);
		PlayAnimation (walkHash);
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
		PlayAnimation (IdleHash);
	}

	private void PlayAnimation(int aniHash){
		if (anim != null)
			anim.Play (aniHash);
	}
}
