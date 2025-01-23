using System;
using UnityEngine;

public class AutoDeath : MonoBehaviour
{
    ParticleSystem ps;

    public Action OnEvent;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
                Destroy(gameObject);
        }
    }

    public void DieOnEndAnimation()
    {
        Destroy(gameObject);
    }

    public void EventOn()
    {
        OnEvent?.Invoke();
    }
}
