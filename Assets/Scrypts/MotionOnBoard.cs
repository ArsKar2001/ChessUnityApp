using System;
using System.Collections;
using System.Globalization;
using ChessGame.Assets.Scrypts;
using UnityEngine;

public class MotionOnBoard : MonoBehaviour {
    DragAndDrop dad = new DragAndDrop ();
    // Update is called once per frame
    void Update () {
        dad.Action ();
    }
}