using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CellController : MonoBehaviour
{
    [SerializeField]string _myCellNum = "";
    public string MyCellNum {get { return _myCellNum;}Å@set { _myCellNum = value; } }
    
    float _rayLength = 10;
    [SerializeField]LayerMask _layerMask;
    DiscController _upDisc;

    private void Start()
    {
        layerChange(2);
        transform.GetChild(0).gameObject.SetActive(false);
        gameObject.name = _myCellNum;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Disc"))
        {
            _upDisc = other.gameObject.GetComponent<DiscController>();
            layerChange(2);
            transform.GetChild(0).gameObject.SetActive(false);
        }
    } 

    public void Active(bool active)
    {
        transform.GetChild(0).gameObject.SetActive(active);
    }

    public void AboveDisc(bool blackColor)
    {
        _upDisc.ChangeColor(blackColor);
    }

    public void layerChange(int layer)
    {
        gameObject.layer = layer;
    }
}
