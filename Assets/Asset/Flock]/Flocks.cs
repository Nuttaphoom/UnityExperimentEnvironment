using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager;
    public Transform _homeTarget; 
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
        _homeTarget = homeTarget;
        behaviorType = type; 
    }

    // Update is called once per frame
    void Update()
    {
        if (behaviorType == 1)
            ApplyFlockingRulesNO2();
        else if (behaviorType == 2)
            ApplyFlockingRulesNO3(); 
        
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

        //Moving away from the obstacle 
        //Debug.DrawRay(transform.position, transform.forward , Color.yellow);

        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.forward, out hit, 3 ))
        //{
        //    Debug.DrawLine(transform.position, hit.point,Color.red);
        //    transform.rotation = Quaternion.Slerp(this.transform.rotation,
        //                               Quaternion.LookRotation(Vector3.Reflect(transform.forward, hit.normal)),
        //                               myManager.rotationSpeed * Time.deltaTime);
        //}

        // if we have neighbors, then we calculate all 3 rules:
        // 1. average of center
        // 2. average of speed [average of heading direction]
        // 3. if too close to any neighbor, move away from the neighbor.
        // 4. if too close too obstacle, move away from the obstacle
        // Then, calculate the right direction for group behavior.
        if (nbSize > 0)
        {
            // RESULT:
            // turn toward the direction of group behaviors: Quaternion.Slerp()
            // computer 'speed' for Translate() afterward.

            // do average
            nbCenter = nbCenter / nbSize;
            //coming closer to FlockManager
            nbCenter = (nbCenter + _homeTarget.position) / 2; 

            // computer target direction
            Vector3 targetDir = (nbCenter + nbAvoid) - this.transform.position;

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

        // if (Physics.Raycast(transform.position, transform.up, out hit, 3))
        //{
        //    Debug.DrawLine(transform.position, hit.point, Color.red);
        //    transform.rotation = Quaternion.Slerp(this.transform.rotation,
        //                               Quaternion.LookRotation(transform.up * -1),
        //                               myManager.rotationSpeed * Time.deltaTime);
        //}

        // if (Physics.Raycast(transform.position, -1 * transform.up, out hit, 3))
        //{
        //    Debug.DrawLine(transform.position, hit.point, Color.red);
        //    transform.rotation = Quaternion.Slerp(this.transform.rotation,
        //                               Quaternion.LookRotation(transform.up),
        //                               myManager.rotationSpeed * Time.deltaTime);
        //}

    }

}
