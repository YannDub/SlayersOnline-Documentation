using System.IO;
using UnityEngine;

public class Map
{
    private Vector2[,] mapLow;
    private Vector2[,] mapHigh;
    public int width;
    public int height;
    public string name;
    public string chipset;

    public Map(string name, string chipset, int width, int height) {
        mapLow = new Vector2[width, height];
        mapHigh = new Vector2[width, height];
        this.width = width;
        this.height = height;
        this.name = name;
        this.chipset = chipset;
    }

    public void SetLow(int x, int y, int tileX, int tileY) {
        mapLow[x,y] = new Vector2(tileX, tileY);
    }

    public void SetHigh(int x, int y, int tileX, int tileY) {
        mapHigh[x,y] = new Vector2(tileX, tileY);
    }
}