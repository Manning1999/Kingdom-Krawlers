using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{

    protected enum AxisToTrackOn { XY, YZ, XZ }

    [SerializeField]
    [Tooltip("This is the two axis that the camera should track it's target on")]
    protected AxisToTrackOn axisToTrackOn = AxisToTrackOn.XZ;

    [SerializeField]
    protected GameObject target = null;


    [SerializeField]
    [Tooltip("TRUE = Camera will lag slightly behind the target and gives a greater sense of movement. FALSE = Camera will always stay exactly above the target")]
    protected bool lagCamera = true;

    protected bool isLaggingCamera;

    [SerializeField]
    protected float speed = 5f;

    protected bool isChangingTarget = false;



    protected bool zoomIn;
    protected bool zoomOut;

    [SerializeField]
    protected float maxZoom;

    [SerializeField]
    protected float MinZoom;

    [SerializeField]
    protected float zoomSpeed = 30;

    protected Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        isLaggingCamera = lagCamera;
        cam = transform.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {


        if (isChangingTarget == true)
        {
            isLaggingCamera = true;


        }

        //get scroll wheel data for zooming in and out
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            zoomIn = true;
            zoomOut = false;

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            zoomOut = true;
            zoomIn = false;
        }




        //if scroll wheel not being used, don't zoom in or out
        if (Input.GetAxis("Mouse ScrollWheel") == 0f)
        {
            zoomIn = false;
            zoomOut = false;
        }
        if (cam.orthographic == false)
        {
            if (zoomIn == true)
            {
                if (transform.position.y > maxZoom)
                {
                    this.transform.Translate(Vector3.forward * Time.deltaTime * zoomSpeed);
                }
            }

            //zoom out by moving the camera further away
            if (zoomOut == true)
            {
                if (transform.position.y < MinZoom)
                {
                    this.transform.Translate(Vector3.back * Time.deltaTime * zoomSpeed);
                }
            }
        }
        else
        {
            if (zoomIn == true)
            {
                if (cam.orthographicSize > maxZoom)
                {
                    cam.orthographicSize -= zoomSpeed * Time.deltaTime;
                }
            }

            //zoom out by moving the camera further away
            if (zoomOut == true)
            {
                if (cam.orthographicSize < MinZoom)
                {
                    cam.orthographicSize += zoomSpeed * Time.deltaTime;
                }
            }
        }






        if (isLaggingCamera == true)
        {
            //speed at which to zoom in/out
            float interpolation = speed * Time.deltaTime;

            //makes the camera move in a way that lags slightly behind the tank and gives a better sense of movement
            Vector3 position = this.transform.position;

            switch (axisToTrackOn)
            {

                case AxisToTrackOn.XZ:
                    position.x = Mathf.Lerp(this.transform.position.x, target.transform.position.x, interpolation);
                    position.z = Mathf.Lerp(this.transform.position.z, target.transform.position.z, interpolation);
                    break;

                case AxisToTrackOn.XY:
                    position.x = Mathf.Lerp(this.transform.position.x, target.transform.position.x, interpolation);
                    position.y = Mathf.Lerp(this.transform.position.y, target.transform.position.y, interpolation);
                    break;

                case AxisToTrackOn.YZ:
                    position.y = Mathf.Lerp(this.transform.position.y, target.transform.position.y, interpolation);
                    position.z = Mathf.Lerp(this.transform.position.z, target.transform.position.z, interpolation);
                    break;

            }

            //Move to the destination determined above
            this.transform.position = position;
        }
        else
        {

            switch (axisToTrackOn)
            {

                case AxisToTrackOn.XZ:

                    this.transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
                    break;

                case AxisToTrackOn.XY:

                    this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
                    break;

                case AxisToTrackOn.YZ:

                    this.transform.position = new Vector3(transform.position.x, target.transform.position.y, target.transform.position.z);
                    break;
            }
        }
    

    }




    public void ChangeTarget(GameObject newTarget)
    {
        target = newTarget;
        isChangingTarget = true;
    }
}
