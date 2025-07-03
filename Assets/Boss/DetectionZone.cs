using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private Tren tren;

    void Update()
    {
        if (_player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);

        if (distanceToPlayer <= detectionRange)
        {
            tren.ActivarTren();
        }
    }

    public void ReiniciarSistema()
    {
        Debug.Log("Reiniciar el tren");
        tren.ReiniciarTren();
    }
}
