using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public string TileName;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    [SerializeField] private bool _isWalkable;

    public BaseMinion OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;


    void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.GameState != GameState.MinionPhase) return;

        if (OccupiedUnit != null)
        {
            if (OccupiedUnit.faction == Faction.Minion)
            {
                UnitManager.Instance.SetSelectedMinion((BaseMinion)OccupiedUnit);
            }
            else
            {
                if (UnitManager.Instance.selectedMinion != null)
                {
                    UnitManager.Instance.SetSelectedMinion(null);
                }
            }
        }
        else
        {
            if (UnitManager.Instance.selectedMinion != null)
            {
                BaseMinion spawnedMinion = Instantiate(UnitManager.Instance.selectedMinion);
                SetUnit(spawnedMinion);
                UnitManager.Instance.SetSelectedMinion(null);
            }
        }

    }

    public void SetUnit(BaseMinion unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

}
