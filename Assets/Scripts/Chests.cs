using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chests : MonoBehaviour
{
    //Variables
    Vector3 chestPosition;

    //Components and Objects
    Boss boss;
    PlayerMovement player;
    SpriteRenderer spriteRenderer;
    GameManager gameManager;
    ItemsAquiredText itemsAquiredText;
    AudioPlayer audioPlayer;
    SaveSystem saveSystem;
    //Bools
    bool isCollected;
    [HideInInspector] public bool wasCollectedByPlayerInScene;

    [Header("Chest sprites")]
    [SerializeField] Sprite emptySprite;
    [SerializeField] Sprite fullChest;


    [Header("Addition chest functionality")]
    [SerializeField] Vector3 addToDistanceOfBossSpawn;


    [Header("Chest information and content:")]
    [SerializeField] bool isIntroChest;
    [SerializeField] int chestID;
    [SerializeField] bool isBadChest;
    [SerializeField] bool hasSmallKey;
    [SerializeField] bool hasLargeKey;
    [SerializeField] bool hasWornDownKey;
    [SerializeField] bool hasDungeonKey;
    [SerializeField] bool hasRuneKey;
    [SerializeField] int money;
    [SerializeField] int magicGems;
    [SerializeField] int sharpGems;
    [SerializeField] int specialGems;
    [SerializeField] int thunderCrystals;
    

    




    public int GetChestID()
    {
        return chestID;
    }
    public void SetWasCollectedByPlayerInScene()
    {
        wasCollectedByPlayerInScene = true;
    }
    public void SetChestSprite()
    {
        spriteRenderer.sprite = emptySprite;
    }
    public int GetGold()
    {
        return money;
    }
    public int GetSpecialGems()
    {
        return specialGems;
    }
    public int GetSharpGems()
    {
        return sharpGems;
    }
    public int GetMagicGems()
    {
        return magicGems;
    }
    public int GetThunderCrystals()
    {
        return thunderCrystals;
    }


    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        boss = FindObjectOfType<Boss>();
        player = FindObjectOfType<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        itemsAquiredText = GetComponent<ItemsAquiredText>();
        saveSystem = FindObjectOfType<SaveSystem>();
    }
    private void Start()
    {
        chestPosition = transform.position;
        chestPosition += addToDistanceOfBossSpawn;
        if (isIntroChest) { gameObject.SetActive(false); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindAudioPlayer();
        if (collision.tag == "Player" && isBadChest)
        {
            BadChest();
        }
        else if(collision.tag == "Player" && isBadChest == false && isCollected == false)
        {
            GoodChest();
        }     
    }

    void BadChest()
    {
        if (wasCollectedByPlayerInScene)
        {
            return;
        }
        audioPlayer.PlayScreamSound();
        StartCoroutine(WaitAndSetChestWasOpened(2));
        boss.BossAppear(chestPosition);
        boss.SetBossSpawned();
        spriteRenderer.sprite = emptySprite;
        StartCoroutine(WaitTillBossAppears());
    }
    void GoodChest()
    {
        if (wasCollectedByPlayerInScene)
        {
            return;
        }
        if (isIntroChest) { saveSystem.introChestCollected = true;}
        audioPlayer.PlayChestClip();
        CheckForKeysAndItems();
        StartCoroutine(WaitAndSetChestWasOpened(2));
        spriteRenderer.sprite = fullChest;
        isCollected = true;      
        StartCoroutine(WaitAndEmptyChest());
        gameManager.numberOfChestsCollected++;
    }
    public void MakeChestGood()
    {
        isBadChest = false;
    }
    public bool HasLargeKey()
    {
        return hasLargeKey;
    }
    public bool HasSmallKey()
    {
        return hasSmallKey;
    }
    void CheckForKeysAndItems()
    {
        if (hasLargeKey)
        {
            ItemFoundText("Large key ");
            gameManager.playerHasLargeKey = true;
        }
        if (hasSmallKey)
        {
            ItemFoundText("Small key ");
            gameManager.playerHasSmallKey = true;
        }
        if (hasWornDownKey)
        {
            ItemFoundText("Worn Down Key ");
            gameManager.playerHasWornDownKey = true;
        }
        if (hasDungeonKey)
        {
            ItemFoundText("Dungeon Key ");
            gameManager.playerHasDungeonKey = true;
        }
        if (hasRuneKey)
        {
            ItemFoundText("Rune Key ");
            gameManager.playerHasRuneKey = true;
        }
        if(money > 0)
        {
            MoneyFoundText(money);
            gameManager.FoundMoney(money);
        }
        if(magicGems > 0)
        {
            if(magicGems > 1)
            {
                ItemFoundText("Magic gems " + magicGems + "x ");
            }
            else
            {
                ItemFoundText("Magic gem ");
            }            
            gameManager.FoundMagicGem(magicGems);
        }
        if (sharpGems > 0)
        {
            if (sharpGems > 1)
            {
                ItemFoundText("Sharp gems " + sharpGems + "x ");
            }
            else
            {
                ItemFoundText("Sharp gem ");
            }
            gameManager.FoundSharpGem(sharpGems);
        }
        if (specialGems > 0)
        {
            if (specialGems > 1)
            {
                ItemFoundText("Special gems " + specialGems + "x ");
            }
            else
            {
                ItemFoundText("Special gem ");
            }
            gameManager.FoundSpecialGem(specialGems);
        }
        if(thunderCrystals > 0)
        {
            ItemFoundText("Thunder crystals found: " + thunderCrystals);
            gameManager.FoundThunderCrystal(thunderCrystals);
        }


    }

    void ItemFoundText(string itemName)
    {
        
        itemsAquiredText.SetItemText(itemName + "found!");
        itemsAquiredText.transform.position = transform.position;
        itemsAquiredText.TurnItemObjectOn(true);
    }
    void MoneyFoundText(int money)
    {
        itemsAquiredText.SetItemText(money + " gold found!");
        itemsAquiredText.transform.position = transform.position;
        itemsAquiredText.TurnItemObjectOn(true);
    }
    void FindAudioPlayer()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    

    IEnumerator WaitAndEmptyChest()
    {
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.sprite = emptySprite;
    }
    IEnumerator WaitTillBossAppears()
    {
        yield return new WaitForSeconds(0.8f);
        player.PlayerDeath();
        boss.BossMagic("mega");
    }
    IEnumerator WaitAndSetChestWasOpened(float delay)
    {
        yield return new WaitForSeconds(delay);
        wasCollectedByPlayerInScene = true;
    }
}

