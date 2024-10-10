using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeAmount;
    private float shakeTime;
    private Vector3 initialPos;

    private void Start()
    {
        initialPos = new Vector3(0, 0, -10);
    }

    private void Update()
    {
        if(shakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * shakeAmount + initialPos;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeTime = 0f;
            transform.position = initialPos;
        }
    }

    public void VibrateForTime(float time)
    {
        shakeTime = time;
    }
}
