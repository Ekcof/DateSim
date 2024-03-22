using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Game Resources/Dialogues/Dialogue Holder")]
public class DialogueHolder: ScriptableObject
{
    [SerializeField] private List<Dialogue> _dialogues;

    public Dialogue GetDialogueById(string id)
    {
        if (_dialogues == null || _dialogues.Count == 0)
            return null;

        for (int i = 0;  i < _dialogues.Count; i++)
        {
            var dialogue = _dialogues[i];
            if (dialogue.ID == id)
            {
                return dialogue;
            }
        }

        return null;
    }
}