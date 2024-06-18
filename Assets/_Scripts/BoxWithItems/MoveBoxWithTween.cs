using DG.Tweening;
using UnityEngine;

namespace _Scripts.BoxWithItems
{
    public static class MoveBoxWithTween
    {
        public static void MoveBox(Transform transform, Transform endPosition)
        {
            if (transform == null || endPosition == null)
            {
                Debug.LogError($"Transform or endPosition is null.Transform {transform} EndPos {endPosition}.");
                return;
            }

            var move = DOTween.Sequence();
            move.Append(transform.DOMoveY(endPosition.position.y, 2).SetEase(Ease.Linear));
        }
    }
}