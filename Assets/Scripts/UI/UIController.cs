using UnityEngine;
using System.Collections.Generic;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject building;

    public enum UIState
    {
        None,
        MainMenu,
        Credits,
        PauseMenu,
        Building
    }

    private UIState _currentState;
    private UIState _priorState;

    // defo a better way to do this than to have two dicts but whatever
    private Dictionary<UIState, GameObject> _objectMap;

    // maps UIStates to all the children of the UI they belong to
    private Dictionary<UIState, RectTransform[]> _childMap;

    private void Awake()
    {
        // set up the child dictionary
        _childMap = new Dictionary<UIState, RectTransform[]>();
        _childMap.Add(UIState.MainMenu, mainMenu.GetComponentsInChildren<RectTransform>());
        _childMap.Add(UIState.Credits, credits.GetComponentsInChildren<RectTransform>());
        _childMap.Add(UIState.PauseMenu, pauseMenu.GetComponentsInChildren<RectTransform>());
        _childMap.Add(UIState.Building, building.GetComponentsInChildren<RectTransform>());

        UIState[] allButMain = new UIState[]
        {
            UIState.Credits, UIState.PauseMenu, UIState.Building
        };

        // set the scale of everything to zero.
        foreach (UIState state in allButMain)
        {
            RectTransform[] forms = _childMap[state];

            foreach (RectTransform rt in forms)
            {
                rt.localScale = Vector3.zero;
            }
        }

        // set up the parent dictionary
        _objectMap = new Dictionary<UIState, GameObject>();

        _objectMap.Add(UIState.MainMenu, mainMenu);
        _objectMap.Add(UIState.Credits, credits);
        _objectMap.Add(UIState.PauseMenu, pauseMenu);
        _objectMap.Add(UIState.Building, building);

        _currentState = UIState.MainMenu;
        _priorState = UIState.MainMenu;
    }

    // Start is called before the first frame update
    void Start()
    {
        // animate into the main menu
        //Invoke(nameof(test), 2f);
    }

    public void ChangeState(UIState state)
    {
        // this works because by the time the onComplete method is invoked, the priorState field will
        // have been reassigned, meaning that we disable the right thing.
        StartTweenOnAll(_childMap[_currentState], Vector3.zero, 0.4f,
            () => { ToggleParent(_objectMap[_priorState], false); }
        );

        if (state != UIState.None)
        {
            ToggleParent(_objectMap[state], true);

            StartTweenOnAll(_childMap[state], Vector3.one, 0.5f);
        }

        _priorState = _currentState;
        _currentState = state;
    }

    void ToggleParent(GameObject parent, bool state)
    {
        parent.SetActive(state);
    }

    int[] StartTweenOnAll(RectTransform[] objects, Vector3 tweenDest, float tweenTime, Action action = null)
    {
        int[] tweenIds = new int[objects.Length];

        int index = 0;

        float tweenTimeOffset = 0.1f;

        foreach (RectTransform rt in objects)
        {
            tweenIds[index] = 
                LeanTween.scale(rt, tweenDest, tweenTime + tweenTimeOffset * index)
                .setOnComplete(action)
                .setEaseInOutBack()
                .id;

            index += 1;
        }
        return tweenIds;
    }
}
