using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    public List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Update()
    {
        foreach (FloatingText text in floatingTexts)
            text.UpdateFloatingText();
    }

    public void Show(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText text = GetFloatingText();

        text.txt.text = message;
        text.txt.fontSize = fontSize;
        text.txt.color = color;

        text.go.transform.position = Camera.main.WorldToScreenPoint(position);
        text.motion = motion;
        text.duration = duration;

        text.Show();
    }

    private FloatingText GetFloatingText()
    {
        FloatingText text = floatingTexts.Find(text => !text.active);

        if (text == null)
        {
            text = new FloatingText();
            text.go = Instantiate(textPrefab);
            text.go.transform.SetParent(textContainer.transform);
            text.txt = text.go.GetComponent<Text>();

            floatingTexts.Add(text);
        }

        return text;
    }
}
