using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Header("��Prefab")] GameObject _DiscPrefab;
    [SerializeField, Header("�}�X�̃��C���[")] LayerMask _layerMask;
    [SerializeField, Header("Ray�̒���")] float _rayLength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���N���b�N
        if(Input.GetMouseButtonDown(0))
        {
            PutDice();
        }
    }

    void PutDice()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, _rayLength,_layerMask))
        {
            //���������}�X�̃Q�[���I�u�W�F�N�g���擾
            GameObject cell = hit.transform.gameObject;
            //�}�X�̃i���o�[���Q��
            //�}�X�̏��ɐ΂𐶐�
            Instantiate(_DiscPrefab, cell.transform.position, Quaternion.identity);
        }
    }
}
