using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace StateMachine.Orb {
    public class OrbStableStatus : OrbStatusBase
    {

        Tweener position_tweener;
        Tweener rotation_tweener;
        public OrbStableStatus(OrbStatusSM orbStatusSM) : base(orbStatusSM) {}

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            rotation_tweener = orbStatusSM.GetOrbController().transform.DORotate(new Vector3(0, 0, 360f), 10f, RotateMode.FastBeyond360).SetRelative(true).SetLoops(-1).SetEase(Ease.Linear);
            position_tweener = orbStatusSM.GetOrbController().transform.DOShakePosition(1f, new Vector3(0.01f, 0.01f, 0f)).SetLoops(-1).SetEase(Ease.Linear);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            rotation_tweener.Kill();
            position_tweener.Kill();
        }
    }

}
