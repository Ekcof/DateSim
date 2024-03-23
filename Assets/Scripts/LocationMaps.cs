using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationMaps", menuName = "Locations/LocationMaps")]
public class LocationMaps : ScriptableObject
{
    [SerializeField] private LocationMap[] _maps;

    public LocationMap GetMapById(string id)
    {
        for (int i = 0; i < _maps.Length; i++)
        {
            if (_maps[i].ID == id)
            {
                return _maps[i];
            }
        }
        Debug.LogAssertion($"______LocationMaps: Didn't find a map with id {id}");
        return null;
    }
}
