using UnityEngine;

public class ContolPanelDisabler : MonoBehaviour
{
    private void Start()
    {
        SceneManager.Instance.OnLose += PlayerLose;
    }

    private void PlayerLose()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        SceneManager.Instance.OnLose -= PlayerLose;
    }
}
