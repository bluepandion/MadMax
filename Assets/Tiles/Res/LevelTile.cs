using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]

public class LevelTile : MonoBehaviour
{
    private Vector3 snap = new Vector3(10f, 5f, 10f);
    private Vector3 previousPosition = new Vector3(0f, 0f, 0f);
    private Quaternion previousRotation;

    private const int LAYER_LEVEL = (1 << 9);
    private Vector3 raycastPos =  new Vector3(0f, 2f, 0f);

    private bool update = false;

    public enum TerrainType
    {
        Terrain,
        Block,
        Special
    }

    public TerrainType type;

    private GameObject trimN = null;
    private GameObject trimE = null;
    private GameObject trimS = null;
    private GameObject trimW = null;
    private GameObject trimNE = null;
    private GameObject trimSE = null;
    private GameObject trimSW = null;
    private GameObject trimNW = null;

    public GameObject skin;
    private WallSkin _skin;

    private Mesh wall;
    private Mesh angledWall;
    private Mesh slopeWall;
    private Mesh bridgeWall;
    private Mesh corner;

    private bool validate = false;

    void OnValidate()
    {
        validate = true;
    }

    void ValidateSkin()
    {
        bool updateMeshes = false;
        WallSkin newSkin = null;

        if (skin == _skin)
        {
            return;
        }

        Debug.Log("Revalidate Tile skin ");

        if (skin != null)
        {
            Debug.Log(" - Skin " + skin.name);
            if ( skin.TryGetComponent(out WallSkin s) )
            {
                Debug.Log(" -- Set new skin");
                newSkin = s;
            } else {
                Debug.Log(" -- Not a valid WallSkin definition");
            }

            if ((_skin != null) && (newSkin == null))
            {
                Debug.Log(" -- Set new skin");
                _skin = null;
                updateMeshes |= ValidateSkinItem(ref wall, null, "Wall");
            }
        }
        if (_skin != newSkin)
        {
            _skin = newSkin;
            updateMeshes |= ValidateSkinItem(ref wall, _skin.wallMesh, "Wall");
            GetComponent<MeshRenderer>().material = _skin.terrainMaterial;
        }
        if (updateMeshes) { SetMeshes(); }
    }

    bool ValidateSkinItem(ref Mesh currentItem, Mesh skinItem, string name)
    {
        if (currentItem != skinItem)
        {
            Debug.Log(" - Changed " + name);
            currentItem = skinItem;
            return true;
        }
        return false;
    }

    void Start()
    {
        Transform t;

        t = transform.Find("N");
        if (t) { trimN = t.gameObject; }
        t = transform.Find("E");
        if (t) { trimE = t.gameObject; }
        t = transform.Find("S");
        if (t) { trimS = t.gameObject; }
        t = transform.Find("W");
        if (t) { trimW = t.gameObject; }

        ValidateSkin();
        if (Application.IsPlaying(gameObject))
        {
            this.enabled = false;
        }
    }

    void Update()
    {
        if (!Application.IsPlaying(gameObject))
        {
            if (validate)
            {
                ValidateSkin();
                validate = false;
            }
            Vector3 pos = transform.localPosition;
            pos.x = Mathf.Floor(pos.x / snap.x) * snap.x;
            pos.y = Mathf.Floor(pos.y / snap.y) * snap.y;
            pos.z = Mathf.Floor(pos.z / snap.z) * snap.z;
            if (update)
            {
                if (pos == transform.localPosition)
                {
                    update = false;
                    SetTrimVisibility(true);
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

    public void SetTrimVisibility(bool recursive)
    {
        RaycastHit hitN;
        RaycastHit hitE;
        RaycastHit hitS;
        RaycastHit hitW;
        //RaycastHit hitUp;
        Vector3 pos = transform.position + raycastPos;
        Debug.Log(pos.ToString());

        hitN = CheckTileInDirection(pos, transform.forward, recursive);
        hitS = CheckTileInDirection(pos, transform.forward * -1f, recursive);
        hitE = CheckTileInDirection(pos, transform.right, recursive);
        hitW = CheckTileInDirection(pos, transform.right * -1, recursive);

        UpdateWall(hitN, trimN);
        UpdateWall(hitE, trimE);
        UpdateWall(hitS, trimS);
        UpdateWall(hitW, trimW);
    }

    RaycastHit CheckTileInDirection(Vector3 pos, Vector3 dir, bool recursive)
    {
        RaycastHit hit;
        Physics.Raycast(pos + dir, dir, out hit, 6.0f, LAYER_LEVEL);
        if (recursive)
        {
            if (hit.collider)
            {
                if (hit.collider.gameObject.TryGetComponent(out LevelTile t))
                {
                    t.SetTrimVisibility(false);
                }
            }
        }
        return hit;
    }

    void SetMaterials()
    {
        foreach (Transform child in transform)
        {

        }
    }

    void SetMeshes()
    {
        Debug.Log("LevelTile :: SetMeshes()");
        foreach (Transform child in transform)
        {
            MeshFilter m = child.GetComponent<MeshFilter>();
            if (_skin != null)
            {
                MeshRenderer r = child.GetComponent<MeshRenderer>();
                r.material = _skin.wallMaterial;
            }
            if (child.gameObject.tag == "Trim-Wall")
            {
                m.mesh = wall;
            }
        }
    }

    void UpdateWall(RaycastHit hit, GameObject trim)
    {
        if (trim != null)
        {
            if (hit.collider)
            {
                Debug.Log(trim.name + " hit " + hit.collider.gameObject.name);
            }
            trim.SetActive((hit.collider == null));
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

    void OnDestroy()
    {
        SetTrimVisibility(true);
    }
}

