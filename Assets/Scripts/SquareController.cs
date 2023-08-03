using UnityEngine;
using static SquareController;

public class SquareController : MonoBehaviour
{
    /// <summary>自分のマスの上にいる石のDiscControllerコンポーネント</summary>
    [SerializeField]GameObject _discOnMe;
    public GameObject DiscOnMe { get { return _discOnMe; } set { _discOnMe = value; } }
    /// <summary>自分のマスの上の石の状態</summary>
    OndiseState _ondiceState = OndiseState.None;
    /// <summary>自分のマスの上の石の状態</summary>
    public OndiseState MyOnDiceState { get { return _ondiceState; } set { _ondiceState = value; } }

    Pos _pos = new Pos();

    public Pos MyPos => _pos;
    private void Start()
    {
        //layerをIgnoreRaycastにし、石を置けなくする
        layerChange(2);
        
        //最初は非表示にする
        PlaceDiscSquares(false);
    }

    public void MyPosSetUp(int row, int column)
    {
        _pos = new Pos(row, column, transform.position);
        gameObject.name = $"{row}{column}";
    }

    /// <summary>当たり判定で自分のマスの上に石があるかどうかを確認</summary>
    private void OnTriggerEnter(Collider other)
    {
        //上に石があったら
        if (other.gameObject.CompareTag("Disc"))
        {
            _discOnMe = other.gameObject;
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
        _discOnMe.GetComponent<DiscController>().ChangeColorAnim(blackColor);
    }

    /// <summary>このゲームオブジェクトのlayerを変えるための関数</summary>
    /// <param name="layer">layerのナンバー</param>
    public void layerChange(int layer)
    {
        gameObject.layer = layer;
    }

    
}

public enum OndiseState
{
    /// <summary>何もない</summary>
    None,
    /// <summary>黒石</summary>
    Black,
    /// <summary>白石</summary>
    White,
}

public struct Pos
{
    int row;
    int column;
    string name;
    Vector3 traPos;
    /// <summary>行列番号</summary>
    public int Row => row;
    /// <summary>列番号</summary>
    public int Column => column;
    public string Name => name;
    public Vector3 TraPos => traPos;
    public Pos(int row, int column, Vector3 traPos)
    {
        this.row = row;
        this.column = column;
        this.name = ((char)(row + 96)).ToString() + column.ToString();
        this.traPos = traPos;
    }
}
