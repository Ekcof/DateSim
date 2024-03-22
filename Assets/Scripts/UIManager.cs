using UniRx;
using UnityEngine;

public enum UIState
{
    MainMenu,
    LocationChoice,
    Location,
    Dialogue,
    Minigame
}

public class UIManager : MonoBehaviour
{
    [SerializeField] private DialoguePanel _dialoguePanel;
    [SerializeField] private StatsPanel _statsPanel;
    [SerializeField] private LocationWindow _locationWindow;

    private UIDialogueState _dialogueState;
    private UILocationState _locationState;
    private UILocationChoiceState _locationChoiceState;

    private UIStateBase _currentState;

    private IReactiveProperty<UIState> _state;
    public IReactiveProperty<UIState> State => _state;
    private bool _isChangingLocked;

    private void Awake()
    {
        _dialogueState = new();
        _locationState = new();
        _locationChoiceState = new();

        EventsBus.Subscribe<OnTryToChangeUIState>(this, OnTryToChangeUIState);
    }

    private void OnTryToChangeUIState(OnTryToChangeUIState data)
    {
        if (_isChangingLocked) return;

        if (_state != null)
        {
            _currentState.Stop();
        }

        if (data.State is UIState.Dialogue)
        {
            _currentState = _dialogueState;
        }
        else if (data.State is UIState.LocationChoice)
        {
            _currentState = _locationChoiceState;
        }
        else if (data.State is UIState.Location)
        {
            _currentState = _locationState;
        }
        if (_currentState.IsReady())
        {
            _currentState.Execute();
        }
    }

}
