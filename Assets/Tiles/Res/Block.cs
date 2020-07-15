using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]

public class Block : MonoBehaviour
{
    private Vector3 snap = new Vector3(10f, 5f, 10f);
    private Vector3 previousPosition = new Vector3(0f, 0f, 0f);
    private Quaternion previousRotation;

    private const int LAYER_LEVEL = (1 << 9);
    private Vector3 raycastPos =  new Vector3(0f, 4.5f, 0f);

    private bool update = false;

    public enum BlockType
    {
        Block,
        Slope,
        SlopeBridge,
        AngledBlock,
        AngledSlopeStart,
        AngledSlopeMid,
        AngledSlopeEnd
    }

    public enum TerrainType
    {
        Terrain,
        Block,
        Special
    }

    public TerrainType type;
    public BlockType blockType;

    private GameObject trimN;
    private GameObject trimE;
    private GameObject trimS;
    private GameObject trimW;
    private GameObject trimNE;
    private GameObject trimSE;
    private GameObject trimSW;
    private GameObject trimNW;
    private GameObject trimAngle;

    public Mesh Wall;
    public Mesh AngledWall;
    public Mesh SlopeWall;
    public Mesh BridgeWall;

    void Start()
    {
        SetTrimVisibility();
    }

    void Update()
    {
        if (!Application.IsPlaying(gameObject))
        {
            Vector3 pos = transform.localPosition;
            pos.x = Mathf.Floor(pos.x / snap.x) * snap.x;
            pos.y = Mathf.Floor(pos.y / snap.y) * snap.y;
            pos.z = Mathf.Floor(pos.z / snap.z) * snap.z;
            if (update)
            {
                if (pos == transform.localPosition)
                {
                    update = false;
                    SetTrimVisibility();
                }
            }
            transform.localPosition = pos;
            if ((pos != previousPosition) || (transform.rotation != previousRotation))
            {
                //Debug.Log("Update tile " + gameObject.name);
                previousPosition = pos;
                previousRotation = transform.rotation;
                update = true;
            }
        }
    }

    void SetTrimVisibility()
    {
        RaycastHit hitN;
        RaycastHit hitE;
        RaycastHit hitS;
        RaycastHit hitW;
        //RaycastHit hitUp;
        Vector3 pos = transform.position + raycastPos;
        //Debug.Log("Update tile at " + transform.position.ToString());

        Physics.Raycast(pos + transform.forward, transform.forward, out hitN, 6.0f, LAYER_LEVEL);
        Physics.Raycast(pos + transform.forward * -1f, transform.forward * -1f, out hitS, 6.0f, LAYER_LEVEL);
        Physics.Raycast(pos + transform.right, transform.right, out hitE, 6.0f, LAYER_LEVEL);
        Physics.Raycast(pos + transform.right * -1f, transform.right * -1f, out hitW, 6.0f, LAYER_LEVEL);

        UpdateWall(hitN, trimN);
        UpdateWall(hitE, trimE);
        UpdateWall(hitS, trimS);
        UpdateWall(hitW, trimW);

    }

    void UpdateWall(RaycastHit hit, GameObject trim)
    {
        if (trim != null)
        {
            if (hit.collider == null)
            {
                trim.SetActive(true);
            } else {
                trim.SetActive(false);
            }
        }
    }

    void UpdateCorner(RaycastHit hit0, RaycastHit hit1, GameObject trim)
    {
        if (trim != null)
        {
            if ((hit0.collider == null) && (hit1.collider == null))
            {
                trim.SetActive(true);
            } else {
                trim.SetActive(false);
            }
        }
    }
}
