using NoneProject;
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
        var player = ActorManager.Instance.Player.transform;
        
        _skillA ??= new AttackStraightFormAngle(player);
        _skillA.Attack("Projectile_IceBolt", count, 0.0f);
    }

    public void SkillB()
    {
        var player = ActorManager.Instance.Player.transform;
        
        _skillB ??= new AttackStraightFormAngle(player);
        _skillB.Attack("Projectile_IceBolt", count, delay);
    }
    
    public void SkillC()
    {
        var directionPoint = GameManager.Instance.InGame.DirectionPoint;
        var player = ActorManager.Instance.Player;
        var playerTransform = player.transform;
        var moveVec = (directionPoint.position - playerTransform.position).normalized;
        
        _skillC ??= new AttackStraight(playerTransform);
        _skillC.SetMoveVec(moveVec);
        _skillC.Attack("Projectile_IceBolt", count, delay);
    }
}
