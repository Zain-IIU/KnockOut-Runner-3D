using UnityEngine;


public class FollowTargetWithOffset : MonoBehaviour
{
     [SerializeField] private Transform targetPos;

     [SerializeField] private Vector3 offsetVector;

     [SerializeField] private float speedToFollow;

     private void Update()
     {
          transform.position = Vector3.Lerp(transform.position, (targetPos.position + offsetVector),
               Time.deltaTime * speedToFollow);
     }
}
