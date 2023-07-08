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
    private Collider2D col;
    private Transform pos;
    private bool hover;
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


    // Update is called once per frame
}