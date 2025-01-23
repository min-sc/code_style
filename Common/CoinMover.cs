using System;
using System.Collections;
using UnityEngine;

public class CoinMover : MonoBehaviour
{
    Action onArrived;

    bool isGoingGold = false;
    Vector2 targetPos;

    float radius = 5f;
    float delay = 0.2f;
    float angle;
    Vector3 initPos;
    Vector3 endPos;
    float duration = 0.3f;
    private float elapsedTime = 0f;

    void Update()
    {
        if (Vector3.Distance(transform.position, endPos) < 0.01f)
            isGoingGold = true;

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            onArrived?.Invoke();

        transform.position = Vector3.Lerp(initPos, endPos, elapsedTime / duration);
        elapsedTime += Time.deltaTime;
    }

    public void Move(Vector2 targetPos, Action onArrived = null)
    {
        this.targetPos = targetPos;
        this.onArrived = onArrived;

        initPos = transform.position;
        angle = UnityEngine.Random.Range(0f, 360f);
        radius = UnityEngine.Random.Range(30f, 50f);
        endPos = new Vector3(initPos.x + Mathf.Cos(angle * Mathf.Deg2Rad) * radius, initPos.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius, initPos.z);

        StartCoroutine(GoToTarget());
    }

    IEnumerator GoToTarget()
    {
        yield return new WaitUntil(() => isGoingGold);
        yield return new WaitForSeconds(delay);

        elapsedTime = 0f;
        initPos = transform.position;
        endPos = targetPos;
    }
}
