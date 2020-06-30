using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//https://blog.naver.com/PostView.nhn?blogId=cheeryca&logNo=220799350908&proxyReferer=https%3A%2F%2Fwww.google.com%2F
//https://www.youtube.com/watch?v=qbl38iPitVY
//https://cyoungesuno.tistory.com/entry/Unity-C%EC%9C%A0%EB%8B%88%ED%8B%B0-%EC%B9%B4%EB%A9%94%EB%9D%BC-%EC%A4%8C%EC%9D%B8-%EC%A4%8C%EC%95%84%EC%9B%83-Pinch-Zoom-in-out 확대 축소
public class CameraManager : MonoBehaviour
{
    Vector2 StartPosition;
    Vector2 DragStartPosition;
    Vector2 DragNewPosition;
    Vector2 Finger0Position;
    float DistanceBetweenFingers;
    bool isZooming = false;
    bool isRotating = false;

    [SerializeField]
    Text debugText;
    [SerializeField]
    Text debugText2;
    [SerializeField]
    Text debugText3;
    [SerializeField]
    Text debugText4;
    [SerializeField]
    Text debugText5;
    [SerializeField]
    Text debugText6;
    [SerializeField]
    Text debugText7;
    [SerializeField]
    Text debugText8;
    [SerializeField]
    Text debugText9;

    const float minPanDistance = 0;
    public float turnAngleDelta;
    public float turnAngle;
    const float pinchTurnRatio = Mathf.PI / 2;
    const float minTurnAngle = 2;

    float oldAngle = 0f;
    float moveSpeed = 500.0f;

    Vector2 startVector;
    float rotGestureWidth;
    public const float TOUCH_ROTATION_WIDTH = 1; // Always3
    public const float TOUCH_ROTATION_MINIMUM = 1;

    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    bool isBuilding = false; // 가구 배치 중이라면 카메라 회전 막기

    //카메라 회전 및 축소 확대 제한
    [SerializeField]
    float maxRotate;
    [SerializeField]
    float maxZoom;
    [SerializeField]
    float minRotate;
    [SerializeField]
    float minZoom;

    [SerializeField]
    float perspectiveZoomSpeed = 0.5f;
    [SerializeField]
    float orthoZoomSpeed = 0.5f;

    [SerializeField]
    Vector3 dayCameraPos;
    [SerializeField]
    Vector3 EndDayCameraPos;

    bool isEndDay = false;

    //void update()
    //{
    //    if (input.getmousebuttondown(0))
    //    {
    //        dragorigin = input.mouseposition;
    //        return;
    //    }

    //    if (!input.getmousebutton(0)) return;

    //    vector3 pos = camera.main.screentoviewportpoint(input.mouseposition - dragorigin);
    //    vector3 move = new vector3(pos.x * dragspeed, 0, pos.y * dragspeed);

    //    transform.translate(move, space.world);
    //    }


