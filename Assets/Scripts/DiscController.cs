using UnityEngine;

public class DiscController : MonoBehaviour
{
    private void Awake()
    {
    　　//生成されたら必ずマスの上に位置を変えるようにした
        Vector3 pos = transform.position;
        pos.y = 0.04f;
        transform.position = pos;
    }
    /// <summary>を石回転させること表面で黒にしたり白にしたりする関数です</summary>
    /// <param name="nowBlack">Trueの時表面が黒になります</param>
    public void ChangeColor(bool nowBlack)
    {
        if(nowBlack)
        {
            //黒
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            //白
            transform.rotation = Quaternion.Euler(180, 0, 0);
        }
    }

    
}
