using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LocationMap : MonoBehaviour
{
    [Serializable]
    public class LocationSmallView
    {
        public string ID;
        public ButtonUI _button;
        public Location _location;
        public Image _image;
        public TextUI _text;
    }

    [SerializeField] private string _id;            
    [SerializeField] private List<LocationSmallView> _locations;
    public string ID => _id;

}
