using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobberBot : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;
    public GameObject[] hidingSpots;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        hidingSpots = GameObject.FindGameObjectsWithTag("hide");
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Flee(Vector3 location)
    {
        Vector3 targetDir = location - agent.transform.position;
        agent.SetDestination(agent.transform.position - targetDir);
    }

    void Pursue()
    {
        Vector3 targetDir = target.transform.position - agent.transform.position;

        float predictionTime = targetDir.magnitude / (agent.speed + target.GetComponent<Drive>().speed);
        Vector3 intercept = target.transform.position + predictionTime * target.transform.forward * 5;

        Seek(intercept);
    }

    void Arrival(Vector3 location)
    {
        float slowdownDistance = 10.0F;
        float slowdownRate = 0.90F;

        Vector3 targetDir = location - agent.transform.position;
        if (targetDir.magnitude < slowdownDistance)
        {
            // slowdown takes effect here.
            location = agent.transform.position + slowdownRate * targetDir;
        }   
        Seek(location);
    }

    Vector3 wanderTarget = Vector3.zero;
    void Wander()
    {
        float wanderRadius = 10;
        float wanderDistance = 20;
        float wanderJitter = 1;

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter,
                                    0,
                                    Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
    }

    void Hide()
    {
        float nearest = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        for (int i = 0; i < hidingSpots.Length; i++)
        {
            Vector3 hidingDir = hidingSpots[i].transform.position - target.transform.position;
            Vector3 hidingPos = hidingSpots[i].transform.position + hidingDir.normalized * 5;

            float dist = Vector3.Distance(this.transform.position, hidingPos);
            if (dist < nearest)
            {
                chosenSpot = hidingPos;
                nearest = dist;
            }
        }

        Seek(chosenSpot);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDetected()) 
        {
            Hide(); 
        } 
        else
        {
            Seek(target.transform.position); 
        }
        // Seek(target.transform.position);
        //Flee(target.transform.position); 
        // Pursue();
        // Arrival(target.transform.position);
        // Wander();
    }

    bool IsDetected()
    {   
        //DotProductTest

        return Vector3.Angle(target.transform.forward, transform.position - target.transform.position) <  45   ; 
    }
}
