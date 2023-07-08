using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManger : MonoBehaviour
{
    public static CardManger instance;
    public CardViewer SelectedCard;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

}
