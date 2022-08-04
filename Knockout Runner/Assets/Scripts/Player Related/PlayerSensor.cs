using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
      [SerializeField] private LayerMask enemyMask;

      [SerializeField] private float sensingDistance;

      [SerializeField] private new Collider collider;
      
      
      public bool CheckEnemyInRange()
      {
            return Physics.BoxCast(collider.bounds.center, new Vector3(1.2f,1,1), transform.forward,
                  Quaternion.identity, sensingDistance,enemyMask);
      }

      void OnDrawGizmos()
      {
            Gizmos.color = Color.red;


            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * sensingDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * sensingDistance, new Vector3(1.2f,1,1));

      }
}