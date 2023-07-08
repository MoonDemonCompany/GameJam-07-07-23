using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HandViewer : MonoBehaviour
{

    public List<Card> Cards;
    public GameObject CardPrefab;
    public float CardSpacing;
    public float CardScale;
    public float CardYOffset;
    public float CardXOffset;
    // Start is called before the first frame update
    void Start()
    {
        Transform transform = GetComponent<Transform>();
        Debug.Log(Cards.Count);
        for (int i = 0; i < Cards.Count; i++)
        { 
            GameObject card = Instantiate(CardPrefab, transform, false);
            card.GetComponent<CardViewer>().card = Cards[i];
            card.transform.localScale = new Vector3(CardScale, CardScale, CardScale);
            card.transform.localPosition += new Vector3(CardSpacing * i, 0, 0);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
