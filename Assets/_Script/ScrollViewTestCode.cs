using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewTestCode : MonoBehaviour
{
    [SerializeField]
    GameObject contentPrefabs;
    [SerializeField]
    Transform _viewTr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Addelement()
    {
        var instance = Instantiate(contentPrefabs);
        instance.transform.SetParent(_viewTr);

    }
}
