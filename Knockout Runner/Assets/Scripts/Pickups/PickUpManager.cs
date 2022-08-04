    using System;
    using UnityEngine;


    [SelectionBase]
    public class PickUpManager : MonoBehaviour
    {
        [SerializeField] private PickUpType pickUpType;
        [SerializeField] private Animator jumperAnimator;
        private static readonly int PlayerPassed = Animator.StringToHash("PlayerPassed");


        private void TakeAction()
        {
            switch (pickUpType)
            {
                case PickUpType.SpeedBooster:
                    EventsManager.SpeedBoosted();
                    break;
                case PickUpType.Jumper:
                    jumperAnimator.SetTrigger(PlayerPassed);
                    EventsManager.Jumper();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                TakeAction();
            }
        }
    }
