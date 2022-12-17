using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    _MenuPanel.SetActive(state == GameState.BeginGame);
}



}
