using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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