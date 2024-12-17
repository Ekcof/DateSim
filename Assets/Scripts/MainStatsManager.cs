using System;
using UniRx;

public interface IMainStatsManager
{
    IReadOnlyReactiveProperty<long> Money { get; }
    IReadOnlyReactiveProperty<int> Health { get; }
    IReadOnlyReactiveProperty<int> Exp { get; }
    IReadOnlyReactiveProperty<int> Level { get; }

    void AddMoney(long moneyToAdd);
    void AddExp(int expToAdd);
    SerializedMainStatsData Serialize();
    void Deserialize(SerializedMainStatsData data);
}

public class MainStatsManager : IMainStatsManager
{
    private ReactiveProperty<int> _health = new();
    private ReactiveProperty<int> _exp = new();
    private ReactiveProperty<int> _level = new();
    private ReactiveProperty<long> _money = new();

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

    public SerializedMainStatsData Serialize()
    {
        return new SerializedMainStatsData
        {
            Health = _health.Value,
            Exp = _exp.Value,
            Level = _level.Value,
            Money = _money.Value,
        };
    }

    public void Deserialize(SerializedMainStatsData data)
    {
        if (data is SerializedMainStatsData statsData)
        {
            _health.Value = statsData.Health;
            _exp.Value = statsData.Exp;
            _level.Value = statsData.Level;
            _money.Value = statsData.Money;
        }
    }
}