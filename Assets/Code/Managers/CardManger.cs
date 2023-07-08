using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class CardManger : MonoBehaviour
{
    public static CardManger instance;
    public CardViewer SelectedCard;
    public static Card[] Deck;
    public static List<Card> Hand;
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
        instance = this;
        Deck = Resources.LoadAll<Card>("Cards");
        Debug.Log(Deck.Length);
        transform.position = new Vector3(0, -0.5f, -0.01f);
        Hand = new List<Card> { Deck[Random.Range(0, Deck.Length)],
                                 Deck[Random.Range(0, Deck.Length)],
                                 Deck[Random.Range(0, Deck.Length)], 
                                 Deck[Random.Range(0, Deck.Length)], 
                                 Deck[Random.Range(0, Deck.Length)] };
        for (int i = 0; i < Hand.Count; i++)
        {
            GameObject card = Instantiate(CardPrefab, new Vector3(1, -0.5f, -0.001f), Quaternion.identity);
            card.name = Hand[i].Name;
            card.GetComponent<CardViewer>().card = Hand[i];
            card.transform.localScale = new Vector3(CardScale, CardScale, CardScale);
            card.transform.localPosition += new Vector3(CardSpacing * i, 0, 0);
            card.transform.SetParent(canvas.transform, false);
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
           GameObject c = Instantiate(CardPrefab, new Vector3(camPos.position.x + i * 2, camPos.position.y, -0.001f), Quaternion.identity);
           c.GetComponent<CardViewer>().card = Cards[i];
        }
    }

}
