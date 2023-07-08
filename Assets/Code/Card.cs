using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardType
{
    Minion,
    Buff,
    Heal
}

[CreateAssetMenu]
public class Card : ScriptableObject
{

    public int Health;
    public int Attack;
    public int Cost;
    public string Name;
    public string Description;
    public Sprite Artwork;
    public CardType type;
    public BaseMinion.MinionType minionType;
    public bool selected;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
