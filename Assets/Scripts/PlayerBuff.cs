using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuff : MonoBehaviour
{
    PlayerMovement player;
    Battle battle;
    DefenderBattleSystem defenderBattleSystem;
    ReaperBattleSystem reaperBattleSystem;
    AudioSource[] audioSources;
    Failsafe failsafe;

    AudioSource holyBarrierAudio;
    [SerializeField] Vector3 correctPosition;
    [SerializeField] bool holyBarrier;
    [SerializeField] bool oneHitShield;
    [SerializeField] bool protect;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        battle = FindObjectOfType<Battle>();
        audioSources = FindObjectsOfType<AudioSource>();
        defenderBattleSystem = FindObjectOfType<DefenderBattleSystem>();
        reaperBattleSystem = FindObjectOfType<ReaperBattleSystem>();
        failsafe = FindObjectOfType<Failsafe>();
    }
    private void Start()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if(holyBarrier && audioSources[i].tag == "HolyShield")
            {
                holyBarrierAudio = audioSources[i];
                holyBarrierAudio.Play();
            }
        }
    }

    private void Update()
    {
        if(failsafe.bringerDestroyed == false)
        {
            if(holyBarrier && battle.holyBarrierTurns == 0)
            {
                Destroy(gameObject);
                holyBarrierAudio.Stop();
                player.RemoveHolyBarrier();
                player.RemovePositiveStatus("- Holy Barrier");
            }
        }
        if(failsafe.reaperDestroyed == false)
        {
            if(holyBarrier && reaperBattleSystem.holyBarrierTurns == 0)
            {
                Destroy(gameObject);
                holyBarrierAudio.Stop();
                player.RemoveHolyBarrier();
                player.RemovePositiveStatus("- Holy Barrier");
            }
        }
        if(failsafe.defenderDestroyed == false)
        {
            if(holyBarrier && defenderBattleSystem.holyBarrierTurns == 0)
            {
                Destroy(gameObject);
                holyBarrierAudio.Stop();
                player.RemoveHolyBarrier();
                player.RemovePositiveStatus("- Holy Barrier");
            }
        }
        if(holyBarrier && player.HasHolyBarrier() == false)
        {
            Destroy(gameObject);
            holyBarrierAudio.Stop();
        }
        if(oneHitShield && battle.playerIsHit)
        {
            battle.playerIsHit = false;
            Destroy(gameObject);
        }
        else if(oneHitShield && player.HasOneHitShield() == false)
        {
            Destroy(gameObject);
        }
        if (protect && player.IsAlive() == false) 
        {
            Destroy(gameObject);
            return; 
        }
        else if(protect && player.HasProtect() == false)
        {
            Destroy(gameObject);
        }
        if(player.IsAlive() == false) { return; }
        transform.position = player.transform.position + correctPosition;
    }

}
