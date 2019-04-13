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

    public String[,] corridorCreater(String[,] floor, String[,] path)
    {
        String[,] newFloor = new String[floorWidth,floorHeight];
        for (int y = 0; y < floorHeight; y++)
        {
            for (int x = 0; x < floorWidth; x++)
            {
                newFloor[x,y] = "____";
            }
        }
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

                        if (!newFloor[x - 1,y].contains(">"))
                        {
                            //Make the room point to the Start if it doesn't already
                            newFloor[x - 1][y] = newFloor[x - 1][y].replaceFirst("_", ">");
                        }

                    }
                    else if (x < w - 1 && path[x + 1][y].equals("X"))
                    {
                        //The next room is to the right
                        if (!cor.contains(">"))
                        {
                            cor = cor.replaceFirst("_", ">");
                        }

                        if (!newFloor[x + 1][y].contains("<"))
                        {
                            //Make the room point to the Start if it doesn't already
                            newFloor[x + 1][y] = newFloor[x + 1][y].replaceFirst("_", "<");
                        }

                    }
                }
                else if (path[x][y].equals("X"))
                {
                    //Now if there is any room along the path, make sure it is connected to the other rooms (Other X's, S, and E)
                    if (y > 0 && (path[x][y - 1].equals("X") || path[x][y - 1].equals("S") || path[x][y - 1].equals("E")))
                    {
                        //The room is above
                        if (!cor.contains("^"))
                        {
                            cor = cor.replaceFirst("_", "^");
                        }

                        if (!newFloor[x][y - 1].contains("v"))
                        {
                            newFloor[x][y - 1] = newFloor[x][y - 1].replaceFirst("_", "v");
                        }

                    }
                    if (y < h - 1 && (path[x][y + 1].equals("X") || path[x][y + 1].equals("S") || path[x][y + 1].equals("E")))
                    {
                        //The room is below
                        if (!cor.contains("v"))
                        {
                            cor = cor.replaceFirst("_", "v");
                        }

                        if (!newFloor[x][y + 1].contains("^"))
                        {
                            newFloor[x][y + 1] = newFloor[x][y + 1].replaceFirst("_", "^");
                        }

                    }
                    if (x > 0 && (path[x - 1][y].equals("X") || path[x - 1][y].equals("S") || path[x - 1][y].equals("E")))
                    {
                        //The room is to the left
                        if (!cor.contains("<"))
                        {
                            cor = cor.replaceFirst("_", "<");
                        }

                        if (!newFloor[x - 1][y].contains(">"))
                        {
                            newFloor[x - 1][y] = newFloor[x - 1][y].replaceFirst("_", ">");
                        }

                    }
                    if (x < w - 1 && (path[x + 1][y].equals("X") || path[x + 1][y].equals("S") || path[x + 1][y].equals("E")))
                    {
                        //The room is to the right
                        if (!cor.contains(">"))
                        {
                            cor = cor.replaceFirst("_", ">");
                        }

                        if (!newFloor[x + 1][y].contains("<"))
                        {
                            newFloor[x + 1][y] = newFloor[x + 1][y].replaceFirst("_", "<");
                        }

                    }
                }

                //Generate more pointers so there isn't just the one path
                if (floor[x][y].equals("S"))
                {
                    //The current room is the Start room.
                    //Find more rooms next to Start and random pointers

                    if (y > 0 && floor[x][y - 1].equals("X"))
                    {
                        chance = r.nextInt(cc);
                        if (chance == 0 && !cor.contains("^"))
                        {
                            cor = cor.replaceFirst("_", "^");
                            if (!newFloor[x][y - 1].contains("v"))
                            {
                                newFloor[x][y - 1] = newFloor[x][y - 1].replaceFirst("_", "v");
                            }
                        }
                    }
                    if (y < h - 1 && floor[x][y + 1].equals("X"))
                    {
                        chance = r.nextInt(cc);
                        if (chance == 0 && !cor.contains("v"))
                        {
                            cor = cor.replaceFirst("_", "v");
                            if (!newFloor[x][y + 1].contains("^"))
                            {
                                newFloor[x][y + 1] = newFloor[x][y + 1].replaceFirst("_", "^");
                            }
                        }
                    }
                    if (x > 0 && floor[x - 1][y].equals("X"))
                    {
                        chance = r.nextInt(cc);
                        if (chance == 0 && !cor.contains("<"))
                        {
                            cor = cor.replaceFirst("_", "<");
                            if (!newFloor[x - 1][y].contains(">"))
                            {
                                newFloor[x - 1][y] = newFloor[x - 1][y].replaceFirst("_", ">");
                            }
                        }
                    }
                    if (x < w - 1 && floor[x + 1][y].equals("X"))
                    {
                        chance = r.nextInt(cc);
                        if (chance == 0 && !cor.contains(">"))
                        {
                            cor = cor.replaceFirst("_", ">");
                            if (!newFloor[x + 1][y].contains("<"))
                            {
                                newFloor[x + 1][y] = newFloor[x + 1][y].replaceFirst("_", "<");
                            }
                        }
                    }

                }
                else if (floor[x][y].equals("E"))
                {
                    if (y > 0 && floor[x][y - 1].equals("X"))
                    {
                        chance = r.nextInt(cc);
                        if (chance == 0 && !cor.contains("^"))
                        {
                            cor = cor.replaceFirst("_", "^");
                            if (!newFloor[x][y - 1].contains("v"))
                            {
                                newFloor[x][y - 1] = newFloor[x][y - 1].replaceFirst("_", "v");
                            }
                        }
                    }
                    if (y < h - 1 && floor[x][y + 1].equals("X"))
                    {
                        chance = r.nextInt(cc);
                        if (chance == 0 && !cor.contains("v"))
                        {
                            cor = cor.replaceFirst("_", "v");
                            if (!newFloor[x][y + 1].contains("^"))
                            {
                                newFloor[x][y + 1] = newFloor[x][y + 1].replaceFirst("_", "^");
                            }
                        }
                    }
                    if (x > 0 && floor[x - 1][y].equals("X"))
                    {
                        chance = r.nextInt(cc);
                        if (chance == 0 && !cor.contains("<"))
                        {
                            cor = cor.replaceFirst("_", "<");
                            if (!newFloor[x - 1][y].contains(">"))
                            {
                                newFloor[x - 1][y] = newFloor[x - 1][y].replaceFirst("_", ">");
                            }
                        }
                    }
                    if (x < w - 1 && floor[x + 1][y].equals("X"))
                    {
                        chance = r.nextInt(cc);
                        if (chance == 0 && !cor.contains(">"))
                        {
                            cor = cor.replaceFirst("_", ">");
                            if (!newFloor[x + 1][y].contains("<"))
                            {
                                newFloor[x + 1][y] = newFloor[x + 1][y].replaceFirst("_", "<");
                            }
                        }
                    }
                }
                else if (floor[x][y].equals("X"))
                {
                    if (y > 0 && floor[x][y - 1].equals("X"))
                    {
                        chance = r.nextInt(cc);
                        if (chance == 0 && !cor.contains("^"))
                        {
                            cor = cor.replaceFirst("_", "^");
                            if (!newFloor[x][y - 1].contains("v"))
                            {
                                newFloor[x][y - 1] = newFloor[x][y - 1].replaceFirst("_", "v");
                            }
                        }
                    }
                    if (y < h - 1 && floor[x][y + 1].equals("X"))
                    {
                        chance = r.nextInt(cc);
                        if (chance == 0 && !cor.contains("v"))
                        {
                            cor = cor.replaceFirst("_", "v");
                            if (!newFloor[x][y + 1].contains("^"))
                            {
                                newFloor[x][y + 1] = newFloor[x][y + 1].replaceFirst("_", "^");
                            }
                        }
                    }
                    if (x > 0 && floor[x - 1][y].equals("X"))
                    {
                        chance = r.nextInt(cc);
                        if (chance == 0 && !cor.contains("<"))
                        {
                            cor = cor.replaceFirst("_", "<");
                            if (!newFloor[x - 1][y].contains(">"))
                            {
                                newFloor[x - 1][y] = newFloor[x - 1][y].replaceFirst("_", ">");
                            }
                        }
                    }
                    if (x < w - 1 && floor[x + 1][y].equals("X"))
                    {
                        chance = r.nextInt(cc);
                        if (chance == 0 && !cor.contains(">"))
                        {
                            cor = cor.replaceFirst("_", ">");
                            if (!newFloor[x + 1][y].contains("<"))
                            {
                                newFloor[x + 1][y] = newFloor[x + 1][y].replaceFirst("_", "<");
                            }
                        }
                    }
                }

                if (!cor.equals("____"))
                {
                    newFloor[x][y] = cor;
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
