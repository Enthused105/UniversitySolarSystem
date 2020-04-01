using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineActive : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }



}
