using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulateHPBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer MR = GetComponent<MeshRenderer>() ;
        MR.material.SetFloat("_HPSlider",(float) (MR.material.GetFloat("_HPSlider") - 0.25)); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
