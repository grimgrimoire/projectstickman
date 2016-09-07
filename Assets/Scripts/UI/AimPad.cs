﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class AimPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {

	static float MOVE_RANGE = 80;

	public GameObject player;

	PlayerMovement playerMove;
	public RectTransform joystickImage;
	float globalXDefault;
	float globalYDefault;

	// Use this for initialization
	void Start () {
		globalXDefault = joystickImage.position.x;
		globalYDefault = joystickImage.position.y;
		playerMove = player.GetComponent<PlayerMovement> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnDrag (PointerEventData data)
	{
		UpdateButtonPosition (data);
		playerMove.UpdateAim (GetAngle());
		if (joystickImage.anchoredPosition.x < 0)
			playerMove.LookLeft ();
		else if(joystickImage.anchoredPosition.x > 0)
			playerMove.LookRight ();
	}

	public void OnPointerUp (PointerEventData data)
	{
		playerMove.RemoveTrigger ();
		joystickImage.anchoredPosition = Vector2.zero;
		playerMove.UpdateAim (90);
	}

	public void OnPointerDown (PointerEventData data)
	{
		playerMove.HoldTrigger ();
		UpdateButtonPosition (data);
	}


	private void UpdateButtonPosition (PointerEventData data)
	{
		Vector2 newPos = Vector2.zero;
		float delta = data.position.x - globalXDefault;
		delta = Mathf.Clamp (delta, -MOVE_RANGE, MOVE_RANGE);
		newPos.x = delta;
		delta = data.position.y - globalYDefault;
		delta = Mathf.Clamp (delta, -MOVE_RANGE, MOVE_RANGE);
		newPos.y = delta;
		joystickImage.anchoredPosition = newPos;
	}

	private float GetAngle ()
	{
		float angle = Vector2.Angle (Vector2.down, joystickImage.anchoredPosition);
		//if (angle > 90) {
		//	angle = 180 - angle;
		//}
		//if (joystickImage.anchoredPosition.y < 0)
		//	angle = -angle;
		return angle;
	}

}