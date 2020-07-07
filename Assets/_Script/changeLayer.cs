using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeLayer : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static void ChangeLayersRecursively(Transform trans, string name)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in trans)
        {
            ChangeLayersRecursively(child, name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Npc")
        {
            if (other.gameObject.layer == 11)
            {
                ChangeLayersRecursively(other.gameObject.transform, "FurniturePrefabs");
            }
            else if (other.gameObject.layer == 10)
            {
                ChangeLayersRecursively(other.gameObject.transform, "outLigting");
            }
        }
    }
}
