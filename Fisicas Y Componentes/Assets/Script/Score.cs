using UnityEngine;

public class ScoreCounter : MonoBehaviour
{

    public float tiempo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tiempo = 0f;

        Debug.Log("El juego consiste en encestar las esferas en la cesta de arriba sin que te oquen los laseres, pero ten cuidado las esferas pueden orbitar en tu player si vas muy rapido.");
        Debug.Log("Cuando la bola caiga en la cesta saldra tu tiempo en la consola. El juego se resetea pulsando R.");
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
        {
            tiempo = 0f;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sphere"))
        {
            Debug.Log("Tiempo final: " + tiempo + " segundos");
        }
    }
}
