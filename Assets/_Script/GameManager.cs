using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    XMLManager xmlManager;

    // Start is called before the first frame update
    void Start()
    {
        xmlManager = gameObject.AddComponent<XMLManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
