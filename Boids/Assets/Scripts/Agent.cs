using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 separationVector;
    public List<Agent> neightbours;
    public Vector3 startVector;

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + velocity);
        Gizmos.DrawWireSphere(transform.position, 2);
    }

    public void updateAgent()
    {
        transform.position += velocity * Time.deltaTime ;
    }
    private void Awake()
    {
        neightbours = new List<Agent>();
    }

    public void addForce(Vector3 f)
    {
        velocity += f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
