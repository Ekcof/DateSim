using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DayTimeStateBase : MonoBehaviour
{
    private Action<OnSpendTime> _onSpendTime;

    public DayTimeStateBase(Action<OnSpendTime> onSpendTime)
    {
        _onSpendTime = onSpendTime;
    }

    private protected int _hoursLeft;

    public abstract void Initialize();

    public abstract void Update();
    public abstract int End();
}
