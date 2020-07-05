using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequstInfo
{
    int id = 0;
    string name = "";
    int type = 0;
    string nextQuest = "";
    int ondData = 0;
    int twoData = 0;
    int gold = 0;
    int rep = 0;
    int exp = 0;
    string descript = "";

    public RequstInfo(int id, string name, int type, string nextQuest, int ondData, int twoData, int gold, int rep, int exp, string descript)
    {
        this.id = id;
        this.name = name;
        this.type = type;
        this.nextQuest = nextQuest;
        this.ondData = ondData;
        this.twoData = twoData;
        this.gold = gold;
        this.rep = rep;
        this.exp = exp;
        this.descript = descript;
    }

    public int ID
    {
        get { return id; }
    }

    public string NAME
    {
        get { return name; }
    }

    public int TYPE
    {
        get { return type; }
    }

    public string NEXTQUEST
    {
        get { return nextQuest; }
    }

    public int ONNDATA
    {
        get { return ondData; }
    }

    public int TWODATA
    {
        get { return twoData; }
    }
    
    public int GOLD
    {
        get { return gold; }
    }

    public int REP
    {
        get { return rep; }
    }

    public int EXP
    {
        get { return exp; }
    }

    public string DESCRIPT
    {
        get { return descript; }
    }
}
