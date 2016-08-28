using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {
  public GameObject Hamsters;
  public GameObject Enemies;

  public GameObject HamsterObject;
  public GameObject EnemyObject;

  public int HamstersCount;
  public int EnemiesCount;

  private int oldHamstersCount = -1;
  private int oldEnemiesCount = -1;

  void FixedUpdate() {
    check(ref oldEnemiesCount, Enemies.transform.childCount);
    check(ref oldHamstersCount, Hamsters.transform.childCount);
  }

  private void check(ref int oldCount, int newCount) {
    if (oldCount == -1) oldCount = newCount;

    if (newCount < oldCount) {
      spawn(EnemyObject, getRandomSpawn(), Enemies.transform);
      spawn(HamsterObject, getRandomSpawn(), Hamsters.transform);
      oldCount = newCount + 1;
    }
  }

  private Vector3 getRandomSpawn() {
    int spawn = Random.Range(0, transform.childCount);
    return transform.GetChild(spawn).transform.position;
  }

  private void spawn(GameObject spawnObject, Vector3 spawnPosition, Transform parent) {
    GameObject ego = Instantiate(spawnObject, parent) as GameObject;
    ego.transform.position = spawnPosition;
  }
}
