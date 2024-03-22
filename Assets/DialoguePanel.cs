using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    [SerializeField] private ScrollRect _variantScroll;
    [SerializeField] private DialogueSlot _slotPrefab;
    [SerializeField] private TextUI _respondText;
    [SerializeField] private TextUI _titleText;


    public void ApplyDialogue(Dialogue dialogue)
    {

    }

    public void ApplyNode(DialogueNode node)
    {

    }

    public void TypeText(string text)
    {
        _respondText.SetText(text);
    }

    public void RefreshVariants()
    {

    }

    public void Hide()
    {

    }

    public void ClearSlots()
    {

    }
}
