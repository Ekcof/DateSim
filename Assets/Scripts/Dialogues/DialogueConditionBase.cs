using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class DialogueConditionBase : MonoBehaviour
{
    public string ID {  get; private set; }

    public abstract bool OnCheckCondition();
}
