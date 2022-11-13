using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTravel : MonoBehaviour
{
    GameManager gameManager;
    SaveSystem saveSystem;
    PlayerMovement player;

    [SerializeField] GameObject listOfWarpLocations;

    Vector3 warpHolyCitadel = new Vector3(10.91f, -13.66f, 0);
    Vector3 warpIslandOfCompletion = new Vector3(10.3f, -8.3f, 0);
    Vector3 warpSouthShore = new Vector3(9.61f, -19f, 0);
    Vector3 warpCanyon = new Vector3(-5.48f, -12.45f, 0);
    Vector3 warpIslandsOfHope = new Vector3(-7.68f, -2.94f, 0);
    Vector3 warpCastle = new Vector3(-0.159f, 9.887f, 0);
    Vector3 warpNorthShore = new Vector3(-0.75f, 16.32f, 0);

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        saveSystem = FindObjectOfType<SaveSystem>();
        player = FindObjectOfType<PlayerMovement>();
    }

    public void TurnOnListOfWarpLocations()
    {
        listOfWarpLocations.SetActive(true);
    }
    public void TurnOfListOfWarpLocations()
    {
        listOfWarpLocations.SetActive(false);
    }

    public void WarpHolyCitadel()
    {
        player.transform.position = warpHolyCitadel;
    }
    public void WarpIslandOfCompletion()
    {
        player.transform.position = warpIslandOfCompletion;
    }
    public void WarpSouthShore()
    {
        player.transform.position = warpSouthShore;
    }
    public void WarpCanyon()
    {
        player.transform.position = warpCanyon;
    }
    public void WarpIslandsOfHope()
    {
        player.transform.position = warpIslandsOfHope;
    }
    public void WarpCastle()
    {
        player.transform.position = warpCastle;
    }
    public void WarpNorthShore()
    {
        player.transform.position = warpNorthShore;
    }
}
