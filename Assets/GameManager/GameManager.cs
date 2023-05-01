using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int BugsRemaining = 10;

    void OnEnable()
    {
        BugBehavior.OnBugDeath += UpdateBugCount;
    }

    void OnDisable()
    {
        BugBehavior.OnBugDeath -= UpdateBugCount;
    }

    private void UpdateBugCount()
    {
        BugsRemaining--;
    }

    void Start()
    {
        BugsRemaining = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
