using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerDeath : NetworkBehaviour
{
    private Player healthScript;
    private RawImage crossHairImage;
    // Start is called before the first frame update
    void Start()
    {
        crossHairImage = GameObject.Find("crossHairImage").GetComponent<RawImage>();
        healthScript = GetComponent<Player>();
        healthScript.EventDie += DisablePlayer;
    }
    void Update()
    {
        
    }
    void OnDisable()
    {
        healthScript.EventDie -= DisablePlayer;
    }

    void DisablePlayer()
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<PlayerShoot>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer ren in renderers)
            ren.enabled = false;

        healthScript.isDead = true;

        if (isLocalPlayer)
        {
            crossHairImage.enabled = false;
            GameObject.Find("GM").GetComponent<GameManager_References>().RespawnButton.SetActive(true);
        }
    }
}
