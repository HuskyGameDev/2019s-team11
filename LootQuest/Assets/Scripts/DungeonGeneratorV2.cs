using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator1 : MonoBehaviour
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
    private int floorWidth = 10;
    [SerializeField]
    private int floorHeight = 10;
    [SerializeField]
    private int startX = 5;
    [SerializeField]
    private int startY = 5;

    [SerializeField]
    private int roomChance = 0;
    [SerializeField]
    private int chanceUp = 0;
    [SerializeField]
    private int chanceDown = -3;
    [SerializeField]
    private int chanceLeft = -7;
    [SerializeField]
    private int chanceRight = -9;

    private string[,] floor;
    private int totalRooms;
    private int eX;
    private int eY;
    private string[,] path;
    // Start is called before the first frame update
    void Start()
    {
        createFloor();
    }

    void createFloor()
    {
        floor = new string[floorWidth, floorHeight];
        for ( int y = 0; y<floorHeight; y++)
        {
            for ( int x = 0; x<floorWidth; x++)
            {
                floor[x,y] = " ";
            }
        }

        floor[startX,startY] = "S";
        totalRooms = 1;

        genRooms(startX, startY);

        genExit();

        findExitPath(startX, startY);
    }

    private string[,] findExitPath(int startX, int startY)
    {
        path = new string[floorWidth,floorHeight];

        for ( int y = 0; y < floorHeight; y++ )
        {
            for ( int x = 0; x < floorWidth; x++ )
            {
                path[x, y] = " ";
            }
        }

        path[startX, startY] = "S";

        createPath(path, startX, startY);

        return path;
    }

    private void createPath(string[,] path, int xP, int yP)
    {
        //Vertical with Exit
        if (eX == xP)
        {
            //Below, Above, Equal
            if (eY < yP)
            {
                path[xP,yP - 1] = "X";
                createPath(path, xP, yP - 1);
            }
            else if (eY > yP)
            {
                path[xP,yP + 1] = "X";
                createPath(path, xP, yP + 1);
            }
            else
            {
                //They are on the exit
                path[xP,yP] = "E";
                return;
            }
        }
        else if (eX > xP)
        {
            if (eY < yP)
            {
                //The exit is to the right and below
                System.Random rand = new System.Random();
                int chance = rand.Next(0, 2);
                if (chance == 0)
                {
                    path[xP,yP - 1] = "X";
                    createPath(path, xP, yP - 1);
                }
                else
                {
                    path[xP + 1,yP] = "X";
                    createPath(path, xP + 1, yP);
                }
            }
            else if (eY > yP)
            {
                //The exit is to the right and above
                System.Random rand = new System.Random();
                int chance = rand.Next(0, 2);
                if (chance == 0)
                {
                    path[xP,yP + 1] = "X";
                    createPath(path, xP, yP + 1);
                }
                else
                {
                    path[xP + 1,yP] = "X";
                    createPath(path, xP + 1, yP);
                }
            }
            else
            {
                path[xP + 1,yP] = "X";
                createPath(path, xP + 1, yP);
            }
        }
        else
        {
            if (eY < yP)
            {
                //The exit is to the left and below
                System.Random rand = new System.Random();
                int chance = rand.Next(0, 2);
                if (chance == 0)
                {
                    path[xP,yP - 1] = "X";
                    createPath(path, xP, yP - 1);
                }
                else
                {
                    path[xP - 1,yP] = "X";
                    createPath(path, xP - 1, yP);
                }
            }
            else if (eY > yP)
            {
                //The exit is to the left and above
                System.Random rand = new System.Random();
                int chance = rand.Next(0, 2);
                if (chance == 0)
                {
                    path[xP,yP + 1] = "X";
                    createPath(path, xP, yP + 1);
                }
                else
                {
                    path[xP - 1,yP] = "X";
                    createPath(path, xP - 1, yP);
                }
            }
            else
            {
                path[xP - 1,yP] = "X";
                createPath(path, xP - 1, yP);
            }
        }
    }

    private void genExit()
    {
        Boolean exitFound = false;
        System.Random rand = new System.Random();
        int chance = 0;
        while (!exitFound)
        {
            for( int y = 0; y < floorHeight; y++ )
            {
                for( int x = 0; x < floorWidth; x++ )
                {
                    if ( floor[x,y].Equals("X") && exitFound == false )
                    {
                        chance = rand.Next(0, floorWidth * floorHeight);
                        if ( chance == 0 )
                        {
                            floor[x,y] = "E";
                            eX = x;
                            eY = y;
                            exitFound = true;
                        }
                    }
                }
            }
        }
    }

    void genRooms(int x, int y)
    {
        System.Random rand = new System.Random();
        int chance = -1;

        for( int i = 0; i < 4; i++ )
        {
            if ( i == 0 )
            {
                chance = rand.Next(0, roomChance + totalRooms) - chanceUp;
                if ( chance <= 0)
                {
                    createRoom("UP", x, y);
                }
            }
            else if ( i == 1 )
            {
                chance = rand.Next(0, roomChance + totalRooms) - chanceDown;
                if (chance <= 0)
                {
                    createRoom("DOWN", x, y);
                }
            }
            else if (i == 2)
            {
                chance = rand.Next(0, roomChance + totalRooms) - chanceLeft;
                if (chance <= 0)
                {
                    createRoom("Left", x, y);
                }
            }
            else
            {
                chance = rand.Next(0, roomChance + totalRooms) - chanceRight;
                if (chance <= 0)
                {
                    createRoom("Right", x, y);
                }
            }
        }

    }

    private void createRoom(string v, int x, int y)
    {
        if ( v.Equals("UP") )
        {
            if ( y < floorHeight && floor[x,y+1].Equals(" ") )
            {
                floor[x,y + 1] = "X";
                totalRooms++;
                genRooms(x, y + 1);
            }
        }
        else if (v.Equals("DOWN"))
        {
            if ( y > 0 && floor[x,y - 1].Equals(" "))
            {
                floor[x,y - 1] = "X";
                totalRooms++;
                genRooms(x, y - 1);
            }
        }
        else if (v.Equals("LEFT"))
        {
            if ( x > 0 && floor[x-1,y].Equals(" "))
            {
                floor[x-1,y] = "X";
                totalRooms++;
                genRooms(x-1, y);
            }
        }
        else if (v.Equals("RIGHT"))
        {
            if (x < floorWidth && floor[x+1,y].Equals(" "))
            {
                floor[x+1,y] = "X";
                totalRooms++;
                genRooms(x+1, y);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
