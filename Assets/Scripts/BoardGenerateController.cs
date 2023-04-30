using UnityEngine;

public class BoardGenerateController : MonoBehaviour
{
    [SerializeField, Tooltip("1マス分のBoardPrefab")] GameObject _squarePrefab;
    [SerializeField, Tooltip("最初の石設置のためのPrefab")] GameObject _discPrefab;
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
        for (int i = 1; i < 9; i++)
        {
            //列
            for (char j = 'a'; j < 'i'; j++)
            {
                GameObject oneCell =  Instantiate(_squarePrefab, new Vector3(_spawnPosX, 0,_spawnPosZ), Quaternion.identity);
                if(oneCell.GetComponent<SquareController>() != null)
                {
                    oneCell.GetComponent<SquareController>().MyCellNum = $"{j}{i}";
                }
                else
                {
                    Debug.Log("１マス用のPrefabObjectにCellControllerをつけてください");
                }
                _spawnPosX += _squareHeight;

                //最初となる石を設置
                StartDiscON(i, j, oneCell.transform.position);
            }
            _spawnPosX = (-_squareHeight * 4) + (_squareHeight / 2);
            _spawnPosZ -= _squareHeight;
        }
    }

    /// <summary>最初の4つの石を生成する関数</summary>
    /// <param name="line">行</param>
    /// <param name="row">列</param>
    /// <param name="spawnPos">生成場所</param>
    void StartDiscON(int line, char row, Vector3 spawnPos)
    {
        GameObject disc;
        if ((line == 4  && row == 'd') || (line == 5 && row == 'e'))
        {
           disc =  Instantiate(_discPrefab, spawnPos, Quaternion.identity);
           disc.GetComponent<DiscController>().ChangeColor(false);
        }
        else if ((line == 4 && row == 'e') || (line == 5 && row == 'd'))
        {
            disc = Instantiate(_discPrefab, spawnPos, Quaternion.identity);
            disc.GetComponent<DiscController>().ChangeColor(true);
        }
    }
}
