using UnityEngine;

// A wrapper for exposing methods for button UnityEvents.
// i thought it would look nicer than having everything in UIController :)
public class UIMethodWrapper : MonoBehaviour
{
    UIController controller;
    CameraPanControl panControl;

    private void Awake()
    {
        controller = GetComponent<UIController>();
        panControl = GetComponent<CameraPanControl>();
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
        controller.ChangeState(UIController.UIState.Building);
    }

    public void GotoGame()
    {
        controller.ChangeState(UIController.UIState.InGame);
        panControl.AnimateCameraPosition(CameraPanControl.CameraPositions.Destination);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
