using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField] private string teleporterPrompt;
    [SerializeField] private Transform teleporterDestination;
    private PlayerManager playerManager => FindFirstObjectByType<PlayerManager>();
    private InteractionPrompt interactionPrompt => FindFirstObjectByType<InteractionPrompt>();
    public void TeleportPlayer()
    {
        playerManager.TransportPlayer(teleporterDestination.position, teleporterDestination.rotation);
    }

}
