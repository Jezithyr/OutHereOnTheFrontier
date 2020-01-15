using UnityEngine;
using UnityEngine.AI;

// This script will be testing the functions of random AI pathing along
// a sample scene in the Engine.
public class RandomPathing : MonoBehaviour
{
    // SerializedFields will be kept up here
    [SerializeField] public float newPathTimer;



    NavMeshAgent newMeshAgent;

    private void Start()
    {
        newMeshAgent =  GetComponent<NavMeshAgent>();
    }

    Vector3 getNewRandomPos()
    {
        // Randomize a pair of X and Z coords for the AI. CAN BE SERIALIZED.
        float x = Random.Range(-20, 20);
        float z = Random.Range(-20, 20);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }
}
