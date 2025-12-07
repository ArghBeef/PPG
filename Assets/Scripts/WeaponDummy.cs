using UnityEngine;

public class WeaponDummy : MonoBehaviour
{
    public bool isDestructable;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            if (isDestructable)
                Destroy(this.gameObject);
            else
                Debug.Log("Bullet HIT");
        }
    }
}
