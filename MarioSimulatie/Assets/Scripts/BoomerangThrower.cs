using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangThrower : MonoBehaviour
{
    private enum Boomerang { going, returning, holding }
    private Boomerang state = Boomerang.holding;
    private GameObject boomerang;
    private Rigidbody boomerangRb;
    private GameObject player;
    private Vector3 target;
    public GameObject boomerangPrefab;
    public Transform spawn;
    public float speed = 1f;
    public float rethrowSpeed = 0.5f;

    private void Awake()
    {
        this.player = GameObject.FindWithTag("Player");
        StartCoroutine(RethrowBoomerang());
    }

    public void FixedUpdate()
    {
        if(boomerang == null)
        {
            state = Boomerang.holding;
            return;
        }

        switch (state)
        {
            case Boomerang.going:
                Vector3 boo = boomerang.transform.position;
                boo.y = 0;
                if (Vector3.Distance(target, boo) <= 0.5f)
                {
                    this.state = Boomerang.returning;
                }
                else
                {
                    Vector3 direction = target - boomerang.transform.position;
                    this.boomerangRb.MovePosition(boomerang.transform.position + direction.normalized * Time.fixedDeltaTime * speed);
                }
                break;
            case Boomerang.returning:
                Vector3 returnDir = this.transform.position - boomerang.transform.position;
                this.boomerangRb.MovePosition(boomerang.transform.position + returnDir.normalized * Time.fixedDeltaTime * speed);
                break;
        }
    }

    IEnumerator RethrowBoomerang()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.rethrowSpeed);
            if (state != Boomerang.holding)
                continue;

            Ray ray = new Ray(this.spawn.position, this.player.transform.position - this.spawn.position);
            RaycastHit hitinfo;
            
            if(Physics.Raycast(ray, out hitinfo))
            {
                if(hitinfo.collider.transform.root == player.transform.root)
                {
                    //Throw boomerang
                    state = Boomerang.going;
                    this.boomerang = Instantiate(this.boomerangPrefab, this.spawn.position, this.spawn.rotation);
                    this.boomerangRb = this.boomerang.GetComponent<Rigidbody>();
                    this.target = player.transform.position + (player.transform.position - boomerang.transform.position);
                    target.y = 0;
                }
            }
        }
    }
}
