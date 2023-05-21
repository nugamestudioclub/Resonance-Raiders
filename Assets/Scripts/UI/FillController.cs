using UnityEngine;
using UnityEngine.UI;

public class FillController : MonoBehaviour
{
    [SerializeField] float ACCURACY = 1f;

    Image image;
    RectTransform recttrans;

    int tweenId = 0;

    private void Awake()
    {
        image = GetComponent<Image>();
        recttrans = GetComponent<RectTransform>();
    }

    public void UpdateDisplay(float amount)
    {
        // if we're maxed out and not resetting the meter, stop
        if (image.fillAmount == 1f && amount >= ACCURACY) return;

        // otherwise if we're done, tween the lean tween tweenleaner
        else if (amount >= ACCURACY)
        {
            tweenId = 
                LeanTween.rotate(recttrans, 10f, 1f)
                .setLoopPingPong()
                .id;
        }

        // otherwise, cancel the tween as we are refilling the meter.
        LeanTween.cancel(gameObject, tweenId);

        image.fillAmount = amount;


    }

    void OnMaxed()
    {

    }
}
