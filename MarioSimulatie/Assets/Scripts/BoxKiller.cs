using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxKiller : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnpoint;
    public float force = 10;
    public float lifetime = 5;
    public float fireDelay = 1;

    private void Start()
    {
        StartCoroutine(FireRoutine());
    }

    public IEnumerator FireRoutine()
    {
        while (true)
        {
            SpawnBullet();
            yield return new WaitForSeconds(fireDelay);
        }
    }

    public void SpawnBullet()
    {
        //Spawn the bullet
        var bul = Instantiate(bullet, spawnpoint.position, spawnpoint.rotation);
        //Get the rigidbody and add force. Notice the elvis operator
        bul.GetComponent<Rigidbody>()?.AddForce(bul.transform.forward * force, ForceMode.Impulse);
        //Destroy the bullet after "lifetime" seconds
        Destroy(bul, lifetime);
    }
}
