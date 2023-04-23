using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Header("石Prefab")] GameObject _DiscPrefab;
    [SerializeField, Header("マスのレイヤー")] LayerMask _layerMask;
    [SerializeField, Header("Rayの長さ")] float _rayLength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //左クリック
        if(Input.GetMouseButtonDown(0))
        {
            PutDice();
        }
    }

    void PutDice()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, _rayLength,_layerMask))
        {
            //当たったマスのゲームオブジェクトを取得
            GameObject cell = hit.transform.gameObject;
            //マスのナンバーを参照
            //マスの所に石を生成
            Instantiate(_DiscPrefab, cell.transform.position, Quaternion.identity);
        }
    }
}
