using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TCharacterCamera : MonoBehaviour
{
    public CinemachineFreeLook fCamera;

    [Range(0.0f, 10.0f)]
    public float xScale;

    [Range(0.0f, 10.0f)]
    public float yScale;

    [Range(0.0f, 1.0f)]
    public float smoothTime;

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
        if (TInputManager.Instance.scroll > 0.0f || TInputManager.Instance.scroll < 0.0f)
        {
            float current = Mathf.Clamp(fCamera.m_YAxis.Value + (-TInputManager.Instance.scroll * Time.deltaTime * yScale), 0.0f, 1.0f);
            fCamera.m_YAxis.Value = Mathf.Lerp(fCamera.m_YAxis.Value, current, smoothTime * 0.3f);
        }
    }
}
