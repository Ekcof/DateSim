using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.Android.Types;
using UnityEngine;

public class DayTimeCircle : MonoBehaviour, ISerializableService
{
    private int _currentDay;
    private int _currentHour;

    private MorningTimeState _morningState;
    private AfterNoonTimeState _afternoonState;
    private EveningTimeState _eveningState;
    private NightTimeState _nightState;

    private DayTimeStateBase _currentDayTimeState;

    private DayTimeStateBase[] _states = new DayTimeStateBase[4];

    private void Start()
    {
        _states = new DayTimeStateBase[]
        {
            new MorningTimeState(SpendTime),
            new AfterNoonTimeState(SpendTime),
            new EveningTimeState(SpendTime),
            new NightTimeState(SpendTime)
        };
    }

    public void Initialize()
    {
        EventsBus.Subscribe<OnSpendTime>(this,SpendTime);
    }

    public void SpendTime(OnSpendTime data)
    {
    }

    public void Deserialize(object data)
    {
        throw new System.NotImplementedException();
    }

    public void Serialize(object data)
    {
        throw new System.NotImplementedException();
    }
}
