using System.Collections.Generic;
using UnityEngine;

public class DiscOnTheBoardData : MonoBehaviour
{
    int[,] _data = new int[10, 10];
    List<CellController> _discOn = new List<CellController>();  
    GameManager _gameManager;
    int _nowTurnColor;
    int _reverseColor;
    bool _is = true;
    int _noneCellCount = 60;
    public bool isReverse { get { return _is; }set { _is = value; } }
    void Start()
    {
        //0を空、1を黒石、-1を白石としてデータに保存する
        for(int i = 0; i < _data.GetLength(0); i++)
        {
            for(int j = 0;  j < _data.GetLength(1); j++)
            {
                _data[i, j] = 0;
            }
        }
        //最初のSetUp
        _data[4, 4] = -1;
        _data[4, 5] = 1;
        _data[5, 4] = 1;
        _data[5, 5] = -1;
        _gameManager = FindObjectOfType<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_is)
        {
            TrunChange();
            NoneCell();
            _is = false;
        }
    }

    void TrunChange()
    {
        _nowTurnColor = _gameManager.NowBlackTurn ? 1 : -1;

    }

    public void DiscDataIn(string cellNum, bool discColor)
    {
        int line = int.Parse(cellNum[1].ToString());
        int row = ((int)cellNum[0] - 96);
        _data[line,row] = discColor ? 1 : -1;
        DiscReverse(line, row);
        foreach (CellController cell in _discOn)
        {
            cell.Active(false);
            cell.layerChange(2);
        }
        _discOn.Clear();
        _noneCellCount--;
    }
    
    int DisePutCheak(int i, int j, int dirI, int dirR)
    {
        int count = 1;
        while (_data[i + dirI * count, j + dirR * count] == (_nowTurnColor * -1) )
        {
            count++;
        }

        if(_data[i + dirI * count , j + dirR * count] == _nowTurnColor && count >= 2)
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
        Debug.Log(_noneCellCount);
        if (_noneCellCount == 0)
        {
            Debug.Log("終了");
        }
        else
        {
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if (_data[i, j] == 0)
                    {
                        AAA(i, j);
                    }
                }
            }
            if (_discOn.Count != 0)
            {
                foreach (CellController cell in _discOn)
                {
                    cell.layerChange(6);
                    cell.Active(true);
                }
            }
            else
            {
                _gameManager.ChangeTrun();
            }
        }
    }

    void AAA(int i, int j)
    {
        for (int dirI = -1; dirI < 2; dirI++)
        {
            for (int dirR = -1; dirR < 2; dirR++)
            {
                if (DisePutCheak(i, j, dirI, dirR) != 0)
                {
                    _discOn.Add(GameObject.Find($"{(char)(j + 96)}{i}").GetComponent<CellController>());
                    return;
                }
            }
        }
    }

    void DiscReverse(int i, int j)
    {
        List<CellController> cell = new List<CellController>();
        for (int dirI = -1; dirI < 2; dirI++)
        {
            for(int dirJ = -1; dirJ < 2;dirJ++)
            {
                int count = DisePutCheak(i, j, dirI, dirJ);
                for(int k = 1; k < count; k++)
                {
                    _data[i + dirI * k, j + dirJ * k] = _nowTurnColor;
                    string cellNum = $"{(char)((j + dirJ * k) + 96)}{i + dirI * k}";
                    cell.Add(GameObject.Find(cellNum).GetComponent<CellController>());
                }
            }
        }
        foreach(var c in cell)
        {
            c.GetComponent<CellController>().AboveDisc(_gameManager.NowBlackTurn);
        }
    }

}
