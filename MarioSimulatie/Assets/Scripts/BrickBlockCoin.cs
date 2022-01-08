using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBlockCoin : MonoBehaviour
{
    public Vector2 angleRange;
    public float fireForce = 10;
    public GameObject coin;
    public string playerTag = "Player";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.tag.Contains(this.playerTag))
        {
            if (collision.contacts[0].point.y < transform.position.y)
            {
                GameObject spawnedCoin = Instantiate(this.coin, transform.position + Vector3.up, transform.rotation);
                var coinRigidbody = spawnedCoin.GetComponent<Rigidbody>();

                Vector3 force = new Vector3(Random.Range(this.angleRange.x, this.angleRange.y), 1, Random.Range(this.angleRange.x, this.angleRange.y));
                force *= fireForce;
                coinRigidbody.AddForce(force, ForceMode.Impulse);                    
            }
        }
    }
}
