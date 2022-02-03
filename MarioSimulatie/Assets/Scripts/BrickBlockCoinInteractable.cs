using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class BrickBlockCoinInteractable : MonoBehaviour
{
    private bool canSpawn = true;
    public Vector2 angleRange;
    public float fireForce = 7.5f;
    public GameObject coin;
    public string playerTag = "Player";
    public float blockHeight = 0.2f;
    public float positionDelay = 1.0f;
    public float spawnDelay = 0.5f;

    private void Awake()
    {
        StartCoroutine(SetBlockHeight());
        StartCoroutine(CoinDelay());
    }

    private IEnumerator SetBlockHeight()
    {
        yield return new WaitForSeconds(positionDelay);
        GameObject head = GameObject.Find("VRCamera");
        Vector3 position = transform.position;
        position.y = head.transform.position.y + blockHeight;
        this.transform.position = position;
        StopCoroutine(SetBlockHeight());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!this.canSpawn)
            return;

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
                this.canSpawn = false;
            }
        }
    }

    private IEnumerator CoinDelay()
    {
        while (true)
        {
            this.canSpawn = true;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
