using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour, ISelectableUI
{
    [SerializeField] private Button _button;
    private SpriteRenderer _selectedSpriteRenderer;
    private UnityEngine.Events.UnityAction _onPress;

    private void Awake()
    {
        _button.onClick.RemoveAllListeners();
        if (_selectedSpriteRenderer != null)
            _selectedSpriteRenderer.enabled = false;
    }

    public void SetListener(UnityEngine.Events.UnityAction onPress)
    {
        if (onPress == null)
            return;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(onPress);
        _onPress = onPress;
    }

    public void OnSelect()
    {
        if (_selectedSpriteRenderer != null)
            _selectedSpriteRenderer.enabled = true;
    }

    public void OnExecute()
    {
        _onPress?.Invoke();
    }
}
