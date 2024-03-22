using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIStateBase
{
    public abstract bool IsReady();
    public abstract void Execute();
    public abstract void Stop();
}
