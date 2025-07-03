using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CreditLoader : MonoBehaviour
{
    public GameObject textPrefab;         // Prefab del texto
    public Transform contentParent;       // Contenedor (Content del ScrollView)
    public float scrollSpeed = 30f;       // Velocidad del desplazamiento automático

    private RectTransform contentRect;

    void Start()
    {
        contentRect = contentParent.GetComponent<RectTransform>();

        string path = Path.Combine(Application.dataPath, "Creditos/credits.txt");


        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                GameObject newText = Instantiate(textPrefab, contentParent);
                newText.GetComponent<Text>().text = line;
            }
        }
        else
        {
            Debug.LogError("Archivo no encontrado: " + path);
        }
    }

    void Update()
    {
        if (contentRect != null)
        {
            contentRect.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Saliendo de los créditos");
            // Aquí puedes cambiar de escena o volver al menú
            // SceneManager.LoadScene("MainMenu");
        }
    }
}
