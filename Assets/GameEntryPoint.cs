using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DateSim.Settings;
//using DateSim.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameEntryPoint : MonoBehaviour
{
    //[Inject] SerializedDataHolder _dataHolder;
    [Inject] ISettingsManager _settingsManager;
    [Inject] ILocalizationManager _localizationManager;
    [Inject] ICoolDownManager _coolDownManager;
    [Inject] SerializedDataHolder _dataHolder;
    [SerializeField] private UnityEngine.UI.Slider _slider;
    [SerializeField] private TextUI _progressText;


    private CancellationTokenSource _cts;
    private void Start()
    {
        _cts = new();
        _ = DeserializeDataAsync(_cts.Token);
    }

    private async UniTask DeserializeDataAsync(CancellationToken token)
    {
        try
        {
            _dataHolder.LoadSerializedData();

            _slider.value = 50f;
            _progressText.SetText("50%");
        }
        catch (Exception e)
        {
            Debug.LogError(e.StackTrace);
            return;
        }

        try
        {
            await LoadSceneWithProgress("MainScene", token);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load MainScene. Error: {e.Message}");
            Debug.LogError(e.StackTrace);
        }
    }

    private async UniTask LoadSceneWithProgress(string sceneName, CancellationToken token)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone && !token.IsCancellationRequested)
        {
            // Get the progress of the load operation.
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            // Update the slider and text components from 50% to 100%.
            _slider.value = 50f + progress * 50f;
            _progressText.SetText($"{(int)((0.5f + progress * 0.5f) * 100)}%");

            // Allow the scene to activate once the load is nearly complete.
            if (asyncOperation.progress >= 0.9f)
            {
                _progressText.SetText("100%");
                _slider.value = 100f;
                asyncOperation.allowSceneActivation = true;
                return;
            }
            try
            {
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
            catch
            {
                return;
            }
        }
    }


    private void OnDestroy()
    {
        if (_cts != null)
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}