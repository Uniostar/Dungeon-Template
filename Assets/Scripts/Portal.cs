using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public bool takeScene;
    public int sceneID;
    //Check for collisions
    protected override void OnCollide(Collider2D coll)
    {
        //If the name of the collision object is "Player"
        if (coll.name == "Player")
        {
            if (takeScene)
            {
                SceneManager.LoadScene(sceneID);
            }
            else
            {
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    //Change scene to a random dungeon
                    SceneManager.LoadScene(Random.Range(1, SceneManager.sceneCountInBuildSettings));
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }
}
