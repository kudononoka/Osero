using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool _nowBlackTurn = true;
    /// <summary>Trueの時黒の手です</summary>
    public bool NowBlackTurn {get { return _nowBlackTurn;} set { _nowBlackTurn = value; } }
    [SerializeField] DiscOnTheBoardData _boardData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTrun()
    {
        _nowBlackTurn = !_nowBlackTurn;
        _boardData.isReverse = true;
    }
}
