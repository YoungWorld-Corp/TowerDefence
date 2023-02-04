using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{
    public Tile startTile;
    public Tile endTile;
    public Tile groundTile;
    public Tile wallTile;
    public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InitializeTilemaps(List<Vector2Int> wayPoints)
    {
        for (int x=0; x<14; x++)
        {
            for (int y=0; y<8; y++)
            {
                Vector3Int p = new Vector3Int(x,-y,0);
                tilemap.SetTile(p, wallTile);
            }
        }

        for (int i=0; i<wayPoints.Count-1; i++)
        {
            if (wayPoints[i].x == wayPoints[i+1].x)
            {
                int maxY = Mathf.Max(wayPoints[i].y, wayPoints[i+1].y);
                int minY = Mathf.Min(wayPoints[i].y, wayPoints[i+1].y);
                for (int y=minY; y<=maxY; y++)
                {
                    Vector3Int p = new Vector3Int(wayPoints[i].x, y,0);
                    tilemap.SetTile(p, groundTile);
                }
            }
            else if (wayPoints[i].y == wayPoints[i+1].y)
            {
                int maxX = Mathf.Max(wayPoints[i].x, wayPoints[i+1].x);
                int minX = Mathf.Min(wayPoints[i].x, wayPoints[i+1].x);
                for (int x=minX; x<=maxX; x++)
                {
                    Vector3Int p = new Vector3Int(x, wayPoints[i].y, 0);
                    tilemap.SetTile(p, groundTile);
                }
            }
            else
            {
                Debug.Log("wayPoints Wrong!");
            }
        }

        tilemap.SetTile(new Vector3Int(wayPoints[0].x, wayPoints[0].y, 0), startTile);
        tilemap.SetTile(new Vector3Int(wayPoints[wayPoints.Count-1].x, wayPoints[wayPoints.Count-1].y, 0), endTile);
    }
}
