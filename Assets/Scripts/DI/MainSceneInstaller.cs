using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.EditorTools;
using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField] private  Camera _mainCamera;
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private DialoguePanel _dialoguePanel;

    [Header ("Serializable objects")]
    [SerializeField] private NPCProgressManager _npcProgressManager;
    [SerializeField] private MainStatsManager _mainStatsManager;
    [SerializeField] private LocationManager _locationManager;



    public override void InstallBindings()
    {
        Container.BindInstance(_mainCamera).WithId("mainCam");
        Bind(_dialogueManager);
        Bind(_dialoguePanel);
        Bind(_mainStatsManager);
        Bind(_npcProgressManager);
    }

    private protected void Bind<T>(T instance)
    {
        Container.BindInterfacesAndSelfTo<T>().FromInstance(instance).AsSingle();
    }

}
