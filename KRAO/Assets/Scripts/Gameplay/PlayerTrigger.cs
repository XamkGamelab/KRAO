using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PlayerTrigger : MonoBehaviour
{
    private Collider player;

    [SerializeField] UnityEvent onEnterEvents;

    [SerializeField] private float onStayEventCooldown;
    [SerializeField] UnityEvent onStayEvents;

    private float cooldownTimer = 0f;

    [SerializeField] UnityEvent onExitEvents;

    private void Start()
    {
        if (!GetComponent<Collider>().isTrigger)
        {
            GetComponent<Collider>().isTrigger = true;
        }
        player = GameObject.FindWithTag("Player").GetComponentInChildren<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            onEnterEvents.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsPlayer(other))
        {
            if(onStayEventCooldown > 0)
            {
                cooldownTimer += Time.deltaTime;
                while (cooldownTimer >= onStayEventCooldown)
                {
                    onStayEvents.Invoke();
                    cooldownTimer -= onStayEventCooldown;
                }
            } else
            {
                onStayEvents.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
        {
            onExitEvents.Invoke();
            cooldownTimer = 0f;
        }
    }

    private bool IsPlayer(Collider collider)
    {
        return collider == player;
    }
}
