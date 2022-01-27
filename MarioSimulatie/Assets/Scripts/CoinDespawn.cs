using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDespawn : MonoBehaviour
{
    public string playerTag = "Player";
    public bool hasDespawnTimer = true;
    public float despawnTimer = 10;

    private void Awake()
    {
        if (this.hasDespawnTimer)
        {
            Destroy(this.gameObject, this.despawnTimer);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Is de tag van de collision de player?
        if (collision.transform.root.tag.Contains(this.playerTag))
        {
            //Vernietig het muntje
            Destroy(this.gameObject);
        }
    }
}
