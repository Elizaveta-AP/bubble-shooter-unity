using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConstantWidth : MonoBehaviour
{
    private Vector2 _defaultScreen = new Vector2(1080, 1920);
    private Camera _mainCamera;
    private float _initialSize;
    private float _aspectRatio;
    private GameObject _bubbles, _topWall, _tilemap, _gun;
    [SerializeField] bool _ignorGameObjects;

    void Awake()
    {
        _mainCamera = GetComponent<Camera>();
        _initialSize = _mainCamera.orthographicSize;
        _aspectRatio = _defaultScreen.x / _defaultScreen.y;

        float constantWidthSize = _initialSize * (_aspectRatio / _mainCamera.aspect);
        _mainCamera.orthographicSize = Mathf.Lerp(constantWidthSize, _initialSize, 0);

        if (!_ignorGameObjects)
        {        
            _bubbles = GameObject.Find("Bubbles");
            _bubbles.transform.position = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.9f, 10));

            _topWall = GameObject.Find("TopWall");
            _topWall.transform.position = _bubbles.transform.position + new Vector3(0, 0.6f, 0);

            _tilemap = GameObject.Find("Grid");
            _tilemap.transform.position = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.9f, 10));

            _gun = GameObject.Find("Gun");
            _gun.transform.position = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, 10)) + new Vector3(0, 0.3f, 0);
        }
    }
}
