using System;

public class AfterNoonTimeState: DayTimeStateBase
{
    public AfterNoonTimeState(Action<OnSpendTime> onSpendTime) : base(onSpendTime)
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