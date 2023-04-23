using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerateController : MonoBehaviour
{
    [SerializeField, Tooltip("1�}�X����BoardPrefab")] GameObject _OneCellBoardPrefab;
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
        //�s
        for (int i = 1; i < 9; i++)
        {
            //��
            for (char j = 'a'; j < 'i'; j++)
            {
                GameObject oneCell =  Instantiate(_OneCellBoardPrefab, new Vector3(_instatePosX, 0,_instatePosZ), Quaternion.identity);
                if(oneCell.GetComponent<CellController>() != null)
                {
                    oneCell.GetComponent<CellController>().MyCellNum = $"{j}{i}";
                }
                else
                {
                    Debug.Log("�P�}�X�p��PrefabObject��CellController�����Ă�������");
                }
                _instatePosX += _OneCellBoardHeight;
            }
            _instatePosX = (-_OneCellBoardHeight * 4) + (_OneCellBoardHeight / 2);
            _instatePosZ -= _OneCellBoardHeight;
        }
    }
}
