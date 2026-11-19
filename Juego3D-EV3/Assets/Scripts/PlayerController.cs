using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    
    [Header("Configuración de Movimiento")]
    public float velocidad = 5f;
    public float maxVelocidad = 7f;

    [Header("Configuración de Salto")]
    public float fuerzaSalto = 5f;
    public LayerMask groundLayer;  
    private bool estaEnSuelo = true;

    [Header("Cámara")]
    public Transform camaraPrincipal;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (camaraPrincipal == null && Camera.main != null)
        {
            camaraPrincipal = Camera.main.transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            estaEnSuelo = false;
        }
    }

    private void FixedUpdate()
    {
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");

        Vector3 dirMovimiento;

        if (camaraPrincipal != null)
        {
            Vector3 camForward = camaraPrincipal.forward;
            Vector3 camRight = camaraPrincipal.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            dirMovimiento = (camForward * movimientoV + camRight * movimientoH);
        }
        else
        {
            dirMovimiento = new Vector3(movimientoH, 0.0f, movimientoV);
        }

        rb.AddForce(dirMovimiento * velocidad);

        Vector3 velocidadHorizontal = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (velocidadHorizontal.magnitude > maxVelocidad)
        {
            velocidadHorizontal = velocidadHorizontal.normalized * maxVelocidad;
            rb.linearVelocity = new Vector3(velocidadHorizontal.x, rb.linearVelocity.y, velocidadHorizontal.z);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            estaEnSuelo = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            estaEnSuelo = false;
        }
    }
}