using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProgressTimer
{
    private float remaining;
    private bool running;

    public bool IsRunning => running;
    public float Remaining => remaining;

    public event Action<float> OnTick;  // remaining seconds
    public event Action OnFinished;

    public void Start(float seconds)
    {
        remaining = Mathf.Max(0f, seconds);
        running = true;
        OnTick?.Invoke(remaining);
    }

    public void Stop()
    {
        running = false;
    }

    public void Update(float dt)
    {
        if (!running) return;

        remaining -= dt;
        if (remaining <= 0f)
        {
            remaining = 0f;
            running = false;
            OnTick?.Invoke(remaining);
            OnFinished?.Invoke();
            return;
        }

        OnTick?.Invoke(remaining);
    }

}
