using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum DialogueEmotion
{
    None,
    Normal,
    Smile,
    Laugh,
    Surprise,
    Scared,
    Angry,
    Dissapointed,
    Flirty
}

public class DialogueManager : MonoBehaviour
{
    [Inject] private DialoguePanel _dialoguePanel;
    private Dialogue _currentDialogue;

    private void Initialize()
    {
        EventsBus.Subscribe<OnSelectAnswer>(this, OnSelectAnswer);
    }

    public void ApplyDialogue(Dialogue dialogue)
    {
        if (dialogue == null) return;
        _currentDialogue = dialogue;

        var node = dialogue.GetStartNode();

        //TODO: Данные аватара NPC

        if (!string.IsNullOrEmpty(node.ID))
        {
            StartNode(node);
            _dialoguePanel.ApplyDialogue(dialogue);
        }
    }

    private void StartNode(DialogueNode node)
    {
        _dialoguePanel.ApplyNode(node);
    }

    private void AbortDialogue()
    {
        _currentDialogue = null;
        _dialoguePanel.Hide();
    }

    private void OnSelectAnswer(OnSelectAnswer data)
    {
        if (_currentDialogue == null)
            return;

        if (string.IsNullOrEmpty(data.Answer.NextNodeId))
        {
            AbortDialogue();
            return;
        }
        else
        {
            for (int i = 0; i < _currentDialogue.Nodes.Length; i++)
            {
                if (_currentDialogue.Nodes[i].ID == data.Answer.NextNodeId)
                {
                    StartNode(_currentDialogue.Nodes[i]);
                    return;
                }
            }
        }
        AbortDialogue();
    }

}
