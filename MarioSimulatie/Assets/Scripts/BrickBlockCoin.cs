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
        //Is de tag van de collision de player?
        if (collision.transform.root.tag.Contains(this.playerTag))
        {
            //Is het contactpunt lager dan het centrum van het brickblock? (De onderkant)
            if (collision.contacts[0].point.y < transform.position.y)
            {
                //Spawn een muntje
                GameObject spawnedCoin = Instantiate(this.coin, transform.position + Vector3.up, transform.rotation);
                //Zoek het rigidbody component van het muntje
                var coinRigidbody = spawnedCoin.GetComponent<Rigidbody>();

                //Maak een random vector naar boven. Random X, 1 voor Y, Random Z
                Vector3 force = new Vector3(Random.Range(this.angleRange.x, this.angleRange.y), 1, Random.Range(this.angleRange.x, this.angleRange.y));
                //Vermenigvuldig de richting met een vuurkracht
                force *= fireForce;
                //Voer de kracht uit op het rigidbody
                coinRigidbody.AddForce(force, ForceMode.Impulse);                    
            }
        }
    }
}
