using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LevelTileEditor : MonoBehaviour
{
    private const float snapX = 10f;
    private const float snapY = 5f;
    private const float snapZ = 10f;

    private LevelTile tile;

    void Start()
    {
        if (!Application.IsPlaying(gameObject))
        {
            tile = gameObject.GetComponent<LevelTile>();
            if (tile)
            {
                tile.Init();
            } else {
                Debug.Log("LevelTileEditor Error: LevelTile component not found");
            }
        }
    }

    void Update()
    {
        if (!Application.IsPlaying(gameObject))
        {
            if (tile.requestUpdate)
            {
                tile.ValidateSkin();
            }
            if (transform.hasChanged)
            {
                Vector3 pos = transform.localPosition;
                pos.x = Mathf.Floor(pos.x / snapX) * snapX;
                pos.y = Mathf.Floor(pos.y / snapY) * snapY;
                pos.z = Mathf.Floor(pos.z / snapZ) * snapZ;

                if (transform.localPosition == pos)
                {
                    tile.SetTrimVisibility(true);
                    transform.hasChanged = false;
                }
                transform.localPosition = pos;
            }
        }
    }
}
