using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    //TODO: ������� ��������, ��������� ������ � �������
    [SerializeField] private SpriteRenderer _bgRender;
    [SerializeField] private Dictionary<Location, ButtonUI> _directionButtons;
    [SerializeField] private Dialogue _npc;
    public Dialogue NPC => _npc;
}
