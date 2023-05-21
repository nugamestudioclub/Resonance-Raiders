using UnityEngine;
using System.Collections.Generic;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject building;

    [SerializeField] GameObject[] contentsToToggle;

    public enum UIState
    {
        InGame,
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
        _childMap.Add(UIState.InGame, gameUI.GetComponentsInChildren<RectTransform>());
        _childMap.Add(UIState.MainMenu, mainMenu.GetComponentsInChildren<RectTransform>());
        _childMap.Add(UIState.Credits, credits.GetComponentsInChildren<RectTransform>());
        _childMap.Add(UIState.PauseMenu, pauseMenu.GetComponentsInChildren<RectTransform>());
        _childMap.Add(UIState.Building, building.GetComponentsInChildren<RectTransform>());

        // set up the parent dictionary
        _objectMap = new Dictionary<UIState, GameObject>();

        _objectMap.Add(UIState.InGame, gameUI);
        _objectMap.Add(UIState.MainMenu, mainMenu);
        _objectMap.Add(UIState.Credits, credits);
        _objectMap.Add(UIState.PauseMenu, pauseMenu);
        _objectMap.Add(UIState.Building, building);


        UIState[] allButMain = new UIState[]
        {
            UIState.InGame, UIState.Credits, UIState.PauseMenu, UIState.Building
        };

        // set the scale of everything to zero, and disabling all non-MainMenu stuff
        foreach (UIState state in allButMain)
        {
            ToggleParent(_objectMap[state], false);

            RectTransform[] forms = _childMap[state];

            foreach (RectTransform rt in forms)
            {
                rt.localScale = Vector3.zero;
            }
        }


        _currentState = UIState.MainMenu;
        _priorState = UIState.MainMenu;

        ToggleVitalGameElements(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.cancelAll();

        // animate into the main menu
        EnableUI(UIState.MainMenu);
    }

    // tweens out the old UI and initiates tweening in of the new as well as toggling it
    public void ChangeState(UIState state)
    {
        switch (state)
        {
            case UIState.Building:
                ToggleVitalGameElements(true);
                break;

            case UIState.MainMenu:
                ToggleVitalGameElements(false);
                break;

            default:
                Debug.Log("No current implementation for toggling elements for state " + state);
                break;
        }

        // this works because by the time the onComplete method is invoked, the priorState field will
        // have been reassigned, meaning that we disable the right thing.
        StartTweenOnAll(_childMap[_currentState], Vector3.zero, 0.4f,
            () => { ToggleParent(_objectMap[_priorState], false); }
        );

        EnableUI(state);
    }

    // enables the UI header and tweens in all of its children
    void EnableUI(UIState state)
    {
        ToggleParent(_objectMap[state], true);

        StartTweenOnAll(_childMap[state], Vector3.one, 0.5f);

        _priorState = _currentState;
        _currentState = state;
    }

    void ToggleVitalGameElements(bool toState)
    {
        foreach (GameObject g in contentsToToggle)
        {
            g.SetActive(toState);
        }
    }

    // toggles the gameobject to state
    void ToggleParent(GameObject parent, bool state)
    {
        // if nothing to update, nothing doing
        if (parent.activeInHierarchy == state) return;

        parent.SetActive(state);
    }

    // starts tweening on all recttransform children of a UI parent
    void StartTweenOnAll(RectTransform[] objects, Vector3 tweenDest, float tweenTime, Action action = null)
    {
        int index = 0;

        float tweenTimeOffset = 0.05f;

        foreach (RectTransform rt in objects)
        {
            LeanTween.scale(rt, tweenDest, tweenTime + tweenTimeOffset * index)
                .setOnComplete(action)
                .setEaseInOutBack();

            index += 1;
        }
    }
}
