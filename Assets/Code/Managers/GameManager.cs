using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;
    public bool isMinionTurn = false;

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
                isMinionTurn = true;
                UnitManager.Instance.MinionPhase(ref isMinionTurn);
                break;
            case GameState.AttackPhase:
                break;
            case GameState.DrawPhase:
                DrawManager.instance.drawCard();
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
