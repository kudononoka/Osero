using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscController : MonoBehaviour
{
    Animation _anim;
    [SerializeField, Tooltip("自分が今白かどうか Trueの時白")] bool _nowWhite;

    private void Awake()
    {
    　　//生成されたら必ずマスの上に位置を変えるようにした
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
