using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Dpad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

	public RectTransform button;
	public PlayerMovement player;

	int moveRange = 80;
	int baseX;
	int baseY;
	string horizontalAxis = "Horizontal";
	string verticalAxis = "Vertical";
	bool isPointerDown = false;

	// Use this for initialization
	void Start ()
	{
		baseX = (int)button.position.x;
		baseY = (int)button.position.y;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(isPointerDown){
			UpdatePlayer ();
		}
	}

	public void OnDrag (PointerEventData data)
	{
		UpdateButtonPosition (data);
	}

	public void OnPointerUp (PointerEventData data)
	{
		isPointerDown = false;
		button.anchoredPosition = Vector2.zero;
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
		button.anchoredPosition = newPos;
	}

	private void UpdatePlayer(){
		if (button.anchoredPosition.x < 0)
			player.MoveLeft (-button.anchoredPosition.x / 80);
		else if(button.anchoredPosition.x > 0)
			player.MoveRight (button.anchoredPosition.x / 80);
	}
}
