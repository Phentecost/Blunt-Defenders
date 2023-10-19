using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadGame( bool b)
    {
        Data_Manager.Tutorial_Activation = b;
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void xd()
    {
        Debug.Log("XD");
    }
}
