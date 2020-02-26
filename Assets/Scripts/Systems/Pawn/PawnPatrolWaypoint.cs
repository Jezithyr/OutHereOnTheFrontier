using UnityEngine;

public class PawnPatrolWaypoint : MonoBehaviour
{
    [SerializeField] private float debugDrawRadius = 1.0f;
    [SerializeField] private PawnModule PawnController;
    [SerializeField] private bool isSpawnPoint = false;

    private void Start()
    {
        PawnController.RegisterNewWaypoint(this);
        if (isSpawnPoint) PawnController.RegisterNewSpawnPosition(gameObject);
    }


    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
    }
}
