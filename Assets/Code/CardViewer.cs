using System.Collections;
using System.Collections.Generic;
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
    public string guid;

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
            if (CardManager.instance.SelectedCard == null)
            {
                Glow.SetActive(true);
                selected = true;
                CardManager.instance.SelectedCard = card;
                CardManager.instance.SelectedCardViewer = this;
                Debug.Log("Clicked" + card.Name);
            } else
            {
                CardManager.instance.SelectedCardViewer.Glow.SetActive(false);
                CardManager.instance.SelectedCard.selected = false;
                selected = true;
                Glow.SetActive(true);
                CardManager.instance.SelectedCard = card;
                CardManager.instance.SelectedCardViewer = this;
            }
        }
    }

    // Update is called once per frame
}