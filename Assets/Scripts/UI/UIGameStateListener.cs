using UnityEngine;
using UnityEngine.Events;

// hacky as all beans but whatever
public class UIGameStateListener : MonoBehaviour
{
    [SerializeField] GameStateController controller;
    [SerializeField] GameState stateToListenFor;

    [SerializeField] UnityEvent toFire;

    bool fired = true;

    private void Update()
    {
        // if we havent already fired this event and we see that the state has
        // been updated to what we are looking for, fire the event
        if (!fired && controller.state == stateToListenFor)
        {
            toFire.Invoke();

            fired = true;
        } else if (fired && controller.state != stateToListenFor)
        {
            fired = false;
        }
    }
}
