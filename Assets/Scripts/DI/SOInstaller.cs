using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
public class SOInstaller : ScriptableObjectInstaller
{
    [SerializeField] private DialogueHolder _dialogueHolder;
    [SerializeField] private LocalizationData _localizationData;

    public override void InstallBindings()
    {
        Bind(_dialogueHolder);
        Bind(_localizationData);
    }


    private void Bind<T>(T instance) where T : ScriptableObject
    {
        if (typeof(T).GetInterfaces().Any())
        {
            Container.BindInterfacesAndSelfTo<T>().FromInstance(instance);
            return;
        }

        Container.BindInstance(instance);
    }
}
