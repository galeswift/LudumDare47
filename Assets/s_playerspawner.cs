using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_playerspawner : MonoBehaviour
{
    public GameObject PlayerObject;
    public List<GameObject> PlayerList = new List<GameObject>();
    private int m_currentPlayerIdx = 0;
    private float m_lastSpawnTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    void SpawnPlayer(int index)
    {        
        GameObject player = GameObject.Instantiate(PlayerObject);
        player.GetComponent<s_player>().SetPlayerIndex(index);
        player.transform.position= transform.position;
        player.transform.rotation = transform.rotation;
        PlayerList.Add(player);
    }

    public void SpawnAllPlayers()
    {
        for (int i=0; i<=m_currentPlayerIdx; i++)
        {
            // last player spawned is the real one
            SpawnPlayer(i);
        }
        
        m_currentPlayerIdx++;
        m_lastSpawnTime = Time.time;
    }
    
    public void DestroyAllPlayers()
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
            GameObject.Destroy(PlayerList[i]);
        }

        PlayerList.Clear();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
