using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public static DrawManager instance;
    public List<Card> deck;

    // Start is called before the first frame update
    void Start()
    {
        instance = this; 
    }

    public void drawCard()
    {
        // Todo
    }
}
