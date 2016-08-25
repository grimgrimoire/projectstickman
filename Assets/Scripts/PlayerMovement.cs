using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

	public GameObject bullet;

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

	//	void OnCollisionEnter2D(Collision2D collision){
	//		Collider2D collider = collision.collider;
	//		if(collider.tag == "World"){
	//			Vector3 contact = collision.contacts [0].point;
	//			Vector3 center = collider.bounds.center;
	//			right = contact.x > center.x;
	//			left = contact.x < center.x;
	//			Debug.DrawLine (contact, center, Color.blue);
	//		}
	//	}

	public void ShootBullet (Vector3 direction)
	{
		GameObject bullets = Instantiate (bullet, transform.position, transform.rotation) as GameObject;
		if (direction == Vector3.zero) {
			bullets.GetComponent<Rigidbody2D> ().velocity = Vector2.right * transform.localScale.x;
			Debug.DrawRay (transform.position, (Vector3.right * transform.localScale.x) * 10, Color.red, 1f);
		} else {
			bullets.GetComponent<Rigidbody2D> ().velocity = new Vector2 (direction.x , direction.y );
		}
			
	}

	public void MoveRight (float multiplier)
	{
		CheckCollision ();
		if (!right)
			rigid.velocity = new Vector2 (moveSpeed * multiplier, rigid.velocity.y);
		transform.localScale = new Vector3 (1, 1, 1);
		anim.Play (walkHash);
	}

	private void CheckCollision ()
	{
		right = Physics.Linecast (transform.position, transform.position + Vector3.right);
		Debug.DrawLine (transform.position, transform.position + Vector3.right, Color.blue);
	}

	public void MoveLeft (float multiplier)
	{
		CheckCollision ();
		if (!left)
			rigid.velocity = new Vector2 (-moveSpeed * multiplier, rigid.velocity.y);
		transform.localScale = new Vector3 (-1, 1, 1);
		anim.Play (walkHash);
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
		anim.Play (IdleHash);
	}
}
