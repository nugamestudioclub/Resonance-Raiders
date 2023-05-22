using UnityEngine;
using System;
using System.Collections.Generic;

public class UIInputListener : MonoBehaviour
{
    Action<UIController.UIState> changeStateInvocation;
    Func<UIController.UIState> getStateInvocation;

    [SerializeField] List<UIController.UIState> denyTransitionFrom;

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
            ForceStateChange(getStateInvocation.Invoke());
        }
    }

    public void RevertFromPause()
    {
        Time.timeScale = 1f;

        changeStateInvocation.Invoke(_stateBeforePause);
    }

    void ForceStateChange(UIController.UIState state)
    {
        if (denyTransitionFrom.Contains(state))
        {
            return;
        }

        if (state != UIController.UIState.PauseMenu)
        {
            _stateBeforePause = state;

            Time.timeScale = 0f;

            changeStateInvocation.Invoke(UIController.UIState.PauseMenu);
        }
        else
        {
            RevertFromPause();
        }
    }
}
