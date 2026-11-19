using UnityEngine;

public class CaidaVacio : MonoBehaviour
{
    [Header("Posición de Reaparición Predeterminada")]
    public Vector3 posicionInicial = new Vector3(0f, 2f, 0f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.name.Contains("Jugador"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            other.transform.position = posicionInicial;
            Debug.Log("¡El jugador cayó al vacío! Reapareciendo en el inicio...");
        }
    }
}