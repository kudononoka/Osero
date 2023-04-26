using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField]string _myCellNum = "";
    public string MyCellNum {get { return _myCellNum;}�@set { _myCellNum = value; } }
    /// <summary>�����̃}�X�̏�ɐ΂��u����Ă��邩�ǂ���</summary>
    [SerializeField]bool _isCisc = false;
    
    private void OnTriggerEnter(Collider other)
    {
        _isCisc = true;
        gameObject.layer = 2;
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
