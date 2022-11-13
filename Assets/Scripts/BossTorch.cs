using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTorch : MonoBehaviour
{
    GameManager gameManager;
    AudioPlayer audioPlayer;

    [SerializeField] GameObject fireExplosion;
    bool explosion;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    private void Start()
    {
        if (gameManager.bringerDefeated) { gameObject.SetActive(false); return; }
    }

    private void Update()
    {
        CheckForBossDeath();
    }

    void CheckForBossDeath()
    {
        if (gameManager.bringerDefeated)
        {
            if (explosion) { return; }
            explosion = true;
            GameObject fireExplosionEffect = Instantiate(fireExplosion,transform.position + new Vector3(0.55f,-0.09f,0), Quaternion.identity);           
            audioPlayer.PlayFireExplosionClip();
            Destroy(gameObject,0.2f);
        }
    }
}
