﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MovePad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    static float MOVE_RANGE = 80;

    public RectTransform joystickImage;
    public GameObject player;

    PlayerMovement playerMove;

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
            UpdatePlayer();
        }
        UseKeyboard();
    }

    void OnEnable()
    {
        isPointerDown = false;
        joystickImage.anchoredPosition = Vector2.zero;
        if (playerMove != null)
            playerMove.Stop();
    }

    void OnDisable()
    {
        isPointerDown = false;
        joystickImage.anchoredPosition = Vector2.zero;
        if (playerMove != null)
            playerMove.Stop();
    }

    private void UseKeyboard()
    {
        if (Input.GetKey(KeyCode.A))
        {
            playerMove.MoveLeft(-1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerMove.MoveRight(1);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            playerMove.Stop();
        }
        if (Input.GetKey(KeyCode.W))
        {
            playerMove.Jump();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerMove.Duck();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerMove.Standing();
        }
    }

    public void OnDrag(PointerEventData data)
    {
        UpdateButtonPosition(data);
    }

    public void OnPointerUp(PointerEventData data)
    {
        isPointerDown = false;
        joystickImage.anchoredPosition = Vector2.zero;
        playerMove.Stop();
    }

    public void OnPointerDown(PointerEventData data)
    {
        isPointerDown = true;
        UpdateButtonPosition(data);
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

    private void UpdatePlayer()
    {
        if (joystickImage.anchoredPosition.x < -20)
        {
            playerMove.MoveLeft(-joystickImage.anchoredPosition.x / 80);
        }
        else if (joystickImage.anchoredPosition.x > 20)
        {
            playerMove.MoveRight(joystickImage.anchoredPosition.x / 80);
        }

        if (joystickImage.anchoredPosition.y > 65)
        {
            playerMove.Jump();
        }
        else if (joystickImage.anchoredPosition.y < -65)
        {
            playerMove.Duck();
        }
        else
        {
            playerMove.Standing();
        }
    }

}
