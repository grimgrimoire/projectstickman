using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Dpad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

	public RectTransform directionalPad;
	public PlayerMovement player;

	public AudioClip gunShot;
	public AudioSource source;

	int moveRange = 80;
	int baseX;
	int baseY;
	bool isPointerDown = false;

	// Use this for initialization
	void Start ()
	{
		baseX = (int)directionalPad.position.x;
		baseY = (int)directionalPad.position.y;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(isPointerDown){
			UpdatePlayer ();
		}

		if(Input.GetKeyDown(KeyCode.X)){
			OnShootButton ();
		}

		if(Input.GetKeyDown(KeyCode.Z)){
			OnJumpButton ();
		}
	}

	public void OnJumpButton(){
		player.Jump ();
	}

	public void OnShootButton(){
		if(gunShot!=null)
			source.PlayOneShot (gunShot);
		player.ShootBullet (directionalPad.anchoredPosition3D);
	}

	public void OnDrag (PointerEventData data)
	{
		UpdateButtonPosition (data);
	}

	public void OnPointerUp (PointerEventData data)
	{
		isPointerDown = false;
		directionalPad.anchoredPosition = Vector2.zero;
		player.Stop ();
	}

	public void OnPointerDown (PointerEventData data)
	{
		isPointerDown = true;
		UpdateButtonPosition (data);
	}

	private void UpdateButtonPosition (PointerEventData data)
	{
		Vector2 newPos = Vector2.zero;
		int delta = (int)(data.position.x - baseX);
		delta = Mathf.Clamp (delta, -moveRange, moveRange);
		newPos.x = delta;
		delta = (int)(data.position.y - baseY);
		delta = Mathf.Clamp (delta, -moveRange, moveRange);
		newPos.y = delta;
		directionalPad.anchoredPosition = newPos;
	}

	private void UpdatePlayer(){
		if (directionalPad.anchoredPosition.x < 0)
			player.MoveLeft (-directionalPad.anchoredPosition.x / 80);
		else if(directionalPad.anchoredPosition.x > 0)
			player.MoveRight (directionalPad.anchoredPosition.x / 80);
	}
}
