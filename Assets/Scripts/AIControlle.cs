using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIControlle : MonoBehaviour
{
    /// <summary>0�����P�𔒃X�^�[�g�Ƃ���</summary>
    [SerializeField, Header("��Prefab")] GameObject _DiscPrefab;
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
    List<Pos> _scoreAddedValuese = new List<Pos>();
    public List<Pos> ScoreAddedValuese { get { return _scoreAddedValuese; } set{ _scoreAddedValuese = value; } }
    List<Pos> _cellStateChange = new List<Pos>();
    [SerializeField] DiscOnTheBoardData _boarddata;
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

    public void AddScoreValueMax()
    {
        Pos maxValueCellPos = new Pos();
        int maxValue = int.MinValue;
        foreach(var score in _scoreAddedValuese)
        {
            int value = _score[score.Row - 1, score.Column - 1];
            if(value > maxValue)
            {
                maxValueCellPos = score;
                maxValue = value;
            }
        }
        _scoreAddedValuese.Clear();
        Debug.Log(maxValue);
        SquareController cell = _boarddata.BoardData[maxValueCellPos.Row, maxValueCellPos.Column];
        //�}�X�̏��ɐ΂𐶐�
        GameObject disc = Instantiate(_DiscPrefab, cell.gameObject.transform.position, Quaternion.identity);
        //���������΂̕\�ʂ𒲐�
        disc.GetComponent<DiscController>().ChangeColor(_gameManager.NowBlackTurn);
        //�΂�u�����ꏊ���{�[�h��̃f�[�^�Ǘ����Ă���V�X�e���X�N���v�g�ɓ`����
        _boarddata.DiscDataIn(cell.MyPos);
        
    }

    public void ScoreSum()
    {
        int score = 0;
        for(int i = 1; i < _cellStateChange.Count; i++)
        {
            score += _score[_cellStateChange[i].Row - 1, _cellStateChange[i].Column - 1];
        }

        if (_gameManager.NowBlackTurn)
        {
            _blackScore += _score[_cellStateChange[0].Row - 1, _cellStateChange[0].Column - 1];
            _blackScore += score;
            _whiteScore -= score;
        }
        else
        {
            _whiteScore += _score[_cellStateChange[0].Row - 1, _cellStateChange[0].Column - 1];
            _blackScore -= score;
            _whiteScore += score;
        }
        _cellStateChange.Clear();
    }

    public void CellStateChange(Pos cellPos)
    {
        _cellStateChange.Add(cellPos);
    }
    public void ScoreAddCell(Pos cellPos)
    {
        _scoreAddedValuese.Add(cellPos);  
    }
}
