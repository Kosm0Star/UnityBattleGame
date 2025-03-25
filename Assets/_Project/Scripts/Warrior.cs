using UnityEngine;

public class Warrior : Character
{
    [SerializeField] private float _stunChance = 0.2f;
    [SerializeField] private float _stunDuration = 2f;

    public override void Attack(Character target, UIManager uIManager)
    {
        if (!target.IsAlive())
        {
            return;
        }

        target.TakeDamage(AttackPower);

        uIManager.ShowDamage(AttackPower, _characterName);

        if (Random.value < _stunChance)
        {
            StunEffect stunEffect = new StunEffect { Duration = _stunDuration };
            target.ApplyEffect(stunEffect, uIManager, _characterName);
        }
    }
}
