using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private bool _isRandom;
    [SerializeField] private GameObject _bubble;
    private System.Random _random = new System.Random();
    private List<int> ColorsSequence = new List<int>() {0, 2, 1, 0, 3, 2, 1, 3};
    private int _currentColor = 0;
    private Bubble _bubbleController;
    private Vector3 _position;
    private GameObject _currentBubble;
    private Camera _mainCamera;
    public bool CanShoot;

    void Start()
    {
        _position = GetComponent<Transform>().position;
        _mainCamera = Camera.main;
        CanShoot = true;
        CreateBall();
    }

    private void Update() 
    {
        if (Input.GetMouseButtonUp(0) && CanShoot)
        {
            _bubbleController.Move((_mainCamera.ScreenToWorldPoint(Input.mousePosition) - _position));
            CreateBall();
            CanShoot = false;
        }
    }

    private void CreateBall()
    {
        _currentBubble = Instantiate(_bubble, _position, Quaternion.Euler(0, 0, 0));
        _bubbleController = _currentBubble.GetComponent<Bubble>();
        _currentBubble.GetComponent<Collider2D>().isTrigger = true;
        _currentBubble.GetComponent<Rigidbody2D>().isKinematic = false;

        if (_isRandom)
        {
            _bubbleController.ChoiseColor(_random.Next(4));
        }
        else 
        {
            _bubbleController.ChoiseColor(ColorsSequence[_currentColor]);
            _currentColor++;
            
            if (_currentColor == ColorsSequence.Count) _currentColor = 0;
        }
    }
}
