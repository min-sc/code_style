using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraShake : MonoBehaviour
{
    static protected List<CameraShake> s_Cameras = new List<CameraShake>();

    public float shakeAmount = 0.1f; 
    public float shakeDuration = 0.5f; 
    private Vector3 originalPos; 

    void Start()
    {
        originalPos = transform.localPosition; 
    }

    public void ShakeCamera()
    {
        transform.localPosition = UnityEngine.Random.insideUnitCircle * shakeAmount; 
        Invoke("StopShaking", shakeDuration); 
    }

    private void StopShaking()
    {
        transform.localPosition = originalPos; 
    }
}