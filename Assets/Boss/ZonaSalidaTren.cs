using UnityEngine;

public class ZonaSalidaTren : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Tren tren = other.GetComponent<Tren>();
        if (tren != null)
        {
            //tren.ReiniciarTren();
        }
    }
}
