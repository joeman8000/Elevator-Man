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
    [SerializeField] private PlayerMovement playerMove;
    [SerializeField] private Health playerHealth;
    public AudioSource powerUp;
    public AudioSource ShopMusic;
    public AudioSource MainTheme;

    public static event Action<GameState> OnGameStateChanged;
    public RandomSpawner RandomSpawningItem;
    public EnemyCounter ECount;
    [SerializeField] private int WavesPerShop;
    private static int floor;
    public Text FloorCounterText;
    public GameObject[] cardsL;
    public GameObject[] cardsR;

    private int randCard;
    private int randCard2;
    [HideInInspector] public static bool enemySpawned = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.BeginGame);
        enemySpawned = false;
    }

    void FixedUpdate()
    {
        if (State == GameState.InGame && enemySpawned)
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                enemySpawned = false;
                if(floor % 3 == 0)
                {
                    UpdateGameState(GameState.Shop);
                }
                else{
                UpdateGameState(GameState.BeginGame);
                }
            }
        }

        if(playerHealth.health <= 0)
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
                FloorCounterText.text = "Floor: " + floor;
                Invoke("HandleSpawning", 4.0f);
                break;
            case GameState.InGame:
                Debug.Log("in game");

                //HandleInGame();
                break;
            case GameState.Shop:
                MainTheme.Stop();
                ShopMusic.Play();
                FloorCounterText.text = "Floor: Shop";
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
        playerMove.bulletDamage = 5;
        playerMove.shootSpeed = 1;
        floor = 0;
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
        randCard = UnityEngine.Random.Range(0, cardsL.Length);
        randCard2 = UnityEngine.Random.Range(0, cardsR.Length);
        Debug.Log("2");
        while(randCard == randCard2)
        {
        randCard2 = UnityEngine.Random.Range(0, cardsR.Length);
        }
        Debug.Log("3");
        Invoke("CardLStart", 1f);
        Invoke("CardRStart", 1f);
        Debug.Log("4");
    }

    public void CardLStart()
    {
        cardsL[randCard].SetActive(true);
    }

    public void CardRStart()
    {
        cardsR[randCard2].SetActive(true);
    }

    public void IncreaseBulletSpeed()
    {
        playerMove.shootSpeed -= .5f;
        powerUp.Play();
        UpdateGameState(GameState.BeginGame);
        cardsL[randCard].SetActive(false);
        cardsR[randCard2].SetActive(false);
        MainTheme.Play();
        ShopMusic.Stop();
    }

    public void IncreaseBulletDamage()
    {
        playerMove.bulletDamage += 2f;
        powerUp.Play();
        UpdateGameState(GameState.BeginGame);
        cardsL[randCard].SetActive(false);
        cardsR[randCard2].SetActive(false);
        MainTheme.Play();
        ShopMusic.Stop();
    }

    public void IncreaseMovementSpeed()
    {
        playerMove._speed += 1f;
        powerUp.Play();
        UpdateGameState(GameState.BeginGame);
        cardsL[randCard].SetActive(false);
        cardsR[randCard2].SetActive(false);
        MainTheme.Play();
        ShopMusic.Stop();
    }

    public void IncreaseMaxHealth()
    {
        playerHealth.maxHealth += 1;
        playerHealth.health++;
        powerUp.Play();
        UpdateGameState(GameState.BeginGame);
        cardsL[randCard].SetActive(false);
        cardsR[randCard2].SetActive(false);
        MainTheme.Play();
        ShopMusic.Stop();
    }

    public void RegainFullHealth()
    {
        playerHealth.health = playerHealth.maxHealth;
        powerUp.Play();
        UpdateGameState(GameState.BeginGame);
        cardsL[randCard].SetActive(false);
        cardsR[randCard2].SetActive(false);
        MainTheme.Play();
        ShopMusic.Stop();
    }

    

}//6:31 https://www.youtube.com/watch?v=4I0vonyqMi8

public enum GameState
{  
    BeginGame,
    InGame,
    Shop,
    Lose
}
