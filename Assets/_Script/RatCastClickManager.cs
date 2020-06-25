using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RatCastClickManager : MonoBehaviour
{
    [SerializeField]
    GameObject productionMenuObj;
    [SerializeField]
    GameObject workstationMenuObj;
    [SerializeField]
    GameObject wasteMenuObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Production")
                    productionMenuObj?.SetActive(true);
                else if (hit.collider.tag == "Workstation")
                    workstationMenuObj?.SetActive(true);
                else if (hit.collider.tag == "Waste")
                    wasteMenuObj?.SetActive(true);
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
