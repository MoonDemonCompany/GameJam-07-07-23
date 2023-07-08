using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;



public class HandViewer : MonoBehaviour
{
    public static Card[] Deck;
    public static List<Card> Cards;
    public GameObject CardPrefab;
    public float CardSpacing;
    public float CardScale;
    public float CardYOffset;
    public float CardXOffset;
    [SerializeField] private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        Deck = Resources.LoadAll<Card>("Cards");
        Debug.Log(Deck.Length);
        Transform transform = GetComponent<Transform>();
        transform.position = new Vector3(0, -0.5f, -0.01f);
        Cards = new List<Card> { Deck[Random.Range(0, Deck.Length)],
                                 Deck[Random.Range(0, Deck.Length)],
                                 Deck[Random.Range(0, Deck.Length)], 
                                 Deck[Random.Range(0, Deck.Length)], 
                                 Deck[Random.Range(0, Deck.Length)] };
        for (int i = 0; i < Cards.Count; i++)
        {
            GameObject card = Instantiate(CardPrefab, new Vector3(-150, -165f, 0.001f), Quaternion.identity);
            card.name = Cards[i].Name;
            card.GetComponent<CardViewer>().card = Cards[i];
            card.transform.localScale = new Vector3(CardScale, CardScale, CardScale);
            card.transform.localPosition += new Vector3(CardSpacing * i, 0, 0);
            card.transform.SetParent(canvas.transform, false);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
