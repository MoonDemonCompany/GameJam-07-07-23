using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public static MenuManager Instance;

    [SerializeField] private GameObject _selectedMinionObject;
    

    void Awake() {
        Instance = this;
    }

    public void ShowSelectedMinion(BaseMinion minion) {
        if (minion == null) {
            _selectedMinionObject.SetActive(false);
            return;
        }

        _selectedMinionObject.GetComponentInChildren<Text>().text = minion.UnitName;
        _selectedMinionObject.SetActive(true);
    }

}
