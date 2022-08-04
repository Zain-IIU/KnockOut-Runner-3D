using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float horizontalSpeed ;
    [SerializeField] private float turningSpeed ;
    private float curSpeed;
    [SerializeField] private float lerpTime;
    [SerializeField] private float maxPositionX = 2.5f;
    private float horizontalValue;
   [SerializeField] private SwerveControl swerve;
   [SerializeField] private Transform playerBody;
   private bool _hasReachedEnd;
   private float _yRot;
   [SerializeField] private float maxRot;
   
   [SerializeField] private float smoothTime = 4f;
   private float moveVal;
   
   private void Start()
    {
        curSpeed = 0;
        EventsManager.OnGameStart += StartPlayer;
        EventsManager.OnSpeedBoosted += BoostPlayerSpeed;
        EventsManager.OnReachedEnd += TweenPlayerToCenter;
        EventsManager.OnFrenzyMode += BoostPlayerSpeed;
    }

    private void OnDisable()
    {
        EventsManager.OnGameStart -= StartPlayer;
        EventsManager.OnSpeedBoosted -= BoostPlayerSpeed;
        EventsManager.OnReachedEnd -= TweenPlayerToCenter;
        EventsManager.OnFrenzyMode -= BoostPlayerSpeed;
    }

    public void HandleMovement()
    {
        GatherInput();
        HandForwardMovement();
        HandleRotation(horizontalValue);
        ClampMovement();
    }

    private void HandForwardMovement()
    {
        moveVal = Mathf.Lerp(moveVal, horizontalValue * horizontalSpeed*Time.deltaTime, Time.deltaTime * smoothTime);
        controller.Move(new Vector3(moveVal, 0, curSpeed * Time.deltaTime));
    }

    private void ClampMovement()
    {
        var localPosition = transform.localPosition;
        localPosition.x = Mathf.Clamp(localPosition.x, -maxPositionX, maxPositionX);
        transform.localPosition = localPosition;
    }
    private void HandleRotation(float value)
    {
        var rotation = playerBody.transform.localRotation;
        value = Mathf.Clamp(value,-1, 1);
        _yRot = maxRot * value;
        var normalRotation = Quaternion.Euler(0, _yRot, 0f);
        playerBody.transform.localRotation = Quaternion.Lerp(rotation, normalRotation, Time.deltaTime * turningSpeed);
    }
    private void GatherInput()
    {
        horizontalValue = swerve.MoveFactorX;
        // if (Input.GetMouseButtonDown(0)) return;
        //  
        // if (Input.GetMouseButton(0))
        //     horizontalValue = Input.GetAxis("Mouse X");
        // else
        //     horizontalValue = 0;
    }

    public void StopThePlayer()
    {
        curSpeed = 0;
        horizontalSpeed = 0;
    }
    
    
    #region Event Callbacks


    private void StartPlayer()
    {
        curSpeed = moveSpeed;
    }

    private void TweenPlayerToCenter()
    {
        maxPositionX = 0;
        _hasReachedEnd = true;
        horizontalSpeed = 0;
    }
    private void BoostPlayerSpeed()
    {
        DoubleTheSpeed();
        StartCoroutine(nameof(NormalSpeed));
    }

    IEnumerator NormalSpeed()
    {
        yield return new WaitForSeconds(2f);
        curSpeed = moveSpeed;
    }
    

    #endregion

    public bool HasPlayerReachedEnd() => _hasReachedEnd;

    public void DoubleTheSpeed()
    {
        curSpeed =20f;
    }

    public void NormalizeSpeed()
    {
        curSpeed = moveSpeed;
    }
}
