﻿/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {

    private Grid<PathNode> grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;
    public int floorCost = 0;

    public bool isWalkable;
    public PathNode cameFromNode;

    public PathNode(Grid<PathNode> grid, int x, int y, int floorCost)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true;

        this.floorCost = floorCost;
    }

    public void CalculateFCost() {
        fCost = gCost + hCost + floorCost ;
    }

    public void SetIsWalkable(bool isWalkable) {
        this.isWalkable = isWalkable;
        grid.TriggerGridObjectChanged(x, y);
    }

    public override string ToString() {
        return x + "," + y;
    }

}
