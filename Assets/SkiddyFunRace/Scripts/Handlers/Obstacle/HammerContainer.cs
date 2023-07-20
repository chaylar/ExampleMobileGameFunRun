using DG.Tweening;
using SkiddyFunRace.Scripts.Handlers.Obstacle.Base;
using UnityEngine;

namespace SkiddyFunRace.Scripts.Handlers.Obstacle
{
    public class HammerContainer : ObstacleBase
    {
        protected override void MoveAction()
        {
            _killHandler.transform.DOLocalRotate(new Vector3(0, 0, 0), speed).SetEase(Ease.OutElastic).OnComplete(GetBackUp);
        }

        protected override void GetBackUp()
        {
            _killHandler.transform.DOLocalRotate(new Vector3(-90, 0, 0), backupSpeed).SetEase(Ease.OutBack).OnComplete(MoveAction);
        }
    }
}