using UnityEngine;

public class CameraPanControl : MonoBehaviour
{
    // the initial height the camera is at during the opening UI
    [SerializeField] float initialYOffset;

    // the height of the camera when it views the game scene
    [SerializeField] float destinationYOffset;

    Camera camcam;

    public enum CameraPositions
    {
        Initial,
        Destination
    }

    void Start()
    {
        camcam = Camera.main;
        camcam.transform.Translate(Vector3.up * initialYOffset);
    }

    public void AnimateCameraPosition(CameraPositions pos)
    {
        LeanTween.moveY(
            camcam.gameObject,
            pos == CameraPositions.Destination ? destinationYOffset : initialYOffset,
            1.25f)
            .setEaseInOutSine();
    }

}
