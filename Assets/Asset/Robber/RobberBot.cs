using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class RobberBot : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;
    public GameObject[] hidingSpots;

    float exhautGage = 0.0f;
    float exhautGageMax = 5.0f;

    float runningSpeed = 1.0f;
    float defaultRunningSpeed = 0;

    [SerializeField]
    private Transform _treasureHoldingSocket;
    GameObject _treasure = null; 

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        defaultRunningSpeed = agent.speed;

        hidingSpots = GameObject.FindGameObjectsWithTag("hide");
    }

    Vector3 Seek(Vector3 location)
    {
        return location; 
        agent.SetDestination(location);
    }

    Vector3 Flee(Vector3 location)
    {
        if (exhautGage < 0)
            exhautGage = 0;

        exhautGage += 1 * Time.deltaTime;

        Vector3 targetDir = location - agent.transform.position;
        return agent.transform.position - targetDir; 
        agent.SetDestination(agent.transform.position - targetDir);
    }

    Vector3 Pursue()
    {
        Vector3 targetDir = target.transform.position - agent.transform.position;

        float predictionTime = targetDir.magnitude / (agent.speed + target.GetComponent<Drive>().speed);
        Vector3 intercept = target.transform.position + predictionTime * target.transform.forward * 5;

        return intercept; 
        Seek(intercept);
    }

    Vector3 Arrival(Vector3 location)
    {
        float slowdownDistance = 10.0F;
        float slowdownRate = 0.90F;

        Vector3 targetDir = location - agent.transform.position;
        if (targetDir.magnitude < slowdownDistance)
        {
            // slowdown takes effect here.
            location = agent.transform.position + slowdownRate * targetDir;
        }

        return location; 

        Seek(location);
    }

    Vector3 wanderTarget = Vector3.zero;
    Vector3 Wander()
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

        return targetWorld;  
        Seek(targetWorld);
    }
    Vector3 Hide()
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

        return chosenSpot ; 
        Seek(chosenSpot) ;
    }

    Vector3 HideAndFlee(Vector3 targetLocation)
    {
       return (Flee(targetLocation) + Hide()) / 2;  ;
    }

    Vector3 StoleTreasure()
    {
        Vector3 ret = transform.position;


        return ret; 
    }

    Vector3 FindTreasure()
    {
        Vector3 ret = transform.position;

        if (_treasure != null)
            return StoleTreasure();

        GameObject treasure = GameObject.FindGameObjectWithTag("Treasure") ;
        Debug.Log((treasure.transform.position - transform.position).magnitude);

        if ( (treasure.transform.position - transform.position).magnitude < 1.0f)
        {
            _treasure = treasure;
            _treasure.transform.parent = _treasureHoldingSocket;
            _treasure.transform.localPosition = Vector3.zero;  
        } else
        {
            ret = (treasure.transform.position - transform.position) ;
            ret = treasure.transform.position;
        }

        return ret;
    }

    // Update is called once per frame
    void Update()
    {
        //Additionality 
        //1.) Exhauted stage which make robber run slower 
        //2.) when Extauted, the robber will try to hide while fleeing 
        //3.) If Not detected, robber will try to rob a treasure 
        agent.speed = defaultRunningSpeed * runningSpeed;
        Vector3 targetVec; 
        if (IsDetected()) 
        {                
            exhautGage += 1 * Time.deltaTime; 
            if (exhautGage < exhautGageMax)
            {
                runningSpeed = 2;
                targetVec = Flee(target.transform.position);
            }else
            {
                runningSpeed = 1;
                targetVec = HideAndFlee(target.transform.position); 
            }
        } 
        else
        {
            exhautGage -= 5 * Time.deltaTime;
            targetVec = FindTreasure(); 
        }

        agent.SetDestination(targetVec); 

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
