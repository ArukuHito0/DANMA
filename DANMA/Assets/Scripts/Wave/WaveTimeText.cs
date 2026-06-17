using TMPro;
using UnityEngine;

public class WaveTimeText : MonoBehaviour
{
    private TextMeshProUGUI waveTimeText;

    private EnemyGenerator enemyGene;

    private void OnDisable()
    {
        enemyGene.OnUpdateWaveTime -= UpdateWaveTime;
    }

    private void Awake()
    {
        waveTimeText = GetComponent<TextMeshProUGUI>();
        enemyGene = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>();
        enemyGene.OnUpdateWaveTime += UpdateWaveTime;
    }

    private void UpdateWaveTime(int time)
    {
        waveTimeText.text = time.ToString("F0");
    }
}
