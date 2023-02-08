using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TTPSCharacter : MonoBehaviour
{
    public Camera tCamera;

    public CinemachineFreeLook tCameraController;

    [Range(0.0f, 20.0f)]
    public float tCameraRotateSpeed = 10.0f;

    [Range(0.0f, 1.0f)]
    public float bodyRotateSmoothTime = 0.2f;

    private float cameraRotateVelocity;

    public Animator at;

    public CharacterController cc;

    public float gravity = -9.8f;

    [HideInInspector]
    public float verticalVelocity;

    [Range(1.0f, 10.0f)]
    public float jumpVelocity = 4.0f;

    private void Update()
    {
        UpdateBodyRotation();
        UpdateCameraControllerValues();
        CalculateGravity();
        Jump();
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

    private void UpdateCameraControllerValues()
    {
        if (TInputManager.Instance.rotate != Vector2.zero)
        {
            tCameraController.m_XAxis.Value += TInputManager.Instance.rotate.x * Time.deltaTime * tCameraRotateSpeed;
        }
    }

    private void CalculateGravity()
    {
        if (cc.isGrounded)
        {
            verticalVelocity = 0.0f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (TInputManager.Instance.isJump && cc.isGrounded)
        {
            verticalVelocity = jumpVelocity;
        }
    }

    private void OnAnimatorMove()
    {
        Vector3 m = at.deltaPosition;
        m.y = verticalVelocity * Time.deltaTime;
        cc.Move(m);
    }

}
