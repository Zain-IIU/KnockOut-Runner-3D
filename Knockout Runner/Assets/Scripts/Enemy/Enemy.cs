using System.Collections;
using DG.Tweening;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    
    //0 is idle
    //1 is walk
    [Header("Enemy States")] [SerializeField]
    private bool isIdle;
    [SerializeField] private Animator animator;
    [SerializeField] private SkinnedMeshRenderer bodyRenderer;
    [SerializeField] private Transform attackSign;
    
    [Space]
    [Header("Chasing Player")]
    

    private PlayerController player;

    private Transform hitPoint;
     [SerializeField]  float normalSpeed;
     [SerializeField]  float chasingSpeed;
     [SerializeField]  float increasedSpeed;
     [SerializeField]  float approacingSpeed;
     [SerializeField]  float accuracy = 1.0f;
    private  Vector3 lookAtGoal;
    [Space]
    [Header("Attack Player")] 
    [SerializeField] private Ball enemyBallPrefab;

    [Header("Particles")]
    [SerializeField] private ParticleSystem hitVFX;
    [SerializeField] private ParticleSystem counterVFX;
    [SerializeField] private ParticleSystem[] fallVFX;
    
    
    
    [SerializeField] private Transform handPos;

    [SerializeField] private CharacterController controller;
    [SerializeField] private EnemySensor sensor;
    
     private float curSpeed;
     private bool startChasing;
     private bool startedMoving;

     #region Animation Hashing

     private static readonly int Chase = Animator.StringToHash("chase");
     private static readonly int Walk = Animator.StringToHash("walk");
     private static readonly int Throw = Animator.StringToHash("Throw");
     private static readonly int Fall = Animator.StringToHash("Fall");
     private static readonly int Hit = Animator.StringToHash("GotHit");

     #endregion
    
     
     [SerializeField] private float bandingDistance;
     
     private void Awake()
     {
         curSpeed = normalSpeed;
     }

     private void Start()
     {
         player = FindObjectOfType<PlayerController>();
         hitPoint = GameObject.FindGameObjectWithTag("HitPoint").transform;
     }

     private void Update()
     {
         if (!EventsManager.GameStarted) return;
            
         if (player.IsPlayerDown() || player.gameObject.GetComponent<PlayerMovement>().HasPlayerReachedEnd())
         {
             animator.SetBool(Chase,false);
             return;
         }
         
         WalkRandomly();
         if(!startChasing) return;
         
         RubberBandingEffect();
          
         LookAtPlayer(player.transform);
         
         ChasePlayer();
     }

     private bool hasShotBall;
    

     private void ChasePlayer()
    {
       
        if (!controller.enabled)  return;
        
        controller.Move(transform.forward*curSpeed*Time.deltaTime);
        if (!(Vector3.Distance(transform.position, lookAtGoal) < accuracy)) return;
        
        if (hasShotBall) return;
            
        hasShotBall = true;
        attackSign.DOScale(Vector3.one, .25f);
        animator.SetTrigger(Throw);
    }

    private void LookAtPlayer(Transform point)
    {
        if (!startChasing) return;
        var position = point.transform.position;
        lookAtGoal = new Vector3(position.x, 
            transform.position.y, 
            position.z);
        transform.LookAt(lookAtGoal);
    }

    private void RubberBandingEffect()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > bandingDistance)
        {
            curSpeed = increasedSpeed;
        }
        else if(Vector3.Distance(transform.position, player.transform.position) < bandingDistance 
                && Vector3.Distance(transform.position, player.transform.position) > accuracy )
        {
            curSpeed = approacingSpeed;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < accuracy)
        {
            curSpeed = chasingSpeed;
        }
    }
    


    #region Animation Events
    private void ThrowBallAtPlayer()
    {
        Ball ball = Instantiate(enemyBallPrefab, handPos, true);
        ball.transform.DOLocalMove(Vector3.zero, 0).OnComplete(() => { ball.ThrowBall(true,hitPoint.position); });
    }

    private void StopChasingPlayer()
    {
        startChasing = false;
        animator.SetBool(Chase,false);
    }

 
    
    
    
    //to be called from event 

    #endregion
   
    public void FallDown()
    {
        fallVFX[Random.Range(0,fallVFX.Length)].Play();
        animator.SetTrigger(Hit);
        curSpeed = normalSpeed = 0;
    }
    public void EnableRagDoll()
    {
        if(hitVFX)
            hitVFX.Play();
        if(counterVFX)
            counterVFX.Play();
        controller.enabled = false;
        if(sensor)
            sensor.gameObject.SetActive(false);
        animator.SetTrigger(Fall);
        ChangeLayer();
        StopAllCoroutines();
        StopChasingPlayer();
        bodyRenderer.material.color=Color.black;
        AudioManager.instance.PlayRandomFromArray();
    }

    public void StartChasing(float time)
    {
        if(!player.IsPlayerDown())
             StartCoroutine(nameof(ChaseAfter),time);
    }

    private void WalkRandomly()
    {
        if(Vector3.Distance(transform.position,player.transform.position)>25) return;
        
        if (isIdle || startChasing) return;
        if (!startedMoving)
        {
            startedMoving = true;
            animator.SetTrigger(Walk);    
        }
        if (!controller.enabled) return;
        controller.Move(transform.forward*normalSpeed*Time.deltaTime);
    }

    
    
   
    IEnumerator ChaseAfter(float time)
    {
        yield return new WaitForSeconds(time);
        startChasing = true;
        animator.SetBool(Chase,startChasing);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacles"))
        {
         //   EnableRagDoll();
        }
    }

    
    private void ChangeLayer()
    {
        var allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            child.gameObject.layer=LayerMask.NameToLayer("Default");
        }
    }
}
