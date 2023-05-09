using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExplosionService : GenericMonoSingleton<ExplosionService>
{
    [SerializeField] GameObject OrbPrefab;
    [SerializeField] int InitialPoolCount;
    List<GameObject> OrbPool;
    
    private void Start() {
        GenerateOrbPool(InitialPoolCount, OrbPrefab);    
    }

    private void GenerateOrbPool(int PoolCount, GameObject OrbPrefab) {
        OrbPool = new List<GameObject>();
        for (int i = 0; i < PoolCount; i++) {
            GameObject Orb = GameObject.Instantiate(OrbPrefab, transform.position, Quaternion.identity, transform);
            Orb.SetActive(false);
            OrbPool.Add(Orb);
        }      
    }

    public GameObject GetOrb() {
        for (int i = 0; i < OrbPool.Count; i++) {
            if (!OrbPool[i].activeInHierarchy) {
                return OrbPool[i];
            }
        }
        GameObject Orb = GameObject.Instantiate(OrbPrefab, transform.position, Quaternion.identity, transform);
        Orb.SetActive(false);
        OrbPool.Add(Orb);
        return Orb;
    }

    public void ReturnOrb(GameObject Orb) {
        Orb.SetActive(false);
    }

    public void ExplodeOrbs(Transform tile, List<Transform> neighbours, Color color) {
        GameObject[] Orbs = new GameObject[neighbours.Count];
        for (int i = 0; i < neighbours.Count; i++) {
            Orbs[i] = GetOrb();
            Orbs[i].GetComponent<SpriteRenderer>().color = color;
            Orbs[i].transform.position = tile.position;
            StartCoroutine(DisplayOrb(Orbs[i]));
            Orbs[i].transform.DOMove(neighbours[i].position, 0.25f);
        }
    }

    private IEnumerator DisplayOrb(GameObject orb) {
        orb.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        orb.SetActive(false);
    }


}
