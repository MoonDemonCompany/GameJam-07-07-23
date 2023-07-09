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
        string text = "Minion: " + minion.UnitName + "\n" + "Attack: " + minion.attack + "\n" + "Max Health: " + minion.maxHealth + "\n" + "Current Health: " + minion.currentHealth;
        _selectedMinionObject.GetComponentInChildren<Text>().text = text;
        _selectedMinionObject.SetActive(true);
    }

}
