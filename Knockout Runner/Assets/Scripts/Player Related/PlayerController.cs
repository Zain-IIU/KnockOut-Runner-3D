using System.Collections;
using DG.Tweening;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerPhysics playerPhysics;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerSensor playerSensor;
    private static readonly int Throw = Animator.StringToHash("Throw");

    [SerializeField] private BallsManager ballsManager;

    #region Animation Hashing
    private static readonly int GameStart = Animator.StringToHash("GameStart");
    private static readonly int SpeedMultiplier = Animator.StringToHash("speedMultiplier");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Fall = Animator.StringToHash("Fall");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Win = Animator.StringToHash("Win");
    
    #endregion

    private bool _hasFellDown=false;
    

    private void Start()
    {
        EventsManager.OnGameStart += StartRunAnimation;
        EventsManager.OnGameWin += StopThePlayer;
        EventsManager.OnSpeedBoosted += IncreaseAnimationSpeed;
        EventsManager.OnPlayerTakeDamage += TakeDamage;
        EventsManager.OnPlayerHitObstacle += PlayerFallDown;
        EventsManager.OnJumper += AnimateForJump;
    }

    private void OnDisable()
    {
        EventsManager.OnGameStart -= StartRunAnimation;
        EventsManager.OnSpeedBoosted -= IncreaseAnimationSpeed;
        EventsManager.OnPlayerTakeDamage -= TakeDamage;
        EventsManager.OnPlayerHitObstacle -= PlayerFallDown;
        EventsManager.OnJumper -= AnimateForJump;
        EventsManager.OnGameWin -= StopThePlayer;
    }

    private void Update()
    {
        playerPhysics.GravityForPlayer();
       if(_hasFellDown) return;
       
       playerMovement.HandleMovement();
        if (playerSensor.CheckEnemyInRange() && ballsManager.HasBall())
            StartCoroutine(nameof(StartThrowingBalls_Coroutine));
        else
            StopAllCoroutines();
    }

   

    #region Event callbacks

    private void SetLayerWeightZero(float weightValue)
    {
        playerAnimator.SetLayerWeight(1, weightValue);
    }

    //to be called from animation event
    public void ThrowBallFromHand()
    {
        EventsManager.BallThrow_Event();
    }

    private void StartThrowingBall()
    {
        playerAnimator.SetTrigger(Throw);
        SetLayerWeightZero(1f);
    }

    private void StartRunAnimation()
    {
        playerAnimator.SetTrigger(GameStart);
    }

    private void PlayerFallDown()
    {
        playerAnimator.SetTrigger(Fall);
        _hasFellDown = true;
        this.enabled = false;
        EventsManager.GameLose();
    }

    private void StopThePlayer()
    {
        playerMovement.StopThePlayer();
        playerAnimator.SetTrigger(Win);
    }
    private void IncreaseAnimationSpeed()
    {
        playerAnimator.SetFloat(SpeedMultiplier,1.33f);
        var speedMultiplier = 1.33f;
        DOTween.To(() => speedMultiplier, x => speedMultiplier =  x, 1, 2.75f).OnComplete(() =>
        {
            playerAnimator.SetFloat(SpeedMultiplier,speedMultiplier);
        });


    }

    private void TakeDamage()
    {
        playerAnimator.SetTrigger(Hit);
    }
    private IEnumerator StartThrowingBalls_Coroutine()
    {
        while (true)
        {
            StartThrowingBall();
            yield return new WaitForSeconds(.7f);
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private void AnimateForJump()
    {
        playerAnimator.SetTrigger(Jump);
    }

    public bool IsPlayerDown() => _hasFellDown;

    #endregion

}
