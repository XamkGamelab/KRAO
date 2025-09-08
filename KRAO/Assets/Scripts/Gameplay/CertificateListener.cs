using UnityEngine;
using UnityEngine.Events;

public class CertificateListener : MonoBehaviour
{
    private PlayerManager playerManager => FindFirstObjectByType<PlayerManager>();

    [SerializeField] private UnityEvent onCertificateActivationEvent;

    private bool hasActivated = false;


    private void Start()
    {
        if (playerManager.HasCertificate)
        {
            hasActivated = true;
            onCertificateActivationEvent.Invoke();
        }
        else
        {
            PlayerManager.OnCertificateActivation += HandleCertificateActivation;
        }
    }

    private void HandleCertificateActivation(PlayerManager playerManager)
    {
        if (!hasActivated)
        {
            hasActivated = true;
            onCertificateActivationEvent.Invoke();
        }
    }
}
