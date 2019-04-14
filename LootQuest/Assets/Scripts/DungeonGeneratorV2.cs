using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

    [SerializeField]
    private int corChance = 5;

    private string[,] floor;
    private int totalRooms;
    private int eX;
    private int eY;
    private string[,] path;
    private string[,] corFloor;

    // Start is called before the first frame update
    void Start()
    {
        createFloor();
        tileFloor();
    }

    private void tileFloor()
    {
        Boolean U = false;
        Boolean D = false;
        Boolean L = false;
        Boolean R = false;

        System.Random rand = new System.Random();

        int chance = 0;
        String[,] room;
        for (int y = 0; y < floorHeight; y++)
        {
            for (int x = 0; x < floorWidth; x++)
            {
                if (floor[x,y].Equals("X"))
                {
                    //Checking for connections to generic room
                    if (corFloor[x,y].Contains("^"))
                    {
                        U = true;
                    }
                    if (corFloor[x,y].Contains("v"))
                    {
                        D = true;
                    }
                    if (corFloor[x,y].Contains("<"))
                    {
                        L = true;
                    }
                    if (corFloor[x,y].Contains(">"))
                    {
                        R = true;
                    }

                    //Creating Generic room or Corridor
                    chance = rand.Next(0, 5) - 2;
                    if (chance <= 0)
                    {
                        floor[x,y] = "C";
                        room = createCorridor(U, D, L, R);
                        addRoom(room, x, y);
                    }
                    else
                    {
                        room = createRoom(U, D, L, R);
                        addRoom(room, x, y);
                    }
                }
                else if (floor[x,y].Equals("S"))
                {
                    //Checking for connections to start room
                    if (corFloor[x,y].Contains("^"))
                    {
                        U = true;
                    }
                    if (corFloor[x,y].Contains("v"))
                    {
                        D = true;
                    }
                    if (corFloor[x,y].Contains("<"))
                    {
                        L = true;
                    }
                    if (corFloor[x,y].Contains(">"))
                    {
                        R = true;
                    }
                    room = createStart(U, D, L, R);
                    addRoom(room, x, y);
                }
                else if (floor[x,y].Equals("E"))
                {
                    //Checking for connections to exit room
                    if (corFloor[x,y].Contains("^"))
                    {
                        U = true;
                    }
                    if (corFloor[x,y].Contains("v"))
                    {
                        D = true;
                    }
                    if (corFloor[x,y].Contains("<"))
                    {
                        L = true;
                    }
                    if (corFloor[x,y].Contains(">"))
                    {
                        R = true;
                    }
                    room = createExit(U, D, L, R);
                    addRoom(room, x, y);
                }
                else
                {
                    room = createEmpty();
                    addRoom(room, x, y);
                }
                U = false;
                D = false;
                L = false;
                R = false;
            }
        }
    }

    private string[,] createEmpty()
    {
        throw new NotImplementedException();
    }

    private string[,] createExit(bool u, bool d, bool l, bool r)
    {
        throw new NotImplementedException();
    }

    private string[,] createStart(bool u, bool d, bool l, bool r)
    {
        throw new NotImplementedException();
    }

    private string[,] createRoom(bool u, bool d, bool l, bool r)
    {
        throw new NotImplementedException();
    }

    private void addRoom(string[,] room, int xOffset, int yOffset)
    {
        //System.Random rand = new System.Random();
        //int name = 0;
        for (int y = 0; y < 16; y++)
        {
            for (int x = 0; x < 16; x++)
            {
                if (room[x,y].Equals("wu"))
                { 
                    Vector3Int pos = new Vector3Int(x*xOffset, y*yOffset, 0);
                    wallMap.SetTile(pos, topWallTile);
                }
                else if (room[x,y].Equals("wd"))
                {
                    Vector3Int pos = new Vector3Int(x * xOffset, y * yOffset, 0);
                    wallMap.SetTile(pos, botWallTile);
                }
                else if (room[x,y].Equals("wl"))
                {
                    Vector3Int pos = new Vector3Int(x * xOffset, y * yOffset, 0);
                    wallMap.SetTile(pos, leftWallTile);
                }
                else if (room[x,y].Equals("wr"))
                {
                    Vector3Int pos = new Vector3Int(x * xOffset, y * yOffset, 0);
                    wallMap.SetTile(pos, rightWallTile);
                }
                else if (room[x,y].Equals("citl"))
                {
                    Vector3Int pos = new Vector3Int(x * xOffset, y * yOffset, 0);
                    wallMap.SetTile(pos, ITLWallTile);
                }
                else if (room[x,y].Equals("citr"))
                {
                    Vector3Int pos = new Vector3Int(x * xOffset, y * yOffset, 0);
                    wallMap.SetTile(pos, ITRWallTile);
                }
                else if (room[x,y].Equals("cibl"))
                {
                    Vector3Int pos = new Vector3Int(x * xOffset, y * yOffset, 0);
                    wallMap.SetTile(pos, IBLWallTile);
                }
                else if (room[x,y].Equals("cibr"))
                {
                    Vector3Int pos = new Vector3Int(x * xOffset, y * yOffset, 0);
                    wallMap.SetTile(pos, IBRWallTile);
                }
                else if (room[x,y].Equals("cotl"))
                {
                    Vector3Int pos = new Vector3Int(x * xOffset, y * yOffset, 0);
                    wallMap.SetTile(pos, OTLWallTile);
                }
                else if (room[x,y].Equals("cotr"))
                {
                    Vector3Int pos = new Vector3Int(x * xOffset, y * yOffset, 0);
                    wallMap.SetTile(pos, OTRWallTile);
                }
                else if (room[x,y].Equals("cobl"))
                {
                    Vector3Int pos = new Vector3Int(x * xOffset, y * yOffset, 0);
                    wallMap.SetTile(pos, OBLWallTile);
                }
                else if (room[x,y].Equals("cobr"))
                {
                    Vector3Int pos = new Vector3Int(x * xOffset, y * yOffset, 0);
                    wallMap.SetTile(pos, OBRWallTile);
                }
                else if (room[x,y].Equals("S"))
                {
                    //spawnX = x + (roomWidth * xOffset);
                    //spawnY = y + (roomHeight * yOffset);
                    Vector3Int pos = new Vector3Int(x * xOffset, y * yOffset, 0);
                    groundMap.SetTile(pos, groundTile);
                }
                else if (room[x,y].Equals("E"))
                {
                    //exitX = x + (roomWidth * xOffset);
                    //exitY = y + (roomHeight * yOffset);
                    Vector3Int pos = new Vector3Int(x * xOffset, y * yOffset, 0);
                    groundMap.SetTile(pos, groundTile);
                }
                else if (room[x,y].Equals("0") || room[x,y].Equals("D") || room[x,y].Equals("1"))
                {
                    //name = ran.nextInt(4);

                    //if (name == 0)
                    //{
                    //    setTile(x + (roomWidth * xOffset), y + (roomHeight * yOffset), new FloorTile((x + (roomWidth * xOffset)) * Tile.size, (y + (roomHeight * yOffset)) * Tile.size, "floor1"));
                    //}
                    //else
                    //{
                    //    setTile(x + (roomWidth * xOffset), y + (roomHeight * yOffset), new FloorTile((x + (roomWidth * xOffset)) * Tile.size, (y + (roomHeight * yOffset)) * Tile.size, "floor2"));
                    //}
                    Vector3Int pos = new Vector3Int(x * xOffset, y * yOffset, 0);
                    groundMap.SetTile(pos, groundTile);
                }
                else if (room[x,y].Equals("e"))
                {
                    Vector3Int pos = new Vector3Int(x * xOffset, y * yOffset, 0);
                    pitMap.SetTile(pos, pitTile);
                }
                else
                {
                    Vector3Int pos = new Vector3Int(x * xOffset, y * yOffset, 0);
                    pitMap.SetTile(pos, pitTile);
                }
            }
        }
    }

    private string[,] createCorridor(bool u, bool d, bool l, bool r)
    {
        throw new NotImplementedException();
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

        corFloor = corridorCreater();
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
    
    public String[,] corridorCreater()
    {
        String[,] newFloor = new String[floorWidth,floorHeight];
        for (int y = 0; y < floorHeight; y++)
        {
            for (int x = 0; x < floorWidth; x++)
            {
                newFloor[x,y] = "____";
            }
        }
        System.Random rand = new System.Random();
        int chance = 0;
        var regex = new Regex(Regex.Escape("_"));

        for (int y = 0; y < floorHeight; y++)
        {
            for (int x = 0; x < floorWidth; x++)
            {
                //Get the current corridor(cor) pointers of the room at floor[x][y]
                String cor = newFloor[x,y];

                if (path[x,y].Equals("E"))
                {
                    //If path[x][y] is E the exit, we need to find the next room and link them with a corridor
                    //cor = cor + "E";

                    if (y > 0 && path[x,y - 1].Equals("X"))
                    {
                        //The next room is above the Exit, make a link on Exit pointing up
                        if (!cor.Contains("^"))
                        {
                            cor = regex.Replace(cor, "^", 1);
                        }

                        if (!newFloor[x,y - 1].Contains("v"))
                        {
                            //If the next room doesn't already point down to the Exit, make it
                            newFloor[x, y - 1] = regex.Replace(newFloor[x, y - 1], "v", 1);
                        }

                    }
                    else if (y < floorHeight - 1 && path[x,y + 1].Equals("X"))
                    {
                        //The next room is below the Exit, make a link on Exit pointing down
                        if (!cor.Contains("v"))
                        {
                            cor = regex.Replace(cor, "v", 1);
                        }

                        if (!newFloor[x,y + 1].Contains("^"))
                        {
                            //If the next room doesn't already point up to the Exit, make it.
                            newFloor[x, y + 1] = regex.Replace(newFloor[x, y + 1], "^", 1);
                        }

                    }
                    else if (x > 0 && path[x - 1,y].Equals("X"))
                    {
                        //The next room is to the left of the Exit, make a link on Exit pointing left
                        if (!cor.Contains("<"))
                        {
                            cor = regex.Replace(cor, "<", 1);
                        }

                        if (!newFloor[x - 1,y].Contains(">"))
                        {
                            //If the next room doesn't already point right to the Exit, make it.
                            newFloor[x - 1, y] = regex.Replace(newFloor[x - 1, y], ">", 1);
                        }

                    }
                    else if (x < floorWidth - 1 && path[x + 1,y].Equals("X"))
                    {
                        //The next room is to the right of the Exit, make a link on Exit pointing right
                        if (!cor.Contains(">"))
                        {
                            cor = regex.Replace(cor, ">", 1);
                        }

                        if (!newFloor[x + 1,y].Contains("<"))
                        {
                            //If the next room doesn't already point left to the Exit, make it.
                            newFloor[x + 1, y] = regex.Replace(newFloor[x + 1, y], "<", 1);
                        }

                    }

                }
                else if (path[x,y].Equals("S"))
                {
                    //If path[x][y] is the Start, do the same thing as Exit and find the next room to go to
                    //cor = cor + "S";

                    if (y > 0 && path[x,y - 1].Equals("X"))
                    {
                        //The next room is above
                        if (!cor.Contains("^"))
                        {
                            cor = regex.Replace(cor, "^", 1);
                        }

                        if (!newFloor[x,y - 1].Contains("v"))
                        {
                            //Make the room point to Start if it doesn't already
                            newFloor[x, y - 1] = regex.Replace(newFloor[x, y - 1], "v", 1);
                        }

                    }
                    else if (y < floorHeight - 1 && path[x,y + 1].Equals("X"))
                    {
                        //The next room is below
                        if (!cor.Contains("v"))
                        {
                            cor = regex.Replace(cor, "v", 1);
                        }

                        if (!newFloor[x,y + 1].Contains("^"))
                        {
                            //Make the room point to Start if it doesn't already
                            newFloor[x, y + 1] = regex.Replace(newFloor[x, y + 1], "^", 1);
                        }

                    }
                    else if (x > 0 && path[x - 1,y].Equals("X"))
                    {
                        //The next room is to the left
                        if (!cor.Contains("<"))
                        {
                            cor = regex.Replace(cor, "<", 1);
                        }

                        if (!newFloor[x - 1,y].Contains(">"))
                        {
                            //Make the room point to the Start if it doesn't already
                            newFloor[x - 1, y] = regex.Replace(newFloor[x - 1, y], ">", 1);
                        }

                    }
                    else if (x < floorWidth - 1 && path[x + 1,y].Equals("X"))
                    {
                        //The next room is to the right
                        if (!cor.Contains(">"))
                        {
                            cor = regex.Replace(cor, ">", 1);
                        }

                        if (!newFloor[x + 1,y].Contains("<"))
                        {
                            //Make the room point to the Start if it doesn't already
                            newFloor[x + 1, y] = regex.Replace(newFloor[x + 1, y], "<", 1);
                        }

                    }
                }
                else if (path[x,y].Equals("X"))
                {
                    //Now if there is any room along the path, make sure it is connected to the other rooms (Other X's, S, and E)
                    if (y > 0 && (path[x,y - 1].Equals("X") || path[x,y - 1].Equals("S") || path[x,y - 1].Equals("E")))
                    {
                        //The room is above
                        if (!cor.Contains("^"))
                        {
                            cor = regex.Replace(cor, "^", 1);
                        }

                        if (!newFloor[x,y - 1].Contains("v"))
                        {
                            newFloor[x, y-1] = regex.Replace(newFloor[x, y-1], "v", 1);
                        }

                    }
                    if (y < floorHeight - 1 && (path[x,y + 1].Equals("X") || path[x,y + 1].Equals("S") || path[x,y + 1].Equals("E")))
                    {
                        //The room is below
                        if (!cor.Contains("v"))
                        {
                            cor = regex.Replace(cor, "v", 1);
                        }

                        if (!newFloor[x,y + 1].Contains("^"))
                        {
                            newFloor[x, y + 1] = regex.Replace(newFloor[x, y + 1], "^", 1);
                        }

                    }
                    if (x > 0 && (path[x - 1,y].Equals("X") || path[x - 1,y].Equals("S") || path[x - 1,y].Equals("E")))
                    {
                        //The room is to the left
                        if (!cor.Contains("<"))
                        {
                            cor = regex.Replace(cor, "<", 1);
                        }

                        if (!newFloor[x - 1,y].Contains(">"))
                        {
                            newFloor[x-1, y] = regex.Replace(newFloor[x-1, y], ">", 1);
                        }

                    }
                    if (x < floorWidth - 1 && (path[x + 1,y].Equals("X") || path[x + 1,y].Equals("S") || path[x + 1,y].Equals("E")))
                    {
                        //The room is to the right
                        if (!cor.Contains(">"))
                        {
                            cor = regex.Replace(cor, ">", 1);
                        }

                        if (!newFloor[x + 1,y].Contains("<"))
                        {
                            newFloor[x + 1, y] = regex.Replace(newFloor[x + 1, y], "<", 1);
                        }

                    }
                }

                //Generate more pointers so there isn't just the one path
                if (floor[x,y].Equals("S"))
                {
                    //The current room is the Start room.
                    //Find more rooms next to Start and random pointers

                    if (y > 0 && floor[x,y - 1].Equals("X"))
                    {

                        chance = rand.Next(0, corChance);

                        if (chance == 0 && !cor.Contains("^"))
                        {
                            cor = regex.Replace(cor, "^", 1);
                            if (!newFloor[x,y - 1].Contains("v"))
                            {
                                newFloor[x, y-1] = regex.Replace(newFloor[x, y-1], "v", 1);
                            }
                        }
                    }
                    if (y < floorHeight - 1 && floor[x,y + 1].Equals("X"))
                    {
                        chance = rand.Next(0, corChance);

                        if (chance == 0 && !cor.Contains("v"))
                        {
                            cor = regex.Replace(cor, "v", 1);
                            if (!newFloor[x,y + 1].Contains("^"))
                            {
                                newFloor[x, y + 1] = regex.Replace(newFloor[x, y + 1], "^", 1);
                            }
                        }
                    }
                    if (x > 0 && floor[x - 1,y].Equals("X"))
                    {
                        chance = rand.Next(0, corChance);

                        if (chance == 0 && !cor.Contains("<"))
                        {
                            cor = regex.Replace(cor, "<", 1);
                            if (!newFloor[x - 1,y].Contains(">"))
                            {
                                newFloor[x-1, y] = regex.Replace(newFloor[x-1,y], ">", 1);
                            }
                        }
                    }
                    if (x < floorWidth - 1 && floor[x + 1,y].Equals("X"))
                    {
                        chance = rand.Next(0, corChance);

                        if (chance == 0 && !cor.Contains(">"))
                        {
                            cor = regex.Replace(cor, ">", 1);
                            if (!newFloor[x + 1,y].Contains("<"))
                            {
                                newFloor[x + 1, y] = regex.Replace(newFloor[x + 1, y], "<", 1);
                            }
                        }
                    }

                }
                else if (floor[x,y].Equals("E"))
                {
                    if (y > 0 && floor[x, y - 1].Equals("X"))
                    {

                        chance = rand.Next(0, corChance);

                        if (chance == 0 && !cor.Contains("^"))
                        {
                            cor = regex.Replace(cor, "^", 1);
                            if (!newFloor[x, y - 1].Contains("v"))
                            {
                                newFloor[x, y - 1] = regex.Replace(newFloor[x, y - 1], "v", 1);
                            }
                        }
                    }
                    if (y < floorHeight - 1 && floor[x, y + 1].Equals("X"))
                    {
                        chance = rand.Next(0, corChance);

                        if (chance == 0 && !cor.Contains("v"))
                        {
                            cor = regex.Replace(cor, "v", 1);
                            if (!newFloor[x, y + 1].Contains("^"))
                            {
                                newFloor[x, y + 1] = regex.Replace(newFloor[x, y + 1], "^", 1);
                            }
                        }
                    }
                    if (x > 0 && floor[x - 1, y].Equals("X"))
                    {
                        chance = rand.Next(0, corChance);

                        if (chance == 0 && !cor.Contains("<"))
                        {
                            cor = regex.Replace(cor, "<", 1);
                            if (!newFloor[x - 1, y].Contains(">"))
                            {
                                newFloor[x - 1, y] = regex.Replace(newFloor[x - 1, y], ">", 1);
                            }
                        }
                    }
                    if (x < floorWidth - 1 && floor[x + 1, y].Equals("X"))
                    {
                        chance = rand.Next(0, corChance);

                        if (chance == 0 && !cor.Contains(">"))
                        {
                            cor = regex.Replace(cor, ">", 1);
                            if (!newFloor[x + 1, y].Contains("<"))
                            {
                                newFloor[x + 1, y] = regex.Replace(newFloor[x + 1, y], "<", 1);
                            }
                        }
                    }
                }
                else if (floor[x,y].Equals("X"))
                {
                    if (y > 0 && floor[x, y - 1].Equals("X"))
                    {

                        chance = rand.Next(0, corChance);

                        if (chance == 0 && !cor.Contains("^"))
                        {
                            cor = regex.Replace(cor, "^", 1);
                            if (!newFloor[x, y - 1].Contains("v"))
                            {
                                newFloor[x, y - 1] = regex.Replace(newFloor[x, y - 1], "v", 1);
                            }
                        }
                    }
                    if (y < floorHeight - 1 && floor[x, y + 1].Equals("X"))
                    {
                        chance = rand.Next(0, corChance);

                        if (chance == 0 && !cor.Contains("v"))
                        {
                            cor = regex.Replace(cor, "v", 1);
                            if (!newFloor[x, y + 1].Contains("^"))
                            {
                                newFloor[x, y + 1] = regex.Replace(newFloor[x, y + 1], "^", 1);
                            }
                        }
                    }
                    if (x > 0 && floor[x - 1, y].Equals("X"))
                    {
                        chance = rand.Next(0, corChance);

                        if (chance == 0 && !cor.Contains("<"))
                        {
                            cor = regex.Replace(cor, "<", 1);
                            if (!newFloor[x - 1, y].Contains(">"))
                            {
                                newFloor[x - 1, y] = regex.Replace(newFloor[x - 1, y], ">", 1);
                            }
                        }
                    }
                    if (x < floorWidth - 1 && floor[x + 1, y].Equals("X"))
                    {
                        chance = rand.Next(0, corChance);

                        if (chance == 0 && !cor.Contains(">"))
                        {
                            cor = regex.Replace(cor, ">", 1);
                            if (!newFloor[x + 1, y].Contains("<"))
                            {
                                newFloor[x + 1, y] = regex.Replace(newFloor[x + 1, y], "<", 1);
                            }
                        }
                    }
                }

                if (!cor.Equals("____"))
                {
                    newFloor[x,y] = cor;
                }
            }
        }
        return newFloor;
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
