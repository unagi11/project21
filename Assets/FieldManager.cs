﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using UnityEngine.Tilemaps;

public class FieldManager : MonoBehaviour
{
    public static FieldManager instance;

    public Tilemap tileMap;

    /// <summary>
    /// Tile.cs의 Category와 똑같이 사용해야 작동한다.
    /// </summary>
    public List<TileBase> tileBases;
    public List<char> tileBasesChar;

    // 현재 전투의 모든 타일
    public Model.Tile[,] field;

    // 필드 데이터
    [TextArea(10,20)]
    public List<string> FieldDatas = new List<string>()
    {
        "WWWWWWWWWWWWWWWW\n" +
        "WWWWWWWWWWWWWWWW\n" +
        "WWWWWWWWWWWWWWWW\n" +
        "WWWFFFFFFFFFFWWW\n" +
        "WWWFFFFFFFFFFWWW\n" +
        "WWWFFFFFFFFFFWWW\n" +
        "WWWFFFFFFFFFFWWW\n" +
        "WWWFFFFFFFFFFWWW\n" +
        "WWWFFFFFFFFFFWWW\n" +
        "WWWFFFFFFFFFFWWW\n" +
        "WWWFFFFFFFFFFWWW\n" +
        "WWWFSFFFFFFFFWWW\n" +
        "WWWFFFFFFFFFFWWW\n" +
        "WWWWWWWWWWWWWWWW\n" +
        "WWWWWWWWWWWWWWWW\n" +
        "WWWWWWWWWWWWWWWW\n"
        ,
        "WWWWWWWWWWWWWWWW\n" +
        "WWWWWWWWWWWWWWWW\n" +
        "WWFFFFFTTFFFFFWW\n" +
        "WWFFFFFTTFFFFFWW\n" +
        "WWFFFFFTTFFFFFWW\n" +
        "WWFFFFFTTFFFFFWW\n" +
        "WWFFFFFTTFFFFFWW\n" +
        "WWTTTTTTTTTTTTWW\n" +
        "WWTTTTTTTTTTTTWW\n" +
        "WWFFFFFTTFFFFFWW\n" +
        "WWFFFFFTTFFFFFWW\n" +
        "WWFFFFFTTFFFFFWW\n" +
        "WWFFFFFTTFFFFFWW\n" +
        "WWFFFFFTTFFFFFWW\n" +
        "WWWWWWWWWWWWWWWW\n" +
        "WWWWWWWWWWWWWWWW\n"
    };

    public void InitField(string fieldData)
    {
        char[] chars = fieldData.ToCharArray();

        int col = 0, row = 0;
        for (int i = 0; i < fieldData.Length; i++)
        {
            if (chars[i] == '\n')
                row++;
        }
        col = fieldData.Length / row - 1;

        field = new Model.Tile[row, col];

        int x = 0, y = row - 1;
        for (int i = 0; i < fieldData.Length; i++)
        {
            if (chars[i] == '\n')
            {
                x = 0;
                y--;
            }
            else
            {
                field[y, x] = new Model.Tile();
                field[y, x].category = (Model.Tile.Category)tileBasesChar.IndexOf(chars[i]);
                tileMap.SetTile(
                    new Vector3Int(x, y, 0), 
                    tileBases[tileBasesChar.IndexOf(chars[i])]
                    );
                x++;
            }
        }

        //field = new Model.Tile[fildData.GetLength(0), fildData.GetLength(1)];

        //for (int y = 0; y < fildData.GetLength(0); y++)
        //{
        //    for (int x = 0; x < fildData.GetLength(1); x++)
        //    {
        //        field[y, x] = new Model.Tile();
        //        field[y, x].category = (Model.Tile.Category)fildData[y, x];
        //    }
        //}

        //for (int y = 0; y < field.GetLength(0); y++)
        //{
        //    for (int x = 0; x < field.GetLength(1); x++)
        //    {
        //        tileMap.SetTile(new Vector3Int(x, y, 0), tileBases[(int) field[y, x].category]);
        //    }
        //}
    }

    private void Awake()
    {
        instance = this;
        tileMap = GetComponentInChildren<Tilemap>();
    }
}
