using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    public float maxHP = 100f;

    float currentHP;
    float hpPart;

    List<GameObject> parts = new List<GameObject>();
    int partsDestroyed = 0;

    void Start()
    {
        currentHP = maxHP;

        foreach (Transform child in transform)
            parts.Add(child.gameObject);

        hpPart = maxHP / parts.Count;
    }

    void TakeDamage(float damage)
    {
        currentHP -= damage;

        int toDestroy = Mathf.FloorToInt((maxHP - currentHP) / hpPart);

        while (partsDestroyed < toDestroy && parts.Count > 0)
        {
            DestroyRandom();
            partsDestroyed++;
        }

        if (currentHP <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void DestroyRandom()
    {
        if (parts.Count == 0) return;

        int index = Random.Range(0, parts.Count);
        GameObject part = parts[index];

        parts.RemoveAt(index);
        Destroy(part);
    }
}
