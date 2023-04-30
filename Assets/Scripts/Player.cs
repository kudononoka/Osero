using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>0�����P�𔒃X�^�[�g�Ƃ���</summary>
    [SerializeField, Header("��Prefab")] GameObject _DiscPrefab;
    [SerializeField, Header("�}�X�̃��C���[")] LayerMask _layerMask;
    [SerializeField, Header("Ray�̒���")] float _rayLength;
    DiscOnTheBoardData _boarddata;
    GameManager _gamemanager;
    // Start is called before the first frame update
    void Start()
    {
        _boarddata = FindObjectOfType<DiscOnTheBoardData>();
        _gamemanager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //���N���b�N
        if(Input.GetMouseButtonDown(0))
        {
            PutDice(_gamemanager.NowBlackTurn);
        }
    }

    void PutDice(bool discColor)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, _rayLength,_layerMask))
        {
            //���������}�X�̃Q�[���I�u�W�F�N�g���擾
            GameObject cell = hit.transform.gameObject;
            //�}�X�̃i���o�[���Q��
            string cellNumber = cell.GetComponent<CellController>().MyCellNum;
            //�}�X�̏��ɐ΂𐶐�
            GameObject disc = Instantiate(_DiscPrefab, cell.transform.position, Quaternion.identity);
            disc.GetComponent<DiscController>().ChangeColor(discColor);
            _boarddata.DiscDataIn(cellNumber, discColor);
            _gamemanager.ChangeTrun();
        }
    }
}
