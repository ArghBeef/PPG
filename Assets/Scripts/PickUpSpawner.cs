using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public BoxCollider spawnArea;

    public GameObject[] pickUps;
    public int spawnCount;

    void Start()
    {
        SpawnPickups();
    }

    void SpawnPickups()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 pos = RandPoint(spawnArea.bounds);
            Instantiate(pickUps[Random.Range(0, pickUps.Length)],pos,Quaternion.identity);
        }
    }

    Vector3 RandPoint(Bounds bounds)
    {
        return new Vector3(Random.Range(bounds.min.x, bounds.max.x), bounds.center.y, Random.Range(bounds.min.z, bounds.max.z));
    }
}
