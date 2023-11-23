using UnityEngine;

public class MeleeWeapon : Weapon
{
    public override void Attack()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(_myCreature.Skin.AttackPoint.position, _attackRange, Vector2.zero, 0f, _myCreature.AttackLayers);

        Creature tar;

        foreach (RaycastHit2D en in hits)
        {
            tar = en.collider.GetComponent<Creature>();
            if (tar.CurrentLine == _myCreature.CurrentLine || tar.LineToMove == _myCreature.CurrentLine)
            {
                bool tarKilled = tar.TakeDamage(_damage, _myCreature.gameObject);
                if (_myCreature is ThisIsPlayer player && tarKilled) player.AddKill();
            }
        }

        Reload();
    }
}
