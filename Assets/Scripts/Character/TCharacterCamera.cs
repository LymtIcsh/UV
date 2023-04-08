using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TCharacterCamera : MonoBehaviour
{
    public CinemachineFreeLook fCamera;

    [Range(0.0f, 10.0f)]
    public float xScale;

    private void Update()
    {
        TransformCameraRotation();
    }

    private void TransformCameraRotation()
    {
        if (TInputManager.Instance.rotate != Vector2.zero)
        {
            fCamera.m_XAxis.Value += TInputManager.Instance.rotate.x * Time.deltaTime * xScale;
        }
    }
}
