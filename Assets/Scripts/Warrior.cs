using UnityEngine;

public class Warrior : Character
{
    public float stunChance = 0.3f;
    public float stunDuration = 2f;

    public override void Attack(Character target)
    {
        base.Attack(target);

        if (Random.value < stunChance)
        {
            Debug.Log($"{characterName} stuns {target.characterName}!");

            target.ApplyStun(stunDuration);
        }
    }
}
