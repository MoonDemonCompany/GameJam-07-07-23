using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;
    public bool isMinionTurn = false;
    public int Gold = 10;
    public int CurrentWave = 0;
    public int TotalHealth = 50;
    public int PrevTotalHealth = 50;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ChangeState(GameState.GenerateGrid);
        CurrentWave = 0;
    }

    private void Update()
    {
        if (GameState == GameState.GenerateGrid)
        {
            GameManager.Instance.ChangeState(GameState.MinionPhase);
        }
    }


    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.MinionPhase:
                StartWave.Instance.button.GetComponentInChildren<Text>().text = "Start Wave";
                CurrentWave++;
                StartWave.Instance.enableStartWaveButton();
                UnitManager.Instance.MinionPhase();
                break;
            case GameState.AttackPhase:
                UnitManager.Instance.SetSelectedMinion(null);
                UnitManager.Instance.AttackPhase();
                break;
            case GameState.DrawPhase:
                AddGold();
                CardManager.instance.drawCard();
                break;
            case GameState.EndGame:
                endGame();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    void endGame()
    {
        SceneManager.LoadScene("EndGame");
    }
    void AddGold() { 
        if(TotalHealth != PrevTotalHealth)
        {
            Gold += 5;
            PrevTotalHealth = TotalHealth;
        }
        else
        {
            Gold += 10;
        }
        

    }
}

public enum GameState
{
    GenerateGrid,
    MinionPhase,
    AttackPhase,
    DrawPhase,
    EndGame
}
