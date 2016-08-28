using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour {
  public GameObject[] parentObjects = new GameObject[3];
  public GameObject[] objects = new GameObject[3];
  public int[] maxCount = new int[3];
  private int[] oldCount = new int[3];

  void Start() {
    for (int i = 0; i < 5; i++) { 
      for (int j = 0; j < objects.Length; j++)
      {
        int roll = Random.Range(0, 100);
        if (roll < 220 - (30 * parentObjects[j].transform.childCount))
        {
          spawnObject(j, getRandomSpawn(true));
        }
      }
    }
    setChildrenCount();
  }

  void FixedUpdate() {
    respawn();
    setChildrenCount();
  }


  private void setChildrenCount() {
    for (int i = 0; i < oldCount.Length - 1; i++) {
      oldCount[i] = parentObjects[i].transform.childCount + GameObject.FindGameObjectsWithTag("HamsterItem").Length;
    }
    oldCount[2] = parentObjects[2].transform.childCount;
  }

  private void respawn() {
    for (int i = 0; i < objects.Length-1; i++) {
      if (parentObjects[i].transform.childCount + GameObject.FindGameObjectsWithTag("HamsterItem").Length < oldCount[i]) {
        for (int j = 0; j < objects.Length-1; j++) {
          int roll = Random.Range(0, 100);
          if (roll < 220 - 30 * (parentObjects[j].transform.childCount + GameObject.FindGameObjectsWithTag("HamsterItem").Length))
          {
            spawnObject(j, getRandomSpawn(false));
          }
        }
      }
    }
    if (parentObjects[2].transform.childCount < oldCount[2])
    {
        int roll = Random.Range(0, 100);
        if (roll < 220 - (30 * parentObjects[2].transform.childCount))
        {
          spawnObject(2, getRandomSpawn(false));
        }
    }
  }

  private Vector3 getRandomSpawn(bool first) {
    List<int> candidates = new List<int>();
    for (int i = 0; i < transform.childCount; i++)
    {
      if (!first) { 
        if (transform.GetChild(i).GetComponent<Renderer>().isVisible)
        {
          candidates.Add(i);
        }
      } else
      {
        candidates.Add(i);
      }
    }
    int spawn = Random.Range(0, candidates.Count);
    return transform.GetChild(spawn).transform.position;
  }

  private void spawnObject(int index, Vector3 spawnPosition) {
    //if (!(parentObjects[index].transform.childCount >= maxCount[index])) {
      GameObject ego = Instantiate(objects[index], parentObjects[index].transform) as GameObject;
      ego.transform.position = spawnPosition;
    //}
    
  }
}
