using UnityEngine;

namespace NoneProject.Tile
{
    // Scripted by Raycast
    // 2025.01.25
    // Tile의 위치를 이동하는 클래스입니다.
    public class Tile : MonoBehaviour
    {
        private const string Tag = "PlayerArea";
        
        private float _moveOffset;

        private void Start()
        {
            _moveOffset = GameManager.Instance.Const.TileOffset * 2.0f;
        }

        public void SetPosition(Vector2 movePos)
        {
            transform.position = movePos;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Tag) is false) 
                return;
            
            var otherPos = other.transform.position;
            var tilePos = transform.position;
            var diffX = Mathf.Abs(otherPos.x - tilePos.x);
            var diffY = Mathf.Abs(otherPos.y - tilePos.y);

            var moveVec = Manager.PlayerManager.Instance.Player.MoveVec;
            var dirX = moveVec.x < 0 ? -1 : 1;
            var dirY = moveVec.y < 0 ? -1 : 1;
            
            if (Mathf.Abs(diffX - diffY) <= 0.1f)
            {
                // 대각선 예외 처리.
                transform.Translate(Vector3.right * dirX * _moveOffset);
                transform.Translate(Vector3.up * dirY * _moveOffset);
            }
            else if(diffX > diffY)
            {
                // 좌측 이동.
                transform.Translate(Vector3.right * dirX * _moveOffset);
            }
            else if(diffX < diffY)
            {
                // 우측 이동.
                transform.Translate(Vector3.up * dirY * _moveOffset);
            }
        }
    }
}