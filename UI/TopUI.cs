using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopUI : MonoBehaviour
{
    private Queue<IEnumerator> coroutineQueue = new Queue<IEnumerator>();
    private Queue<int> nextGoldQueue = new Queue<int>();
    private Queue<int> processGoldQueue = new Queue<int>();
    private Action onRoutine;
    int curJewelry;
    int curGold;
    int nextGold;

    [SerializeField]
    TMPro.TMP_Text jewelryTxt;
    [SerializeField]
    TMPro.TMP_Text goldTxt;

    bool isRunning;

    void Awake()
    {
        //temp
        curJewelry = 5390;
        curGold = 13592;

        isRunning = false;
    }

    void Start()
    {
        SetJewelryTxt(curJewelry);
        SetGoldTxt(curGold);

        onRoutine = () => { nextGold = curGold + nextGoldQueue.Dequeue(); };
    }

    private void SetJewelryTxt(int jewelry)
    {
        jewelryTxt.text = jewelry.ToString();
    }

    private void SetGoldTxt(int gold)
    {
        goldTxt.text = gold.ToString();
    }

    public void AddGold(int amount)
    {
        nextGoldQueue.Enqueue(amount);
        coroutineQueue.Enqueue(GoldAnimation());
        
        if (!IsRunning())
            StartCoroutine(RunCoroutines());
    }

    private IEnumerator GoldAnimation()
    {
        SetGoldProcess();

        int queueCount = processGoldQueue.Count;

        for (int i = 0; i < queueCount; i++)
        {
            SetGoldTxt(processGoldQueue.Dequeue());
            yield return new WaitForSeconds(0.03f);
        }
    }

    private IEnumerator RunCoroutines()
    {
        isRunning = true;

        while (coroutineQueue.Count > 0)
        {
            IEnumerator currentRoutine = coroutineQueue.Dequeue();
            onRoutine?.Invoke();

            while (currentRoutine.MoveNext())
                yield return currentRoutine.Current;
        }

        isRunning = false;
    }

    private void SetGoldProcess()
    {
        int minus = (nextGold - curGold) * 2;

        for (int i = 11; i > 1; i--)
            processGoldQueue.Enqueue(curGold + (minus / i));
    }

    private bool IsRunning()
    {
        return isRunning;
    }

    private bool IsCoroutineRunning(string name)
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).name == name)
            {
                return true;
            }
        }
        return false;
    }


}
