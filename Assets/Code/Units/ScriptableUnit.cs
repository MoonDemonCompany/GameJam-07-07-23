using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Minion",menuName = "Minion")]
public class ScriptableUnit : ScriptableObject {
    public Faction faction;
    public BaseUnit unitPrefab;
}

public enum Faction {
    Minion = 0,
    Hero = 1
}