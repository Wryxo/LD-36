using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {
  public GameObject[] parentObjects = new GameObject[3];
  public GameObject[] objects = new GameObject[3];
  public int[] maxCount = new int[3];
  private int[] oldCount = new int[3];

  void Awake() {
    setChildrenCount();
  }

  void FixedUpdate() {
    respawn();
    setChildrenCount();
  }


  private void setChildrenCount() {
    for (int i = 0; i < oldCount.Length; i++) {
      oldCount[i] = parentObjects[i].transform.childCount;
    }
  }
  private void respawn() {
    for (int i = 0; i < objects.Length; i++) {
      if (parentObjects[i].transform.childCount < oldCount[i]) {
        for (int j = 0; j < objects.Length; j++) {
          spawnObject(j, getRandomSpawn());
        }
      }
    }
  }

  private Vector3 getRandomSpawn() {
    int spawn = Random.Range(0, transform.childCount);
    return transform.GetChild(spawn).transform.position;
  }

  private void spawnObject(int index, Vector3 spawnPosition) {
    if (!(parentObjects[index].transform.childCount >= maxCount[index])) {
      GameObject ego = Instantiate(objects[index], parentObjects[index].transform) as GameObject;
      ego.transform.position = spawnPosition;
    }
    
  }
}
