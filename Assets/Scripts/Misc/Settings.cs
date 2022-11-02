using UnityEngine;

public static class Settings
{
    //Player Movement
    public const float runningSpeed = 10f;
    public const float walkingSpeed = 5f;

    //Player Animation Parameters
    public static int xInput;
    public static int yInput;
    public static int xMousePosition;
    public static int yMousePosition;

    //Player Aniamation
    public static int isRunning;
    public static int isWalking;
    public static int isIdle;
    public static int isDashing;
    public static int idleLeftDown;
    public static int idleLeftUp;
    public static int idleRightDown;
    public static int idleRightUp;

    //Constructor
    static Settings()
    {
        xMousePosition = Animator.StringToHash("xMousePosition");
        yMousePosition = Animator.StringToHash("yMousePosition");
        xInput = Animator.StringToHash("xInput");
        yInput = Animator.StringToHash("yInput");
        isRunning = Animator.StringToHash("isRunning");
        isWalking = Animator.StringToHash("isWalking");
        isDashing = Animator.StringToHash("isDashing");
        idleLeftDown = Animator.StringToHash("idleLeftDown");
        idleLeftUp = Animator.StringToHash("idleLeftUp");
        idleRightDown = Animator.StringToHash("idleRightDown");
        idleRightUp = Animator.StringToHash("idleRightUp");
    }
}
