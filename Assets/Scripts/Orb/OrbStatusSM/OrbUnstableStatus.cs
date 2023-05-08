using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrbUnstableStatus : OrbStatusBase
{
    Tweener position_tweener;
    Tweener rotation_tweener;
    public OrbUnstableStatus(OrbStatusSM orbStatusSM) : base(orbStatusSM) {}

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        rotation_tweener = orbStatusSM.GetOrbService().transform.DORotate(new Vector3(0, 0, 360f), 2f, RotateMode.FastBeyond360).SetRelative(true).SetLoops(-1).SetEase(Ease.Linear);
        position_tweener = orbStatusSM.GetOrbService().transform.DOShakePosition(1f, new Vector3(0.05f, 0.05f, 0f)).SetLoops(-1).SetEase(Ease.Linear);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        rotation_tweener.Kill();
        position_tweener.Kill();
    }
}
