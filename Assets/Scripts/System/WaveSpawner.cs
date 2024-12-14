using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using static EnemyUnitData;

/// <summary>
/// �e�E�F�[�u���ƂɃG�l�~�[�̐��Ȃǂ�ݒ肷��
/// </summary>
public class WaveSpawner : MonoBehaviour
{
    // �X�|�[��������Enemy
    public List<GameObject> enemyPrefabs;
    public Transform spawnPoint;
    // �E�F�[�u�Ԃ̎���
    public float timeBetweenWaves = 5.0f;
    public Text waveCountDownText;
    public Text waveCountText;
    // �ő�E�F�[�u��
    public int maxWave = 10;

    private float countDown = 2.0f;

    // Wave�ԍ�
    private int waveNumber = 0;

    public List<WaveData> waves;

    private bool waveInProgress = false;

    void Update()
    {
        if(!waveInProgress && countDown <= 0.0f)
        {
            if(waveNumber < maxWave)
            {
                // �E�F�[�u�𐶐�����
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
    /// �E�F�[�u�𐶐�����
    /// </summary>
    IEnumerator CreateWave()
    {
        waveNumber++;
        Debug.Log("Create Wave " + waveNumber);
        waveInProgress = true;
        var nextWaveIntarval = 0.0f;

        // ���݂̃E�F�[�u�̐ݒ���擾
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

        // �E�F�[�u������̑ҋ@����
        yield return new WaitForSeconds(nextWaveIntarval);
        // ���̃E�F�[�u�܂ł̃J�E���g�_�E�������Z�b�g
        countDown = 0;
        waveInProgress = false;
    }

    /// <summary>
    /// Enemy�𐶐�����
    /// </summary>
    void SpawnEnemy(EnemyUnitData.EnemyType enemyType)
    {
        if(enemyPrefabs.Count == 0)
        {
            return;
        }

        // �Ή�����G�l�~�[��I��
        GameObject chosenEnemy = enemyPrefabs.Find(enemy => enemy.GetComponent<Enemy>().enemyUnitData.enemyType == enemyType);

        if (chosenEnemy != null)
        {
            GameObject clone = Instantiate(chosenEnemy, spawnPoint.position, spawnPoint.rotation);
            clone.name = chosenEnemy.name + "_" + (waveNumber - 1) + "_" + (Time.frameCount % 100); // ���j�[�N�Ȗ��O
        }
        else
        {
            Debug.LogWarning($"No enemy found for type: {enemyType}. Double-check your enemy prefabs.");
        }
    }
}
