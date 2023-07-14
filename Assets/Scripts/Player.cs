using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>0を黒１を白スタートとする</summary>
    [SerializeField, Header("石Prefab")] GameObject _DiscPrefab;
    /// <summary>置くことができるマスのlayer</summary>
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
        if(Input.GetMouseButtonDown(0) && _gamemanager.NextTurn)
        {
            PutDice(_gamemanager.NowBlackTurn);
        }
    }

    void PutDice(bool discColor)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, _rayLength,_layerMask))
        {
            //当たったマスのゲームオブジェクトを取得
            GameObject cell = hit.transform.gameObject;
            //マスのナンバーを参照
            //string cellNumber = cell.GetComponent<SquareController>().MyCellNum;
            Pos pos = cell.GetComponent<SquareController>().MyPos;
            //マスの所に石を生成
            GameObject disc = Instantiate(_DiscPrefab, cell.transform.position, Quaternion.identity);
            //生成した石の表面を調整
            disc.GetComponent<DiscController>().ChangeColor(discColor);
            //石を置いた場所をボード上のデータ管理しているシステムスクリプトに伝える
            _boarddata.DiscDataIn(pos);
            //相手のターン
            _gamemanager.ChangeTrun();
        }
    }
}
