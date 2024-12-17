using UniRx;

namespace DateSim.Settings
{
    public interface ISettingsManager
    {
        IReadOnlyReactiveProperty<GraphicsLevel> GraphicsLevel { get; }
        IReadOnlyReactiveProperty<float> SoundVolume { get; }
        IReadOnlyReactiveProperty<float> MusicVolume { get; }
        void SetGraphicsLevel(GraphicsLevel value);
        void Deserialize(SerializedPlayerSettings serializedData);
        SerializedPlayerSettings Serialize();
    }

    public class SettingsManager : ISettingsManager
    {
        private ReactiveProperty<GraphicsLevel> _graphicsLevel = new();
        private ReactiveProperty<float> _soundVolume = new();
        private ReactiveProperty<float> _musicVolume = new();
        public IReadOnlyReactiveProperty<GraphicsLevel> GraphicsLevel => _graphicsLevel;
        public IReadOnlyReactiveProperty<float> SoundVolume => _soundVolume;
        public IReadOnlyReactiveProperty<float> MusicVolume => _musicVolume;

        public void Deserialize(SerializedPlayerSettings serializedData)
        {
            _graphicsLevel.Value = (GraphicsLevel)serializedData.GraphicsPreset;
            _soundVolume.Value = serializedData.SoundVolume;
            _musicVolume.Value = serializedData.MusicVolume;
        }

        public SerializedPlayerSettings Serialize()
        {
            var serializedData = new SerializedPlayerSettings();

            serializedData.GraphicsPreset = (int)_graphicsLevel.Value;
            serializedData.SoundVolume = _soundVolume.Value;
            serializedData.MusicVolume = _musicVolume.Value;

            return serializedData;
        }

        public void SetGraphicsLevel(GraphicsLevel value)
        {
            _graphicsLevel.Value = value;
        }
    }
}
