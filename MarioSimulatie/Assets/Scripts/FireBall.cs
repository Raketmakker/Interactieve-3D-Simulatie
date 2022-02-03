using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int maxBounces = 2;

    private void OnCollisionEnter(Collision collision)
    {
        maxBounces--;
        if(maxBounces <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
