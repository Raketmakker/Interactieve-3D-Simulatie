using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagChanger : MonoBehaviour
{
    public Renderer renderer;
    public Material otherFlag;
    public string playerTag = "Player";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.tag.Equals(this.playerTag))
        {
            this.renderer.material = otherFlag;
        }
    }
}
