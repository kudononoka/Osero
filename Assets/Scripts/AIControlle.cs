using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControlle : MonoBehaviour
{
    GameManager _gameManager;
    [SerializeField]int _whiteScore = -2;
    [SerializeField]int _blackScore = -2;
    public int WhiteScore { get { return _whiteScore; } set { _whiteScore = value; } }
    public int BlackScore { get { return _blackScore; } set { _blackScore = value; } }
    int[,] _score =
    {
        {30, -12, 0, -1, -1, 0, -12, 30},
        {-12, -15, -3, -3, -3, -3, -15, -12},
        {0, -3, 0, -1, -1, 0, -3, 0},
        {-1, -3, -1, -1, -1, -1, -3, -1},
        {-1, -3, -1, -1, -1, -1, -3, -1},
        {0, -3, 0, -1, -1, 0, -3, 0},
        {-12, -15, -3, -3, -3, -3, -15, -12},
        {30, -12, 0, -1, -1, 0, -12, 30},
    };
    
    List<Pos> cellStateChange = new List<Pos>();
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MaxScore()
    {

    }

    void MinScore()
    {

    }

    public void ScoreSum()
    {
        int score = 0;
        for(int i = 1; i < cellStateChange.Count; i++)
        {
            score += _score[cellStateChange[i].Row - 1, cellStateChange[i].Column - 1];
        }

        if (_gameManager.NowBlackTurn)
        {
            _blackScore += _score[cellStateChange[0].Row - 1, cellStateChange[0].Column - 1];
            _blackScore += score;
            _whiteScore -= score;
        }
        else
        {
            _whiteScore += _score[cellStateChange[0].Row - 1, cellStateChange[0].Column - 1];
            _blackScore -= score;
            _whiteScore += score;
        }
        cellStateChange.Clear();
    }

    public void CellStateChange(Pos cellPos)
    {
        cellStateChange.Add(cellPos);
    }
}
