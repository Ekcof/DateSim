using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DateSim.Rewards;
using UniRx;
using UnityEngine;

public interface ICoolDownManager
{
    IReadOnlyReactiveCollection<RewardCoolDownData> RewardCoolDowns { get; }
    void Initialize();
    void Deserialize(SerializedCoolDowns serialized);
    SerializedCoolDowns Serialize();
    void AddRewardCoolDown(RewardCoolDownData rewardCoolDown);
}

public class CoolDownManager : ICoolDownManager, IDisposable
{
    private ReactiveCollection<RewardCoolDownData> _rewardCoolDowns = new();
    private CancellationTokenSource _cts = new();
    public IReadOnlyReactiveCollection<RewardCoolDownData> RewardCoolDowns => _rewardCoolDowns;

    public void Initialize()
    {
        _ = CoolDownTimer(_cts.Token);
        EventsBus.Subscribe<OnGetRewards>(this, OnGetRewards);
    }

    private void OnGetRewards(OnGetRewards data)
    {
        foreach (var reward in data.Rewards)
        {
            Debug.Log($"Try to add coolDOwn for {reward.Id}");
            if (reward.HasCoolDown && !_rewardCoolDowns.Any(x => x.RewardId == reward.Id))
            {
                long expireAt = DateTime.UtcNow.Ticks + TimeSpan.FromMinutes(reward.CoolDownMinutes).Ticks;
                var newCoolDown = new RewardCoolDownData(reward.Id, expireAt);
                _rewardCoolDowns.Add(newCoolDown);
                Debug.Log($"ADd coolDOwn for {reward.Id}");
            }
        }
    }

    public void Deserialize(SerializedCoolDowns serialized)
    {
        if (serialized == null || serialized.CoolDowns == null)
        {
            Debug.Log("SerializedCoolDowns or CoolDowns array is null");
            return;
        }

        _rewardCoolDowns.Clear();

        foreach (var cooldown in serialized.CoolDowns)
        {
            if (!cooldown.IsExpired)
                _rewardCoolDowns.Add(cooldown);
        }

        Initialize();
    }

    public SerializedCoolDowns Serialize()
    {
        var serialized = new SerializedCoolDowns
        {
            CoolDowns = _rewardCoolDowns.ToArray()
        };

        return serialized;
    }

    private async UniTask CoolDownTimer(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                await UniTask.Delay(1000, cancellationToken: token);
                foreach (var cooldown in _rewardCoolDowns)
                {
                    if (cooldown == null || cooldown.CoolDownLong <= DateTime.UtcNow.Ticks)
                    {
                        _rewardCoolDowns.Remove(cooldown);
                    }
                }
            }
            catch
            {
                return;
            }
        }
    }

    public void AddRewardCoolDown(RewardCoolDownData rewardCoolDown)
    {
        _rewardCoolDowns.Add(rewardCoolDown);
    }

    public void Dispose()
    {
        if (_cts != null)
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}