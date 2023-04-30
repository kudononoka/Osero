using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiscController : MonoBehaviour
{
    
    [SerializeField, Tooltip("�������������ǂ��� True�̎���")] bool _nowBlack;
 
    public bool NowBlack { get { return _nowBlack; } set { _nowBlack = value; } } 
    private void Awake()
    {
    �@�@//�������ꂽ��K���}�X�̏�Ɉʒu��ς���悤�ɂ���
        Vector3 pos = transform.position;
        pos.y = 0.04f;
        transform.position = pos;
        
    }
    void Start()
    {
       
    }

    void Update()
    {
        
    }

    public void ChangeColor(bool nowBlack)
    {
        if(nowBlack)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            NowBlack = true;
        }
        else
        {
            transform.rotation = Quaternion.Euler(180, 0, 0);
            NowBlack = false;
        }
    }

    
}
