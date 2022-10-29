using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class Bubble : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private Transform[] CheckNearBubbles;
    [SerializeField] private Sprite[] Colors = new Sprite[4];
    [SerializeField] private Material[] Materials = new Material[4];
    [SerializeField] private ParticleSystem _particleSystem;
    private Transform _transform;
    private Collider2D _collider;
    private Rigidbody2D _rb;
    private Tilemap _map;
    private Gun _gun;
    private LevelController _levelController;
    private Sprite _mySprite;
    private AudioController _audioController;
    static List<GameObject> KillList = new List<GameObject>();

    private void Start()
    {
        _audioController = FindObjectOfType<AudioController>();
        _gun = FindObjectOfType<Gun>();
        _levelController = FindObjectOfType<LevelController>();
        _transform = GetComponent<Transform>();
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _mySprite = GetComponent<SpriteRenderer>().sprite;
        _map = FindObjectOfType<Tilemap>();
    }
    
    public void ChoiseColor(int indexOfColor)
    {
        _mySprite = Colors[indexOfColor];
        GetComponent<SpriteRenderer>().sprite = _mySprite;
    }

    private void Update() 
    {
        if (_transform.position.y < -6)
        {
            _gun.CanShoot = true;
            _levelController.DeleteBubble(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void Move(Vector2 direct)
    {
        _levelController.AddBubble(this.gameObject);
        _collider.isTrigger = false;
        _rb.AddForce(direct.normalized * _speed, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if ((other.gameObject.tag == "Bubble") && (!_rb.isKinematic))
        {
            _rb.velocity = new Vector2(0, 0);
            Vector3 newPosition = _map.CellToWorld(_map.WorldToCell(_transform.position));
            _transform.position = newPosition;
            _rb.isKinematic = true;
            
            Invoke("ChoiseKillBubble", Time.deltaTime);
            StartCoroutine(KillBubbles());
        }
    }

    public void ChoiseKillBubble()
    {
        KillList.Add(this.gameObject);

        foreach (Transform checkingBubble in CheckNearBubbles)
        {
            RaycastHit2D hit = Physics2D.Raycast(checkingBubble.position, checkingBubble.right, 0.2f);

            if(hit.collider != null) 
            {
                GameObject nearBubble = hit.collider.gameObject;

                if ((nearBubble.GetComponent<SpriteRenderer>().sprite == _mySprite) && (!KillList.Contains(nearBubble)))
                {
                    nearBubble.GetComponent<Bubble>().ChoiseKillBubble();
                }
            }
        }
    }

    IEnumerator KillBubbles()
    {
        yield return new WaitForSeconds(Time.deltaTime*2);
        
        if (KillList.Count >=3)
        {
            _levelController.SetScore(KillList.Count);
            
            foreach (GameObject bubble in KillList)
            {
                bubble.GetComponent<Bubble>().Death();
            }
        }
        _gun.CanShoot = true;
        KillList.Clear();
    }

    public void Death()
    {
        ParticleSystem newParticles = Instantiate(_particleSystem, _transform.position, Quaternion.Euler(0, 0, 0));
        newParticles.GetComponent<Renderer>().material = Materials[Array.IndexOf(Colors, _mySprite)];

        _levelController.DeleteBubble(this.gameObject);

        _audioController.PlayPop();

        Destroy(this.gameObject);
    }
}