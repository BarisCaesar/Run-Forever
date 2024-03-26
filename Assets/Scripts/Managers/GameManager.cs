using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private GameState gameState;

    public static Action<GameState> OnGameStateChanged;

    public enum GameState
    {
        Menu,
        Game,
        LevelComplete,
        GameOver
    }

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
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
    }

    public void SetGameState(GameState state)
    {
        gameState = state;
        OnGameStateChanged?.Invoke(state);

        Debug.Log("Game state changed to : " + gameState);
    }

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}
