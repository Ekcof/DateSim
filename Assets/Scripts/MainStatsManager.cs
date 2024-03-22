using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MainStatsManager : MonoBehaviour, ISaveData
{
    private IReactiveProperty<int> _health;
    private IReactiveProperty<int> _exp;
    private IReactiveProperty<int> _level;
    private IReactiveProperty<long> _money;

    public IReadOnlyReactiveProperty<long> Money => _money;
    public IReadOnlyReactiveProperty<int> Health => _health;
    public IReadOnlyReactiveProperty<int> Exp => _exp;
    public IReadOnlyReactiveProperty<int> Level => _level;

    public void AddMoney(long moneyToAdd)
    {
        _money.Value += moneyToAdd;
        Math.Clamp(_money.Value, 0, long.MaxValue);
    }

    public void AddExp(int expToAdd)
    {
        _exp.Value += expToAdd;
    }

    public SerializableData Serialize()
    {
        return new MainStatsData
        {
            Health = _health.Value,
            Exp = _exp.Value,
            Level = _level.Value,
            Money = _money.Value,
        };
    }

    public void Deserialize(SerializableData data)
    {
        if (data is MainStatsData statsData)
        {
            _health.Value = statsData.Health;
            _exp.Value = statsData.Exp;
            _level.Value = statsData.Level;
            _money.Value = statsData.Money;
        }
    }
}

public class MainStatsData: SerializableData
{
    public int Health;
    public int Exp;
    public int Level;
    public long Money;

    public List<NPCProgress> NPCs;
}
