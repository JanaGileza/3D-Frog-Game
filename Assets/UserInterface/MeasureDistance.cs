using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeasureDistance : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform houseTransform;
    TMP_Text distanceText; 

    void Awake()
    {
        distanceText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        int distance = (int) Vector3.Distance(playerTransform.position, houseTransform.position);
        distanceText.SetText(distance.ToString() + " in from home");
    }
}
