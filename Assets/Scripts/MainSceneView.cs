using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Responsible for UI on MainScreen
/// </summary>
public class MainSceneView : MonoBehaviour
{
    [SerializeField] private ButtonUI _locationButton;
    [SerializeField] private GameObject _descriptionPanel;
    [SerializeField] private TextUI _descriptionText;

    private void Awake()
    {

    }

    private void Switch()
    {
        _descriptionText.SetText(GetActualText(), true);

    }

    private string GetActualText()
    {
        return string.Empty;
    }
}
