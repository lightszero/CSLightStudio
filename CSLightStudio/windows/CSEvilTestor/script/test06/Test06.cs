using CSEvilTestor;
//using CSEvilTestor;
using System;
using System.Collections.Generic;
using System.Text;

public class Block
{
    public Block(string name, int blockType, int inLayer, bool canMove, string imgName)
    {
        blockName = name;
        type = blockType;
        layer = inLayer;
        move = canMove;
        spriteName = imgName;
    }
    public string blockName;
    public string spriteName;
    public int type;
    public int layer;
    public bool move;
}
