using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] string mainMenuScene;

   public void MainMenuButton()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
