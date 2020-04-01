using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene("Game");
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("Solar system");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene("Menu");
        }
    }

}