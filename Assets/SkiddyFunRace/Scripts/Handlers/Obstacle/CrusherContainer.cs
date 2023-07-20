using DG.Tweening;
using SkiddyFunRace.Scripts.Handlers.Obstacle.Base;
using UnityEngine;

namespace SkiddyFunRace.Scripts.Handlers.Obstacle
{
    public class CrusherContainer : ObstacleBase
    {
        protected override void MoveAction()
        {
            _killHandler.transform.DOLocalMove(new Vector3(0, 0, 0), speed).SetEase(Ease.OutExpo).OnComplete(GetBackUp);
        }

        protected override void GetBackUp()
        {
            _killHandler.transform.DOLocalMove(new Vector3(0, 12f, 0), backupSpeed).SetEase(Ease.OutBack).OnComplete(MoveAction);
        }
    }
}