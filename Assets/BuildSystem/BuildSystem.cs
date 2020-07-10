using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildSystem : MonoBehaviour
{
    public Action<int, int> ConfirmationFurniture;
    public Action<GameObject> DisplayFurniture;
    public Action<bool> building;

    [SerializeField]
    GameObject[] furniturePrefabs;
    [SerializeField]
    GameObject[] furniturePreviewPrefabs;
    [SerializeField]
    GameObject FurnitureSelection;
    [SerializeField]
    GameObject FurnitureDisplayMenu;
    [SerializeField]
    GameManager gm;
    public LayerMask layer;

    public BuildSelector selector;

    private GameObject preview;//this is the preview object that you will be moving around in the scene
    private PreviewObj previewScript;//this is the script that is sitting on that object
    private ItemInfo furnitureInfo;
    public GameObject curfurnGo;
    private int SatisfactionLevel;

    private bool isBuilding = false;
    //public GameObject 가구1;
    //public GameObject 가구2;

    public bool Relocation; // 재배치버튼
    public GameObject CancelBtn;
    public Sprite deletebtn;
    public Sprite CancelBtnspt;

    public void isBuildStartOn()
    {
        CancelBtn.GetComponent<Image>().sprite = deletebtn;
        gm.GetComponent<UIManager>().OnStartBuild(0);
        SatisfactionLevel -= 10;
        selector.isBuildStart = true;
        FurnitureSelection.SetActive(true);
        Relocation = true;
        building?.Invoke(true);
    }

    public bool IsPointerOverUIObject(Vector2 touchPos)
    {
        PointerEventData eventDataCurrentPosition
            = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = touchPos;

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current
        .RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }

    private void Update()
    {
        int mask = 1 << LayerMask.NameToLayer("FurniturePrefabs");

        if (!IsPointerOverUIObject(Input.mousePosition))
        {
            if (selector.isBuildStart == false)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0) && isBuilding && previewScript.CanBuild())//pressing LMB, and isBuiding = true, and the Preview Script -> canBuild = true
            {
                //BuildIt();//then build the thing
            }

            else if (Input.GetMouseButtonDown(1) && isBuilding)//stop build
            {
                previewScript.GetComponent<PreviewObj>().SetOriginalColor();
                StopBuild();
            }

            else if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hit, 9999f, mask);

                if (!isBuilding)
                {
                    if (hit.collider.CompareTag("Furniture") && Relocation)
                    {
                        selector.furnitureId = int.Parse(hit.collider.name.Substring(0, 3));
                        Destroy(hit.collider.gameObject);
                        isBuilding = true;
                        GameObject go = null;
                        foreach (var item in furniturePreviewPrefabs)
                        {
                            if (item.name.Contains(selector.furnitureId.ToString()))
                                go = item;
                        }
                        if (go != null)
                            NewBuild(go);

                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && isBuilding)//for rotation
        {
            preview.transform.Rotate(0f, 90f, 0f);//spins like a top, in 90 degree turns
        }

        if (isBuilding)
        {
            DoRay();
        }

    }

    public void NewBuild(GameObject _go)//this gets called by one of the buttons 
    {
        preview = Instantiate(_go, Vector3.zero, Quaternion.identity);//set the preview = to something
        previewScript = preview.GetComponent<PreviewObj>();//grab the script that is sitting on the preview
        preview.transform.position = new Vector3(0, -6f, 0);
        //isBuilding = true;//we can now build
    }

    private void StopBuild()
    {
        Destroy(preview);//get rid of the preview
        preview = null;//not sure if you need this actually
        previewScript = null;//
        selector.TogglePanel();
        isBuilding = false;
        FurnitureSelection.SetActive(false);
        building?.Invoke(false);
        if (curfurnGo != null)
            Destroy(curfurnGo);
    }

    private void BuildIt()//actually build the thing
    {
        DisplayFurniture?.Invoke(previewScript.Build(furnitureInfo));//just calls the Build() method on the previewScript
        StopBuild();
    }

    private void DoRay()//simple ray cast from the main camera. Notice there is no range
    {
        if (!IsPointerOverUIObject(Input.mousePosition))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out hit);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))//notice there is a layer that we are worried about
            {
                PositionObj(hit.point);
            }
        }


    }

    private void PositionObj(Vector3 _pos)
    {
        int x = Mathf.RoundToInt(_pos.x);//just round the x,y,z values to the nearest int
        //int y = Mathf.RoundToInt(_pos.y);//personal preferance to comment this out. I hard coded in my y value
        int z = Mathf.RoundToInt(_pos.z);

        preview.transform.position = new Vector3(x, -6f, z);//set the previews transform postion to a new Vector3 made up of the x,y,z that you roundedToInt

    }


    public bool GetIsBuilding()//just returns the isBuilding bool, so it cant get changed by another script
    {
        return isBuilding;
    }
    public void Rotation()
    {
        if (isBuilding)//for rotation
        {
            preview.transform.Rotate(0f, 90f, 0f);//spins like a top, in 90 degree turns
        }
    }
    public void Confirmation()
    {
        if (previewScript == null)
            return;

        if (previewScript.CanBuild())
        {
            SatisfactionLevel += 10;
            BuildIt();
            //selector.TogglePanel();
            ConfirmationFurniture?.Invoke(selector.furnitureId, SatisfactionLevel);
            selector.isBuildStart = false;
            building?.Invoke(false);
        }
    }

    public void Cancel()
    {
        if (!Relocation)
        {
            SatisfactionLevel -= 10;
        }
        StopBuild();
        Relocation = false;

        CancelBtn.GetComponent<Image>().sprite = CancelBtnspt;
    }

    public void OnGetFurnitureInfo(ItemInfo itemInfo)
    {
        furnitureInfo = new ItemInfo(itemInfo);
    }


}