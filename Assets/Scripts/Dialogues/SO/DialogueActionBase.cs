using System;
using UnityEngine;

[Serializable]
public abstract class DialogueActionBase
{
    [field: SerializeField] public string ID;
    public abstract void Invoke();
}