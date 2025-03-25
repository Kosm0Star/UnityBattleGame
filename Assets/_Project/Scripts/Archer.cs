using UnityEngine;

public class Archer : Character
{
    [SerializeField] private float _poisonChance = 0.3f;
    [SerializeField] private float _poisonDuration = 3f;
    [SerializeField] private float _poisonDamagePerTick = 5f;

    public override void Attack(Character target, UIManager uIManager)
    {
        if (IsStunned())
        {
            Debug.Log($"{_characterName} is stunned and cannot attack!");
            return;
        }

        if (!target.IsAlive())
        {
            return;
        }

        target.TakeDamage(AttackPower);

        uIManager.ShowDamage(AttackPower, _characterName);

        if (Random.value < _poisonChance)
        {
            PoisonEffect poisonEffect = new PoisonEffect
            {
                Duration = _poisonDuration,
                DamagePerTick = _poisonDamagePerTick,
            };

            target.ApplyEffect(poisonEffect, uIManager, _characterName);
        }
    }
}
