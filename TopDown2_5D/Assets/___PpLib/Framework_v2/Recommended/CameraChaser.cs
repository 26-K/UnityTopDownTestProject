using UnityEngine;

namespace PPD
{
    public class CameraChaser : PPD_MonoBehaviour
    {
        public Transform localTarget;
        new Transform transform;
        Vector3 firstDistacne;
        Vector3 targetPos;

        private void Awake()
        {
            this.transform = base.transform;
            this.firstDistacne = transform.localPosition - localTarget.localPosition;
        }

        private void LateUpdate()
        {
            var p = localTarget.transform.localPosition;
            targetPos = p + firstDistacne;
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition,targetPos,0.05f);
        }
    }
}

