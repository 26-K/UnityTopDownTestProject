using DG.Tweening;
using UnityEngine;

/// <summary>
/// Y座標は固定
/// ターゲットを中央に捉え続ける
/// 左右のステージの端は映らないように
/// 回転はしない
/// </summary>

namespace PPD
{
    public class CameraMover : SingletonMonoBehaviour<CameraMover>
    {
        public Tweener ShakeCamera() => Camera.main.DOShakePosition(0.25f, 0.2f, 3);
        [SerializeField]
        private Camera camera = null;

        [SerializeField]
        public Transform target = null;

        [SerializeField]
        private Vector3 offset = Vector3.zero;

        Vector3 TargetPos => this.target.position + this.offset;
        public Vector3 targetDisplayPos;
        public float cameraAngleChangePosZ = -3;
        float shakeCoolTime = 0.0f;
        public Transform originalRotate;

        private void Start()
        {
            originalRotate = this.camera.transform;
        }

        public void CritShakeCamera()
        {
            if (shakeCoolTime <= 0.0f)
            {
                shakeCoolTime = 1.5f;
                ShakeCamera();
            }
        }

        // Update is called once per frame
        private void Update()
        {
            shakeCoolTime -= Time.deltaTime;
            if (((UnityEngine.Object)this.target) == null)
            {
                return;
            }

            // カメラの中央にターゲットを捉えられる座標を求める
            var centeringPosition = this.CalculateCenteringPosition();
            // 計算に利用するため、先に1回設定する
            //カメラの滑らかな移動のためにコメントアウト
            // this.camera.transform.position = centeringPosition;

            //カメラの滑らかな移動
            targetDisplayPos = centeringPosition;
            targetDisplayPos.x = Mathf.Lerp(this.camera.transform.position.x, centeringPosition.x, 0.05f);
            targetDisplayPos.y = Mathf.Lerp(this.camera.transform.position.y, centeringPosition.y, 0.05f);
            targetDisplayPos.z = Mathf.Lerp(this.camera.transform.position.z, centeringPosition.z, 0.05f);
            this.camera.transform.position = targetDisplayPos;
            // if (cameraAngleChangePosZ <= this.camera.transform.position.z)
            // {
            //     this.camera.transform.SetLocalRotateX(originalRotate.transform.localEulerAngles.x + (cameraAngleChangePosZ - this.camera.transform.position.z) / 3); //雑実装
            // }
            // else
            // {
            //     this.camera.transform.SetLocalRotateX(originalRotate.transform.localEulerAngles.x); //雑実装
            // }
        }


        private Vector3 CalculateCenteringPosition()
        {
            var nowPosition = this.camera.transform.position;
            var nowRotation = this.camera.transform.rotation;

            var targetPosition = this.TargetPos;

            float height = Mathf.Abs(targetPosition.y - nowPosition.y);
            var hypotenuseNormalizedVecotor = (nowRotation * Vector3.forward).normalized;
            float angle = Vector3.Angle(hypotenuseNormalizedVecotor, Vector3.up);
            float cos = Mathf.Cos(angle * Mathf.Deg2Rad);
            var resultVecotor = hypotenuseNormalizedVecotor * (height / cos);

            return targetPosition + resultVecotor;
        }

        protected override void UnityAwake()
        {
        }
    }
}