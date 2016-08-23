using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

	float moveSpeed = 5f;
	float jumpHeight = 8f;
	Rigidbody2D rigid;
	bool right;
	bool left;

	// Use this for initialization
	void Start ()
	{
		rigid = GetComponent<Rigidbody2D> ();
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

	public void MoveRight (float multiplier)
	{
		CheckCollision ();
		if (!right)
			rigid.velocity = new Vector2 (moveSpeed * multiplier, rigid.velocity.y);
		transform.localScale = new Vector3(1, 1, 1);
	}

	private void CheckCollision ()
	{
		right = Physics.Linecast (transform.position, transform.position + Vector3.right);
	}

	public void MoveLeft (float multiplier)
	{
		CheckCollision ();
		if (!left)
			rigid.velocity = new Vector2 (-moveSpeed * multiplier, rigid.velocity.y);
		transform.localScale = new Vector3(-1, 1, 1);
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
