using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BugCountdown : MonoBehaviour
{
    TMP_Text bugCountText;
    [SerializeField] GameManager gameManager;
    
    // Awake is called when the script instance is being loaded (only once, before Start())
    void Awake()
    {
        bugCountText = GetComponent<TMP_Text>();
    }
    
    void Update()
    {
        bugCountText.text = "Bugs left to catch: " + gameManager.BugsRemaining.ToString();
    }

}
