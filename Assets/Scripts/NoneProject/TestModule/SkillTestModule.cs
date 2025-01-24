using NoneProject.Actor.Component.Attack;
using NoneProject.Manager;
using UnityEngine;

public class SkillTestModule : MonoBehaviour
{
    private AttackStraightFormAngle _skillA;
    private AttackStraightFormAngle _skillB;
    private AttackStraight _skillC;

    [SerializeField] private int count;
    [SerializeField] private float delay;
    
    public void SkillA()
    {
        var startTransform = NoneProject.Manager.PlayerManager.Instance.Player.PlayerCenter;
        
        _skillA ??= new AttackStraightFormAngle(startTransform);
        _skillA.Attack("Projectile_IceBolt", count, 0.0f);
    }

    public void SkillB()
    {
        var startTransform = NoneProject.Manager.PlayerManager.Instance.Player.PlayerCenter;
        
        _skillB ??= new AttackStraightFormAngle(startTransform);
        _skillB.Attack("Projectile_IceBolt", count, delay);
    }
    
    public async void SkillC()
    {
        await EnemyManager.Instance.Get("Enemy_Normal_00");
        // var directionPoint = GameManager.Instance.InGame.DirectionPoint;
        // var player = ActorManager.Instance.Player;
        // var playerTransform = player.transform;
        // var moveVec = (directionPoint.position - playerTransform.position).normalized;
        //
        // _skillC ??= new AttackStraight(playerTransform);
        // _skillC.SetMoveVec(moveVec);
        // _skillC.Attack("Projectile_IceBolt", count, delay);
    }
}
