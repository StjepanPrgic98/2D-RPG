using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip thunderStrike;
    [SerializeField] AudioClip thunderSplash;
    [SerializeField] AudioClip thunderHawkEagle;
    [SerializeField] AudioClip thunderHawkImpact;
    [SerializeField] AudioClip thunderProjectileStart;
    [SerializeField] AudioClip waterTornado;
    [SerializeField] AudioClip waterProjectileImpact;
    [SerializeField] AudioClip waterProjectile;
    [SerializeField] AudioClip fireExplosion;
    [SerializeField] AudioClip nukeExplosion;
    [SerializeField] AudioClip nukeShine;
    [SerializeField] AudioClip slow;
    [SerializeField] AudioClip haste;
    [SerializeField] AudioClip drawSword;
    [SerializeField] AudioClip sheatSword;
    [SerializeField] AudioClip iceSplash;
    [SerializeField] AudioClip iceGround;
    [SerializeField] AudioClip iceProjectileStart;
    [SerializeField] AudioClip iceProjectile;
    [SerializeField] AudioClip holyGround;
    [SerializeField] AudioClip holyProjectileImpact;
    [SerializeField] AudioClip holyProjectile;
    [SerializeField] AudioClip dispel;
    [SerializeField] AudioClip slash;
    [SerializeField] AudioClip poison;
    [SerializeField] AudioClip heal;
    [SerializeField] AudioClip protect;
    [SerializeField] AudioClip oneHitShield;
    [SerializeField] AudioClip doubleHp;
    [SerializeField] AudioClip windBreath;
    [SerializeField] AudioClip windProjectile;
    [SerializeField] AudioClip windProjectileImpact;
    [SerializeField] AudioClip inventory;
    [SerializeField] AudioClip upgradeClip;
    [SerializeField] AudioClip shopBuyClip;
    [SerializeField] AudioClip cantBuyClip;
    [SerializeField] AudioClip chestClip;
    [SerializeField] AudioClip slap;
    [SerializeField] AudioClip swing1;
    [SerializeField] AudioClip swing2;
    [SerializeField] AudioClip swing3;
    [SerializeField] AudioClip spikes;
    [SerializeField] AudioClip summon;
    [SerializeField] AudioClip scream;
    [SerializeField] AudioClip darkSpell;
    [SerializeField] AudioClip darkBoulder;
    [SerializeField] AudioClip darkCast;
    [SerializeField] AudioClip curse;
    [SerializeField] AudioClip oblivion;
    [SerializeField] AudioClip nightborneDeath;
    [SerializeField] AudioClip pressurePlate;
    [SerializeField] AudioClip lockedDoor;
    [SerializeField] AudioClip unlockDoor;
    [SerializeField] AudioClip button;
    [SerializeField] AudioClip sidequestDone;
    [SerializeField] AudioClip lever;
    [SerializeField] AudioClip holyWaterBarrel;
    [SerializeField] AudioClip totemBeep;
    [SerializeField] AudioClip totemEnd;
    [SerializeField] AudioClip energyBallLoop;
    [SerializeField] AudioClip energyBallImpact;
    [SerializeField] [Range(0,1f)] float volume;
    [SerializeField] [Range(0,1f)] float lowVolume;
    [SerializeField] [Range(0,1f)] float highVolume;


    PlayerMovement player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();

    }
    private void Start()
    {
        FindPlayer();
    }
    private void Update()
    {
        FindPlayer();   
    }



    public void PlayThunderStrikeClip()
    {
        AudioSource.PlayClipAtPoint(thunderStrike, player.transform.position, volume);
    }
    public void PlayThunderSplashClip()
    {
        AudioSource.PlayClipAtPoint(thunderSplash, player.transform.position, volume);
    }
    public void PlayWaterTornadoClip()
    {
        AudioSource.PlayClipAtPoint(waterTornado, player.transform.position, volume);
    }
    public void PlaySlowClip()
    {
        AudioSource.PlayClipAtPoint(slow, player.transform.position, lowVolume);
    }
    public void PlayHasteClip()
    {
        AudioSource.PlayClipAtPoint(haste, player.transform.position, lowVolume);
    }
    public void PlayDrawSwordClip()
    {
        AudioSource.PlayClipAtPoint(drawSword, player.transform.position, lowVolume);
    }
    public void PlaySheatSwordClip()
    {
        AudioSource.PlayClipAtPoint(sheatSword, player.transform.position, lowVolume);
    }
    public void PlayIceGroundClip()
    {
        AudioSource.PlayClipAtPoint(iceGround, player.transform.position, volume);
    }
    public void PlayIceSplashClip()
    {
        AudioSource.PlayClipAtPoint(iceSplash, player.transform.position, volume);
    }
    public void PlayerInventoryClip()
    {
        AudioSource.PlayClipAtPoint(inventory, player.transform.position, volume);
    }
    public void PlayUpgradeClip()
    {
        AudioSource.PlayClipAtPoint(upgradeClip, player.transform.position, volume);
    }
    public void PlayShopBuyClip()
    {
        AudioSource.PlayClipAtPoint(shopBuyClip, player.transform.position, volume);
    }
    public void PlayCantBuyClip()
    {
        AudioSource.PlayClipAtPoint(cantBuyClip, player.transform.position, volume);
    }
    public void PlayChestClip()
    {
        AudioSource.PlayClipAtPoint(chestClip, player.transform.position, volume);
    }
    public void PlayThunderHawkEagle()
    {
        AudioSource.PlayClipAtPoint(thunderHawkEagle, player.transform.position, volume);
    }
    public void PlayThunderHawkImpact()
    {
        AudioSource.PlayClipAtPoint(thunderHawkImpact, player.transform.position, volume);
    }
    public void PlayFireExplosionClip()
    {
        AudioSource.PlayClipAtPoint(fireExplosion, player.transform.position, volume);
    }
    public void PlayNukeExplosionClip()
    {
        AudioSource.PlayClipAtPoint(nukeExplosion, player.transform.position, volume);
    }
    public void PlayNukeShineClip()
    {
        AudioSource.PlayClipAtPoint(nukeShine, player.transform.position, volume);
    }
    public void PlayWaterProjectileImpactClip()
    {
        AudioSource.PlayClipAtPoint(waterProjectileImpact, player.transform.position, volume);
    }
    public void PlayWaterProjectileClip()
    {
        AudioSource.PlayClipAtPoint(waterProjectile, player.transform.position, volume);
    }
    public void PlayHealClip()
    {
        AudioSource.PlayClipAtPoint(heal, player.transform.position, lowVolume);
    }
    public void PlayDispelClip()
    {
        AudioSource.PlayClipAtPoint(dispel, player.transform.position, volume);
    }
    public void PlaySlashClip()
    {
        AudioSource.PlayClipAtPoint(slash, player.transform.position, volume);
    }
    public void PlayWindBreathClip()
    {
        AudioSource.PlayClipAtPoint(windBreath, player.transform.position, volume);
    }
    public void PlayWindProjectileClip()
    {
        AudioSource.PlayClipAtPoint(windProjectile, player.transform.position, volume);
    }
    public void PlayWindProjectileImpactClip()
    {
        AudioSource.PlayClipAtPoint(windProjectileImpact, player.transform.position, volume);
    }
    public void PlayHolyGroundClip()
    {
        AudioSource.PlayClipAtPoint(holyGround, player.transform.position, volume);
    }
    public void PlayHolyProjectileImpact()
    {
        AudioSource.PlayClipAtPoint(holyProjectileImpact, player.transform.position, volume);
    }
    public void PlayHolyProjectile()
    {
        AudioSource.PlayClipAtPoint(holyProjectile, player.transform.position, volume);
    }
    public void PlayIceProjectileStartClip()
    {
        AudioSource.PlayClipAtPoint(iceProjectileStart, player.transform.position, volume);
    }
    public void PlayIceProjectieClip()
    {
        AudioSource.PlayClipAtPoint(iceProjectile, player.transform.position, volume);
    }
    public void PlayThunderProjectileStartClip()
    {
        AudioSource.PlayClipAtPoint(thunderProjectileStart, player.transform.position, volume);
    }
    public void PlayOneHitShieldClip()
    {
        AudioSource.PlayClipAtPoint(oneHitShield, player.transform.position, volume);
    }
    public void PlayProtectClip()
    {
        AudioSource.PlayClipAtPoint(protect, player.transform.position, volume);
    }
    public void PlayDoubleHpClip()
    {
        AudioSource.PlayClipAtPoint(doubleHp, player.transform.position, volume);
    }
    public void PlayPoisonClip()
    {
        AudioSource.PlayClipAtPoint(poison, player.transform.position, volume);
    }
    public void PlaySlapClip()
    {
        AudioSource.PlayClipAtPoint(slap, player.transform.position, volume);
    }
    public void PlaySwing1Clip()
    {
        AudioSource.PlayClipAtPoint(swing1, player.transform.position, volume);
    }
    public void PlaySwing2Clip()
    {
        AudioSource.PlayClipAtPoint(swing2, player.transform.position, volume);
    }
    public void PlaySwing3Clip()
    {
        AudioSource.PlayClipAtPoint(swing3, player.transform.position, volume);
    }
    public void PlaySpikesClip()
    {
        AudioSource.PlayClipAtPoint(spikes, player.transform.position, volume);
    }
    public void PlaySummonClip()
    {
        AudioSource.PlayClipAtPoint(summon, player.transform.position, volume);
    }
    public void PlayScreamSound()
    {
        AudioSource.PlayClipAtPoint(scream, player.transform.position, volume);
    }
    public void PlayDarkSpellClip()
    {
        AudioSource.PlayClipAtPoint(darkSpell, player.transform.position, volume);
    }
    public void PlayDarkBoulderClip()
    {
        AudioSource.PlayClipAtPoint(darkBoulder, player.transform.position, volume);
    }
    public void PlayCurseClip()
    {
        AudioSource.PlayClipAtPoint(curse, player.transform.position, volume);
    }
    public void PlayOblivionClip()
    {
        AudioSource.PlayClipAtPoint(oblivion, player.transform.position, volume);
    }
    public void PlayDarkCastClip()
    {
        AudioSource.PlayClipAtPoint(darkCast, player.transform.position, volume);
    }
    public void PlayNightBorneDeathClip()
    {
        AudioSource.PlayClipAtPoint(nightborneDeath, player.transform.position, volume);
    }
    public void PlayPressurePlateClip()
    {
        AudioSource.PlayClipAtPoint(pressurePlate, player.transform.position, volume);
    }
    public void PlayLockedDoorClip()
    {
        AudioSource.PlayClipAtPoint(lockedDoor, player.transform.position, volume);
    }
    public void PlayUnlockedDoorClip()
    {
        FindPlayer();
        AudioSource.PlayClipAtPoint(unlockDoor, player.transform.position, volume);
    }
    public void PlayButtonClip()
    {
        AudioSource.PlayClipAtPoint(button, player.transform.position, volume);
    }
    public void PlaySidequestDoneClip()
    {
        AudioSource.PlayClipAtPoint(sidequestDone, player.transform.position, volume);
    }
    public void PlayLeverClip()
    {
        AudioSource.PlayClipAtPoint(lever, player.transform.position, volume);
    }
    public void PlayHolyWaterBarrerClip()
    {
        AudioSource.PlayClipAtPoint(holyWaterBarrel, player.transform.position, volume);
    }
    public void PlayTotemBeepClip()
    {
        AudioSource.PlayClipAtPoint(totemBeep, player.transform.position, lowVolume);
    }
    public void PlayTotemEndClip()
    {
        AudioSource.PlayClipAtPoint(totemEnd, player.transform.position, lowVolume);
    }
    public void PlayEnergyBallLoop()
    {
        FindPlayer();
        AudioSource.PlayClipAtPoint(energyBallLoop, player.transform.position, volume);
    }
    public void PlayEnergyBallImpact()
    {
        AudioSource.PlayClipAtPoint(energyBallImpact, player.transform.position, volume);
    }


    void FindPlayer()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
}
