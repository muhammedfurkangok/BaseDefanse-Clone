using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject teleportPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            player.transform.position = teleportPosition.transform.position;
        }
    }
}