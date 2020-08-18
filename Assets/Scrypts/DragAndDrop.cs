using UnityEngine;

namespace ChessGame.Assets.Scrypts {
    /// <summary>
    /// Обработчик для перетаскивания фигур.
    /// </summary>
    public class DragAndDrop {
        State state;
        /// <summary>
        /// Получает игровой объект на сцене.
        /// </summary>
        GameObject gameObject;
        /// <summary>
        /// Зпоминает позицию мыши
        /// </summary>
        Vector2 GetVectorOffSet;
        public DragAndDrop () {
            Drop ();
        }
        /// <summary>
        /// Определение возможных состояний фигуры.
        /// </summary>
        enum State {
            /// <summary>
            /// Фигура стоит
            /// </summary>
            none,
            /// <summary>
            /// Фигура перетаскивается.
            /// </summary>
            drag
        }
        /// <summary>
        /// Вхаимодействие с фигурами на доске.
        /// </summary>
        public void Action () {
            Debug.Log (state);
            switch (state) {
                case State.none:
                    if (IsMouseButtunPress ()) {
                        PickUp ();
                    }
                    break;
                case State.drag:
                    if (IsMouseButtunPress ()) {
                        Drag ();
                    } else {
                        Drop ();
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Конец перетаскивания.
        /// </summary>
        private void Drop () {
            gameObject = null;
            state = State.none;
        }
        /// <summary>
        /// Фигура перетаскивается.
        /// </summary>
        private void Drag () {
            gameObject.transform.position = GetClickPosition () + GetVectorOffSet;
        }

        private bool IsMouseButtunPress () => Input.GetMouseButton (0);

        private void PickUp () {
            Vector2 clickPosition = GetClickPosition ();
            Transform clickedItem = GetItenAt (clickPosition);
            if (clickedItem == null) {
                return;
            }
            state = State.drag;
            GetVectorOffSet = (Vector2)clickedItem.position - clickPosition;
            gameObject = clickedItem.gameObject;
            Debug.Log (message: gameObject);
        }

        private Vector2 GetClickPosition () {
            return Camera.main.ScreenToWorldPoint (Input.mousePosition);
        }

        private Transform GetItenAt (Vector2 position) {
            RaycastHit2D[] raycastHit2Ds = Physics2D.RaycastAll (position, position, 0.5f);
            if (raycastHit2Ds.Length == 0) {
                return null;
            }
            return raycastHit2Ds[0].transform;
        }

    }
}