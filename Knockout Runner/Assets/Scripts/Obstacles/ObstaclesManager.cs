using System;
using DG.Tweening;
using UnityEngine;

[SelectionBase]
public class ObstaclesManager : MonoBehaviour
{
    [SerializeField] private ObstacleType obstacleType;
    [SerializeField] private Transform trap;

    [SerializeField] private int trapID;


    [SerializeField] private float xTweenValue;
    [SerializeField] private float tweenTime;
    [SerializeField] private float destroyingForce;

    private void Start()
    {
        EventsManager.OnPlayerEnteredTrap += TweenTrap;
    }

    private void OnDisable()
    {
        EventsManager.OnPlayerEnteredTrap -= TweenTrap;
    }

    private void TakeAction(Collider other)
    {
        switch (obstacleType)
        {
            case ObstacleType.SimpleObstacle:
                EventsManager.PlayerHitObstacle(); 
                other.enabled=false;
                break;
            case ObstacleType.Trap:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void TweenTrap(int id)
    {
        if (id != trapID) return;       
        trap.DOLocalMoveX(xTweenValue,tweenTime);
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerFrenzyMode frenzyMode) && obstacleType==ObstacleType.SimpleObstacle)
        {
            if (frenzyMode && frenzyMode.inFrenzyMode)
            {
                GetComponent<Collider>().enabled = false;
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().AddForce(new Vector3(0,.75f,1)* destroyingForce,ForceMode.Impulse);
                GetComponent<Rigidbody>().AddTorque((Vector3.up + Vector3.forward)* destroyingForce,ForceMode.Impulse);
            }
            else
                TakeAction(other);  

        }

        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.EnableRagDoll();
        }
        
    }
}
