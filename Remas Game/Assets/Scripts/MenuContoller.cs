using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuContoller : MonoBehaviour
{
    public void StartCandy()
    {
        SceneManager.LoadScene("MainGame 1");
    }

    public void ExitCandy()
    {
        Application.Quit();
    }

}
