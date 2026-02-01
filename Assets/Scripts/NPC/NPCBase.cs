using UnityEngine;
using UnityEngine.AI;

public class NPCBase : MonoBehaviour
{
    public float radius = 10f;
    public float waitTime = 3f;

    NavMeshAgent agent;
    float timer;
    bool paused;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (paused) return;

        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            Vector3 randomPos = Random.insideUnitSphere * radius + transform.position;

            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, radius, NavMesh.AllAreas))
                agent.SetDestination(hit.position);

            timer = 0;
        }
    }

    public void Pause()
    {
        paused = true;
        agent.isStopped = true;
    }

    public void Resume()
    {
        paused = false;
        agent.isStopped = false;
    }

    public void MoveTo(Vector3 pos)
    {
        agent.SetDestination(pos);
    }
}

