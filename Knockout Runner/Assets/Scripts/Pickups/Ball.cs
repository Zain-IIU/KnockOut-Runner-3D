using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float ballThrowForce = 15f;
    [SerializeField] private Rigidbody ballRb;
    [SerializeField] private Collider ballCol;

    [SerializeField] private TrailRenderer trail;
    private Transform parentTransform;
    public bool isThrown;
    public bool isPicked;
    private int id;
    private static int _ballID;
    private void Awake()
    {
        parentTransform = GameObject.FindGameObjectWithTag("playerHand").transform;
        _ballID++;
        id = _ballID;
    }

    private void Start()
    {
        EventsManager.OnBallPicked += AttachBallToHand;
    }

    private void OnDisable()
    {
        EventsManager.OnBallPicked -= AttachBallToHand;
    }

    private void AttachBallToHand(int eventId)
    {
        if (eventId != id || isPicked) return;
        
        isPicked = true;
        transform.parent = parentTransform;
        transform.DOLocalMove(Vector3.zero, 0.1f);
        transform.DOLocalRotate(Vector3.zero, 0f);
        transform.DOScale(Vector3.one, 0);

    }

    public void ThrowBall()
    {
        if(isThrown) return;

        trail.emitting = true;
        isThrown = true;
        transform.parent = null;
        ballRb.isKinematic = false;
        ballRb.AddForce(new Vector3(0f, 0, ballThrowForce), ForceMode.Impulse);
        ballRb.AddTorque(Vector3.right* ballThrowForce*2f,ForceMode.Impulse);
    }
    public void ThrowBall(bool toHitPlayer,Vector3 player)
    {
        if(isThrown) return;
        
        isThrown = true;
        var transform1 = transform;
        transform1.parent = null;
        ballRb.isKinematic = false;

        var direction = (player - transform1.position).normalized;
        
        ballRb.AddForce(direction* ballThrowForce, ForceMode.Impulse);
        ballRb.AddTorque(Vector3.right* ballThrowForce*2f,ForceMode.Impulse);
        StartCoroutine(nameof(EnableTrail));
    }
    IEnumerator EnableTrail()
    {
        yield return new WaitForSeconds(.05f);
        trail.emitting = false;
    }

    public int GetID() => id;

}

