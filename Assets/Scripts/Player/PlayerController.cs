using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Elements")]

    [SerializeField]
    private CrowdSystem crowdSystem;

    [SerializeField]
    private PlayerAnimator playerAnimator;

    [Header("Settings")]

    [SerializeField] 
    private float moveSpeed = 2f;
    [SerializeField] 
    private float roadWidth;

    private bool canMove;

    [Header("Control")]

    [SerializeField]
    private float slideSpeed = 2f;

    private Vector3 clickedScreenPosition;
    private Vector3 clickedPlayerPosition;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        canMove = false;
    }
    private void Start()
    {
        GameManager.OnGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallback;
    }

    private void Update()
    {
        if(canMove)
        {
            MoveForward();
            HandleHorizontalMovement();
        }
        
    }

    private void StartMoving()
    {
        canMove = true;
        playerAnimator.Run();
    }

    private void StopMoving()
    {
        canMove = false;
        playerAnimator.Idle();
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }

    private void HandleHorizontalMovement()
    {
        if(Input.GetMouseButtonDown(0))
        {
            clickedScreenPosition = Input.mousePosition;
            clickedPlayerPosition = transform.position;
        }
        else if(Input.GetMouseButton(0))
        {
            float xScreenDifference = Input.mousePosition.x - clickedScreenPosition.x;

            xScreenDifference /= Screen.width;
            xScreenDifference *= slideSpeed;

            Vector3 position = transform.position;
            position.x = clickedPlayerPosition.x + xScreenDifference;

            position.x = Mathf.Clamp(position.x, -roadWidth / 2 + crowdSystem.GetCrowdRadius(), roadWidth / 2 - crowdSystem.GetCrowdRadius());

            transform.position = position;
        }
    }

    private void GameStateChangedCallback(GameManager.GameState state)
    {
        if(state == GameManager.GameState.Game)
        {
            StartMoving();
        }
        else if(state == GameManager.GameState.GameOver)
        {
            StopMoving();
        }
        else if(state == GameManager.GameState.LevelComplete)
        {
            StopMoving();
        }
    }
}
