using UnityEngine;
using TMPro;

public class UIGameEndListener : MonoBehaviour
{
    UIMethodWrapper wrapper;
    [SerializeField] TextMeshProUGUI winLoseText;
    [SerializeField] PlayerValues values;

    bool fired;

    private void Awake()
    {
        wrapper = GetComponent<UIMethodWrapper>();
        fired = false;
    }

    public void Update()
    {
        if (!GameStateController.Controller)
        {
            return;
        }

        if (!fired && GameStateController.Controller.gameIsOver)
        {
            fired = true;

            winLoseText.text = (values.playerHealth <= 0) ? "You lose..." : "You win!";

            wrapper.GotoGameEnd();
        }
        else if (!GameStateController.Controller.gameIsOver)
        {
            fired = false;
        }
    }
}
