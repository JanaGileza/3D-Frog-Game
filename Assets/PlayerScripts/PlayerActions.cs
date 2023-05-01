using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [Header("Tongue Mechanics")]
    [SerializeField] GameObject tongue;
    [SerializeField] Transform cameraOrientation;
    [SerializeField] float tongueDurationInSeconds = 1f;
    [SerializeField] float targetTongueLength = 2f;
    bool tongueIsOut;
    Vector3 originalScale;
    [SerializeField] KeyCode tongueKey = KeyCode.E;

    Coroutine _growTongueCoroutine;
    Coroutine _shrinkTongueCoroutine;
    
    void Start()
    {
        originalScale = tongue.transform.localScale;
        tongue.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(tongueKey))
            TryToShootTongue();
        
        if (tongueIsOut)
            tongue.transform.eulerAngles = cameraOrientation.eulerAngles + new Vector3(90, 0, 0);
    }

    void TryToShootTongue()
    {
        Debug.Log("Tried to shoot tongue");
        if (_growTongueCoroutine != null || tongueIsOut)
            return;
        
        _growTongueCoroutine = StartCoroutine(ShootTongue());
    }

    IEnumerator ShootTongue()
    {
        Debug.Log("In grow tongue coroutine");
        tongue.gameObject.SetActive(true);
        tongueIsOut = true;

        float t = 0;
        while (tongue.transform.localScale.y < targetTongueLength)
        {
            t += Time.deltaTime;
            float currentTongueLength = tongue.transform.localScale.y;
            float newTongueLength = Mathf.Lerp(currentTongueLength, targetTongueLength, t/tongueDurationInSeconds);
            Debug.Log("Current lengbth = " + currentTongueLength + ", newtonguelength=" + newTongueLength);
            tongue.transform.localScale += new Vector3(0, newTongueLength-currentTongueLength, 0);
            yield return null;
        }

        tongue.transform.localScale = new Vector3(tongue.transform.localScale.x, targetTongueLength, tongue.transform.localScale.z);
        Debug.Log("Finished grow tongue coroutine");
        yield return StartCoroutine(ShrinkTongue());
    }

    IEnumerator ShrinkTongue()
    {
        float t = 0;
        while (Vector3.Distance(tongue.transform.localScale, originalScale) > Mathf.Epsilon)
        {
            t += Time.deltaTime;
            Vector3 currentScale = tongue.transform.localScale;
            Vector3 newTongueScale = Vector3.Lerp(currentScale, originalScale, t/(tongueDurationInSeconds/2));
            tongue.transform.localScale = newTongueScale;
            yield return null;
        }

        tongue.transform.localScale = originalScale;
        tongueIsOut = false;
        tongue.gameObject.SetActive(false);
        _growTongueCoroutine = null;
    }
}
