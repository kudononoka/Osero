using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DiscOnTheBoardData : MonoBehaviour
{
    [SerializeField, Header("棋譜")] Text _kihuText;
    GameManager _gameManager;
    /// <summary>棋譜用のためのList</summary>
    List<List<Pos>> _kihu = new List<List<Pos>>();
    //List<string> dir = new List<string>();
    int _kihuReturnCount = 0;
    SquareController[,] _boardData = new SquareController[10, 10];
    /// <summary>ターンごとに石を置くことができるマスをその都度保存する用</summary>
    List<SquareController> _discOn = new List<SquareController>();
    /// <summary>現在のターンの色   黒だと１、白だと-1と表現する</summary>
    OndiseState _nowTurnDiscColor = OndiseState.None;
    /// <summary>次のターンの準備にとりかかってもいいかどうか</summary>
    bool _isReverse = true;
    /// <summary>マスの上に石がない状態、０になったらゲーム終了</summary>
    int _noneCellCount = 60;
    [SerializeField] GameObject _DiscPrefab;
    bool _isUnRetrun = false;
    [SerializeField] AIControlle _aiControlle = null;
    /// <summary>次のターンの準備にとりかかってもいいかどうか</summary>
    public bool isReverse { get { return _isReverse; }set { _isReverse = value; } }
    public SquareController[,] BoardData => _boardData;
    void Start()
    {
        StartSetUp();
        _gameManager = FindObjectOfType<GameManager>();
    }
    
    void Update()
    {
        if(_isReverse)
        {
            TrunChange();
            NoneCell();
            _isReverse = false;
        }
    }
    public void BoardDataSetUp(int row, int column, SquareController squareControlle)
    {
        _boardData[row, column] = squareControlle;
    }
    /// <summary>ゲームを始める前の準備</summary>
    void StartSetUp()
    {
        //0を空、1を黒石、-1を白石として10 * 10のマスデータに保存
        for (int i = 0; i < _boardData.GetLength(0); i++)
        {
            for (int j = 0; j < _boardData.GetLength(1); j++)
            {
                _boardData[i, j].MyOnDiceState = OndiseState.None;
            }
        }
        //最初置く石として中央となる場所に白-1黒１を設定
        _boardData[4, 4].MyOnDiceState = OndiseState.White;
        _boardData[4, 5].MyOnDiceState = OndiseState.Black;
        _boardData[5, 4].MyOnDiceState = OndiseState.Black;
        _boardData[5, 5].MyOnDiceState = OndiseState.White;
    }

    void TrunChange()
    {
        _nowTurnDiscColor = _gameManager.NowBlackTurn ? OndiseState.Black : OndiseState.White;
    }

    /// <summary>石を置いた後の処理</summary>
    /// <param name="squarePos">石を置いたマスのナンバー</param>
    public void DiscDataIn(Pos squarePos)
    {
        if(_kihuReturnCount > 0 && !_isUnRetrun)
        {
            _kihu.RemoveRange(_kihu.Count - _kihuReturnCount, _kihuReturnCount);
            _kihuReturnCount = 0;
        }
        int row = squarePos.Row;
        //列はアルファベット文字で表現しているのでaが１となるようにInt型へキャスト
        int column = squarePos.Column;
        //置いた場所をキーとしてバリューに現在表面になっている色を代入
        _boardData[row,column].MyOnDiceState = _nowTurnDiscColor;
        _aiControlle.CellStateChange(squarePos);
        //挟んだ石の反転
        DiscReverse(row, column, _boardData[row, column].MyPos);
        //置くことが可能なマスのリセット
        foreach (SquareController cell in _discOn)
        {
            cell.PlaceDiscSquares(false);
            cell.layerChange(2);
        }
        _discOn.Clear();
        //石を置けるマスを1マスなくす
        _noneCellCount--;
        _gameManager.ChangeTrun();
        _kihuText.text += squarePos.Name;
    }
    
    /// <summary>自分の石を置いたとして何個相手の石をひっくり返せるかを確認する関数</summary>
    /// <param name="row">マスの行ナンバー</param>
    /// <param name="column">マスの列ナンバー</param>
    /// <param name="dirR">行の方向</param>
    /// <param name="dirC">列の方向</param>
    /// <returns>ひっくり返す石があった場合その個数をなかった場合０を返します</returns>
    int DisePutCheak(int row, int column, int dirR, int dirC)
    {
        int count = 1;
        OndiseState nowAiteState = _nowTurnDiscColor == OndiseState.Black ? OndiseState.White : OndiseState.Black;
        //相手の石だったら調べる方向の矢印を伸ばしていく
        //矢印の先が空、自分の石だったらwhileを抜ける
        while (_boardData[row + dirR * count, column + dirC * count].MyOnDiceState == nowAiteState)
        {
            count++;
        }
        //矢印の先が自分の石かつ相手の石を１つでも挟んでいたら
        if(_boardData[row + dirR * count , column + dirC * count].MyOnDiceState == _nowTurnDiscColor && count >= 2)
        {
            return count;
        }
        else
        {
            return 0;
        }
        
    }

    /// <summary>石を置ける場所があるかどうか</summary>
    void NoneCell()
    {
        //なかったら終了
        if (_noneCellCount == 0)
        {
            _gameManager.IsWinnerBlack = WinnerJudge();
            _gameManager.GameEnd();
        }
        //あったら
        else
        {
            _discOn.Clear();
            _aiControlle.ScoreAddedValuese.Clear();
            //１マスずつ上に石があるかどうか調べる
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    //そのマスに上に石がなかったら
                    if (_boardData[i, j].MyOnDiceState == OndiseState.None)
                    {
                        SearchPutDiscSquare(_boardData[i, j].MyPos);
                    }
                }
            }
            //自分のターンの時石を置ける所があったら
            if (_discOn.Count != 0)
            {
                foreach (SquareController cell in _discOn)
                {
                    //置ける場所だけRayに反応するようにする
                    cell.layerChange(6);
                    //表示
                    cell.PlaceDiscSquares(true);
                    _aiControlle.ScoreAddCell(cell.MyPos);
                }
            }
            //なかったら
            else
            {
                //相手のターンに変更
                _gameManager.ChangeTrun();
            }
        }
    }

    /// <summary>石が置けるかどうかの処理</summary>
    /// <param name="i">マスの行ナンバー</param>
    /// <param name="j">マスの列ナンバー</param>
    void SearchPutDiscSquare(Pos pos)
    {
        //自分のマスからみて全方位のマスを調べる
        for (int dirI = -1; dirI < 2; dirI++)
        {
            for (int dirR = -1; dirR < 2; dirR++)
            {
                //戻り値が０以外だったら
                if (DisePutCheak(pos.Row, pos.Column, dirI, dirR) != 0)
                {
                    _discOn.Add(_boardData[pos.Row, pos.Column]);
                    //一方方向だけでも石を反転させることがわかればいい
                    return;
                }
            }
        }
    }

    /// <summary>挟んだ石を反転させる処理</summary>
    /// <param name="i">マスの行ナンバー</param>
    /// <param name="j">マスの列ナンバー</param>
    void DiscReverse(int i, int j, Pos squareNum)
    {
        List<Pos> dir = new List<Pos>();
        //反転予定の石の真下のマスのSquareControllerコンポーネントを追加するためのリスト
        List<SquareController> square = new List<SquareController>();
        dir.Add(squareNum);
        //全方位調べる
        for (int dirI = -1; dirI < 2; dirI++)
        {
            for(int dirJ = -1; dirJ < 2;dirJ++)
            {
                //その方向に対して何個相手の石を反転させるか調べる
                int count = DisePutCheak(i, j, dirI, dirJ);
                for (int k = 1; k < count; k++)
                {
                    _boardData[i + dirI * k, j + dirJ * k].MyOnDiceState = _nowTurnDiscColor;
                    dir.Add(_boardData[i + dirI * k, j + dirJ * k].MyPos);
                    //リストに追加
                    square.Add(_boardData[i + dirI * k, j + dirJ * k]);
                }
            }
        }
        foreach(var c in square)
        {
            //反転
            c.AboveDisc(_gameManager.NowBlackTurn);
            _aiControlle.CellStateChange(c.MyPos);
        }
        _aiControlle.ScoreSum();
        _kihu.Add(dir);
    }
    public void UnReturn()
    {
        Debug.Log(_kihu.Count);
        if(_kihuReturnCount <= _kihu.Count && _kihuReturnCount > 0 && _gameManager.NextTurn)
        {
            _isUnRetrun = true;
            int i = _kihu.Count - _kihuReturnCount;
            GameObject disc = Instantiate(_DiscPrefab, _kihu[i][0].TraPos, Quaternion.identity);
            disc.GetComponent<DiscController>().ChangeColor(_gameManager.NowBlackTurn);
            DiscDataIn(_kihu[i][0]);
            _gameManager.NowBlackTurn = !_gameManager.NowBlackTurn;
            _kihu.RemoveAt(_kihu.Count - 1);
            isReverse = true;
            _isUnRetrun = false;
            _kihuReturnCount--;
        }
        
    }
    public void Retrun()
    {
        Debug.Log(_kihu.Count);
        if(_kihuReturnCount < _kihu.Count && _kihu.Count > 0 && _gameManager.NextTurn)
        {
            _kihuReturnCount++;
            int i = _kihu.Count - _kihuReturnCount;
            int row = _kihu[i][0].Row;
            int column = _kihu[i][0].Column;
            _boardData[row, column].MyOnDiceState = OndiseState.None;
            Destroy(_boardData[row, column].DiscOnMe);
            _boardData[row, column].layerChange(6);
            for (var j = 1; j < _kihu[i].Count; j++)
            {
                int numline = _kihu[i][j].Row;
                int numrow = _kihu[i][j].Column;
                _boardData[numline, numrow].MyOnDiceState = _nowTurnDiscColor;
                _boardData[numline, numrow].AboveDisc(_gameManager.NowBlackTurn);
            }
            foreach (SquareController cell in _discOn)
            {
                cell.PlaceDiscSquares(false);
                cell.layerChange(2);
            }
            _discOn.Clear();
            //_gameManager.NowBlackTurn = !_gameManager.NowBlackTurn;

            //isReverse = true;
            _gameManager.ChangeTrun();
        }
        KihuText();
    }

    void KihuText()
    {
        _kihuText.text = "";
        for (var i = 0; i < _kihu.Count - _kihuReturnCount; i++)
        {
            _kihuText.text += $"{_kihu[i][0].Name}";
        }
    }

    /// <summary>勝利判定</summary>
    /// <returns>Trueの時黒の勝利</returns>
    bool WinnerJudge()
    {
        int black = 0;
        int white = 0;
        for (int i = 1; i < _boardData.GetLength(0) - 1; i++)
        {
            for(int j = 1; j < _boardData.GetLength(1) - 1; j++)
            {
                if(_boardData[i, j].MyOnDiceState == OndiseState.Black)
                {
                    black++;
                }
                else
                {
                    white++;
                }
            }
        }

        return black > white ? true : false;
    }


}