    // Update is called once per frame
    void LateUpdate()
    {
        if (isEndDay)
            return;

        if (IsPointerOverUIObject())
            return;

        Quaternion desiredRotation = transform.rotation;

        DetectTouchMovement.Calculate();

        //debugText8.text = "eulerAngles : " + transform.localRotation.eulerAngles;
        debugText9.text = "position : " + transform.position;

        if (Input.touchCount == 0 && (isZooming || isRotating))
        {
            isZooming = false;
            isRotating = false;
        }

        if (Input.touchCount == 1)
        {
            if (!isZooming && !isRotating && !isBuilding)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector2 NewPosition = GetWorldPosition();
                    Vector3 PositionDifference = NewPosition - StartPosition;
                    //Vector2 PositionDifference = (NewPosition - StartPosition).normalized;
                    Vector3 vec = new Vector3(PositionDifference.x, PositionDifference.y, 0);
                    //transform.Translate(-PositionDifference);
                    transform.Translate(-PositionDifference * moveSpeed * Time.deltaTime);
                    //transform.position -= vec * moveSpeed * Time.deltaTime;
                    debugText7.text = "NewPosition : " + NewPosition;
                    debugText8.text = "StartPosition : " + StartPosition;
                }
                StartPosition = GetWorldPosition();
            }
        }
        else if (Input.touchCount == 2)
        {
            debugText.text = "turnAngleDelta : " + Mathf.Abs(DetectTouchMovement.turnAngleDelta);
            debugText2.text = "pinchDistanceDelta : " + Mathf.Abs(DetectTouchMovement.pinchDistanceDelta);
            debugText3.text = "isRotating : " + isRotating;
            debugText4.text = "isZooming  : " + isZooming;

            if (Mathf.Abs(DetectTouchMovement.turnAngleDelta) > 0.5 && !isZooming)  //rotate
            {
                isRotating = true;
                debugText.text = "isRotating  : " + isRotating;
            }
            if (Mathf.Abs(DetectTouchMovement.pinchDistanceDelta) > 5 && !isRotating)   //zoom
            {

                isZooming = true;

                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                debugText5.text = "fieldOfView : " + GetComponent<Camera>().fieldOfView;
                debugText8.text = "deltaMagnitudeDiff" + deltaMagnitudeDiff;

                if (maxZoom >= GetComponent<Camera>().fieldOfView && deltaMagnitudeDiff < 0)
                {
                    debugText6.text = "true bigger";
                    deltaMagnitudeDiff = 0;
                }

                if (minZoom <= GetComponent<Camera>().fieldOfView && deltaMagnitudeDiff > 0)
                {
                    deltaMagnitudeDiff = 0;
                }

                if (GetComponent<Camera>().orthographic)
                {
                    GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
                    GetComponent<Camera>().orthographicSize = Mathf.Max(GetComponent<Camera>().orthographicSize, 0.1f);
                }
                else
                {
                    GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
                    GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 0.1f, 179.9f);
                }

                //DragNewPosition = GetWorldPositionOfFinger(1);
                //Vector2 PositionDifference = DragNewPosition - DragStartPosition;
                //float tempMagnitude;
                //    tempMagnitude = Mathf.Clamp(PositionDifference.magnitude, 0.01f, 1);
                //debugText5.text = "move.magnitude? : ";
                //debugText6.text = "tempMagnitude? : " + tempMagnitude;
                //if (tempMagnitude > 0.8)
                //{
                //    tempMagnitude = Mathf.Clamp(tempMagnitude, 0.01f, 0.5f);
                //    //return;
                //}
                //if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
                //{
                //    if (Vector2.Distance(DragNewPosition, Finger0Position) < DistanceBetweenFingers)
                //        GetComponent<Camera>().orthographicSize += (tempMagnitude);

                //    if (Vector2.Distance(DragNewPosition, Finger0Position) >= DistanceBetweenFingers)
                //        GetComponent<Camera>().orthographicSize -= (tempMagnitude);
                //}
                //DistanceBetweenFingers = Vector2.Distance(DragNewPosition, Finger0Position);

                //DragStartPosition = GetWorldPositionOfFinger(1);
                //Finger0Position = GetWorldPositionOfFinger(0);
            }

            if (isRotating)
                RotateCamera();
            //else if (isZooming)
                //ZoomCamera();


        }



    }

    void ZoomCamera()
    {
        isZooming = true;

            DragNewPosition = GetWorldPositionOfFinger(1);
        Vector2 PositionDifference = DragNewPosition - DragStartPosition;

        if (Vector2.Distance(DragNewPosition, Finger0Position) < DistanceBetweenFingers)
            GetComponent<Camera>().orthographicSize += (PositionDifference.magnitude);

        if (Vector2.Distance(DragNewPosition, Finger0Position) >= DistanceBetweenFingers)
            GetComponent<Camera>().orthographicSize -= (PositionDifference.magnitude);

        DistanceBetweenFingers = Vector2.Distance(DragNewPosition, Finger0Position);


        DragStartPosition = GetWorldPositionOfFinger(1);
        Finger0Position = GetWorldPositionOfFinger(0);

        debugText.text = "왜 호출이 안되??";
    }

    void RotateCamera()
    {
        //두지점의 좌표값을 가져온다. 
        var _firstPoint = Input.GetTouch(0).position;
        var _secondPoint = Input.GetTouch(1).position;

        //두점 사이의 x의 길이 y의 길이를 구한다.
        var v2 = _firstPoint - _secondPoint;

        //두 지점의 각도를 구한다. -3.14 ~ 3.14
        var newAngle = Mathf.Atan2(v2.y, v2.x);

        //180/Mathf.PI 를 곱해서 -180~180 범위로 변경한다.
        newAngle = newAngle * 180 / Mathf.PI;

        //뒤 각도에서 앞 각도빼기.
        var deltaAngle = Mathf.DeltaAngle(newAngle, oldAngle);
        //비교할 old값 셋팅
        oldAngle = newAngle;

        //첫 터치에는 newAngle 값밖에 없기때문에ㅐ deltaAngle을 구할수가 없다. 
        //그러므로 터치가 처음시작될경우는 제외시켜주고, TouchPhase 가 Moved일 경우에 회전시켜준다. 
        if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(1).phase == TouchPhase.Began || Mathf.Abs(deltaAngle) > 11)
        {
            debugText.text = "얘는 일 안함??";
            return;
        }
        else if (Mathf.Abs(DetectTouchMovement.turnAngleDelta) > 0 && !isZooming/*Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved && !isZooming*/)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                debugText.text = "해치웠나?";
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit[] hits = Physics.RaycastAll(ray, 30);

            Vector3 position = new Vector3();
            foreach (RaycastHit hit in hits)
            {
                position = hit.transform.position;
            }
            transform.RotateAround(position, Vector3.up, -deltaAngle);
            debugText.text = "deltaAngle?" + deltaAngle;
            isRotating = true;
        }
    }

    Vector2 GetWorldPosition()
    {
        return GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition);
    }

    Vector2 GetWorldPositionOfFinger(int FingerIndex)
    {
        return GetComponent<Camera>().ScreenToWorldPoint(Input.GetTouch(FingerIndex).position);
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void OnEndDay(bool isEndDay)
    {
        //if (isEndDay)
        //    transform.position = EndDayCameraPos;
        //else
        //    transform.position = dayCameraPos;
        StartCoroutine(WaitFadeScene(isEndDay));
        this.isEndDay = isEndDay;
    }

    IEnumerator WaitFadeScene(bool isEndDay)
    {
        yield return new WaitForSeconds(0.45f);

        if (isEndDay)
        {
            transform.position = EndDayCameraPos;
            transform.eulerAngles = new Vector3(35.861f, 55.052f, -0.505f);
            GetComponent<Camera>().fieldOfView = 30;
        }
        else
        {
            transform.position = dayCameraPos;
            transform.eulerAngles = new Vector3(35.861f, 55.052f, -0.505f);
            GetComponent<Camera>().fieldOfView = 30;
        }
    }

    public void OnStartBuild(int temp)
    {
        isBuilding = true;
    }

    public void OnEndBuild(int temp)
    {
        isBuilding = false;

    }
}
