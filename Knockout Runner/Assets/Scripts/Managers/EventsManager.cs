using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{
    public static bool GameStarted = false;
    
    public static event Action OnGameStart; 
    public static event Action OnGameWin; 
    public static event Action OnReachedEnd; 
    public static event Action OnGameLose; 
    public static event Action OnCoinPickUp; 
    
    
    public static event Action OnBallThrow;
    public static event Action<int> OnBallPicked;

    public static event Action<int> OnPlayerEnteredTrap;
    
    public static event Action OnBallHit;
    public static event Action OnSpeedBoosted;
    public static event Action OnJumper;
    public static event Action OnPlayerTakeDamage;
    public static event Action OnPlayerHitObstacle;
    public static event Action OnFrenzyMode;
    public static event Action OnNextLevel;

   

    public static void BallThrow_Event()
    {
        AudioManager.instance.PlayWithAudioSource("Throw");
        OnBallThrow?.Invoke();
    }

    public static void BallPicked_Event(int id)
    {
        AudioManager.instance.PlayWithAudioSource("PickPillow");
        OnBallPicked?.Invoke(id);
    }

    public  void GameStart()
    {
        OnGameStart?.Invoke();
        GameStarted = true;
    }

    public static void BallHit()
    {
        OnBallHit?.Invoke();
    }

   
    public static void SpeedBoosted()
    {
        OnSpeedBoosted?.Invoke();
    }

    public static void PlayerTakeDamage()
    {
        OnPlayerTakeDamage?.Invoke();
    }

    public static void PlayerHitObstacle()
    {
        OnPlayerHitObstacle?.Invoke();
    }

    public static void PlayerEnteredTrap(int obj)
    {
        OnPlayerEnteredTrap?.Invoke(obj);
    }

    public static void GameWin()
    {
        AudioManager.instance.Play("Win");
        OnGameWin?.Invoke();
    }
    public static void GameLose()
    {
        AudioManager.instance.Play("Lose");
        OnGameLose?.Invoke();
    }

    public static void CoinPickUp()
    {
        AudioManager.instance.PlayWithAudioSource("CoinPickUp");
        OnCoinPickUp?.Invoke();
    }

    public static void Jumper()
    {
        AudioManager.instance.PlayWithAudioSource("Jumper");
        OnJumper?.Invoke();
    }

    public static void ReachedEnd()
    {
        OnReachedEnd?.Invoke();
    }

    public static void FrenzyMode()
    {
        OnFrenzyMode?.Invoke();
    }

    public static void NextLevel()
    {
        OnNextLevel?.Invoke();
    }
}
