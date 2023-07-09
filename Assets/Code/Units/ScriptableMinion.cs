using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Minion",menuName = "Minion")]
public class ScriptableMinion : ScriptableObject {
    public BaseMinion unitPrefab;
}