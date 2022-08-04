using DG.Tweening;
using UnityEngine;


public class ParticlesManager : MonoBehaviour
{
     public static ParticlesManager Instance;

     private void Awake()
     {
          Instance = this;
     }

     [SerializeField] private ParticleSystem hitParticles;
     

   
     public void PlayHitVFXAt(Vector3 hitPos)
     {
          hitParticles.transform.DOMove(hitPos, 0f).OnComplete(() =>
          {
               hitParticles.Play();
          });
     }
   
}
