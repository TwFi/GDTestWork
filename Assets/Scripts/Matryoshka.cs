using UnityEngine;

public class Matryoshka : Enemie
{
    const int childCount = 2;
    const float spawnOffset = 1f;

    protected override void Die()
    {
        for (int i = 0; i < childCount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-spawnOffset, spawnOffset), 0, Random.Range(-spawnOffset, spawnOffset));
            EnemyCreator.Instance.Create(Types.SmallGoblin, transform.position + offset);
        }

        base.Die();
    }
}
