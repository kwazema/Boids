using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{

    [SerializeField]
    GameObject agentPrefab;

    [SerializeField]
    int numBoids = 10;

    Agent[] agents;

    [SerializeField]
    float agentRadius = 2.0f;

    [SerializeField]
    float separationWeight = 1.0f, cohesionWeight = 1.0f, alignmentWeight = 1.0f;

    private void Awake()
    {
        List<Agent> agentlist = new List<Agent>();

        for(int i = 0; i<numBoids; i++)
        {
            Vector3 position = Vector3.up * Random.Range(0, 10)
                + Vector3.right * Random.Range(0, 10) + Vector3.forward * Random.Range(0, 10);
            agentlist.Add(Instantiate(agentPrefab, position, Quaternion.identity).GetComponent<Agent>());

        }
        agents = agentlist.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Agent a in agents)
        {
            a.velocity = Vector3.zero;
            checkForNeightBours(a);
            calculateSeparation(a);
            calculateAlignment(a);
            calculateCohesion(a);
            a.updateAgent();
            a.neightbours.Clear();
         
        }
    }

    void checkForNeightBours(Agent a)
    {
        
    }

    void calculateSeparation(Agent a)
    {
        a.addForce(Vector3.up, Agent.DEBUGforceType.SEPARATION);
       
    }

    void calculateCohesion(Agent a)
    {
        a.addForce(Vector3.forward, Agent.DEBUGforceType.COHESION);

    }

    void calculateAlignment(Agent a)
    {
        a.addForce(Vector3.right, Agent.DEBUGforceType.ALIGNMENT);
    }
}
