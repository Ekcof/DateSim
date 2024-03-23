using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocationMap : MonoBehaviour
{
    [Serializable]
    public class LocationSmallView
    {
        public ButtonUI _button;
        public Location _location;
        public Sprite _sprite;
    }

    [SerializeField] private string _id;            
    [SerializeField] private List<LocationSmallView> _locations;
    [SerializeField] private Sprite _bgSprite;
    public string ID => _id;




}
