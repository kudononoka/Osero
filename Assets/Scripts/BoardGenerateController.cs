using UnityEngine;

public class BoardGenerateController : MonoBehaviour
{
    [SerializeField, Tooltip("1マス分のBoardPrefab")] GameObject _squarePrefab;
    [SerializeField, Tooltip("最初の石設置のためのPrefab")] GameObject _discPrefab;
    [SerializeField] DiscOnTheBoardData _borddata;
    /// <summary>1マス分の大きさ</summary>
    float _squareHeight = 1;
    /// <summary>マスの生成場所X</summary>
    float _spawnPosX;
    /// <summary>マスの生成場所Y</summary>
    float _spawnPosZ;

    private void Awake()
    {
        BoardGenerate();
    }
    
    /// <summary>マスの生成関数</summary>
    void BoardGenerate()
    {
        _spawnPosX = (-_squareHeight * 4) + (_squareHeight / 2);
        _spawnPosZ = (_squareHeight * 4) - (_squareHeight / 2);
        //行
        for (int i = 0; i < 10; i++)
        {
            //列
            for (int j = 0; j < 10; j++)
            {
                SquareController squareData = new SquareController();
                if (i == 0 || j == 9 || i == 9 || j == 0)
                {
                    _borddata.BoardDataSetUp(i, j, squareData);
                }
                else
                {
                    GameObject oneCell = Instantiate(_squarePrefab, new Vector3(_spawnPosX, 0, _spawnPosZ), Quaternion.identity);
                    squareData = oneCell.GetComponent<SquareController>();
                    //最初となる石を設置
                    StartDiscON(i, j, oneCell.transform.position, squareData);
                    if (squareData != null)
                    {
                        //oneCell.GetComponent<SquareController>().MyCellNum = $"{j}{i}";
                        squareData.MyPosSetUp(i, j);
                        _borddata.BoardDataSetUp(i, j, squareData);
                    }
                    else
                    {
                        Debug.Log("１マス用のPrefabObjectにCellControllerをつけてください");
                    }
                    _spawnPosX += _squareHeight;
                    
                }
            }
            _spawnPosX = (-_squareHeight * 4) + (_squareHeight / 2);
            _spawnPosZ -= _squareHeight;
        }
    }

    /// <summary>最初の4つの石を生成する関数</summary>
    /// <param name="line">行</param>
    /// <param name="row">列</param>
    /// <param name="spawnPos">生成場所</param>
    void StartDiscON(int line, int row, Vector3 spawnPos, SquareController controlle)
    {
        GameObject disc;
        if ((line == 4  && row == 4) || (line == 5 && row == 5))
        {
           disc =  Instantiate(_discPrefab, spawnPos, Quaternion.identity);
           controlle.MyOnDiceState = OndiseState.White;
           disc.GetComponent<DiscController>().ChangeColor(false);
        }
        else if ((line == 4 && row == 5) || (line == 5 && row == 4))
        {
            disc = Instantiate(_discPrefab, spawnPos, Quaternion.identity);
            controlle.MyOnDiceState = OndiseState.Black;
            disc.GetComponent<DiscController>().ChangeColor(true);
        }
    }
}
