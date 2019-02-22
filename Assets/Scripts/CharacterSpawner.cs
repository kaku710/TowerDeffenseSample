using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    private GameObject character;
    private GameObject parent;
    private float spawnTimer;
    [SerializeField] private float interval; //生成間隔。Inspector上で設定
    [SerializeField] private string spawnCharaTag; //Inspectorで設定

    private void Start()
    {
        switch(spawnCharaTag)
        {
            case "Player":
                parent = GameObject.Find("Players"); 
                break;
            case "Enemy":
                parent = GameObject.Find("Enemies"); 
                break;
        }
    }
    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= interval)
        {
            switch(spawnCharaTag)
            {
                case "Player":
                    PlayerSpawn();
                    break;
                case "Enemy":
                    EnemySpawn();
                    break;
            }
        }
    }
    private void PlayerSpawn()
    {
        int x = Random.Range(0, 7);
        character = Resources.Load<GameObject>("Players/Player" + x);
        var createdPlayer = Instantiate(character, transform.position, Quaternion.identity);
        createdPlayer.transform.SetParent(parent.transform);
        spawnTimer = 0;
    }
    private void EnemySpawn()
    {
        int x = Random.Range(0, 3);
        character = Resources.Load<GameObject>("Enemies/Enemy" + x);
        var createdEnemy = Instantiate(character, transform.position, Quaternion.identity);
        createdEnemy.transform.SetParent(parent.transform);
        spawnTimer = 0;
    }
}
