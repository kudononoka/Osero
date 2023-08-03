using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text _winner;
    [SerializeField] Text _turnColor;
    bool _nowBlackTurn = true;
    bool _nextTurn = false;
    bool _iswinnerBlack = false;
    /// <summary>Trueの時黒の手です</summary>
    public bool NowBlackTurn {get { return _nowBlackTurn;} set { _nowBlackTurn = value; } }
    public bool NextTurn { get { return _nextTurn; } set { _nextTurn = value; } }
    public bool IsWinnerBlack { get { return _iswinnerBlack; } set { _iswinnerBlack = value; } }
    [SerializeField] DiscOnTheBoardData _boardData;
    // Start is called before the first frame update
    void Start()
    {
        _nextTurn = true;
        _winner.text = "";
        _turnColor.text = "黒";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>次のターンのための準備とターン変更処理</summary>
    public void ChangeTrun()
    {
        _nowBlackTurn = !_nowBlackTurn;
        _turnColor.text = _nowBlackTurn ? "黒" : "白";
        _boardData.isReverse = true;
    }

    public void GameEnd()
    {
        _winner.text = _iswinnerBlack ? "黒" : "白";
        _turnColor.text = "";
    }
}
