using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud : MonoBehaviour
{
    //Toggle Gameplay
    public void pause()
    {
        Time.timeScale = 0;
    }
    public void play()
    {
        Time.timeScale = 1;
    }
}
