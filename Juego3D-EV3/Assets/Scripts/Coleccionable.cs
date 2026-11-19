using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class Coleccionable : MonoBehaviour
{
    [Header("Configuración del Ítem")]
    [Tooltip("Usa 1 para normal, 3 para especial, o -1 para trampa")]
    public int valorPuntos = 1; 
    public float velocidadGiro = 50f;

    private static int puntajeTotal = 0;
    private static int metaPuntaje = 12;

    void Start()
    {
        ActualizarTextoUI();
    }

    void Update()
    {
        transform.Rotate(Vector3.up * velocidadGiro * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.name.Contains("Jugador"))
        {
            puntajeTotal += valorPuntos;
            puntajeTotal = Mathf.Max(0, puntajeTotal);

            ActualizarTextoUI();

            Debug.Log($"¡Ítem recolectado! Valor: {valorPuntos} | Puntaje Total Actual: {puntajeTotal} / {metaPuntaje}");

            if (puntajeTotal >= metaPuntaje)
            {
                Debug.Log("¡Meta alcanzada! Reiniciando nivel automáticamente...");
                puntajeTotal = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            Destroy(gameObject);
        }
    }

    void ActualizarTextoUI()
    {
        GameObject objetoTexto = GameObject.Find("TextoPuntaje");
        if (objetoTexto != null)
        {
            TextMeshProUGUI textoUI = objetoTexto.GetComponent<TextMeshProUGUI>();
            if (textoUI != null)
            {
                textoUI.text = "Puntos: " + puntajeTotal + " / " + metaPuntaje;
            }
        }
    }
}