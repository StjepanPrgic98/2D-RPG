using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ShopInformation : MonoBehaviour
{

    Shop shop;
    GameManager gameManager;
    TextMeshProUGUI[] textObject;
    TextMeshProUGUI ShopResetTimer;
    GameObject[] objects;
    GameObject ShopResetTimerObject;

    
    int currentTime = 500;
    bool reduceTimer = true;
    bool found;

    [HideInInspector] public bool startTimer;

    [HideInInspector] public bool holyBarrierBought;
    [HideInInspector] public bool protectBought;
    [HideInInspector] public bool oneHitShieldBought;
    [HideInInspector] public bool doubleHpBought;
    [HideInInspector] public bool healBought;
    [HideInInspector] public bool largeHealBought;
    [HideInInspector] public bool regenBought;
    [HideInInspector] public bool damageBreakBought;
    [HideInInspector] public bool defenceBreakBought;
    [HideInInspector] public bool fireExplosionBought;
    [HideInInspector] public bool nukeExplosionBought;
    [HideInInspector] public bool waterProjectileBought;
    [HideInInspector] public bool waterTornadoBought;
    [HideInInspector] public bool thunderProjectileBought;
    [HideInInspector] public bool thunderSplashBought;
    [HideInInspector] public bool thunderHawkBought;
    [HideInInspector] public bool thunderStrikeBought;
    [HideInInspector] public bool iceProjectileBought;
    [HideInInspector] public bool iceSplashBought;
    [HideInInspector] public bool iceGroundBought;
    [HideInInspector] public bool holyProjectileBought;
    [HideInInspector] public bool holyGroundBought;
    [HideInInspector] public bool poisonBought;
    [HideInInspector] public bool hasteBought;
    [HideInInspector] public bool slowBought;
    [HideInInspector] public bool dispelBought;
    [HideInInspector] public bool windProjectileBought;
    [HideInInspector] public bool windBreathBought;

    private void Awake()
    {
        ManageSingleton();
        shop = FindObjectOfType<Shop>();
        gameManager = FindObjectOfType<GameManager>();
        textObject = FindObjectsOfType<TextMeshProUGUI>();
        objects = FindObjectsOfType<GameObject>();
    }
    private void Start()
    {
        for (int i = 0; i < textObject.Length; i++)
        {
            if (textObject[i].tag == "ShopTimer")
            {
                ShopResetTimer = textObject[i];
            }
        }
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].tag == "ShopTimer")
            {
                ShopResetTimerObject = objects[i];
            }
        }
        ShopResetTimerObject.SetActive(false);
    }
    private void Update()
    {
        Timer();
    }


    void Timer()
    {
        if (startTimer == false) { return; }
        if (currentTime <= 0) { ResetSpells(); ShopResetTimer.text = ""; return; }
        if (reduceTimer == false) { return; }
        StartCoroutine(WaitAndReduceTimer(1));
    }
    IEnumerator WaitAndReduceTimer(float delay)
    {
        reduceTimer = false;
        yield return new WaitForSeconds(delay);
        currentTime--;
        ShopResetTimer.text = "Time till shop reset: " + "\n" + currentTime + " seconds!";
        reduceTimer = true;
    }


    public void ResetSpells()
    {
        ShopResetTimer.text = "";
        currentTime = 500;
        startTimer = false;
        holyBarrierBought = false;
        protectBought = false;
        oneHitShieldBought = false;
        doubleHpBought = false;
        healBought = false;
        largeHealBought = false;
        regenBought = false;
        damageBreakBought = false;
        fireExplosionBought = false;
        nukeExplosionBought = false;
        waterProjectileBought = false;
        waterTornadoBought = false;
        thunderProjectileBought = false;
        thunderSplashBought = false;
        thunderHawkBought = false;
        thunderStrikeBought = false;
        iceProjectileBought = false;
        iceSplashBought = false;
        iceGroundBought = false;
        holyProjectileBought = false;
        holyGroundBought = false;
        poisonBought = false;
        defenceBreakBought = false;
        windBreathBought = false;
        windProjectileBought = false;
        dispelBought = false;
        hasteBought = false;
        slowBought = false;
    }
    public void TurnTimerOnorOff(bool argument)
    {
        if (argument)
        {
            ShopResetTimerObject.SetActive(true);
        }
        else
        {
            ShopResetTimerObject.SetActive(false);
        }
    }
    public void FindTimerText()
    {
        textObject = FindObjectsOfType<TextMeshProUGUI>();
        objects = FindObjectsOfType<GameObject>();
        for (int i = 0; i < textObject.Length; i++)
        {
            if (textObject[i].tag == "ShopTimer")
            {
                ShopResetTimer = textObject[i];
            }
        }
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].tag == "ShopTimer")
            {
                ShopResetTimerObject = objects[i];
            }
        }
        ShopResetTimerObject.SetActive(false);
    }













    void ManageSingleton()
    {
        int instanceCount = FindObjectsOfType(GetType()).Length;
        if (instanceCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
