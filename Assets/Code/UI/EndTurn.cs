using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour
{
    public Button button;
    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        GameManager.Instance.ChangeState(GameState.AttackPhase);
    }
}
