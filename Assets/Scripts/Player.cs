using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;

    [SyncVar]
    public float currHealth;
    private Text healthText;
    private bool shouldDie = false;
    public bool isDead = false;

    public delegate void DieDelegate();
    public event DieDelegate EventDie;

    public delegate void RespawnDelegate ();
    public event RespawnDelegate EventRespawn;

    void Awake()
    {
        currHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currHealth -= damage;
        Debug.Log(transform.name + "has " + currHealth);
    }
    void Start()
    {
        healthText = GameObject.Find("healthText").GetComponent<Text>();
        SetHealthText();
    }
    void Update()
    {
        CheckCondition();
        SetHealthText();
    }
    void SetHealthText()
    {
        if (isLocalPlayer)
            healthText.text = "Health: " + currHealth.ToString();
    }

    void CheckCondition()
    {
        if(currHealth<= 0 && !shouldDie && !isDead)
        {
            shouldDie = true;
        }
        if (currHealth <= 0 && shouldDie)
        {
            if (EventDie != null)
            {
                EventDie();
            }
        }
        shouldDie = false;

        if (currHealth > 0 && isDead)
        {
            if (EventRespawn != null)
            {
                EventRespawn();
            }
            isDead = false;
        }
    }

    public void ResetHealth()
    {
        currHealth = maxHealth;
    }
}
