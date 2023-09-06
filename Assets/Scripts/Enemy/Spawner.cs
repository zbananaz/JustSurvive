using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Bat;
    public float spawnRadius = 20f;
    public float spawnInterval = 1f;
    public Transform player;

    private float spawnTimer = 0f;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 playerPosition = player.position; // Đây là vị trí của người chơi
        float randomAngle = Random.Range(0f, 360f); // Góc ngẫu nhiên trong khoảng 0-360 độ
        float angleInRadian = randomAngle * Mathf.Deg2Rad; // Chuyển góc sang radian

        float spawnX = playerPosition.x + spawnRadius * Mathf.Cos(angleInRadian);
        float spawnY = playerPosition.y + spawnRadius * Mathf.Sin(angleInRadian);

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        Bat = ObjectPooling.instance.GetObjectFromPool("Bat");
        Bat.transform.position = spawnPosition;
        Bat.SetActive(true);
        //Instantiate(Bat, spawnPosition, Quaternion.identity);
    }
}
