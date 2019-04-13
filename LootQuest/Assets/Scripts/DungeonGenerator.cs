using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField]
    private Tile groundTile;
    [SerializeField]
    private Tile pitTile;
    [SerializeField]
    private Tile topWallTile;
    [SerializeField]
    private Tile botWallTile;
    [SerializeField]
    private Tile rightWallTile;
    [SerializeField]
    private Tile leftWallTile;
    [SerializeField]
    private Tile ITRWallTile;
    [SerializeField]
    private Tile ITLWallTile;
    [SerializeField]
    private Tile IBRWallTile;
    [SerializeField]
    private Tile IBLWallTile;
    [SerializeField]
    private Tile OTRWallTile;
    [SerializeField]
    private Tile OTLWallTile;
    [SerializeField]
    private Tile OBRWallTile;
    [SerializeField]
    private Tile OBLWallTile;

    [SerializeField]
    private Tilemap groundMap;
    [SerializeField]
    private Tilemap pitMap;
    [SerializeField]
    private Tilemap wallMap;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private int deviationRate = 10;
    [SerializeField]
    private int roomRate = 15;
    [SerializeField]
    private int maxRouteLength;
    [SerializeField]
    private int maxRoutes = 20;

    private int routeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        int x = 0;
        int y = 0;
        int routeLength = 0;
        GenerateSquare(x, y, 1);
        Vector2Int previousPos = new Vector2Int(x, y);
        y += 3;
        GenerateSquare(x, y, 1);
        NewRoute(x, y, routeLength, previousPos);

        FillWalls();

        FixCorners();
    }

    private void FixCorners()
    {
        BoundsInt bounds = groundMap.cellBounds;
        for (int xMap = bounds.xMin - 10; xMap <= bounds.xMax + 10; xMap++)
        {
            for (int yMap = bounds.yMin - 10; yMap <= bounds.yMax + 10; yMap++)
            {
                Vector3Int pos = new Vector3Int(xMap, yMap, 0);
                Vector3Int posBellow = new Vector3Int(xMap, yMap - 1, 0);
                Vector3Int posAbove = new Vector3Int(xMap, yMap + 1, 0);
                Vector3Int posRight = new Vector3Int(xMap - 1, yMap, 0);
                Vector3Int posLeft = new Vector3Int(xMap + 1, yMap, 0);

                TileBase tile = wallMap.GetTile(pos);
                TileBase tileBellow = wallMap.GetTile(posBellow);
                TileBase tileAbove = wallMap.GetTile(posAbove);
                TileBase tileRight = wallMap.GetTile(posRight);
                TileBase tileLeft = wallMap.GetTile(posLeft);

                if ( tile == topWallTile )
                {
                    if ( tileAbove == rightWallTile && tileLeft == topWallTile )
                    {
                        wallMap.SetTile(pos, OBLWallTile);
                    }
                    else if ( tileAbove == leftWallTile && tileRight == topWallTile )
                    {
                        wallMap.SetTile(pos, OBRWallTile);
                    }
                }
                if ( tile == botWallTile )
                {
                    if ( tileBellow == rightWallTile )
                    {
                        wallMap.SetTile(pos, OTLWallTile);
                    }
                    else if ( tileBellow == leftWallTile )
                    {
                        wallMap.SetTile(pos, OTRWallTile);
                    }
                }

                //Checking for pit (filler) tiles
                tile = pitMap.GetTile(pos);

                if ( tile == pitTile )
                {
                    if ( tileAbove == rightWallTile && tileRight == botWallTile )
                    {
                        wallMap.SetTile(pos, IBRWallTile);
                    }
                    else if ( tileAbove == leftWallTile && tileLeft == botWallTile )
                    {
                        wallMap.SetTile(pos, IBLWallTile);
                    }
                    else if ( tileBellow == leftWallTile && tileLeft == topWallTile )
                    {
                        wallMap.SetTile(pos, ITLWallTile);
                    }
                    else if ( tileBellow == rightWallTile && tileRight == topWallTile )
                    {
                        wallMap.SetTile(pos, ITRWallTile);
                    }
                }
            }
        }
    }

    private void FillWalls()
    {
        BoundsInt bounds = groundMap.cellBounds;
        for (int xMap = bounds.xMin-10; xMap <= bounds.xMax+10; xMap++)
        {
            for (int yMap = bounds.yMin - 10; yMap <= bounds.yMax + 10; yMap++)
            {
                Vector3Int pos = new Vector3Int(xMap, yMap, 0);
                Vector3Int posBellow = new Vector3Int(xMap, yMap - 1, 0);
                Vector3Int posAbove = new Vector3Int(xMap, yMap + 1, 0);
                Vector3Int posRight = new Vector3Int(xMap - 1, yMap, 0);
                Vector3Int posLeft = new Vector3Int(xMap + 1, yMap, 0);

                TileBase tile = groundMap.GetTile(pos);
                TileBase tileBellow = groundMap.GetTile(posBellow);
                TileBase tileAbove = groundMap.GetTile(posAbove);
                TileBase tileRight = groundMap.GetTile(posRight);
                TileBase tileLeft = groundMap.GetTile(posLeft);

                if (tile == null)
                {
                    pitMap.SetTile(pos, pitTile);
                    if (tileBellow != null)
                    {
                        wallMap.SetTile(pos, topWallTile);
                    }
                    else if (tileAbove != null)
                    {
                        wallMap.SetTile(pos, botWallTile);
                    }
                    else if (tileRight != null)
                    {
                        wallMap.SetTile(pos, rightWallTile);
                    }
                    else if (tileLeft != null)
                    {
                        wallMap.SetTile(pos, leftWallTile);
                    }
                }

            }
        }
    }

    private void NewRoute(int x, int y, int routeLength, Vector2Int previousPos)
    {
        if (routeCount < maxRoutes)
        {
            routeCount++;
            while (++routeLength < maxRouteLength)
            {
                bool routeUsed = false;
                int xOffset = x - previousPos.x;
                int yOffset = y - previousPos.y;
                int roomSize = 1;
                if (UnityEngine.Random.Range(1, 100) <= roomRate)
                {
                    roomSize = UnityEngine.Random.Range(3, 6);

                }
                previousPos = new Vector2Int(x, y);

                //Go Straight
                if (UnityEngine.Random.Range(1, deviationRate) == 1)
                {
                    if(routeUsed)
                    {
                        GenerateSquare(previousPos.x + xOffset, previousPos.y + yOffset, roomSize);
                        NewRoute(previousPos.x + xOffset, previousPos.y + yOffset, UnityEngine.Random.Range(routeLength, maxRouteLength), previousPos);

                    }
                    else
                    {
                        x = previousPos.x + xOffset;
                        y = previousPos.y + yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }

                //Go Left
                if (UnityEngine.Random.Range(1, deviationRate) == 1)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x - yOffset, previousPos.y + xOffset, roomSize);
                        NewRoute(previousPos.x - yOffset, previousPos.y + xOffset, UnityEngine.Random.Range(routeLength, maxRouteLength), previousPos);

                    }
                    else
                    {
                        y = previousPos.y + xOffset;
                        x = previousPos.x - yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }

                //Go Right
                if (UnityEngine.Random.Range(1, deviationRate) == 1)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x + yOffset, previousPos.y - xOffset, roomSize);
                        NewRoute(previousPos.x + yOffset, previousPos.y - xOffset, UnityEngine.Random.Range(routeLength, maxRouteLength), previousPos);

                    }
                    else
                    {
                        x = previousPos.x + yOffset;
                        y = previousPos.y - xOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }

                if (!routeUsed)
                {
                    x = previousPos.x + xOffset;
                    y = previousPos.y + yOffset;
                    GenerateSquare(x, y, roomSize);
                }
            }
        }
    }

    private void GenerateSquare(int x, int y, int radius)
    {
        for (int tileX = x-radius; tileX <= x + radius; tileX++)
        {
            for (int tileY = y-radius; tileY <= y + radius; tileY++)
            {
                Vector3Int tilePos = new Vector3Int(tileX, tileY, 0);
                groundMap.SetTile(tilePos, groundTile);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
