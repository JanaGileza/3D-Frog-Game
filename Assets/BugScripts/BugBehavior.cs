using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BugBehavior : MonoBehaviour
{
    public static Action OnBugDeath;

    [SerializeField] float moveRange = 1.0f;
    [SerializeField] float duration = 1.0f;
    [SerializeField] float minWaitTime = 0f;
    [SerializeField] float maxWaitTime = 10f;

    Vector3 startingPosition;
    Coroutine bugPathingCoroutine;

    IEnumerator BugPathing()
    {
        while(true){
            yield return StartCoroutine(MoveToRandomPosition());
            float randomWaitTime = UnityEngine.Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(randomWaitTime);
        }
    }

    IEnumerator MoveToRandomPosition()
    {
        var oldPosition = transform.position;
        var newPosition = startingPosition + (UnityEngine.Random.insideUnitSphere * moveRange);
        float t = 0;
        while(Vector3.Distance(transform.position, newPosition) > Mathf.Epsilon){
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(oldPosition, newPosition, t/duration);
            yield return null;
        }
    }

    void Start()
    {
        startingPosition = transform.position;
        if (bugPathingCoroutine != null)
            StopCoroutine(bugPathingCoroutine);
        
        bugPathingCoroutine = StartCoroutine(BugPathing());
    }

    void OnDisable() => StopCoroutine(bugPathingCoroutine);
    void OnDestroy()
    {
        StopCoroutine(bugPathingCoroutine);
    }

    public void GetEaten()
    {
        // FX go here :)
        HandleDeath();
    }

    void HandleDeath()
    {
        OnBugDeath?.Invoke();
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        if (startingPosition == null || startingPosition == Vector3.zero) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(startingPosition, moveRange);
    }
}
