using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hero",menuName = "Hero")]
public class ScriptableHero : ScriptableObject {
    public BaseHero unitPrefab;
}