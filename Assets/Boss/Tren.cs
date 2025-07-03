using UnityEngine;

public class Tren : MonoBehaviour
{
    [SerializeField] private float velocidad = 5f;
    private bool activado = false;
    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    public void ActivarTren()
    {
        activado = true;
    }

    public void ReiniciarTren()
    {
        activado = false;
        transform.position = posicionInicial;
    }

    void Update()
    {
        if (activado)
        {
            transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("El tren detectó a: " + other.name);

        Debug.Log(other.gameObject.layer + " | " + gameObject.layer);

        if (other.CompareTag("MainPlayer") && other.gameObject.layer == gameObject.layer)
        {
            Debug.Log("Jugador atropellado");

            ReiniciarTren();
            // FindObjectOfType<DetectionZone>()?.ReiniciarSistema();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Map") && other.gameObject.layer == gameObject.layer)
        {
            ReiniciarTren();
        }
    }
}
