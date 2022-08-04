using System;
using UnityEngine;


    public class Multiplier : MonoBehaviour
    {
        [SerializeField] private int multiplyValue;
        [SerializeField] private GameObject confettiVFX;

        private void Start()
        {
            confettiVFX.SetActive(false);
        }

        public int GetMultiplierValue() => multiplyValue;
        public void SetMultiplier(int val) => multiplyValue = val;

        public void EnableVFX() => confettiVFX.SetActive(true);


    }
