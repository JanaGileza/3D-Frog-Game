using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneTrigger : MonoBehaviour
{
    public string goodEnd;
    public string okayEnd;
    public string badEnd;

    [SerializeField] GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && gameManager.BugsRemaining == 0)
        {
            SceneManager.LoadScene(goodEnd);
        }

        if(other.CompareTag("Player") && gameManager.BugsRemaining < 9 && gameManager.BugsRemaining > 1)
        {
            SceneManager.LoadScene(okayEnd);
        }

        if(other.CompareTag("Player") && gameManager.BugsRemaining == 10)
        {
            SceneManager.LoadScene(badEnd);
        }
    }
}
