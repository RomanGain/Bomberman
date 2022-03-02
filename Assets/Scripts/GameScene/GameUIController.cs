using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{ 
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}