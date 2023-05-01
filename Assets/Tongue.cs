using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bug"))
        {
            other.gameObject.GetComponent<BugBehavior>().GetEaten();
        }
    }
}
