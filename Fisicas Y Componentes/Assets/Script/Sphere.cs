using UnityEngine;

public class Spheres : MonoBehaviour
{
    #region Atributes

    //Public Atributes
    public Transform PlayerTransform;
    public Transform SphereTransform;
    public bool magneticMode;
    public float magneticForce = 20f;
    public float magneticDistance = 5f;

    private float tiempoSinLaser = 0f;
    public float tiempoReactivar = 2f;

    #endregion

    void Start()
    {
        magneticMode = true;
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = Random.ColorHSV();

        magneticForce = 20f;
        magneticDistance = 5f;
    }

    void FixedUpdate()
    {
        if (magneticMode == true)
        {
            Vector3 direction = (PlayerTransform.position - transform.position).normalized;
            float distance = Vector3.Distance(PlayerTransform.position, transform.position);
            
            if (distance < magneticDistance)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.AddForce(direction * magneticForce * (magneticDistance - distance), ForceMode.Acceleration);
            }

        }

        if (magneticMode == false)
        {
            tiempoSinLaser += Time.fixedDeltaTime;
            if (tiempoSinLaser >= tiempoReactivar)
            {
                magneticMode = true;
                tiempoSinLaser = 0f;
            }
        }
        else
        {
            tiempoSinLaser = 0f;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SphereTransform.position = new Vector3(0, 3, 0);

            magneticMode = true;

        }
    }
}
