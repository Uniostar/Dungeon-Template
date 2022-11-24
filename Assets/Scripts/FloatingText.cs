using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    //Delcare variables
    public bool active;
    public GameObject go;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    //Show the text
    public void Show()
    {
        active = true;
        lastShown = Time.unscaledTime;
        go.SetActive(true);
    }

    //Hide the text
    public void Hide()
    {
        active = false;
        go.SetActive(false);
    }

    //Update the text
    public void UpdateFloatingText()
    {
        if (!active)
            return;
        if ((Time.unscaledTime - lastShown) > duration)
            Hide();

        go.transform.position += motion * Time.deltaTime;
    }
}
