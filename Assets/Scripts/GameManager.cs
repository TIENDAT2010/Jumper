using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player playerPrefab;
    public Platform platformPrefab;
    public float minSpawnX;
    public float maxSpawnX;
    public float minSpawnY;
    public float maxSpawnY;
    public float powerBarUp;
    private Player m_player;
    private int m_Score;

    public CamController mainCam;

    private bool m_isGameStarted;

    public bool IsGameStarted { get => m_isGameStarted; }

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        base.Start();

        GameGUIManager.Ins.UpdateScoreCounting(m_Score);

        GameGUIManager.Ins.UpdatePowerBar(0, 1);
    }

    public void PlayGame()
    {
        StartCoroutine(PlatformInt());

        GameGUIManager.Ins.ShowGameGUI(true);
    }

    IEnumerator PlatformInt()
    {
        Platform platformClone = null;
        if(platformPrefab != null)
        {
            platformClone = Instantiate(platformPrefab,new Vector2(0, Random.Range(minSpawnY,maxSpawnY)),Quaternion.identity);
            platformClone.id = platformClone.gameObject.GetInstanceID();
        }

        yield return new WaitForSeconds(0.5f);

        if(platformPrefab != null )
        {
            m_player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            m_player.lastPlatformId = platformClone.id;
        }

        if(platformPrefab != null )
        {
            float spawnX = m_player.gameObject.transform.position.x + minSpawnX;

            float spawnY = Random.Range(minSpawnY,maxSpawnY);

            Platform platformClone02 = Instantiate(platformPrefab, new Vector2(spawnX,spawnY),Quaternion.identity);
            platformClone02.id = platformClone02.gameObject.GetInstanceID();
        }

        yield return new WaitForSeconds(0.5f);

        m_isGameStarted = true;
    }    

    public void CreatePlatform()
    {
        if (!platformPrefab || !m_player) return;

        float spawnX = Random.Range(m_player.gameObject.transform.position.x + minSpawnX, m_player.gameObject.transform.position.x + maxSpawnX);

        float spawnY = Random.Range(minSpawnY,  maxSpawnY);

        Platform platformClone = Instantiate(platformPrefab, new Vector2( spawnX,spawnY), Quaternion.identity);
        platformClone.id = platformClone.gameObject.GetInstanceID();
    }    

    public void CreatePlatformandLerp(float playerXPos)
    {
        if(mainCam != null)
        {
            mainCam.LerpTrigger(playerXPos + minSpawnX);
        }
        CreatePlatform() ;
    }

    public void AddScore()
    {
        m_Score++;
        Prefs.bestScore = m_Score;
        GameGUIManager.Ins.UpdateScoreCounting(m_Score);
    }
}
