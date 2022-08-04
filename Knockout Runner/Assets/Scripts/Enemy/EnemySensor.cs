using DG.Tweening;
using UnityEngine;
using MoreMountains.NiceVibrations;



public class EnemySensor : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Transform warningSign;

    private bool isFound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ball ball) && other.gameObject.CompareTag("Ball"))
        {
            MMVibrationManager.Haptic(HapticTypes.MediumImpact, false,true, this);
          
            if(isFound) return;
            if(!ball.isThrown) return;
            isFound = true;
            warningSign.DOScale(Vector3.one, .15f).OnComplete(() =>
            {
                warningSign.DOScale(Vector3.zero, .15f);
            });
            enemy.StartChasing(0.75f);
            GetComponent<Collider>().enabled = false;
        }
        if (other.gameObject.CompareTag("Player") )
        {
            MMVibrationManager.Haptic(HapticTypes.MediumImpact, false,true, this);
            enemy.FallDown();
            GetComponent<Collider>().enabled = false;
        }
    }
   
}
