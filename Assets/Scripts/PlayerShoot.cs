using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    public Weapon weapon;
    public AudioClip shotSFX;
    public AudioSource _audioSource;

    [SerializeField]
    private Camera cam;

    public Transform bulletSpawnPoint;
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private LayerMask mask;
    
    void Start()
    {
        if (cam == null) { 
            Debug.LogError("PlayerShoot: No Camera");
            this.enabled = false;
        }
        //if (isLocalPlayer == true)
         //   bulletSpawnPoint = transform.Find("Graphics/Camera/bulletSpawnPoint");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            CmdShoot();
        }

        
    }

    [Command]
    void CmdShoot()
    {
        GameObject _bullet = (GameObject)Instantiate(bullet.gameObject, bulletSpawnPoint.transform.position, Quaternion.identity);
        _bullet.transform.rotation = bulletSpawnPoint.transform.rotation;
        NetworkServer.SpawnWithClientAuthority(_bullet, connectionToClient);

        //RaycastHit _hit;
        //if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        //{
            //print("We shot at " + _hit.collider.name);
            //if (_hit.collider.tag == "Player")
           // {
            //  CmdPlayerShoot(_hit.collider.name, weapon.damage);
          //  }
        //}
        

    }
}
