using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTurnWaitController : MonoBehaviour
{
    List<bool> _isComplateAnimations = new List<bool>();
    GameManager _gameManager;
    public List<bool> IsComplateAnimations { get { return _isComplateAnimations; } set { _isComplateAnimations = value; } }
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isComplateAnimations.Count == 0)
        {
            _gameManager.NextTurn = true;
        }
        else
        {
            _gameManager.NextTurn = false;
        }
    }
}
