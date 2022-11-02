using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestAnimation : MonoBehaviour
{
    public float xInput;
    public float yInput;
    public float xMousePosition;
    public float yMousePosition;
    public bool isIdle;
    public bool isWalking;
    public bool isRunning;
    public bool isDashing;
    public bool idleLeftDown;
    public bool idleLeftUp;
    public bool idleRightDown;
    public bool idleRightUp;

    private void Update()
    {
        EventHandler.CallMovementEvent(xInput, yInput,
            xMousePosition, yMousePosition,
            isRunning, isWalking, isIdle, isDashing,
            idleLeftDown, idleLeftUp, idleRightDown, idleRightUp);
    }
}
