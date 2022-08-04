using MoreMountains.NiceVibrations;
using UnityEngine;

public class BallEventListener : MonoBehaviour
{
    public int id;
    private Ball thisBall;
    [SerializeField] private ParticlesManager particlesManager;
    
    [SerializeField] private bool forPlayer;
    private void Awake()
    {
        thisBall = GetComponent<Ball>();
    }

    private void Start()
    {
        particlesManager=ParticlesManager.Instance;
        id = thisBall.GetID();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BallsManager sensor) && !thisBall.isPicked && !thisBall.isThrown && forPlayer)
        {
            sensor.AddBall(thisBall);
            MMVibrationManager.Haptic(HapticTypes.MediumImpact, false,true, this);
            EventsManager.BallPicked_Event(id);
        }
        if (other.gameObject.TryGetComponent(out Enemy enemy) && forPlayer && thisBall.isThrown)
        {
            other.gameObject.layer= LayerMask.NameToLayer("NoCollision");
            gameObject.SetActive(false);
            enemy.EnableRagDoll();
            EventsManager.BallHit();
        }

        if (other.gameObject.TryGetComponent(out PlayerController player) && !forPlayer)
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact, false,true, this);
            particlesManager.PlayHitVFXAt(other.transform.position + new Vector3(0,2,0));
            gameObject.SetActive(false);
            EventsManager.PlayerTakeDamage();
        }
    }

}
