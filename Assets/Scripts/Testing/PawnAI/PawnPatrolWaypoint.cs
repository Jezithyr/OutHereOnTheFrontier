using UnityEngine;

public class PawnPatrolWaypoint : MonoBehaviour
{
    [SerializeField] private float debugDrawRadius = 1.0f;

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
    }
}
