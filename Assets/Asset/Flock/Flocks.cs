using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager;
    public Transform _Target; 
    float speed;

    float time; 
    int behaviorType = 0; 

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }

    public void Init(Transform homeTarget , int type)
    {
        _Target = homeTarget;
        behaviorType = type;

        if (behaviorType == 2) 
            transform.Rotate(Vector3.up, Random.Range(-180.0f, 180.0f)); 
    }

    // Update is called once per frame
    void Update()
    {
        if (behaviorType == 1)
            ApplyFlockingRulesNO2();
        else if (behaviorType == 2)
            ApplyFlockingRulesNO3();
        else if (behaviorType == 3)
            ApplyFlockingRulesNO4(); 
        
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void ApplyFlockingRulesNO1()
    {
        // check our neighbors within neighborDistance
        GameObject[] all = myManager.allFish;

        Vector3 nbCenter = Vector3.zero;     // Rule #1
        float nbSpeed = 0.0f;                // Rule #2
        Vector3 nbAvoid = Vector3.zero;      // Rule #3
        int nbSize = 0;

        foreach (GameObject fish in all)
        {
            if (fish != this.gameObject)
            {
                // calculate the distance & check if it is our neighbor.
                float nDistance = Vector3.Distance(fish.transform.position, this.transform.position);
                if (nDistance <= myManager.neighborDistance)
                {
                    // collect data for each rules in group behavior
                    // Rule#1 : grouping toward the center
                    nbCenter += fish.transform.position;
                    nbSize++;

                    // Rule#2 : moving along the flock
                    nbSpeed += fish.GetComponent<Flock>().speed;

                    // Rule#3 : moving away when too close
                    if (nDistance < 1.0f)
                    {
                        nbAvoid += this.transform.position - fish.transform.position;
                    }
                }
            }
        }

        // if we have neighbors, then we calculate all 3 rules:
        // 1. average of center
        // 2. average of speed [average of heading direction]
        // 3. if too close to any neighbor, move away from the neighbor.
        // Then, calculate the right direction for group behavior.
        if (nbSize > 0)
        {
            // RESULT:
            // turn toward the direction of group behaviors: Quaternion.Slerp()
            // computer 'speed' for Translate() afterward.

            // do average
            nbCenter = nbCenter / nbSize;
            nbSpeed = nbSpeed / nbSize;

            // computer target direction
            Vector3 targetDir = (nbCenter + nbAvoid) - this.transform.position;

            // turning toward target direction
            transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                   Quaternion.LookRotation(targetDir),
                                                   myManager.rotationSpeed * Time.deltaTime);
        }
    }

    void ApplyFlockingRulesNO2()
    {

        // check our neighbors within neighborDistance
        GameObject[] all = myManager.allFish;

        Vector3 nbCenter = Vector3.zero ;     // Rule #1
        float nbSpeed = 0.0f;                // Rule #2
        Vector3 nbAvoid = Vector3.zero;      // Rule #3
        int nbSize = 0;

        foreach (GameObject fish in all)
        {
            if (fish != this.gameObject)
            {
                // calculate the distance & check if it is our neighbor.
                float nDistance = Vector3.Distance(fish.transform.position, this.transform.position);
                if (nDistance <= myManager.neighborDistance / 2)
                {
                    // collect data for each rules in group behavior
                    // Rule#1 : grouping toward the center
                    nbCenter += fish.transform.position;
                    nbSize++;

                    // Rule#2 : moving along the flock
                    nbSpeed += fish.GetComponent<Flock>().speed;

                    // Rule#3 : moving away when too close
                    if (nDistance < 1.0f)
                    {
                        nbAvoid += this.transform.position - fish.transform.position;
                    }


                }
            }
        }

        // if we have neighbors, then we calculate all 3 rules:
        // 1. average of center
        // 2. average of speed [average of heading direction]
        // 3. if too close to any neighbor, move away from the neighbor.
        // Then, calculate the right direction for group behavior.
        if (nbSize > 0)
        {
            // RESULT:
            // turn toward the direction of group behaviors: Quaternion.Slerp()
            // computer 'speed' for Translate() afterward.

            // do average
            nbCenter = nbCenter / nbSize;
            //coming closer to FlockManager
            nbCenter = (nbCenter + _Target.transform.position) / 2    ; 

            // computer target direction
            Vector3 targetDir = (nbCenter + nbAvoid  ) - this.transform.position;
 
            // turning toward target direction
            transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                   Quaternion.LookRotation(targetDir),
                                                   myManager.rotationSpeed * Time.deltaTime);
        }
    }

    Coroutine rotationCoroutine;

    private IEnumerator RotateToDirection(Vector3 direction )
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                       Quaternion.LookRotation(direction),
                                        t  );
            yield return null; 
        }

        rotationCoroutine = null; 
    }
    void ApplyFlockingRulesNO3()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.25f))
        {
             
            
            //rotationCoroutine = StartCoroutine(RotateToDirection((Vector3.Reflect(transform.forward, hit.normal)))) ;
            Debug.DrawLine(transform.position, hit.point, Color.red);
            transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                               Quaternion.LookRotation(Vector3.Reflect(transform.forward, hit.normal)),
                                               1);
            Debug.Log("myManager.rotationSpeed * Time.deltaTime : " + myManager.rotationSpeed ); 
        }
 

    }

    void ApplyFlockingRulesNO4()
    {
        Vector3 directVector = _Target.transform.position - transform.position;

        float degree = Vector3.Angle(directVector, transform.forward);

        if (directVector.magnitude > 1.0f)
        {
            // Apply the rotation to the object
            transform.rotation = Quaternion.LookRotation(directVector.normalized);
        }
        //Rotate around the target 
        else if (degree > 5 || degree < 3) {

            Vector3 targetRotation = Vector3.Cross(directVector.normalized, transform.up);

            // Apply the rotation to the object
            transform.rotation = Quaternion.LookRotation(-targetRotation.normalized); 
        }

        //Getting closer to the target 


    }

}
