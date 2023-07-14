using UnityEngine;
using DG.Tweening;

public class DiscController : MonoBehaviour
{
    bool isConplate = false;
    NextTurnWaitController _nextTurnWaitController;
    private void Awake()
    {
    　　//生成されたら必ずマスの上に位置を変えるようにした
        Vector3 pos = transform.position;
        pos.y = 0.04f;
        transform.position = pos;
        _nextTurnWaitController = FindObjectOfType<NextTurnWaitController>();
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

    /// <summary>石を反転させるアニメーションを指示する関数</summary>
    public void ChangeColorAnim(bool nowBlack)
    {
        _nextTurnWaitController.IsComplateAnimations.Add(true);
        if (nowBlack)
        {
           transform.DORotate(new Vector3(0, 0, 0), 0.3f).SetEase(Ease.Linear);
        }
        else
        {
           transform.DORotate(new Vector3(180, 0, 0), 0.3f).SetEase(Ease.Linear);
        }
        transform.DOMove(new Vector3(transform.position.x, 0.6f, transform.position.z), 0.3f)
                 .SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo)
                 .OnComplete(() => _nextTurnWaitController.IsComplateAnimations.RemoveAt(0));
    }
}
