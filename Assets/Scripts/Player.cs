using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>0を黒１を白スタートとする</summary>
    [SerializeField, Header("石Prefab")] GameObject[] _DiscPrefab;
    [SerializeField, Header("マスのレイヤー")] LayerMask _layerMask;
    [SerializeField, Header("Rayの長さ")] float _rayLength;
    DiscOnTheBoardData _boarddata;
    GameManager _gamemanager;
    // Start is called before the first frame update
    void Start()
    {
        _boarddata = FindObjectOfType<DiscOnTheBoardData>();
        _gamemanager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //左クリック
        if(Input.GetMouseButtonDown(0))
        {
            //0を黒１を白スタートとする
            int discPrefabIndex = ((_gamemanager.NowBlackTurn) ? 0 : 1);
            PutDice(discPrefabIndex);
        }
    }

    void PutDice(int discNumber)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, _rayLength,_layerMask))
        {
            //当たったマスのゲームオブジェクトを取得
            GameObject cell = hit.transform.gameObject;
            //マスのナンバーを参照
            string cellNumber = cell.GetComponent<CellController>().MyCellNum;
            //マスの所に石を生成
            GameObject disc = Instantiate(_DiscPrefab[discNumber], cell.transform.position, Quaternion.identity);
            bool diceStartColor = disc.GetComponent<DiscController>().NowWhite;
            _boarddata.DiscDataIn(cellNumber, disc);
            _gamemanager.NowBlackTurn = !_gamemanager.NowBlackTurn;
        }
    }
}
