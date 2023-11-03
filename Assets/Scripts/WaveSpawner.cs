using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// �e�E�F�[�u���ƂɃG�l�~�[�̐��Ȃǂ�ݒ肷��
/// </summary>
public class WaveSpawner : MonoBehaviour
{
    // �X�|�[��������Enemy
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    // �E�F�[�u�Ԃ̎���
    public float timeBetweenWaves = 5.0f;
    public Text waceCountDownText;

    private float countDown = 2.0f;

    // Wave�ԍ�
    private int waveNumber = 0;

    void Update()
    {
        if(countDown <= 0.0f)
        {
            // �E�F�[�u�𐶐�����
            StartCoroutine(CreateWave());
            countDown = timeBetweenWaves;
        }

        countDown -= Time.deltaTime;
        // (�����_�ȉ��؂�̂�)
        waceCountDownText.text = Mathf.Round(countDown).ToString();
    }

    /// <summary>
    /// �E�F�[�u�𐶐�����
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
    /// Enemy�𐶐�����
    /// </summary>
    void SpawnEnemy()
    {
        int count = 0;
        GameObject clone = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        count++;
        clone.name = clone.name + count.ToString();
    }
}
