using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardViewer : MonoBehaviour {
    public Card card;
    public TextMesh HealthText;
    public TextMesh AttackText;
    public TextMesh NameText;
    public TextMesh DescriptionText;
    public GameObject Glow;
    private Collider2D col;
    private Transform pos;
    private bool selected;
    
    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>();
        col = GetComponent<Collider2D>();
        HealthText.text = card.Health.ToString();
        AttackText.text = card.Attack.ToString();
        NameText.text = card.Name;
        DescriptionText.text = card.Description;
    }
    
     void OnMouseEnter()
    {
        pos.localPosition += new Vector3(0, 10f, 0);
    }

    void OnMouseExit()
    {
        pos.localPosition -= new Vector3(0, 10f, 0);
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.GameState == GameState.MinionPhase)
        {
            if (CardManger.instance.SelectedCard == null)
            {
                Glow.SetActive(true);
                selected = true;
                CardManger.instance.SelectedCard = this; 
                Debug.Log("Clicked" + card.Name);
            } else
            {
                CardManger.instance.SelectedCard.Glow.SetActive(false);
                CardManger.instance.SelectedCard.selected = false;
                selected = true;
                Glow.SetActive(true);
                CardManger.instance.SelectedCard = this; 
            }
        }
    }

    // Update is called once per frame
}