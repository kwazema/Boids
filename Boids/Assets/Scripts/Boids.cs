using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{[SerializeField]
    GameObject agent;
    List<Agent> agents;

    float agentRadius = 2.0f;
    [SerializeField]
    float separationRatio = 1.0f, cohesionRatio = 1.0f, alignmentRatio = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        agents = new List<Agent>();
        for(int i = 0; i<20; i++)
        {
            Vector3 position = Vector3.up * Random.Range(0,10) 
                +Vector3.right * Random.Range(0,10) + Vector3.forward * Random.Range(0,10);
            agents.Add(Instantiate(agent, position, Quaternion.identity).GetComponent<Agent>());
            agents[i].startVector = Quaternion.Euler(0.0f, Random.Range(0, 180), 0.0f) * Vector3.forward ;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach(Agent a in agents)
        {
            a.velocity = Quaternion.Euler(0.0f, Random.Range(-30, 30), 0.0f) * a.startVector;
            checkForNeightBours(a);
            calculateSeparation(a);
            calculateAlignment(a);
            calculateCohesion(a);
            a.updateAgent();
            a.neightbours.Clear();
            a.velocity = Vector3.zero;
        }
    }

    void checkForNeightBours(Agent a)
    {
        Collider[] checks = Physics.OverlapSphere(a.transform.position, agentRadius);
        foreach (Collider c in checks)
        {
            a.neightbours.Add(c.GetComponent<Agent>());
        }
        Debug.Log(checks.Length);
    }

    void calculateSeparation(Agent a)
    {
      
            Vector3 separationVector = Vector3.zero;
            foreach(Agent neightbour in a.neightbours)
            {
                float distance = Vector3.Distance(a.transform.position, neightbour.transform.position);
                distance /= agentRadius;
                distance = (1 - distance);
                a.addForce(distance * (a.transform.position - neightbour.transform.position)* separationRatio);
                
            }
    }

    void calculateCohesion(Agent a)
    {
        Vector3 cohesionVector = Vector3.zero;

        Vector3 centralPoint = Vector3.zero;
        foreach (Agent neightbour in a.neightbours)
        {
            centralPoint += neightbour.transform.position;
        }
        centralPoint /= a.neightbours.Count;
        a.addForce((centralPoint-a.transform.position) * cohesionRatio);

    }

    void calculateAlignment(Agent a)
    {
        Vector3 alignmentVector = Vector3.zero;
        foreach (Agent neightbour in a.neightbours)
        {
            alignmentVector += neightbour.velocity;
        }
        alignmentVector /= a.neightbours.Count;
        a.addForce((alignmentVector) * alignmentRatio);
    }
}
