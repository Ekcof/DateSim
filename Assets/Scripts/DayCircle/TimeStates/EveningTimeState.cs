using System;

public class EveningTimeState : DayTimeStateBase
{
    public EveningTimeState(Action<OnSpendTime> onSpendTime) : base(onSpendTime)
    {
    }

    public override int End()
    {
        throw new System.NotImplementedException();
    }

    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}