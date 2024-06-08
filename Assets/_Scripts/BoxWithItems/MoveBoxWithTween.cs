using DG.Tweening;
using UnityEngine;

namespace _Scripts.BoxWithItems
{
    public class MoveBoxWithTween
    {
        public static void MoveBox(Transform transform, Transform endPosition)
        {
            var move = DOTween.Sequence();
            move.Append(transform.DOMoveY(endPosition.position.y, 1).SetEase(Ease.Linear));
        }
    }
}