using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTPSAnimator : MonoBehaviour
{
    public enum TDirection
    {
        Idle,
        W,
        S,
        A,
        D,
        WA,
        WD,
        SA,
        SD,
    }

    public TTPSCharacter tps;

    [HideInInspector]
    public TDirection currentDirection = TDirection.Idle;

    private void Update()
    {
        UpdateDirection();
        UpdateAnimatorValues();
    }

    public void UpdateDirection()
    {
        if (TInputManager.Instance.movement == Vector2.zero)
        {
            currentDirection = TDirection.Idle;
        }
        else if (TInputManager.Instance.movement.y > 0.85f)
        {
            currentDirection = TDirection.W;
        }
        else if (TInputManager.Instance.movement.y < -0.85f)
        {
            currentDirection = TDirection.S;
        }
        else if (TInputManager.Instance.movement.x < -0.85f)
        {
            currentDirection = TDirection.A;
        }
        else if (TInputManager.Instance.movement.x > 0.85f)
        {
            currentDirection = TDirection.D;
        }
        else if (TInputManager.Instance.movement.y > 0.55f && TInputManager.Instance.movement.x < -0.55f)
        {
            currentDirection = TDirection.WA;
        }
        else if (TInputManager.Instance.movement.y > 0.55f && TInputManager.Instance.movement.x > 0.55f)
        {
            currentDirection = TDirection.WD;
        }
        else if (TInputManager.Instance.movement.y < -0.55f && TInputManager.Instance.movement.x < -0.55f)
        {
            currentDirection = TDirection.SA;
        }
        else if (TInputManager.Instance.movement.y < -0.55f && TInputManager.Instance.movement.x > 0.55f)
        {
            currentDirection = TDirection.SD;
        }
        else
        {
            currentDirection = TDirection.Idle;
        }
    }

    private void UpdateAnimatorValues()
    {
        UpdateFloat();
        UpdateInt();
        UpdateBool();
    }

    private void UpdateFloat()
    {
        tps.at.SetFloat("X", TInputManager.Instance.movement.x, 0.1f, Time.deltaTime);
        tps.at.SetFloat("Y", TInputManager.Instance.movement.y, 0.1f, Time.deltaTime);
    }

    public void UpdateInt()
    {
        tps.at.SetInteger("Direction", (int)currentDirection);
    }

    public void UpdateBool()
    {

    }
}
