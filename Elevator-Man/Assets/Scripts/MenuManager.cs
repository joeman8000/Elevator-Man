using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
[SerializeField] private  GameObject _MenuPanel;
[SerializeField] private TextMesh _stateText;

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



}
