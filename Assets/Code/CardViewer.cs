using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardViewer : MonoBehaviour {
    public Card card;
    public TextMesh HealthText;
    public TextMesh AttackText;
    public TextMesh NameText;
    public TextMesh DescriptionText;
    public TextMesh CostText;
    public SpriteRenderer sprite;
    public GameObject Glow;
    private Collider2D col;
    private Transform pos;
    public string guid;
    public bool inShop;
    public GameObject confirm;
    
    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>();
        col = GetComponent<Collider2D>();
        HealthText.text = "H: " + card.Health.ToString();
        AttackText.text = "A: " + card.Attack.ToString();
        NameText.text = card.Name;
        DescriptionText.text = card.Description;
        sprite.sprite = card.Artwork;
        CostText.text = "C: " + card.Cost.ToString();
    }
    
     void OnMouseEnter()
    {
        if (GameManager.Instance.GameState != GameState.MinionPhase && !inShop) return;
        pos.localPosition += new Vector3(0, 10f, 0);
    }

    void OnMouseExit()
    {
        if (GameManager.Instance.GameState != GameState.MinionPhase && !inShop) return;
        pos.localPosition -= new Vector3(0, 10f, 0);
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.GameState == GameState.MinionPhase)
        {
            if (CardManager.instance.SelectedCard == null)
            {
                Glow.SetActive(true);
                CardManager.instance.SelectedCard = card;
                CardManager.instance.SelectedCardViewer = this;
                Debug.Log("Clicked" + card.Name);
            } else
            {
                CardManager.instance.SelectedCardViewer.Glow.SetActive(false);
                CardManager.instance.SelectedCard.selected = false;
                Glow.SetActive(true);
                CardManager.instance.SelectedCard = card; 
                CardManager.instance.SelectedCardViewer = this;
            }
        } else if (GameManager.Instance.GameState == GameState.DrawPhase && inShop)
        {
            if (CardManager.instance.Hand.Count >= 7) { 
                FadingText.text.text = "Can not hold more than 7 cards in hand";
                StartCoroutine(FadingText.FadeTextToZeroAlpha(5));
                return;
            }
            if (card.Cost <= GameManager.Instance.Gold)
            {
                GameManager.Instance.Gold -= card.Cost;
                inShop = false;
                CardManager.instance.ShopHand.Remove(this.gameObject);
                CardManager.instance.Hand.Add(this.gameObject);
            }
            
        }
    }


}