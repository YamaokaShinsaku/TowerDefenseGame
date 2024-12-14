using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using static EnemyUnitData;

/// <summary>
/// 各ウェーブごとにエネミーの数などを設定する
/// </summary>
public class WaveSpawner : MonoBehaviour
{
    // スポーンさせるEnemy
    public List<GameObject> enemyPrefabs;
    public Transform spawnPoint;
    // ウェーブ間の時間
    public float timeBetweenWaves = 5.0f;
    public Text waveCountDownText;
    public Text waveCountText;
    // 最大ウェーブ数
    public int maxWave = 10;

    private float countDown = 2.0f;

    // Wave番号
    private int waveNumber = 0;

    public List<WaveData> waves;

    private bool waveInProgress = false;

    void Update()
    {
        if(!waveInProgress && countDown <= 0.0f)
        {
            if(waveNumber < maxWave)
            {
                // ウェーブを生成する
                StartCoroutine(CreateWave());
                countDown = timeBetweenWaves;
            }
        }

        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0.0f, Mathf.Infinity);
        waveCountDownText.text = string.Format("{0:00.00}", countDown);
        waveCountText.text =  waveNumber.ToString();
    }

    /// <summary>
    /// ウェーブを生成する
    /// </summary>
    IEnumerator CreateWave()
    {
        waveNumber++;
        Debug.Log("Create Wave " + waveNumber);
        waveInProgress = true;
        var nextWaveIntarval = 0.0f;

        // 現在のウェーブの設定を取得
        if(waveNumber - 1 < waves.Count)
        {
            WaveData currentWaveData = waves[waveNumber - 1];
            nextWaveIntarval = currentWaveData.waveIntarval;
            for (int i = 0; i < currentWaveData.enemyCount; i++)
            {
                SpawnEnemy(currentWaveData.enemyType);
                yield return new WaitForSeconds(currentWaveData.spawnIntarval);
            }
        }

        // ウェーブ完了後の待機時間
        yield return new WaitForSeconds(nextWaveIntarval);
        // 次のウェーブまでのカウントダウンをリセット
        countDown = 0;
        waveInProgress = false;
    }

    /// <summary>
    /// Enemyを生成する
    /// </summary>
    void SpawnEnemy(EnemyUnitData.EnemyType enemyType)
    {
        if(enemyPrefabs.Count == 0)
        {
            return;
        }

        // 対応するエネミーを選択
        GameObject chosenEnemy = enemyPrefabs.Find(enemy => enemy.GetComponent<Enemy>().enemyUnitData.enemyType == enemyType);

        if (chosenEnemy != null)
        {
            GameObject clone = Instantiate(chosenEnemy, spawnPoint.position, spawnPoint.rotation);
            clone.name = chosenEnemy.name + "_" + (waveNumber - 1) + "_" + (Time.frameCount % 100); // ユニークな名前
        }
        else
        {
            Debug.LogWarning($"No enemy found for type: {enemyType}. Double-check your enemy prefabs.");
        }
    }
}
