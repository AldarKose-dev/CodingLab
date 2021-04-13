using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class PlayerRespawn : NetworkBehaviour
{
    private Player healthScript;
    private RawImage crossHairImage;
    private GameObject respawnButton;
    // Start is called before the first frame update
    void Start()
    {
        healthScript = GetComponent<Player>();
        healthScript.EventRespawn += EnablePlayer;

        crossHairImage = GameObject.Find("crossHairImage").GetComponent<RawImage>();
        SetRespawnButton();
    }

    void OnDisable()
    {
        healthScript.EventRespawn -= EnablePlayer;
    }
    void SetRespawnButton()
    {
            respawnButton = GameObject.Find("GM").GetComponent<GameManager_References>().RespawnButton;
            respawnButton.GetComponent<Button>().onClick.AddListener(CommenceRespawn);
            respawnButton.SetActive(false);
    }

    void EnablePlayer()
    {
        GetComponent<CharacterController>().enabled = true;
        GetComponent<PlayerShoot>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer ren in renderers)
            ren.enabled = true;

        if (isLocalPlayer)
        {
            crossHairImage.enabled = true;
            respawnButton.SetActive(false);
        }
    }

    void CommenceRespawn()
    {
        CmdRespawnOnServer();
    }

    [Command]
    void CmdRespawnOnServer()
    {
        healthScript.ResetHealth();
    }
}
