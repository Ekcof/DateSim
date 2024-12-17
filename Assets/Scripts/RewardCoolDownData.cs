using System;

namespace DateSim.Rewards
{
    [Serializable]
    public class RewardCoolDownData
    {
        public string RewardId;
        public string CoolDown;
        [NonSerialized] public long CoolDownLong;

        public RewardCoolDownData(string id, long coolDown)
        {
            RewardId = id;
            CoolDownLong = coolDown;
            SetCoolDownFromLong(coolDown);
        }
        public bool IsExpired => CoolDownLong <= DateTime.UtcNow.Ticks;

        public void SetCoolDownFromLong(long value)
        {
            CoolDown = value.ToString();
        }
    }
}