using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMinion : BaseUnit
{
    public MinionType minionType;
    public int maxHealth;
    public int currentHealth;
    public int attack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum MinionType
    {
        Skeleton,
        Zombie,
        SkeleKnight
    }
}
