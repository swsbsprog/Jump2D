using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
            SceneManager.LoadScene(1);
    }
}
