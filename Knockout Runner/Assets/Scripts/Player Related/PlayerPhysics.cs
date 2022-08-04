using UnityEngine;

public class PlayerPhysics : PhysicsBaseClass
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float jumpHeight;

    private static readonly int Land = Animator.StringToHash("Land");

    [SerializeField] private AudioSource footstepSource;

    private bool hasStarted;

    protected override void Start()
    {
        base.Start();
        EventsManager.OnJumper += MakePlayerJump;
        EventsManager.OnGameStart += PlayFootSteps;
        EventsManager.OnGameWin += StopFootSteps;
        EventsManager.OnGameLose += StopFootSteps;
    }

    protected override void OnDisable()
    {
        base.Start();
        EventsManager.OnJumper -= MakePlayerJump;
        EventsManager.OnGameStart -= PlayFootSteps;
        EventsManager.OnGameWin -= StopFootSteps;
        EventsManager.OnGameLose -= StopFootSteps;
    }

    public void GravityForPlayer()
    {
        if (!hasStarted) return;

        ApplyGravity();
        playerAnimator.SetBool(Land, IsGrounded);

        footstepSource.enabled = IsGrounded;
    }


    private void MakePlayerJump()
    {
        if (!IsGrounded) return;

        Velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityAmount);
    }

    private void PlayFootSteps()
    {
        hasStarted = true;
    }

    private void StopFootSteps()
    {
        footstepSource.volume = 0;
    }


}
