using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField]string _myCellNum = "";
    public string MyCellNum {get { return _myCellNum;}Å@set { _myCellNum = value; } }
    
    float _rayLength = 10;
    [SerializeField]LayerMask _layerMask;
    DiscController _upDisc;

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        gameObject.name = _myCellNum;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Disc"))
        {
            _upDisc = other.gameObject.GetComponent<DiscController>();
            gameObject.layer = 2;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    } 

    public void Active()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void AboveDisc(bool blackColor)
    {
        _upDisc.ChangeColor(blackColor);
    }
}
