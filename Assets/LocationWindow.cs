using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationWindow : BaseWindow
{
    [SerializeField] private LocationMap _currentMap;

    private protected override void Awake()
    {
        base.Awake();
    }

    public override void OnRequest()
    {
        Open();
    }

    public override void Close()
    {
        throw new System.NotImplementedException();
    }

    public override void Open()
    {
        _windowCanvas.enabled = true;
    }
}
