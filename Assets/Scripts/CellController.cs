using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField]string _myCellNum = "";
    public string MyCellNum {get { return _myCellNum;}Å@set { _myCellNum = value; } }
}
