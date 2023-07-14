using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool _nowBlackTurn = true;
    bool _nextTurn = false;
    /// <summary>Trueの時黒の手です</summary>
    public bool NowBlackTurn {get { return _nowBlackTurn;} set { _nowBlackTurn = value; } }
    public bool NextTurn { get { return _nextTurn; } set { _nextTurn = value; } }
    [SerializeField] DiscOnTheBoardData _boardData;
    // Start is called before the first frame update
    void Start()
    {
        _nextTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>次のターンのための準備とターン変更処理</summary>
    public void ChangeTrun()
    {
        _nowBlackTurn = !_nowBlackTurn;
        _boardData.isReverse = true;
    }
}
