using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipClose : MonoBehaviour
{
    void OnMouseDown()
    {
        if (GameManager.Instance.GameState != GameState.MinionPhase) return;

        if (GridManager.Instance.GetTileAtPosition(Input.mousePosition) == null)
        {
            UnitManager.Instance.SetSelectedMinion(null);
        };
    }
}
