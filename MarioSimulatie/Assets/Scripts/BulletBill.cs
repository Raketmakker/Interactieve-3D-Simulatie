using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBill : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.tag.Contains("Player"))
        {
            Debug.Log("Player got hit!");
        }
        Destroy(this.gameObject);
    }
}
