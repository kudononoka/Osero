using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscController : MonoBehaviour
{
    Animation _anim;
    [SerializeField, Tooltip("�������������ǂ��� True�̎���")] bool _nowWhite;

    private void Awake()
    {
    �@�@//�������ꂽ��K���}�X�̏�Ɉʒu��ς���悤�ɂ���
        Vector3 pos = transform.position;
        pos.y = 0.04f;
        transform.position = pos;
    }
    void Start()
    {
        _anim = GetComponent<Animation>();
    }

    void Update()
    {
        
    }

}
