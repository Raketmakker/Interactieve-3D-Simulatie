using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.tag.Equals("Player"))
        {
            Debug.Log("Player got hit!");
        }
        Destroy(this.gameObject);
    }
}
