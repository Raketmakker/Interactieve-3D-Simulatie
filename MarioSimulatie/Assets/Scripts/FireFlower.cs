using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class FireFlower : MonoBehaviour
{
    private Interactable interactable;
    private bool canFire = true;
    public SteamVR_Action_Boolean fireAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "GrabPinch");
    public float fireDelay = 0.5f;
    public GameObject fireBall;
    public Transform fireBallSpawn;
    public float fireForce = 10;

    private void Awake()
    {
        this.interactable = GetComponent<Interactable>();
        this.interactable.onAttachedToHand += Interactable_onAttachedToHand;
        this.interactable.onDetachedFromHand += Interactable_onDetachedFromHand;
        StartCoroutine(FireReset());
    }

    private void OnDestroy()
    {
        this.interactable.onAttachedToHand -= Interactable_onAttachedToHand;
        this.interactable.onDetachedFromHand -= Interactable_onDetachedFromHand;
        StopAllCoroutines();
    }

    private void Interactable_onDetachedFromHand(Hand hand)
    {
        this.enabled = false;
    }

    private void Interactable_onAttachedToHand(Hand hand)
    {
        this.enabled = true;
    }

    private void Update()
    {
        //The flower is not in the players hand. Return
        if (!this.interactable.attachedToHand)
            return;

        //The player cant fire. Return
        if (!canFire)
            return;

        SteamVR_Input_Sources hand = interactable.attachedToHand.handType;
        bool fire = fireAction[hand].stateDown;
        
        if (fire)
        {
            Vector3 relativePos = (this.fireBallSpawn.position + this.fireBallSpawn.forward) - transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            GameObject newBall = Instantiate(this.fireBall, this.fireBallSpawn.position, rotation);
            Vector3 force = newBall.transform.forward * this.fireForce;
            newBall.GetComponent<Rigidbody>()?.AddForce(force, ForceMode.Impulse);
        }
    }

    private IEnumerator FireReset()
    {
        while (true)
        {
            this.canFire = true;
            yield return new WaitForSeconds(this.fireDelay);
        }
    }
}
