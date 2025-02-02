using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private KeyCode leftKey = KeyCode.Q;
    [SerializeField]
    private KeyCode rightKey = KeyCode.E;
    [SerializeField]
    private KeyCode overheadKey = KeyCode.Space;

    public int xAngle = 60;

    public Material wallMat;

    private GameObject cam;
    private GameObject player;

    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float delayTime = 0.25f;
    private float timeDiff;

    private float targetYRotation, targetXRotation = -60;

    public int side = 2;


    void Start()
    {
        side = 2;
        targetYRotation = transform.eulerAngles.y;
        timeDiff = 0.0f;
        cam = Camera.main.gameObject;
        targetXRotation = -xAngle;
        player = GameObject.FindGameObjectWithTag("Player");

        UniversalAdditionalCameraData camData = Camera.main.GetUniversalAdditionalCameraData();
        camData.cameraStack.Add(GameObject.FindGameObjectWithTag("OverlayCamera").GetComponent<Camera>());
    }

    private void Update()
    {
        //Debug.Log("c.side: " + side);
        if (Input.GetKeyDown(leftKey) && Time.time >= timeDiff) {
            timeDiff = Time.time + delayTime;
            targetYRotation -= 90;
            side++;
            Debug.Log("SIDE: " + side);
            if (targetYRotation < 0) {
                targetYRotation += 360;
            }
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, (transform.eulerAngles.y - 90) % 360, transform.eulerAngles.z);
            StartCoroutine(QueryHelper.record("Camera:left"));
        }

        if (Input.GetKeyDown(rightKey) && Time.time >= timeDiff)
        {
            timeDiff = Time.time + delayTime;
            targetYRotation += 90;
            side--;
            Debug.Log("SIDE: " + side);
            if (targetYRotation > 360)
            {
                targetYRotation -= 360;
            }
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, (transform.eulerAngles.y + 90) % 360, transform.eulerAngles.z);
            StartCoroutine(QueryHelper.record("Camera:right"));
        }
        if (Input.GetKeyDown(overheadKey))
        {
            targetYRotation -= 30;
            targetXRotation = -90;
            if (targetYRotation > 360)
            {
                targetYRotation -= 360;
            }
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, (transform.eulerAngles.y + 90) % 360, transform.eulerAngles.z);
            StartCoroutine(QueryHelper.record("Camera:overhead_start"));
        }

        if (Input.GetKeyUp(overheadKey))
        {
            targetYRotation += 30;
            targetXRotation = -xAngle;
            if (targetYRotation > 360)
            {
                targetYRotation -= 360;
            }
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, (transform.eulerAngles.y + 90) % 360, transform.eulerAngles.z);
            StartCoroutine(QueryHelper.record("Camera:overhead_end"));
        }

        if (side < 0) side = 3;
        if (side > 3) side = 0;

        transform.eulerAngles = new Vector3(Mathf.LerpAngle(transform.eulerAngles.x, targetXRotation, Time.deltaTime * rotationSpeed), Mathf.LerpAngle(transform.eulerAngles.y, targetYRotation, Time.deltaTime * rotationSpeed), transform.eulerAngles.z);

    }
}
