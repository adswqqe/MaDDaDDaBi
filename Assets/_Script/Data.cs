using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    int lever;
    int exp;
    int gold;
    int reputation;
    int bagSpace;
    int max_bagSpace;

    public Data(int lever, int exp, int gold, int reputation, int bagSpace, int max_bagSpace)
    {
        this.lever = lever;
        this.exp = exp;
        this.gold = gold;
        this.reputation = reputation;
        this.bagSpace = bagSpace;
        this.max_bagSpace = max_bagSpace;
    }

    public int Level
    {
        set { lever = value; }
        get { return lever; }
    }

    public int EXP
    {
        set { exp = value; }
        get { return exp; }
    }

    public int GOLD
    {
        set { gold = value; }
        get { return gold; }
    }

    public int REPUTATION
    {
        set { reputation = value; }
        get { return reputation; }
    }

    public int BAGSPACE
    {
        set { bagSpace = value; }
        get { return bagSpace; }
    }

    public int MAX_BAGSPCE
    {
        set { max_bagSpace = value; }
        get { return max_bagSpace; }
    }
}
