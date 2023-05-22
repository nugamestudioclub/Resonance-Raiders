using UnityEngine;
using System;

// A wrapper for exposing methods for button UnityEvents.
// i thought it would look nicer than having everything in UIController :)
public class UIMethodWrapper : MonoBehaviour
{
    UIController controller;
    CameraPanControl panControl;

    [SerializeField] GameStateController gameController;
    Action nextRoundAction;

    private void Awake()
    {
        controller = GetComponent<UIController>();
        panControl = GetComponent<CameraPanControl>();
        nextRoundAction = gameController.NextRound;
    }

    public void GotoMain()
    {
        controller.ChangeState(UIController.UIState.MainMenu);
    }

    public void GotoPause()
    {
        controller.ChangeState(UIController.UIState.PauseMenu);
    }

    public void GotoCredits()
    {
        controller.ChangeState(UIController.UIState.Credits);
    }

    public void GotoBuild()
    {
        // we start off the game in preliminary state so i shouldnt need to do update state/nextRoundAction.Invoke() here.

        panControl.AnimateCameraPosition(CameraPanControl.CameraPositions.Destination);
        controller.ChangeState(UIController.UIState.Building);
    }

    public void GotoGame()
    {
        nextRoundAction.Invoke();
        controller.ChangeState(UIController.UIState.InGame);
    }

    public void GotoGameEnd()
    {
        panControl.AnimateCameraPosition(CameraPanControl.CameraPositions.Initial);
        controller.ChangeState(UIController.UIState.GameEnd);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
