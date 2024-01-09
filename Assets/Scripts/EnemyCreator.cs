using UnityEngine;

public class EnemyCreator : Singleton<EnemyCreator>
{
    public Enemie Create(Enemie.Types type, Vector3 position)
    {
        if (SceneManager.Instance.Entities.TryGetPrefub(type, out GameObject prefub))
            return Create(prefub, position);
        else
            return null;
    }

    public Enemie CreateOnRandomPosition(Enemie.Types type)
    {
        if (SceneManager.Instance.Entities.TryGetPrefub(type, out GameObject prefub))
            return CreateOnRandomPosition(prefub);
        else
            return null;
    }

    public Enemie CreateOnRandomPosition(GameObject prefub)
    {
        Vector3 position = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        return Create(prefub, position);
    }

    public Enemie Create(GameObject prefub, Vector3 position)
    {
        if (!prefub.TryGetComponent(out Enemie e))
        {
            Debug.LogError("Attempt create enemy from non enemie prefub");
            return null;
        }

        Enemie enemy = Instantiate(prefub, position, Quaternion.identity).GetComponent<Enemie>();
        SceneManager.Instance.AddEnemie(enemy);

        return enemy;
    }
}
