using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        GetComponent<Text>().text = "Gold: " + GameManager.Instance.Gold.ToString();
    }

   }
