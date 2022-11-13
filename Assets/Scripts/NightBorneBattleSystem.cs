using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NightBorneBattleSystem : MonoBehaviour
{
    NightBorne nightBorne;
    PlayerMovement player;
    AudioPlayer audioPlayer;
    GameManager gameManager;
    [SerializeField] PathFinder pathFinder;
    [HideInInspector] public Vector3 moveAwayFromPlayer;
    [SerializeField] GameObject bossWarpStart;
    [SerializeField] GameObject bossWarpEnd;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] TextMeshProUGUI playerHpText;
    [SerializeField] TextMeshProUGUI bossHpText;
    [SerializeField] TextMeshProUGUI playerHpDamage;
    [SerializeField] TextMeshProUGUI bossHpDamage;
    [SerializeField] Spikes spikes;
    [HideInInspector] public bool seenByNightBorne;
    [HideInInspector] public bool isAlive = true;
    [SerializeField] int playerHp;
    [SerializeField] int bossHp;
    [SerializeField] GameObject backgroundMusic;
    [SerializeField] GameObject nightborneBattleMusic;
    int maxPlayerHp;
    int maxBossHp;
    int damageAmmout = 99999;
    bool isHit;
    

    private void Awake()
    {
        nightBorne = FindObjectOfType<NightBorne>();
        player = FindObjectOfType<PlayerMovement>();
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        
    }

    private void Start()
    {
        maxBossHp = bossHp;
        maxPlayerHp = gameManager.GetPLayerHp();
        playerHp = maxPlayerHp;
        playerHpText.text = Mathf.Clamp(playerHp,0,maxPlayerHp) + "/" + maxPlayerHp;
        bossHpText.text = bossHp + "/" + maxBossHp;
    }







    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            pathFinder.enabled = false;
            seenByNightBorne = true;
            BossWarpStart();          
            nightBorne.BossIdle("isRunning");          
            nightBorne.transform.position = player.transform.position + moveAwayFromPlayer;
            BossWarpEnd();
            nightBorne.BossAttack();
            playerHpDamage.text = "-" + damageAmmout.ToString();
            playerHp -= damageAmmout;
            UpdateHp();
            StartCoroutine(WaitAndTurnOffDamageText(1));         
            player.PlayerDeath();
        }
    }

    public void DamageBoss(int damage)
    {
        if (isHit) { return; }
        if(spikes.ableToHurtBoss == false) { return; }
        isHit = true;
        bossHpDamage.text = "-" + damage.ToString();
        bossHp -= Mathf.Clamp(damageAmmout, 5000, 5000);
        UpdateHp();
        StartCoroutine(WaitAndTurnOffDamageText(1));
        StartCoroutine(WaitAndUnHitBoss(5));
        if(bossHp <= 0)
        {
            pathFinder.enabled = false;
            nightBorne.BossIdle("isRunning");
            nightBorne.BossDeath();
            isAlive = false;
            playerHpText.text = "";
            bossHpText.text = "";
            backgroundMusic.SetActive(true);
            nightborneBattleMusic.SetActive(false);
        }
    }

    void UpdateHp()
    {
        playerHpText.text = Mathf.Clamp(playerHp, 0, maxPlayerHp) + "/" + maxPlayerHp;
        bossHpText.text = Mathf.Clamp(bossHp,0,maxBossHp) + "/" + maxBossHp;
    }

    void BossWarpStart()
    {
        spriteRenderer.enabled = false;
        GameObject bossWarpStartEffect = Instantiate(bossWarpStart, transform.position, Quaternion.identity);
        Destroy(bossWarpStartEffect, 0.5f);
    }
    void BossWarpEnd()
    {
        StartCoroutine(WaitAndTurnBossRenderOn(0.1f));
        GameObject bossWarpEndEffect = Instantiate(bossWarpEnd, transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity);
        audioPlayer.PlayThunderStrikeClip();
        Destroy(bossWarpEndEffect, 0.5f);
    }

    IEnumerator WaitAndTurnBossRenderOn(float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRenderer.enabled = true;
    }
    IEnumerator WaitAndTurnOffDamageText(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerHpDamage.text = "";
        bossHpDamage.text = "";
    }

    IEnumerator WaitAndUnHitBoss(float delay)
    {
        yield return new WaitForSeconds(delay);
        isHit = false;
    }



}
