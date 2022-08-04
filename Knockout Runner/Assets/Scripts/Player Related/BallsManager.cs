using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;


public class BallsManager : MonoBehaviour
{
   public List<Ball> attachedBalls = new List<Ball>();

   [SerializeField] private TextMeshPro pillowsCount;

   public int hitCounter;
   [SerializeField] private int totalCounts;
   
    private void Start()
    {
        hitCounter = 0;
        EventsManager.OnBallThrow += ThrowBall;
        EventsManager.OnPlayerTakeDamage += IncrementHitCounter;
    }

    private void OnDisable()
    {
        EventsManager.OnBallThrow -= ThrowBall;
        EventsManager.OnPlayerTakeDamage -= IncrementHitCounter;
    }

    private void ThrowBall()
    {
        if(attachedBalls.Count==0) return;
        
        attachedBalls[attachedBalls.Count-1].ThrowBall();
        attachedBalls.RemoveAt(attachedBalls.Count-1);
        pillowsCount.text = attachedBalls.Count.ToString();
    }


    public void AddBall(Ball newBall)
    {
        attachedBalls.Add(newBall);
        pillowsCount.text = attachedBalls.Count.ToString();
    }

    public bool HasBall() => attachedBalls.Count > 0;


    private void IncrementHitCounter()
    {
        hitCounter++;
        if (hitCounter >= totalCounts)
        {
            EventsManager.PlayerHitObstacle();
        }
    }
    
}
