﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class AimPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    static float MOVE_RANGE = 80;

    public GameObject player;

    PlayerMovement playerMove;
    public RectTransform joystickImage;
    float globalXDefault;
    float globalYDefault;
    bool isPointerDown;

    // Use this for initialization
    void Start()
    {
        globalXDefault = joystickImage.position.x;
        globalYDefault = joystickImage.position.y;
        playerMove = player.GetComponent<PlayerMovement>();
        if (player == null)
        {
            GameObject.FindGameObjectWithTag(ConstMask.TAG_PLAYER);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPointerDown)
        {

            UpdatePlayerLook();
            playerMove.UpdateAim(GetAngle());
        }
    }

    void OnEnable()
    {
        joystickImage.anchoredPosition = Vector2.zero;
        if(playerMove!=null)
            playerMove.RemoveTrigger();
    }

    void OnDisable()
    {
        joystickImage.anchoredPosition = Vector2.zero;
        if (playerMove != null)
            playerMove.RemoveTrigger();
    }

    public void OnDrag(PointerEventData data)
    {
        UpdateButtonPosition(data);
        //UpdatePlayerLook();
        //playerMove.UpdateAim(GetAngle());
    }

    private void UpdatePlayerLook()
    {
        if (joystickImage.anchoredPosition.x < 0)
            playerMove.LookLeft();
        else if (joystickImage.anchoredPosition.x > 0)
            playerMove.LookRight();
    }

    public void OnPointerUp(PointerEventData data)
    {
        isPointerDown = false;
        playerMove.RemoveTrigger();
        joystickImage.anchoredPosition = Vector2.zero;
        //playerMove.UpdateAim (90);
    }

    public void OnPointerDown(PointerEventData data)
    {
        UpdateButtonPosition(data);
        UpdatePlayerLook();
        playerMove.UpdateAim(GetAngle());
        playerMove.HoldTrigger();
        isPointerDown = true;
    }


    private void UpdateButtonPosition(PointerEventData data)
    {
        Vector2 newPos = Vector2.zero;
        float delta = data.position.x - globalXDefault;
        delta = Mathf.Clamp(delta, -MOVE_RANGE, MOVE_RANGE);
        newPos.x = delta;
        delta = data.position.y - globalYDefault;
        delta = Mathf.Clamp(delta, -MOVE_RANGE, MOVE_RANGE);
        newPos.y = delta;
        joystickImage.anchoredPosition = newPos;
    }

    private float GetAngle()
    {
        float angle = Vector2.Angle(Vector2.down, joystickImage.anchoredPosition);
        //if (angle > 90) {
        //	angle = 180 - angle;
        //}
        //if (joystickImage.anchoredPosition.y < 0)
        //	angle = -angle;
        return angle;
    }

}