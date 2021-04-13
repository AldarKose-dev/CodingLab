using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
    public float expireRate;
    private float currTimer;
    public float movSpeed;
    private GameObject otherPlayer;
    public Weapon weap;
    void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * movSpeed);
        currTimer += 1 * Time.deltaTime;

        if (currTimer >= expireRate)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            otherPlayer = other.gameObject;
            DealDamage(otherPlayer);
        }

    }
    public override void OnStartLocalPlayer()
    {
        print("OnStartLocalPlayer");
        GameObject player = ClientScene.FindLocalObject(netId);
        print("player y: " + player.transform.position.y);
    }

    void DealDamage(GameObject otherPlayer)
    {
        otherPlayer.GetComponent<Player>().TakeDamage(weap.damage);
        Destroy(this.gameObject);
    }
}
