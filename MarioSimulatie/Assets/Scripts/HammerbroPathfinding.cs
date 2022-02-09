using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class HammerbroPathfinding : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 destination;
    private enum MOVESTATE { MOVING, OUTOFRANGE, SIDEWAYS }
    private MOVESTATE state = MOVESTATE.OUTOFRANGE;
    private Rigidbody rb;
    public GameObject player;
    public string playerTag = "Player";
    public float range = 16;
    public float positionDelay = 0.5f;
    public float sidewaysDistance = 3f;
    public float jumpforce = 100;

    private void Awake()
    {
        this.agent = GetComponent<NavMeshAgent>();
        this.rb = GetComponent<Rigidbody>();
        this.player = GameObject.FindWithTag(this.playerTag);
        this.StartCoroutine(SetPosition());
    }
    
    // Update is called once per frame
    IEnumerator SetPosition()
    {
        while (true)
        {
            switch (state)
            {
                case MOVESTATE.MOVING:
                    if(Vector3.Distance(this.destination, this.transform.position) <= 0.5f)
                    {
                        if(Vector3.Distance(this.player.transform.position, this.transform.position) <= this.range)
                            this.state = MOVESTATE.SIDEWAYS;
                        else
                            this.state = MOVESTATE.OUTOFRANGE;
                    }
                    break;
                case MOVESTATE.SIDEWAYS:
                    Vector3 dir = (this.player.transform.position - this.transform.position).normalized * sidewaysDistance;
                    if(Random.Range(0, 10) % 2 == 0)
                        this.destination = new Vector3(dir.z, 0, -dir.x);
                    else
                        this.destination = new Vector3(-dir.z, 0, dir.x);

                    this.destination += this.transform.position;
                    this.destination.y = 0;
                    this.agent.SetDestination(this.destination);
                    this.state = MOVESTATE.MOVING;
                    break;
                case MOVESTATE.OUTOFRANGE:
                    Vector3 direction = this.transform.position + this.player.transform.position;
                    direction = direction.normalized * (this.range - 1);
                    this.destination = this.player.transform.position + direction;
                    this.destination.y = 0;
                    this.agent.SetDestination(this.destination);
                    this.state = MOVESTATE.MOVING;
                    break;
            }
            yield return new WaitForSeconds(this.positionDelay);
        }        
    }

    public void Jump()
    {
        agent.updatePosition = false;
        agent.updateRotation = false;
        agent.isStopped = true;
        this.rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        agent.updatePosition = true;
        agent.updateRotation = true;
        agent.isStopped = false;
    }
}
