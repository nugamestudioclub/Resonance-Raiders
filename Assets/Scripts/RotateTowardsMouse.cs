using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsMouse : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private Vector3 _screenPosition = Vector3.zero;

    private Vector3 _worldPosition = Vector3.zero;

    Plane plane = new Plane(Vector3.down, 0);

    [SerializeField] private Transform _cursorTransform;

    [SerializeField] private Camera _mainCam;

    //[SerializeField] private LayerMask _layersToHit;

    private void Start()
    {
        plane = new Plane(Vector3.down, _playerTransform.position.y);
    }

    // Update is called once per frame
    void Update()
    {

        _screenPosition = Input.mousePosition;
        
        Ray ray = _mainCam.ScreenPointToRay(_screenPosition);

        if(plane.Raycast(ray, out float distance))
        {
            _worldPosition = ray.GetPoint(distance);
        }

/*        if(Physics.Raycast(ray, out RaycastHit hitData, 100, _layersToHit))
        {
            _worldPosition = hitData.point;
        }
*/
        _cursorTransform.position = _worldPosition;

        _playerTransform.LookAt(_cursorTransform.position); 
        
    }


}
