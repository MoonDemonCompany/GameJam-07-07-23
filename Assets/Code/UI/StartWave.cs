using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StartWave : MonoBehaviour
{

    public static StartWave Instance;
    public Button button;

    void Awake()
    {
        Instance = this;

    }
    void Start()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        GameManager.Instance.ChangeState(GameState.AttackPhase);
        button.gameObject.SetActive(false);
    }

    public void enableStartWaveButton()
    {
        button.gameObject.SetActive(true);
    }
}
