using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPlayerContact : MonoBehaviour
{
    public string playerTag = "Player";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.tag.Equals(this.playerTag))
        {
            if(collision.contacts[0].point.y < transform.position.y)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
