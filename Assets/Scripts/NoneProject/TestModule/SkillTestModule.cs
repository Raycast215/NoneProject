using System.Collections;
using System.Collections.Generic;
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
        var player = ActorManager.Instance.Player;
        
        _skillC ??= new AttackStraight(player.Direction);
        _skillC.Attack("Projectile_IceBolt", count, delay);
    }
}
