using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiscController : MonoBehaviour
{
    
    [SerializeField, Tooltip("�������������ǂ��� True�̎���")] bool _nowWhite;

    public bool NowWhite => _nowWhite; 
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

}
