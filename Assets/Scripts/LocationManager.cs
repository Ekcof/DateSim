using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour, ISaveData
{
    private Location _currentLocation;

    public bool TryGetOnTheLocation(Location location)
    {
        if (location.NPC == null)
        {
            EventsBus.Publish(new OnTryToChangeUIState { State = UIState.Location });
        }
        else
        {
            EventsBus.Publish(new OnTryToChangeUIState { State = UIState.Dialogue });
        }
        return true;
    }

    public void Deserialize(SerializableData data)
    {
        throw new System.NotImplementedException();
    }

    public SerializableData Serialize()
    {
        throw new System.NotImplementedException();
    }
}

public class LocationData : SerializableData
{

}
