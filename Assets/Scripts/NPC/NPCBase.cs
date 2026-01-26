using UnityEngine;
using UnityEngine.AI;

public class NPCBase : MonoBehaviour
{
    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 pos)
    {
        agent.SetDestination(pos);
    }
}
