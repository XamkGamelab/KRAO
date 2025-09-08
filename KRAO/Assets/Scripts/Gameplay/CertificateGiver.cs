using UnityEngine;

public class CertificateGiver : MonoBehaviour
{
    private PlayerManager playerManager => FindFirstObjectByType<PlayerManager>();

    public void GiveCertificate()
    {
        if (!playerManager.HasCertificate)
        {
            playerManager.ActivateCertificate();
        }
    }
}
