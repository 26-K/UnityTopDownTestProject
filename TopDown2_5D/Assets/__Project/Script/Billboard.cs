using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField, Title("完全にカメラの方向を向きます（通常はY軸無視）")] private bool unholdY;
    // [SerializeField] private bool invalidParentScale;
    private Transform m_CameraTransform;
    private new Transform transform;
    // private Vector3 defaultLocalScale;

    private void Start()
    {
        Init();
    }

    [Button]
    private void LookInInspector()
    {
        Init();
        LookAtMainCamera();
    }

    private void Init()
    {
        transform = base.transform;
    }

    void LateUpdate() => LookAtMainCamera();

    private void LookAtMainCamera()
    {
        if (m_CameraTransform == null)
        {
            m_CameraTransform = Camera.main.transform;
        }

        if (unholdY)
        {
            this.transform.LookAt(m_CameraTransform);
        }
        else
        {
            this.transform.LookAt(
                transform.position + m_CameraTransform.rotation * Vector3.forward,
                m_CameraTransform.rotation * Vector3.up
            );
        }
    }
}