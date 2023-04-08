using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCharacterTransform : MonoBehaviour
{
    public Camera tCamera;

    [Range(0.0f, 1.0f)]
    public float bodyRotateSmoothTime = 0.2f;

    private float cameraRotateVelocity;

    public Animator at;

    private void Update()
    {
        UpdateBodyRotation();
    }
    private void UpdateBodyRotation()
    {
        if (TInputManager.Instance.movement != Vector2.zero && !TInputManager.Instance.isAround)
        {
            // float targetRotation = Mathf.Atan2(TInputManager.Instance.movement.x, TInputManager.Instance.movement.y) * Mathf.Rad2Deg + tCamera.transform.eulerAngles.y;
            float targetRotation = tCamera.transform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref cameraRotateVelocity, bodyRotateSmoothTime);
        }
    }

    private void OnAnimatorMove()
    {
        Vector3 m = at.deltaPosition;
        m.y = 0.0f;
        transform.Translate(m, Space.Self);
    }
}
