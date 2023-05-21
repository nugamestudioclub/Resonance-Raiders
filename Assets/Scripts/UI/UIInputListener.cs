using UnityEngine;
using System;

public class UIInputListener : MonoBehaviour
{
    Action<UIController.UIState> changeStateInvocation;
    Func<UIController.UIState> getStateInvocation;

    private UIController.UIState _stateBeforePause;

    private void Awake()
    {
        UIController wrapper = GetComponent<UIController>();

        if (wrapper)
        {
            changeStateInvocation = wrapper.ChangeState;
            getStateInvocation = wrapper.GetCurrentState;
        } else
        {
            Debug.LogError("UIMethodWrapper not found on " + name);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var result = getStateInvocation.Invoke();

            if (result != UIController.UIState.PauseMenu)
            {
                _stateBeforePause = result;

                Time.timeScale = 0f;

                changeStateInvocation.Invoke(UIController.UIState.PauseMenu);
            } else
            {
                Time.timeScale = 1f;

                changeStateInvocation.Invoke(_stateBeforePause);
            }
        }
    }
}
