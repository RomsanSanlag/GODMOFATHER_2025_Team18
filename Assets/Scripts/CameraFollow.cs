using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 2, -10); // ajuste comme tu veux

    void LateUpdate()
    {
        // Compare les positions en X (distance parcourue)
        Transform leader = (player1.position.x > player2.position.x) ? player1 : player2;

        // Position cible = leader + offset
        Vector3 desiredPosition = leader.position + offset;

        // On garde la position Z de la caméra si tu veux pas qu’elle bouge en profondeur
        desiredPosition.z = transform.position.z;

        // Smooth follow
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
