using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
[SerializeField] private GameObject _MenuPanel;
public GameObject helpUI;
public GameObject startUI;


void Awake()
{
     
    GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
}

void OnDestroy()
{
    GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
}

private void GameManagerOnOnGameStateChanged(GameState state)
{
    _MenuPanel.SetActive(state == GameState.Shop);

}

public void StartGame()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
}

public void HelpUIActivate()
{
    startUI.SetActive(false);
    helpUI.SetActive(true);
}

public void HelpUIDeactivate()
{
    startUI.SetActive(true);
    helpUI.SetActive(false);
}



}
