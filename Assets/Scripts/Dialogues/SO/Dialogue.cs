using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Game Resources/Dialogues/Dialogue")]
public class Dialogue : ScriptableObject
{
    [field: SerializeField] public string ID { get; private set; }
    [field: SerializeField] public NPCAvatar Avatar { get; private set; }
    [SerializeField] private DialogueNode[] _nodes;
    private List<string> _watchedDialogueNodes = new();
    public DialogueNode[] Nodes => _nodes;

    public SerializedDialogueData Serialize()
    {
        var data = new SerializedDialogueData();

        data.ID = ID;
        data.WatchedNodeIds = _watchedDialogueNodes.ToArray();

        return data;
    }

    public void Deserialize(SerializedDialogueData data)
    {
        if (data.ID != ID)
            return;

        _watchedDialogueNodes = new List<string>();
        foreach (var node in data.WatchedNodeIds)
        {
            _watchedDialogueNodes.Add(node.ToString());
        }
    }

    /// <summary>
    /// Gets the start node of dialogue according to specific conditions
    /// </summary>
    /// <returns></returns>
    public DialogueNode GetStartNode()
    {
        if (_nodes == null || _nodes.Length == 0)
            return default;

        for (int i = 0; i < _nodes.Length; i++)
        {
            if (_nodes[i].IsStartNode && (_nodes[i].Condition != null || _nodes[i].Condition.OnCheckCondition()))
            {
                return _nodes[i];
            }
        }

        return _nodes[0];
    }


    /// <summary>
    /// Get node with specific ID in this dialogue
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public DialogueNode GetNodeByID(string id)
    {
        if (_nodes == null || _nodes.Length == 0)
            return default;

        for (int i = 0; i < _nodes.Length; i++)
        {
            if (_nodes[i].ID == id)
            {
                return _nodes[i];
            }
        }

        return default;
    }

    /// <summary>
    /// Get if node has already been watched
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool IsNodeWatched(string id) => _watchedDialogueNodes.Contains(id);
}

[Serializable]
public struct DialogueNode
{
    [field: SerializeField] public string ID { get; private set; }
    [field: SerializeField] public string _text { get; private set; }
    [field: SerializeField] public DialogueAnswer[] _answers { get; private set; }
    [field: SerializeField] public DialogueEmotion _presetEmotion { get; private set; }
    [field: SerializeField] public DialogueConditionBase Condition { get; private set; }
    [field: SerializeField] public bool IsStartNode { get; private set; }
}

[Serializable]
public class DialogueAnswer
{
    [field: SerializeField] public string ID { get; private set; }
    [field: SerializeField] public string Text { get; private set; }
    [field: SerializeField] public string NextNodeId { get; private set; }

    [field: SerializeField] public DialogueConditionBase Condition { get; private set; }
    [field: SerializeField] public DialogueActionBase OnSelect { get; private set; }
    [field: SerializeField] public DialogueEmotion Emotion { get; private set; }

    public void Answer()
    {
        OnSelect?.Invoke();
        EventsBus.Publish(new OnSelectAnswer() { Answer = this });
    }
}

public struct SerializedDialogueData
{
    public string ID;
    public string[] WatchedNodeIds;
}
