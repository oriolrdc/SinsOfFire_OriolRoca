using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector2 _maxPosition;
    [SerializeField] private Vector2 _minPosition;
    [SerializeField] private float _interpolationRatio = 0.5f;

    void Awake()
    {
        _playerTransform = GameObject.Find("personaje").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if(_playerTransform == null)
        {
            return;
        }

        Vector3 desiredPosition = _playerTransform.position + _offset;

        float clampX = Mathf.Clamp(desiredPosition.x, _minPosition.x, _maxPosition.x);
        float clampY = Mathf.Clamp(desiredPosition.y, _minPosition.y, _maxPosition.y);
        Vector3 clampedPosition = new Vector3(clampX, clampY, desiredPosition.z);

        Vector3 lerpedPosition = Vector3.Lerp(transform.position, clampedPosition, _interpolationRatio);

        transform.position = lerpedPosition;
    }
}
