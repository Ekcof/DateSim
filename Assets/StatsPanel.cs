using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UniRx;
using UnityEngine;
using Zenject;

[System.Serializable]
public class StatsPanel : MonoBehaviour
{
    [Inject] MainStatsManager _mainStatsManager;
    [SerializeField] private PanelText _moneyText;
    [SerializeField] private PanelText _levelText;
    [SerializeField] private PanelText _expText;

    private void Awake()
    {
        _mainStatsManager.Money
            .Subscribe(newMoneyValue =>
            {
                SetText(_moneyText, newMoneyValue);
            })
            .AddTo(this);
        _mainStatsManager.Level
            .Subscribe(newLevelValue =>
            {
                SetText(_levelText, newLevelValue);
            })
            .AddTo(this);
        _mainStatsManager.Exp
            .Subscribe(newLevelValue =>
            {
                SetText(_expText, newLevelValue);
            })
            .AddTo(this);

    }

    private void SetText(PanelText panelText, long number)
    {
        panelText.text = number.ToString();
    }

    private void SetText(PanelText panelText, int number)
    {
        panelText.text = number.ToString();
    }

    private void SetExpText(PanelText panelText, int number)
    {
        // TODO set exp text
    }

}
