using UnityEngine;
using UnityEngine.UI;

public class MainNotify : MonoBehaviour
{
    [SerializeField] Text text;

    const string winText = "You Win";
    const string loseText = "You Lose";

    private void Start()
    {
        SceneManager.Instance.OnWin += PlayerWin;
        SceneManager.Instance.OnLose += PlayerLose;
    }

    private void PlayerLose()
    {
        text.text = loseText;
    }

    private void PlayerWin()
    {
        text.text = winText;
    }

    private void OnDestroy()
    {
        SceneManager.Instance.OnWin -= PlayerWin;
        SceneManager.Instance.OnLose -= PlayerLose;
    }
}
