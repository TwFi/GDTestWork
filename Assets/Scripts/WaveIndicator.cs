using UnityEngine;
using UnityEngine.UI;

public class WaveIndicator : MonoBehaviour
{
    [SerializeField] Text text;

    int maxWaves;
    private void Start()
    {
        maxWaves = SceneManager.Instance.MaxWaves;
        SceneManager.Instance.OnWaveSpawned += WaveChanged;
    }

    private void WaveChanged(int wave)
    {
        text.text = $"Wave {wave}/{maxWaves}";
    }

    private void OnDestroy()
    {
        SceneManager.Instance.OnWaveSpawned -= WaveChanged;
    }
}
