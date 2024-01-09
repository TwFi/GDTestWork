using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CooldownButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image cooldownIcon;
    [SerializeField] float cooldownTime;
    public UnityEvent OnClick;

    const float checkNearestEnemyTime = 0.1f;
    Player player;
    bool isCooldown = false;
    bool allowInteractable = true;

    private void Start()
    {
        player = SceneManager.Instance.Player;
        StartCoroutine(CheckNeareastEnemy());
    }

    public void Click()
    {
        Disable();

        OnClick.Invoke();
    }

    private void Disable()
    {
        isCooldown = true;
        button.interactable = false;
        cooldownIcon.gameObject.SetActive(true);
        cooldownIcon.fillAmount = 1;

        StartCoroutine(DelayedEnable(cooldownTime));
    }

    private void Enable()
    {
        isCooldown = false;

        if (allowInteractable)
            button.interactable = true;

        cooldownIcon.gameObject.SetActive(false);
    }

    private IEnumerator DelayedEnable(float time)
    {
        float step = 1 / cooldownTime;
        float value = 1;

        while(value != 0)
        {
            value -= step * Time.deltaTime;
            if (value < 0)
                value = 0;
            cooldownIcon.fillAmount = value;

            yield return null;
        }

        Enable();
    }

    private IEnumerator CheckNeareastEnemy()
    {
        while (true)
        {
            SetAllowInteraction(player.CheckNearestEnemy());
            yield return new WaitForSeconds(checkNearestEnemyTime);
        }
    }

    private void SetAllowInteraction(bool state)
    {
        allowInteractable = state;

        if (allowInteractable)
        {
            if (!button.interactable && !isCooldown)
                button.interactable = true;
        }
        else
        {
            if (button.interactable)
                button.interactable = false;
        }
    }
}
