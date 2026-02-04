using UnityEngine;

public class Laser : MonoBehaviour
{
    #region Atributes
    public Transform eyector;
    public Transform captor;
    public Transform SpheresParent;

    private LineRenderer line;
    private Spheres[] spheres;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line = GetComponent<LineRenderer>();
        spheres = GameObject.FindObjectsOfType<Spheres>();

        eyector = transform.Find("Eyector");
        captor = transform.Find("Captor");

        line.SetPosition(0, eyector.position);
        line.SetPosition(1, captor.position);

        line.startWidth = 0.1f;
        line.endWidth = 0.1f;   

        line.material = new Material(Shader.Find("Unlit/Color"));
        line.material.color = Color.red;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (captor.position - eyector.position).normalized;
        float distance = Vector3.Distance(eyector.position, captor.position);

        Ray ray = new Ray(eyector.position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            line.SetPosition(1, hit.point);
            if (hit.collider.CompareTag("Player"))
            {
                Spheres[] spheres = SpheresParent.GetComponentsInChildren<Spheres>();
                foreach (Spheres s in spheres)
                {
                    s.magneticMode = false;
                }
            }
            
        }
        else
        {
            line.SetPosition(1, captor.position);
        }   

    }
}
