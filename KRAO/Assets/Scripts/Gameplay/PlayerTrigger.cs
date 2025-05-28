using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PlayerTrigger : MonoBehaviour
{
    private Collider player;

    [SerializeField] UnityEvent onEnterEvents;

    [SerializeField] private bool visualizeStayTime;
    [SerializeField] private float onStayEventCooldown;
    [SerializeField] UnityEvent onStayEvents;
    private ProgressFiller progressFiller;

    private float cooldownTimer = 0f;

    [SerializeField] UnityEvent onExitEvents;

    private void Start()
    {
        if (!GetComponent<Collider>().isTrigger)
        {
            GetComponent<Collider>().isTrigger = true;
        }
        player = GameObject.FindWithTag("Player").GetComponentInChildren<CharacterController>();
        progressFiller = GameObject.Find("ProgressIndicator").GetComponent<ProgressFiller>();
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
                if (visualizeStayTime)
                {
                    progressFiller.ShowProgress(cooldownTimer / onStayEventCooldown);
                }
                while (cooldownTimer >= onStayEventCooldown)
                {
                    onStayEvents.Invoke();
                    cooldownTimer -= onStayEventCooldown;
                    progressFiller.ResetProgress();
                    return;
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
            if (visualizeStayTime)
            {
                progressFiller.ResetProgress();
            }
        }
    }

    private bool IsPlayer(Collider collider)
    {
        return collider == player;
    }
}
