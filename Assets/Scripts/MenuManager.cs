using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public void PlayGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void PlayGameScene1()
    {
        SceneManager.LoadScene("GameScene1");
    }
}
