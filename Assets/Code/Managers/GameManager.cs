using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;
    public bool isMinionTurn = false;
    public int Gold = 10;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ChangeState(GameState.GenerateGrid);
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
                StartWave.Instance.enableStartWaveButton();
                UnitManager.Instance.MinionPhase();
                break;
            case GameState.AttackPhase:
                break;
            case GameState.DrawPhase:
                CardManger.instance.drawCard();
                break;
            case GameState.EndGame:

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
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
