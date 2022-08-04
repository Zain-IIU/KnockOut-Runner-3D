using System.Collections;
using UnityEngine;

public class PlayerFrenzyMode : MonoBehaviour
{
        [SerializeField] private PlayerMovement playerMovement;

        [SerializeField] private GameObject frenzyFloorTrail;
        [SerializeField] private GameObject normalFloorTrail;

        
        public bool inFrenzyMode;
        private void Start()
        {
                EventsManager.OnFrenzyMode += BoostPlayerSpeed;
                EventsManager.OnSpeedBoosted += EnableBoostedVFX;
        }

        private void OnDisable()
        {
                EventsManager.OnFrenzyMode -= BoostPlayerSpeed;
                EventsManager.OnSpeedBoosted -= EnableBoostedVFX;
        }


        #region Event Callbacks

        private void BoostPlayerSpeed()
        {
                AudioManager.instance.PlayWithAudioSource("Booster");
                normalFloorTrail.SetActive(false);
                frenzyFloorTrail.SetActive(true);
                playerMovement.DoubleTheSpeed();
                inFrenzyMode = true;
                StartCoroutine(nameof(NormalizePlayer));
        }

        private void EnableBoostedVFX()
        { 
                normalFloorTrail.SetActive(true);
                StartCoroutine(nameof(NormalMovement));
        }
        

        #endregion


        IEnumerator NormalizePlayer()
        {
                yield return new WaitForSeconds(3f);
                playerMovement.NormalizeSpeed();
                frenzyFloorTrail.SetActive(false);
                inFrenzyMode = false;
        }
        IEnumerator NormalMovement()
        {
                yield return new WaitForSeconds(2f);
                normalFloorTrail.SetActive(false);
        }
        
}
