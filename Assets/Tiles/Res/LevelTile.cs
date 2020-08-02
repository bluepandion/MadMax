using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTile : MonoBehaviour
{
    private const int LAYER_LEVEL = (1 << 9);
    public Vector3 raycastPos =  new Vector3(0f, 2f, 0f);

    private GameObject trimN = null;
    private GameObject trimE = null;
    private GameObject trimS = null;
    private GameObject trimW = null;
    //private GameObject trimNE = null;
    //private GameObject trimSE = null;
    //private GameObject trimSW = null;
    //private GameObject trimNW = null;

    public LevelTileSkin skin;
    private LevelTileSkin previousSkin = null;

    private bool update = false;
    public bool requestUpdate
    {
        get
        {
            return update;
        }
        set
        {
            update = value;
        }
    }

    void OnValidate()
    {
        requestUpdate = true;
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (update)
        {
            update = false;
            ValidateSkin();
        }
    }

    public void Init()
    {
        //Debug.Log("LevelTile :: Init()");
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
        SetTrimVisibility(false);
    }

    public void ValidateSkin()
    {
        if (skin == previousSkin) { return; }
        previousSkin = skin;
        //Debug.Log("LevelTile :: ValidateSkin()");
        if (skin)
        {
            //Debug.Log(skin.object.name);
            SetMesh(trimN);
            SetMesh(trimE);
            SetMesh(trimS);
            SetMesh(trimW);
            MeshRenderer r = gameObject.GetComponent<MeshRenderer>();
            r.material = skin.terrainMaterial;
        } else {
            //Debug.Log("Clear skin");
            ClearSkin();
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
        //Debug.Log(pos.ToString());

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

    void SetMesh(GameObject o)
    {
        if (!o) { return; }
        MeshFilter m = o.GetComponent<MeshFilter>();
        if (!m) { return; }

        if (skin)
        {
            o.SetActive(true);
            MeshRenderer r = o.GetComponent<MeshRenderer>();
            r.material = skin.wallMaterial;
            switch (o.gameObject.tag)
            {
                case "Tile-Wall":
                    m.mesh = skin.wallMesh;
                    break;
                case "Tile-AngleWall":
                    m.mesh = skin.angledWallMesh;
                    break;
                case "Tile-SlopeWall":
                    m.mesh = skin.slopeWallMesh;
                    break;
            }
        } else {
            o.SetActive(false);
        }
    }

    void ClearSkin()
    {
        if (trimN) { trimN.SetActive(false); }
        if (trimE) { trimE.SetActive(false); }
        if (trimS) { trimS.SetActive(false); }
        if (trimW) { trimW.SetActive(false); }
        //MeshRenderer r = gameObject.GetComponent<MeshRenderer>();
        //r.material = _skin.wallMaterial;
    }

    void UpdateWall(RaycastHit hit, GameObject trim)
    {
        if (trim != null)
        {
            if (hit.collider)
            {
                //Debug.Log(trim.name + " hit " + hit.collider.gameObject.name);
            }
            trim.SetActive((hit.collider == null));
        }
    }
}
