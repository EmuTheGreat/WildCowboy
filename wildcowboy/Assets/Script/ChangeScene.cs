using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void OpenLevelMenu()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenLevel1()
    {
        SceneManager.LoadScene(3);
    }
}
