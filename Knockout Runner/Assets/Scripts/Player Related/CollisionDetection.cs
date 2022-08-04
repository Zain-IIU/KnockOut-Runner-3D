using DG.Tweening;
using UnityEngine;
using MoreMountains.NiceVibrations;



    public class CollisionDetection : MonoBehaviour
    {
        [SerializeField] private BallsManager pillowManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Coins"))
            {
                MMVibrationManager.Haptic(HapticTypes.LightImpact, false,true, this);
                EventsManager.CoinPickUp();
                other.gameObject.SetActive(false);
            }

            if (other.transform.CompareTag("Finish"))
            {
                transform.DOLocalMoveX(0, .05f);
                EventsManager.ReachedEnd();
            }
            if (other.gameObject.TryGetComponent(out Multiplier multiplier))
            {
                if (pillowManager.HasBall())
                {
                    MMVibrationManager.Haptic(HapticTypes.MediumImpact, false,true, this);
                    CoinsManager.Instance.MultiplyCoins(multiplier.GetMultiplierValue());
                    if (multiplier.GetMultiplierValue() != 10) return;
                    EventsManager.GameWin();
                    multiplier.EnableVFX();
                }
                else
                {
                    EventsManager.GameWin();
                    multiplier.EnableVFX();
                }
            }
        }
       

    }
