using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnitManager : MonoBehaviour {
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;
    public BaseMinion selectedMinion;

    void Awake() {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();

    }

  
    public void MinionPhase(ref bool isMinionTurn) 
    {
       
        
    }

   



    public void SpawnHeroes()
    {

        GameManager.Instance.ChangeState(GameState.DrawPhase);
    }

    private T GetSpecifiedMinionUnit<T>(BaseMinion.MinionType minionType) where T : BaseMinion
    {
        return (T)_units.Where(u => u.faction == Faction.Minion).Where(u => u.unitPrefab is BaseMinion).Where(u => ((BaseMinion)u.unitPrefab).minionType == minionType).First().unitPrefab;
    }

    public void SetSelectedMinion(BaseMinion minion) {
        selectedMinion = minion;
        MenuManager.Instance.ShowSelectedMinion(minion);
    }
}
