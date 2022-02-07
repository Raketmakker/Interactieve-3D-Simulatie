using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class HammerbroPathfinding : MonoBehaviour
{
    private NavMeshAgent agent;
    private enum MOVESTATE { LEFT, RIGHT, OUTOFRANGE }
    private MOVESTATE state = MOVESTATE.OUTOFRANGE;
    public GameObject player;
    public string playerTag = "Player";
    public float distance = 16;
    public float positionDelay = 0.5f;
    public float sidewaysDistance = 3f;

    private void Awake()
    {
        this.agent = GetComponent<NavMeshAgent>();
        this.player = GameObject.FindWithTag(this.playerTag);
        this.StartCoroutine(SetPosition());
    }
    
    // Update is called once per frame
    IEnumerator SetPosition()
    {
        while (true)
        {
            switch (this.state)
            {
                case MOVESTATE.OUTOFRANGE:
                    //Is the player in range
                    if (Vector3.Distance(this.player.transform.position, this.transform.position) > this.distance)
                    {
                        //From the player to the agent (direction) = agent - player
                        Vector3 direction = this.agent.transform.position + this.player.transform.position;
                        //Nomralize the direction (a length of 1). And multiply it by the distance
                        direction = direction.normalized * this.distance;
                        //Start at the player and add the direction (wished position from the player)
                        this.agent.SetDestination(this.player.transform.position + direction);
                    }
                    else
                    {
                        this.state = MOVESTATE.LEFT;
                    }
                    break;
                
            }
            
            yield return new WaitForSeconds(this.positionDelay);
        }        
    }
}
