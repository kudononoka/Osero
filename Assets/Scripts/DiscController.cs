using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiscController : MonoBehaviour
{
    
    [SerializeField, Tooltip("自分が今白かどうか Trueの時白")] bool _nowWhite;

    public bool NowWhite => _nowWhite; 
    private void Awake()
    {
    　　//生成されたら必ずマスの上に位置を変えるようにした
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
