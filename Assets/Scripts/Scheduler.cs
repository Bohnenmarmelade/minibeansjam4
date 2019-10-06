using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class Scheduler {
    public float TotalTimeElapased;
    private readonly float _timeToFire;
    private readonly Action _callback;
    private readonly bool _repeat;

    private bool _hasBeenCalled;

    public Scheduler(float timeToFire, Action callback, bool repeat = false)
    {
        _timeToFire = timeToFire;
        _callback = callback;
        _repeat = repeat;
    }

    public void Update(float timeElapsed)
    {
        if (_hasBeenCalled && !_repeat) return;
        
        TotalTimeElapased += timeElapsed;
        if (TotalTimeElapased >= _timeToFire)
        {
            _callback();
            TotalTimeElapased = TotalTimeElapased % _timeToFire;
            _hasBeenCalled = true;
        }
    }

    public static Scheduler Schedule(float timeToFire, Action callback, bool repeat = false)
    {
        return new Scheduler(timeToFire, callback, repeat);
    }
}
