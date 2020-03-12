using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public bool randomBombs = false;
    public bool patternBombs = false;
    public GameObject bombPrefab;

    [System.Serializable]
    public class RandomBombs
    {
        public int maxBombs = 5;
        public float xMaxRange = 15;
        public float yMaxRange = 15;
        public float bombDelay = 0.5f;
        [HideInInspector]
        public bool spawnBomb = true;
        
    }

    [System.Serializable]
    public class PatternBombs
    {
        public List<GameObject> patterns;
        [HideInInspector]
        public List<GameObject> patternNodes;
        public float bombDelay = 0.5f;
        [HideInInspector]
        public bool spawnBomb = true;
        [HideInInspector]
        public int currNode;

    }

    public PatternBombs pBomb;
    public RandomBombs rBomb;
    private float timer = 0;
    [HideInInspector]
    public int currBombCount = 0;
    // Start is called before the first frame update
    private int patternNum = 0;

    void Start()
    {    
        if (patternBombs)
        {
            foreach (Transform child in pBomb.patterns[0].transform)
            {
                pBomb.patternNodes.Add(child.gameObject);
            }
        }
    }

    // Update is called once per frame
    void  Update()
    {

        /////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (patternNum >= pBomb.patterns.Count - 1)
            {
                patternNum = 0;
                switchPatterns(patternNum);
            }
            else
            {
                patternNum++;
                switchPatterns(patternNum);
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (randomBombs)
            {
                randomBombs = false;
            }
            else
            {
                randomBombs = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (patternBombs)
            {
                patternBombs = false;
            }
            else
            {
                patternBombs = true;
            }
        }

        /////////////////////////////////////

        if (randomBombs)
        {
            randomBombing();
        }
        if (patternBombs)
        {
            patternBombing();
        }
    }

    private void randomBombing()
    {   
        if (rBomb.spawnBomb && currBombCount < rBomb.maxBombs)
        {
            timer = Time.time;
            Vector3 area = new Vector3(transform.position.x + Random.Range(-rBomb.xMaxRange, rBomb.xMaxRange), transform.position.y + Random.Range(-rBomb.yMaxRange, rBomb.yMaxRange));
            Instantiate(bombPrefab, area, Quaternion.identity);
            rBomb.spawnBomb = false;
        }
        if (Time.time - timer >= rBomb.bombDelay)
        {
            rBomb.spawnBomb = true;
        }
    }

    private void patternBombing()
    {
        if (pBomb.spawnBomb && pBomb.currNode < pBomb.patternNodes.Count)
        {
            timer = Time.time;
            Vector3 node = new Vector3(pBomb.patternNodes[pBomb.currNode].transform.position.x, pBomb.patternNodes[pBomb.currNode].transform.position.y);
            Instantiate(bombPrefab, node, Quaternion.identity);
            pBomb.currNode++;
            pBomb.spawnBomb = false;
        }
        if (Time.time - timer >= pBomb.bombDelay)
        {
            pBomb.spawnBomb = true;
        }
    }

    public void switchPatterns(int index)
    {
        pBomb.patternNodes.Clear();
        pBomb.currNode = 0;
        foreach (Transform child in pBomb.patterns[index].transform)
        {
            pBomb.patternNodes.Add(child.gameObject);
        }
    }

}
