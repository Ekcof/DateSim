using System.Linq;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
public class SOInstaller : ScriptableObjectInstaller
{
    [SerializeField] private DialogueHolder _dialogueHolder;
    public override void InstallBindings()
    {
        Bind(_dialogueHolder);
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
