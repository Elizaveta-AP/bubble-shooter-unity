using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject _bubble, _bubbles, _winWindow;
    [SerializeField] private bool _isRandom;
    [SerializeField] private TMP_Text ScoreText;
    private System.Random _random = new System.Random();
    private Gun _gun;
    private Tilemap _map;
    private List<GameObject> Bubbles = new List<GameObject>();
    private int Score;

    void Start()
    {
        Score = 0;
        _map = FindObjectOfType<Tilemap>();
        if (_isRandom)
        {
            for (int column = -5; column < 6; column++)
            {
                for (int row = 2; row < 12; row++)
                {
                    if ((column == 5) && (row%2 != 0)) continue;
                    Vector3 positionBubble = _map.CellToWorld(new Vector3Int(column, row, 0));
                    GameObject bubble = Instantiate(_bubble, positionBubble, Quaternion.Euler(0, 0, 0));
                    bubble.transform.parent = _bubbles.transform;
                    bubble.GetComponent<Bubble>().ChoiseColor(_random.Next(4));
                }

            }
        }
        _gun = FindObjectOfType<Gun>();
        
        foreach (Transform bubble in _bubbles.transform)
        {
            Bubbles.Add(bubble.gameObject);
        }
    }

    public void SetScore(int bubbleCount)
    {
        Score += 10*(bubbleCount + 1)*bubbleCount/2;
        ScoreText.text = $"Счет: {Score}";
    }

    public void DeleteBubble(GameObject bubble)
    {
        Bubbles.Remove(bubble);

        if (Bubbles.Count == 0)
        {
            _gun.enabled = false;
            _winWindow.SetActive(true);
        }
    }

    public void AddBubble(GameObject bubble)
    {
        Bubbles.Add(bubble);
    }
}
