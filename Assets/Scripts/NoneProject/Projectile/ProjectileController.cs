using UnityEngine;

namespace NoneProject.Projectile
{
    /// 발사체를 관리하는 클래스입니다.
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private GameObject targetObject;
        [SerializeField] private float speed;
        
        private void FixedUpdate()
        {
            // // 플레이어가 존재하는지 체크.
            // if (GameManager.Instance.InGame.Player is null)
            // {
            //     Debug.LogWarning("[SYSTEM] Player is null...");
            //     return;
            // }
            
            // var tr = transform;
            // var position = tr.position;
            // var targetPosition = targetObject.transform.position;
            // var dirVec = (targetPosition - position).normalized;
            // var angle = Vector2.Angle(GameManager.Instance.InGame.Player.MoveVec, dirVec);
            //
            // position += dirVec * (Time.deltaTime * speed);
            // tr.position = position;
            // tr.rotation = Quaternion.Euler(angle, 90.0f, 0.0f);
            //
            // var distance = Vector3.Distance(targetPosition, position);
            //
            // if (Mathf.Abs(distance) < 0.5f)
            // {
            //     gameObject.SetActive(false);
            //     transform.position = GameManager.Instance.InGame.Player.MoveVec;
            //     gameObject.SetActive(true);
            // }
        }
    }
}


