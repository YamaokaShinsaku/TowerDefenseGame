using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 各ウェーブごとにエネミーの数などを設定する
/// </summary>
public class WaveSpawner : MonoBehaviour
{
    // スポーンさせるEnemy
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    // ウェーブ間の時間
    public float timeBetweenWaves = 5.0f;
    public Text waceCountDownText;

    private float countDown = 2.0f;

    // Wave番号
    private int waveNumber = 0;

    void Update()
    {
        if(countDown <= 0.0f)
        {
            // ウェーブを生成する
            StartCoroutine(CreateWave());
            countDown = timeBetweenWaves;
        }

        countDown -= Time.deltaTime;
        // (小数点以下切り捨て)
        waceCountDownText.text = Mathf.Round(countDown).ToString();
    }

    /// <summary>
    /// ウェーブを生成する
    /// </summary>
    IEnumerator CreateWave()
    {
        waveNumber++;
        Debug.Log("Create Wave " + waveNumber);
        for(int i = 0; i < waveNumber; i++) 
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    /// <summary>
    /// Enemyを生成する
    /// </summary>
    void SpawnEnemy()
    {
        int count = 0;
        GameObject clone = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        count++;
        clone.name = clone.name + count.ToString();
    }
}
