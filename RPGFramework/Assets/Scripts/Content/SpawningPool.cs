using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0;
    [SerializeField]
    int _reserveCount = 0;
    [SerializeField]
    int _keepMonsterCount = 0;

    [SerializeField]
    Vector3 _spawnPos;
    [SerializeField]
    float _spawnRadius = 15.0f;
    [SerializeField]
    float _spawnTime = 5.0f;

    public void AddMonsterCount(int value) { _monsterCount += value; }
    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }

    void Start()
    {
        GeneralManager.Object.OnSpawnEvent -= AddMonsterCount;
        GeneralManager.Object.OnSpawnEvent += AddMonsterCount;
    }

    void Update()
    {
        while (_monsterCount + _reserveCount < _keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
        }
    }

    IEnumerator ReserveSpawn()
    {
        _reserveCount++;

        yield return new WaitForSeconds(Random.Range(0, _spawnTime));
        GameObject obj = GeneralManager.Object.Spawn(Define.WorldObject.Monster, "Knight");
        NavMeshAgent agent = obj.GetOrAddComponent<NavMeshAgent>();

        Vector3 randPos;
        while (true)
        {
            Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius);
            randDir.y = 0;
            randPos = _spawnPos + randDir;

            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(randPos, path))
                break;
        }

        obj.transform.position = randPos;
        _reserveCount--;
    }
}
