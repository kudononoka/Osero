using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField]string _myCellNum = "";
    public string MyCellNum {get { return _myCellNum;}Å@set { _myCellNum = value; } }
    
    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        gameObject.name = _myCellNum;
    }
    private void OnTriggerEnter(Collider other)
    {

        gameObject.layer = 2;
        transform.GetChild(0).gameObject.SetActive(false);
    } 

    public void Active()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
