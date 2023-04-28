using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DiscOnTheBoardData : MonoBehaviour
{
    int[,] _data = new int[10, 10];
    List<GameObject> _discOn = new List<GameObject>();  
    GameManager _gameManager;
    int _nowTurnColor;
    int _reverseColor;
    bool _is = true;
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
        _data[5, 5] = -1;
        _data[5, 6] = 1;
        _data[6, 5] = 1;
        _data[6, 6] = -1;
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
        int line = int.Parse(cellNum[1].ToString()) - 1;
        int row = ((int)cellNum[0] - 97);

        _data[line,row] = discColor ? 1 : -1;
        
        Debug.Log($"{ line} { row} : {_data[line, row]}");
    }
    
    void DisePutCheak(int i, int j)
    {
        
        for(int dirI = -1; dirI < 2; dirI++)
        {      
            for(int dirR = -1; dirR < 2; dirR++)
            {
                Debug.Log(_nowTurnColor * -1);
                int count = 1;
                while (_data[i + dirI * count, j + dirR * count] == (_nowTurnColor * -1) )
                {
                    count++;
                    Debug.Log("ある");
                }

                if(_data[i + dirI * count , j + dirR * count] == _nowTurnColor && count >= 2)
                {
                    string cellNum = $"{(char)(j - 1 + 96)}{i - 1}";
                    Debug.Log($"{(char)(j - 1 + 96)}{i - 1}");
                    _discOn.Add(GameObject.Find(cellNum));
                }
            }
        }

        
        
    }

    /// <summary>石を置ける場所があるかどうか</summary>
    void NoneCell()
    {
        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                if(_data[i, j] == 0)
                {
                    
                    DisePutCheak(i, j);
                }
            }
        }
        foreach(GameObject cell in _discOn)
        {
            cell.GetComponent<CellController>().Active();
        }
    }

}
