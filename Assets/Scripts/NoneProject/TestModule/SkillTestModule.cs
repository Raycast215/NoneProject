using Cysharp.Threading.Tasks;
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
    
    public void SkillC()
    {
        var player = NoneProject.Manager.PlayerManager.Instance.Player;
        var playerTransform = player.transform;
     
        _skillC ??= new AttackStraight(playerTransform);
        _skillC.SetStartPos(player.PlayerCenter.position);
        _skillC.SetMoveVec(player.Direction);
        _skillC.Attack("Projectile_IceBolt", count, delay);
    }

    private bool _isTest;
    
    private async void FixedUpdate()
    {
        if (_isTest)
            return;

        if (GameManager.Instance.isInitialized is false)
            return;

        if (GameManager.Instance.InGame is null)
            return;
        
        if (EnemyManager.Instance.IsEnemyActivate)
        {
            var player = NoneProject.Manager.PlayerManager.Instance.Player;
            var playerTransform = player.transform;
            var enemy = EnemyManager.Instance.GetNearEnemy(playerTransform.position);

            if (enemy is null) 
                return;

            _isTest = true;
            
            var dir = (enemy.transform.position - playerTransform.position).normalized;
                
            _skillC ??= new AttackStraight(playerTransform);
            _skillC.SetStartPos(player.PlayerCenter.position);
            _skillC.SetMoveVec(dir);
            _skillC.Attack("Projectile_IceBolt", 1, 3.0f);

            await UniTask.WaitForSeconds(3.0f);
            
            _isTest = false;
        }
    }
}
