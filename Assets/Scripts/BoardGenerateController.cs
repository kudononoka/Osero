using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerateController : MonoBehaviour
{
    [SerializeField, Tooltip("1マス分のBoardPrefab")] GameObject _OneCellBoardPrefab;
    [SerializeField, Tooltip("最初のDisc設置のためのPrefab")] GameObject[] _discPrefab;
    float _OneCellBoardHeight = 1;
    float _instatePosX;
    float _instatePosZ;
    void Start()
    {
        BoardGenerate();
    }
    void BoardGenerate()
    {
        _instatePosX = (-_OneCellBoardHeight * 4) + (_OneCellBoardHeight / 2);
        _instatePosZ = (_OneCellBoardHeight * 4) - (_OneCellBoardHeight / 2);
        //行
        for (int i = 1; i < 9; i++)
        {
            //列
            for (char j = 'a'; j < 'i'; j++)
            {
                GameObject oneCell =  Instantiate(_OneCellBoardPrefab, new Vector3(_instatePosX, 0,_instatePosZ), Quaternion.identity);
                if(oneCell.GetComponent<CellController>() != null)
                {
                    oneCell.GetComponent<CellController>().MyCellNum = $"{j}{i}";
                }
                else
                {
                    Debug.Log("１マス用のPrefabObjectにCellControllerをつけてください");
                }
                _instatePosX += _OneCellBoardHeight;

                //最初となる石を設置
                StartDiscON(i, j, oneCell.transform.position);
            }
            _instatePosX = (-_OneCellBoardHeight * 4) + (_OneCellBoardHeight / 2);
            _instatePosZ -= _OneCellBoardHeight;
        }
    }

    void StartDiscON(int line, char row, Vector3 pos)
    {
        if((line == 4  && row == 'd') || (line == 5 && row == 'e'))
        {
            Instantiate(_discPrefab[1], pos, Quaternion.identity);
        }
        else if ((line == 4 && row == 'e') || (line == 5 && row == 'd'))
        {
            Instantiate(_discPrefab[0], pos, Quaternion.identity);
        }
    }
}
