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
        if (GameManager.Instance.GameState == GameState.MinionPhase)
        {
            button.gameObject.SetActive(false);
            GameManager.Instance.ChangeState(GameState.AttackPhase);
        } else if (GameManager.Instance.GameState == GameState.DrawPhase)
        {
            foreach (GameObject card in CardManager.instance.ShopHand)
            {
                Destroy(card);
            }
            CardManager.instance.ShopHand.Clear();
            button.gameObject.SetActive(false);
            GameManager.Instance.ChangeState(GameState.MinionPhase);
        }
    }

    public void enableStartWaveButton()
    {
        button.gameObject.SetActive(true);
    }
}
