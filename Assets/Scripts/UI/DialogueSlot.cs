using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSlot : MonoBehaviour, ISelectableUI
{
    [SerializeField] private ButtonUI _button;
    [SerializeField] private TextUI _text;

    public void OnExecute()
    {
        throw new System.NotImplementedException();
    }

    public void OnSelect()
    {
        throw new System.NotImplementedException();
    }
}