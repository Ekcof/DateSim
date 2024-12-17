using System.Collections.Generic;
using System.Linq;

public interface INPCProgressManager
{
    void SaveNPCProgress(NPCProgress progress);
    bool TryGetNPCProgress(string name, out NPCProgress progress);
    SerializedNPCData Serialize();
    void Deserialize(SerializedNPCData data);
}

public class NPCProgressManager : INPCProgressManager, ISaveData
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

    public SerializedNPCData Serialize()
    {
        return new SerializedNPCData { NPCs = _npcs.ToArray() };
    }

    public void Deserialize(SerializedNPCData data)
    {
        _npcs = data.NPCs.ToList();
    }
}
