using UnityEngine;

public abstract class BaseWindow : MonoBehaviour
{
    [SerializeField] protected RectTransform _rt;
    [SerializeField] protected Canvas _windowCanvas;

    public static BaseWindow CurrentWindow;

    private protected virtual void Awake()
    {
        EventsBus.Subscribe<OnRequestToOpenWindow>(this, OnRequestToOpenWindow);
    }

    private void OnRequestToOpenWindow(OnRequestToOpenWindow arg)
    {
        if (arg.WindowType == GetType())
        {
            OnRequest();
        }
    }

    public abstract void OnRequest();

    public abstract void Open();

    public abstract void Close();
}
