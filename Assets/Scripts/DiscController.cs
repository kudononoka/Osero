using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiscController : MonoBehaviour
{
    
    [SerializeField, Tooltip("自分が今白かどうか Trueの時黒")] bool _nowBlack;

    public bool NowBlack { get { return _nowBlack; } set { _nowBlack = value; } } 
    private void Awake()
    {
    　　//生成されたら必ずマスの上に位置を変えるようにした
        Vector3 pos = transform.position;
        pos.y = 0.04f;
        transform.position = pos;
        
    }
    void Start()
    {
        ChangeColor();
    }

    void Update()
    {
        
    }

    void ChangeColor()
    {
        if(_nowBlack)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(180, 0, 0);
        }
    }
}
