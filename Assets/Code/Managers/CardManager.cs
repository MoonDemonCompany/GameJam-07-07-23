using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public Card SelectedCard;
    public CardViewer SelectedCardViewer;
    public static Card[] Deck;
    public ObservableCollection<GameObject> Hand;
    public GameObject CardPrefab;
    public float CardSpacing;
    public float CardScale;
    public float CardYOffset;
    public float CardXOffset;
    public Transform camPos;
    public Canvas canvas; 
    // Start is called before the first frame update
    void Awake()
    {
        Hand = new ObservableCollection<GameObject>();
        instance = this;
        Deck = Resources.LoadAll<Card>("Cards");
        Debug.Log(Deck.Length);
        transform.position = new Vector3(0, -0.5f, -0.01f);
        List<Card> tempHand = new List<Card> { Deck.Where(c => c.type == CardType.Minion).OrderBy(o => Random.value).First(),
                                 Deck[Random.Range(0, Deck.Length)],
                                 Deck[Random.Range(0, Deck.Length)], 
                                 Deck[Random.Range(0, Deck.Length)], 
                                 Deck[Random.Range(0, Deck.Length)] };
        for (int i = 0; i < tempHand.Count; i++)
        {
            GameObject card = Instantiate(CardPrefab, new Vector3(-150, -165, 0.001f), Quaternion.identity);
            card.name = tempHand[i].Name;
            card.GetComponent<CardViewer>().card = tempHand[i];
            card.GetComponent<CardViewer>().guid = Guid.NewGuid().ToString();
            card.transform.localScale = new Vector3(CardScale, CardScale, CardScale);
            card.transform.localPosition += new Vector3(CardSpacing * i, 0, 0);
            card.transform.SetParent(canvas.transform, false);
            Hand.Add(card);
        }
    }

    
    public void drawCard()
    {
        List<Card> Cards = new List<Card>();
        for (int i = 0; i < 3; i++)
        {
            Card newCard = Deck[Random.Range(0, Deck.Length)];
            if (!Cards.Contains(newCard))
            {
                Cards.Add(newCard);
            } else
            {
                i--;
            }
        }

        for (int i = 0; i < Cards.Count; i++) 
        {
           GameObject c = Instantiate(CardPrefab, new Vector3(camPos.position.x + i * 2, camPos.position.y, 0.001f), Quaternion.identity);
            c.transform.localScale = new Vector3(CardScale, CardScale, CardScale);
            c.transform.localPosition += new Vector3(CardSpacing * i, 0, 0);
            c.transform.SetParent(canvas.transform, false);
            c.GetComponent<CardViewer>().card = Cards[i];
        }
    }

    public void rearrangeCards()
    {
        for (int i = 0; i < Hand.Count; i++)
        {
            Hand[i].transform.localPosition = new Vector3(-150, -165, 0.001f);
            Hand[i].transform.localScale = new Vector3(CardScale, CardScale, CardScale);
            Hand[i].transform.localPosition += new Vector3(CardSpacing * i, 0, 0);
        }
    }

}
