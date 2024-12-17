using DateSim.Settings;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public LocalizationData localizationData;

    public override void InstallBindings()
    {
        Container.Bind<LocalizationData>().FromInstance(localizationData).AsSingle();

        Container.Bind<ILocalizationManager>().To<LocalizationManager>().AsSingle().NonLazy();
        Container.Bind<ISettingsManager>().To<SettingsManager>().AsSingle().NonLazy();
        Container.Bind<ICoolDownManager>().To<CoolDownManager>().AsSingle().NonLazy();
        Container.Bind<INPCProgressManager>().To<NPCProgressManager>().AsSingle().NonLazy();
        Container.Bind<IMainStatsManager>().To<MainStatsManager>().AsSingle().NonLazy();
    }
}