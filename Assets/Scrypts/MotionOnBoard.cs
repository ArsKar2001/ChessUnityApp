using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using ChessGame.Assets.Scrypts;
using UnityEngine;
using ChessLibrary;

public class MotionOnBoard : MonoBehaviour 
{
    Dictionary<string, GameObject> squares;
    Dictionary<string, GameObject> figures;

    DragAndDrop dad;
    Chess Chess;

    public MotionOnBoard()
    {
        squares = new Dictionary<string, GameObject>();
        figures = new Dictionary<string, GameObject>();
        Chess = new Chess();
        dad = new DragAndDrop(DropObject, PickObject);
    }

    private void PickObject(Vector2 from)
    {
        MarkSquaresTo(VectorToSquare(from));
    }

    private void MarkSquaresTo(string v)
    {
        UnmarkSquares();        
        foreach (var item in Chess.YieldValidMoves())
        {
            if (v == item.Substring(1, 2))
                ShowSquare(item[3] - 'a', item[4] - '1', true);
        }
    }

    void UnmarkSquares()
    {
        for (int y = 0; y < 8; y++)
            for (int x = 0; x < 8; x++)
            {
                ShowSquare(x, y, false);
            }
    }

    private void Start()
    {
        InitGameObjects();
        ShowFigures();
        MarkSquaresFrom();
    }

    private void InitGameObjects()
    {
        for (int y = 0; y < 8; y++)
            for (int x = 0; x < 8; x++)
            {
                string key = "" + x + y;
                GameObject square = GameObject.Find(WhatSquare(x, y));
                squares[key] = Instantiate(square);
                squares[key].transform.position = new Vector2(x * 2, y * 2);

                figures[key] = Instantiate(GameObject.Find("p"));
                figures[key].transform.position = squares[key].transform.position;
                figures[key].name = "p";

            }
    }

    string WhatSquare(int x, int y) => 
        (x + y) % 2 == 0 ? "BlackSquare" : "WhiteSquare";

    void ShowFigures()
    {
        for (int y = 7; y >= 0; y--)
            for (int x = 0; x < 8; x++) 
            {
                string key = "" + x + y;
                string figure = Chess.GetFigure(y, x).ToString();
                figures[key].transform.position = squares[key].transform.position;
                if (figures[key].name == figure) continue;
                figures[key].GetComponent<SpriteRenderer>().sprite = 
                    GameObject.Find(figure).GetComponent<SpriteRenderer>().sprite;
                figures[key].GetComponent<SpriteRenderer>().sortingOrder = 1;
                figures[key].name = figure;
            }
    }

    // Update is called once per frame
    void Update () {
        dad.Action ();
    }

    void DropObject(Vector2 from, Vector2 to)
    {
        Debug.Log(from + " " + to);
        string fromTo = VectorToSquare(from);
        string toFrom = VectorToSquare(to);
        string figure = Chess.GetFigure(fromTo).ToString();
        string move = figure + fromTo + toFrom;
        Chess = Chess.Move(move);
        Debug.Log(move);
        ShowFigures();
        MarkSquaresFrom();
    }

    string VectorToSquare(Vector2 vector)
    {
        int x = Convert.ToInt32(vector.x) / 2;
        int y = Convert.ToInt32(vector.y) / 2;
        if (x >= 0 && x <= 7 && y >= 0 && y <= 7)
            return ((char)('a' + x)).ToString() + (y + 1).ToString();
        return "";
    }

    void ShowSquare(int x, int y, bool mark)
    {
        string square = WhatSquare(x, y);
        if (mark)
            square += "Mark";
        squares["" + x + y].GetComponent<SpriteRenderer>().sprite =
            GameObject.Find(square).GetComponent<SpriteRenderer>().sprite;
    }
    public void MarkSquaresFrom()
    {
        UnmarkSquares();
        foreach (var item in Chess.YieldValidMoves())
        {
            ShowSquare(item[1] - 'a', item[2] - '1', true);
        }
    }
}