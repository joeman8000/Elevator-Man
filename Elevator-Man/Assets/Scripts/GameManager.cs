using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;
    public RandomSpawner RandomSpawningItem;
    public EnemyCounter ECount;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.BeginGame);
    }

    void FixedUpdate()
    {
        if (State == GameState.InGame)
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                UpdateGameState(GameState.BeginGame);
            }
        }
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch(newState)
        {
            case GameState.BeginGame:
                Invoke("HandleSpawning", 3.0f);
                break;
            case GameState.InGame:
                Debug.Log("in game");

                //HandleInGame();
                break;
            case GameState.Shop:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        Debug.Log("extra");
        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleSpawning()
    {
        int numOfE = 1;
        RandomSpawningItem.EnemySpawn(numOfE);
        ECount.SetEnemiesAmount(numOfE);
        UpdateGameState(GameState.InGame);
        //EnemyManager.EnemySpawn(1);
    }

}//6:31 https://www.youtube.com/watch?v=4I0vonyqMi8

public enum GameState
{  
    BeginGame,
    InGame,
    Shop,
    Lose
}
