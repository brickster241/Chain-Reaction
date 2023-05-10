using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace StateMachine.Orb {
    /*
        OrbUnstableStatus class. Handles all the Logic when Orb is in UNSTABLE OrbStatus state.
    */
    public class OrbUnstableStatus : OrbStatusBase
    {
        Tweener position_tweener;
        Tweener rotation_tweener;
        public OrbUnstableStatus(OrbStatusSM orbStatusSM) : base(orbStatusSM) {}
        
        /*
            OnStateEnter Function. Executed when OrbStatus of Orb switches to UNSTABLE state.
            Starts Rotating & Shaking Transform rigorously.
        */
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            rotation_tweener = orbStatusSM.GetOrbController().transform.DORotate(new Vector3(0, 0, 360f), 2f, RotateMode.FastBeyond360).SetRelative(true).SetLoops(-1).SetEase(Ease.Linear);
            position_tweener = orbStatusSM.GetOrbController().transform.DOShakePosition(1f, new Vector3(0.05f, 0.05f, 0f)).SetLoops(-1).SetEase(Ease.Linear);
        }

        /*
            OnStateEcit Function. Executed when OrbStatus of Orb exits from UNSTABLE state.
            Kills the Tweens.
        */
        public override void OnStateExit()
        {
            base.OnStateExit();
            rotation_tweener.Kill();
            position_tweener.Kill();
        }
    }

}
