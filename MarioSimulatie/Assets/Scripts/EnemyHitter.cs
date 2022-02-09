using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitter : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.tag.Equals("Enemy"))
        {
            Destroy(collision.transform.root.gameObject);
            Destroy(this.gameObject);
        }
    }
}
