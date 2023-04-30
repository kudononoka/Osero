using UnityEngine;

public class SquareController : MonoBehaviour
{
    /// <summary>このゲームオブジェクトのマスのナンバー</summary>
    string _myCellNum = "";
    /// <summary>このゲームオブジェクトのマスのナンバー</summary>
    public string MyCellNum {get { return _myCellNum;}　set { _myCellNum = value; } }
    /// <summary>自分のマスの上にいる石のDiscControllerコンポーネント</summary>
    DiscController _discOnMe;

    private void Start()
    {
        //layerをIgnoreRaycastにし、石を置けなくする
        layerChange(2);
        
        //最初は非表示にする
        PlaceDiscSquares(false);
        //わかりやすいようオブジェクトの名前をマスナンバーする
        gameObject.name = _myCellNum;
    }

    /// <summary>当たり判定で自分のマスの上に石があるかどうかを確認</summary>
    private void OnTriggerEnter(Collider other)
    {
        //上に石があったら
        if (other.gameObject.CompareTag("Disc"))
        {
            _discOnMe = other.gameObject.GetComponent<DiscController>();
            //layerをIgnoreRaycastにしこれ以上石を置けないようにした
            layerChange(2);
            PlaceDiscSquares(false);
        }
    }

    /// <summary>子オブジェクト(小さな球体)を表示非表示することで、石を置けるマスであるかどうかを表現する</summary>
    /// <param name="active">Trueの時表示する</param>
    public void PlaceDiscSquares(bool active)
    {
        // 子オブジェクトを表示非表示
        transform.GetChild(0).gameObject.SetActive(active);
    }

    /// <summary>自分の上の石が持ってるDiscControllerコンポーネントの、石を反転させる関数を呼ぶための関数</summary>
    /// <param name="blackColor">Trueの時表面が黒になる</param>
    public void AboveDisc(bool blackColor)
    {
        _discOnMe.ChangeColor(blackColor);
    }

    /// <summary>このゲームオブジェクトのlayerを変えるための関数</summary>
    /// <param name="layer">layerのナンバー</param>
    public void layerChange(int layer)
    {
        gameObject.layer = layer;
    }
}
