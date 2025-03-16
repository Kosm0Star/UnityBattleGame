using System.Collections;
using UnityEngine;

public class Archer : Character
{
    public int poisonDamage = 5;
    public float poisonDuration = 3f;
    public float poisonChance = 0.3f;

    public override void Attack(Character target)
    {
        base.Attack(target);

        Debug.Log($"{characterName} applies poison to {target.characterName}!");

        if (Random.value < poisonChance)
        {
            target.ApplyPoison(poisonDamage, poisonDuration);
        }
    }
}
