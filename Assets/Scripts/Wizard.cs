using UnityEngine;

public class Wizard : Character
{
    public float debuffMultiplier = 0.5f;
    public float debuffDuration = 2f;

    public override void Attack(Character target)
    {
        base.Attack(target);

        Debug.Log($"{characterName} applies debuff to {target.characterName}!");

        target.ApplyDebuff(debuffMultiplier, debuffDuration);
    }
}
