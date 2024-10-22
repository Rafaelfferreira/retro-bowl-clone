using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    PreSnap,
    QBWithBall,
    LiveBall,
    AfterPlay
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Making this a singleton
    public static Action<GameState> OnGameStateChanged;
    // MARK: - Stored game assets
    public GameState currentState = GameState.PreSnap;

    // MARK: - Life cycle methods
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.PreSnap);   
    }

    // MARK: - State machine methods
    public void UpdateGameState(GameState newState) {
        if (currentState == newState) {
            return;
        }

        currentState = newState;

        // We can throw a switch with the new state here to handle any additional logic needed

        OnGameStateChanged?.Invoke(newState);
    }
}
