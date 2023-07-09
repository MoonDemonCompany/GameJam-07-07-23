using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardType
{
    Minion,
    Buff,
    Heal
}

public enum BuffType
{
    AttackPlus2,
    AttackPlus4,
    HealthPlus2,
    HealthPlus4
}

public enum HealType
{
    PartHeal,
    FullHeal
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
    public BuffType buffType;
    public HealType healType;
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
