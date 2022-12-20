using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;
    public RandomSpawner RandomSpawningItem;
    public EnemyCounter ECount;
    public Health h;
    [SerializeField] private int WavesPerShop;
    private static int floor;
    public GameObject[] cardsL;
    public GameObject[] cardsR;

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
                if(floor % 3 == 0)
                {
                    UpdateGameState(GameState.Shop);
                }
                else{
                UpdateGameState(GameState.BeginGame);
                }
            }
        }

        if(h.health <= 0)
        {
            Death();
        }
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch(newState)
        {
            case GameState.BeginGame:
                ++floor;
                Invoke("HandleSpawning", 3.0f);
                break;
            case GameState.InGame:
                Debug.Log("in game");

                //HandleInGame();
                break;
            case GameState.Shop:
                Debug.Log("0");
                HandleShop();
                Debug.Log("6");
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        //Debug.Log("extra");
        OnGameStateChanged?.Invoke(newState);
    }

    public void Death()
    {
        Invoke("ToMainMenu", 1.7f);
    }
    private void ToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

    private void HandleSpawning()
    {
        int numOfE = floor + 1;
        RandomSpawningItem.EnemySpawn(numOfE);
        ECount.SetEnemiesAmount(numOfE);
        UpdateGameState(GameState.InGame);
        //EnemyManager.EnemySpawn(1);
    }

    private void HandleShop()
    {
        Debug.Log("1");
        int randCard = UnityEngine.Random.Range(0, cardsL.Length);
        int randCard2 = UnityEngine.Random.Range(0, cardsR.Length);
        Debug.Log("2");
        while(randCard == randCard2)
        {
        randCard2 = UnityEngine.Random.Range(0, cardsR.Length);
        }
        Debug.Log("3");
        cardsL[randCard].SetActive(true);
        cardsR[randCard2].SetActive(true);
        Debug.Log("4");
    }

}//6:31 https://www.youtube.com/watch?v=4I0vonyqMi8

public enum GameState
{  
    BeginGame,
    InGame,
    Shop,
    Lose
}
