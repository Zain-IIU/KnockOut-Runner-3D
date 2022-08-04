using DG.Tweening;
using UnityEngine;


public class TrapEventListener : MonoBehaviour
{ 
    [SerializeField] private int trapId;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.Play("TrapPress");
            transform.DOLocalMoveY(0, .14f);
            EventsManager.PlayerEnteredTrap(trapId);
        }
    }
    
}
