using System;
using UnityEngine;

  public class CameraManager : MonoBehaviour
    {
        [SerializeField] private GameObject followCam;
        [SerializeField] private GameObject startCam;
        [SerializeField] private GameObject winCam;
        [SerializeField] private GameObject loseCam;
        [SerializeField] private GameObject endCam;

        private void Start()
        {
            EventsManager.OnGameStart += EnableFollowCam;
            EventsManager.OnPlayerHitObstacle += EnableLoseCam;
            EventsManager.OnGameWin += EnableWinCam;
            EventsManager.OnReachedEnd += EnableEndCam;
        }


        #region Event Call Backs

        private void EnableFollowCam()
        {
            followCam.SetActive(true);
            startCam.SetActive(false);
        }

        private void EnableLoseCam()
        {
            loseCam.SetActive(true);
            followCam.SetActive(false);
        }

        private void EnableWinCam()
        {
            winCam.SetActive(true);
            followCam.SetActive(false);
        }

        private void EnableEndCam()
        {
            endCam.SetActive(true);
            followCam.SetActive(false);
        }
        
        #endregion

        private void OnDisable()
        {
            EventsManager.OnGameStart -= EnableFollowCam;
            EventsManager.OnPlayerHitObstacle -= EnableLoseCam;
            EventsManager.OnGameWin -= EnableWinCam;
            EventsManager.OnReachedEnd -= EnableEndCam;
        }
    }
