using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCProgressManager : MonoBehaviour, ISaveData
{
    private List<NPCProgress> _npcs;

    public void SaveNPCProgress(NPCProgress progress)
    {
        for (int i = 0; i < _npcs.Count; i++)
        {
            if (_npcs[i].Name == progress.Name)
            {
                _npcs[i] = progress;
                return;
            }
        }
        _npcs.Add(progress);
    }

    public bool TryGetNPCProgress(string name, out NPCProgress progress)
    {
        for (int i = 0; i < _npcs.Count; i++)
        {
            if (_npcs[i].Name == name)
            {
                progress = _npcs[i];
                return true;
            }
        }
        progress = new NPCProgress();
        return false;
    }

    public SerializableData Serialize()
    {
        return new NPCData { NPCs = _npcs};
    }

    public void Deserialize(SerializableData data)
    {
        if (data is NPCData npcs)
        {
            _npcs = npcs.NPCs;
        }
    }
}

[System.Serializable]
public struct NPCProgress
{
    public string Name;
    public float Progress;
    public string[] UsedNodeIds;
}


public class NPCData: SerializableData
{
    public List<NPCProgress> NPCs;
}


