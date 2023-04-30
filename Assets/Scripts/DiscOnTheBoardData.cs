using System.Collections.Generic;
using UnityEngine;

public class DiscOnTheBoardData : MonoBehaviour
{
    GameManager _gameManager;
    /// <summary>ボード上にある石場所を管理する用の配列　
    /// Keyはマスのナンバー　Valueを現在表面になっている色をさす</summary>
    int[,] _boardData = new int[10, 10];
    /// <summary>ターンごとに石を置くことができるマスをその都度保存する用</summary>
    List<SquareController> _discOn = new List<SquareController>();  
    /// <summary>現在のターンの色   黒だと１、白だと-1と表現する</summary>
    int _nowTurnDiscColor;
    /// <summary>次のターンの準備にとりかかってもいいかどうか</summary>
    bool _isReverse = true;
    /// <summary>マスの上に石がない状態、０になったらゲーム終了</summary>
    int _noneCellCount = 60;
    /// <summary>次のターンの準備にとりかかってもいいかどうか</summary>
    public bool isReverse { get { return _isReverse; }set { _isReverse = value; } }
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

    /// <summary>ゲームを始める前の準備</summary>
    void StartSetUp()
    {
        //0を空、1を黒石、-1を白石として10 * 10のマスデータに保存
        for (int i = 0; i < _boardData.GetLength(0); i++)
        {
            for (int j = 0; j < _boardData.GetLength(1); j++)
            {
                _boardData[i, j] = 0;
            }
        }
        //最初置く石として中央となる場所に白-1黒１を設定
        _boardData[4, 4] = -1;
        _boardData[4, 5] = 1;
        _boardData[5, 4] = 1;
        _boardData[5, 5] = -1;
    }

    void TrunChange()
    {
        _nowTurnDiscColor = _gameManager.NowBlackTurn ? 1 : -1;
    }

    /// <summary>石を置いた後の処理</summary>
    /// <param name="squareNum">石を置いたマスのナンバー</param>
    public void DiscDataIn(string squareNum)
    {
        int line = int.Parse(squareNum[1].ToString());
        //列はアルファベット文字で表現しているのでaが１となるようにInt型へキャスト
        int row = ((int)squareNum[0] - 96);
        //置いた場所をキーとしてバリューに現在表面になっている色を代入
        _boardData[line,row] = _nowTurnDiscColor;
        //挟んだ石の反転
        DiscReverse(line, row);
        //置くことが可能なマスのリセット
        foreach (SquareController cell in _discOn)
        {
            cell.PlaceDiscSquares(false);
            cell.layerChange(2);
        }
        _discOn.Clear();
        //石を置けるマスを1マスなくす
        _noneCellCount--;
    }
    
    /// <summary>自分の石を置いたとして何個相手の石をひっくり返せるかを確認する関数</summary>
    /// <param name="i">マスの行ナンバー</param>
    /// <param name="j">マスの列ナンバー</param>
    /// <param name="dirI">行の方向</param>
    /// <param name="dirR">列の方向</param>
    /// <returns>ひっくり返す石があった場合その個数をなかった場合０を返します</returns>
    int DisePutCheak(int i, int j, int dirI, int dirR)
    {
        int count = 1;
        //相手の石だったら調べる方向の矢印を伸ばしていく
        //矢印の先が空、自分の石だったらwhileを抜ける
        while (_boardData[i + dirI * count, j + dirR * count] == (_nowTurnDiscColor * -1) )
        {
            count++;
        }
        //矢印の先が自分の石かつ相手の石を１つでも挟んでいたら
        if(_boardData[i + dirI * count , j + dirR * count] == _nowTurnDiscColor && count >= 2)
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
            Debug.Log("終了");
        }
        //あったら
        else
        {
            //１マスずつ上に石があるかどうか調べる
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    //そのマスに上に石がなかったら
                    if (_boardData[i, j] == 0)
                    {
                        SearchPutDiscSquare(i, j);
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
    void SearchPutDiscSquare(int i, int j)
    {
        //自分のマスからみて全方位のマスを調べる
        for (int dirI = -1; dirI < 2; dirI++)
        {
            for (int dirR = -1; dirR < 2; dirR++)
            {
                //戻り値が０以外だったら
                if (DisePutCheak(i, j, dirI, dirR) != 0)
                {
                    _discOn.Add(GameObject.Find($"{(char)(j + 96)}{i}").GetComponent<SquareController>());
                    //一方方向だけでも石を反転させることがわかればいい
                    return;
                }
            }
        }
    }

    /// <summary>挟んだ石を反転させる処理</summary>
    /// <param name="i">マスの行ナンバー</param>
    /// <param name="j">マスの列ナンバー</param>
    void DiscReverse(int i, int j)
    {
        //反転予定の石の真下のマスのSquareControllerコンポーネントを追加するためのリスト
        List<SquareController> square = new List<SquareController>();
        //全方位調べる
        for (int dirI = -1; dirI < 2; dirI++)
        {
            for(int dirJ = -1; dirJ < 2;dirJ++)
            {
                //その方向に対して何個相手の石を反転させるか調べる
                int count = DisePutCheak(i, j, dirI, dirJ);
                for(int k = 1; k < count; k++)
                {
                    _boardData[i + dirI * k, j + dirJ * k] = _nowTurnDiscColor;
                    string cellNum = $"{(char)((j + dirJ * k) + 96)}{i + dirI * k}";
                    //リストに追加
                    square.Add(GameObject.Find(cellNum).GetComponent<SquareController>());
                }
            }
        }
        foreach(var c in square)
        {
            //反転
            c.AboveDisc(_gameManager.NowBlackTurn);
        }
    }

}
