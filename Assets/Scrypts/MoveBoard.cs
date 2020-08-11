using System.Collections;
using System.Globalization;
using UnityEngine;

public class MoveBoard : MonoBehaviour {
    DragAndDrop dad = new DragAndDrop ();
    // Update is called once per frame
    void Update()
    {
        dad.Action();
    }
}

class DragAndDrop
{
    State state;
    GameObject gameObject;

    public DragAndDrop(GameObject gameObject = null)
    {
        state = State.none;
        this.gameObject = gameObject;
    }

    enum State {
        none,
        drag
    }

    public void Action () {
        Debug.Log (state);
        switch (state) {
            case State.none:
                if (IsMouseButtunPress ()) {
                    PickUp();
                }
                break;
            case State.drag:
                //if (IsMouseButtunPress())
                //{
                //    drag();
                //} else {
                //    drop();  
                //}
                break;
            default:
                break;
        }
    }
    private bool IsMouseButtunPress() => Input.GetMouseButton(0);

    private void PickUp()
    {
        Vector2 clickPosition = GetClickPosition();
        Transform clickedItem = GetItenAt(clickPosition);
        if (Equals(clickedItem))
        {
            return;
        }
        Debug.Log(clickedItem.gameObject.name);
    }

    private Vector2 GetClickPosition () {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition.normalized);
    }
    private Transform GetItenAt(Vector2 position)
    {
       RaycastHit2D[] raycastHit2Ds = Physics2D.RaycastAll(position, position, 0.5f);
        if (raycastHit2Ds.Length == 0)
        {
            return null;
        }
        return raycastHit2Ds[0].transform;
    }

}