using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tick : MonoBehaviour
{
    public static event EventHandler OnTick;

    private const float TICK_TIMER_MAX = .2f;
    private float tickTimer;

    // Update is called once per frame
    void Update()
    {
        if(tickTimer < TICK_TIMER_MAX)
        {
            tickTimer += Time.deltaTime;
            return;
        }

        OnTick?.Invoke(this, null);
        tickTimer = 0f;
    }
}
