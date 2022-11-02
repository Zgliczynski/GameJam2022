using System;
using System.Collections.Generic;

public delegate void MovementDelegate(float xInput, float yInput,
    float xMousePosition, float yMousePosition,
        bool isRunning, bool isWalking, bool isIdle, bool isDashing,
        bool idleLeftDown, bool idleLeftUp, bool idleRightDown, bool idleRightUp);

public static class EventHandler
{
    public static event MovementDelegate MovementEvent;

    //Call Movement Event
    public static void CallMovementEvent(float xInput, float yInput,
        float xMousePosition, float yMousePosition,
        bool isRunning, bool isWalking, bool isIdle, bool isDashing,
        bool idleLeftDown, bool idleLeftUp, bool idleRightDown, bool idleRightUp)
    {
        if (MovementEvent != null)
            MovementEvent(xInput, yInput,
                xMousePosition, yMousePosition,
                isRunning, isWalking, isIdle, isDashing,
                idleLeftDown, idleLeftUp, idleRightDown, idleRightUp);
    }
        
}
