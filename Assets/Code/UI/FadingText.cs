using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingText : MonoBehaviour
{

    public static Text text;
    private static Outline outline;

    private void Start()
    {
        text = GetComponent<Text>();
        outline = GetComponent<Outline>();
    }

    public static IEnumerator FadeTextToZeroAlpha(float t)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / t));
            outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, outline.effectColor.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
