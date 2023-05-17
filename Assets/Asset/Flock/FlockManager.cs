using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject fishPrefab;
    public int numFish = 20;
    public GameObject[] allFish;
    public Vector3 swimLimits = new Vector3(5, 5, 5);

    [Header("Fish Settings")]
    [Range(0.0f, 5.0f)]
    public float minSpeed = 3.0f;
    [Range(0.0f, 5.0f)] 
    public float maxSpeed = 5.0f;

    [Range(1.0f, 20.0f)]
    public float neighborDistance = 10.0f ;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed = 2.0f;

    [SerializeField]
    private Transform[] _homeTransforms;

    [SerializeField]
    private Transform[] _huntTarget; 

    [SerializeField]
    private Transform[] _spawningTank; 

    // Start is called before the first frame update
    void Start()
    {
        allFish = new GameObject[numFish * 3];
        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = _spawningTank[0].transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                                                                Random.Range(-swimLimits.y, swimLimits.y),
                                                                Random.Range(-swimLimits.z, swimLimits.z));
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            allFish[i].GetComponent<Flock>().myManager = this;
            allFish[i].GetComponent<Flock>().Init(_homeTransforms[i % 2],1) ; 
        }

        for (int i = numFish ; i < numFish * 2; i++)
        {
            Vector3 pos = _spawningTank[1].transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                                                                Random.Range(-swimLimits.y, swimLimits.y),
                                                                Random.Range(-swimLimits.z, swimLimits.z));
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            allFish[i].GetComponent<Flock>().myManager = this;
            allFish[i].GetComponent<Flock>().Init(_homeTransforms[i % 2],2);
        }

        for (int i = numFish * 2; i < numFish * 3; i++)
        {
            Vector3 pos = _spawningTank[2].transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                                                                Random.Range(-swimLimits.y, swimLimits.y),
                                                                Random.Range(-swimLimits.z, swimLimits.z));
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            allFish[i].GetComponent<Flock>().myManager = this;
            allFish[i].GetComponent<Flock>().Init(_huntTarget[i % _huntTarget.Count()], 3);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

  
}
