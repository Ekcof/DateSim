using System;

public class OnStartDialogue
{
    public Dialogue Dialogue;
}

public class OnSelectAnswer
{
    public DialogueAnswer Answer;
}

public class OnTryToChangeUIState
{
    public UIState State;
}

public class OnRequestToOpenWindow
{
    public Type WindowType;
}

public class OnStartLevel
{
    public int Level;
}

public class OnFailLevel
{
    public int level;
}

public class OnFinishLevel
{
}

public class OnGetRewards
{
    public Reward[] Rewards;
}