using TMPro;
using UnityEngine;

[System.Serializable]
public class PanelText
{
    [SerializeField] TMP_Text _textField;
    private const int _shortenThreshold = 10000;

    public string text
    {
        get
        {
            if (_textField != null) return _textField.text;
            return string.Empty;
        }

        set
        {
            if (_textField != null) _textField.text = value;
        }
    }
}