using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void OpenLevelMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenLevel1()
    {
        SceneManager.LoadScene(3);
    }
}
