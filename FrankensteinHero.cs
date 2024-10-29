using MelonLoader;
using BTD_Mod_Helper;
using FrankensteinHero;
using HarmonyLib;
using Il2CppAssets.Scripts.Simulation.SimulationBehaviors;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using BTD_Mod_Helper.Extensions;
using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using System.Collections.Generic;
using BTD_Mod_Helper.Api.Components;
using Il2CppAssets.Scripts.Simulation.Input;
using BTD_Mod_Helper.Api;
using Il2CppNinjaKiwi.Common.ResourceUtils;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Utils;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
using System;
using FrankensteinHerolevels;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Profile;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Simulation.Objects;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Models;
using static BTD_Mod_Helper.Api.ModContent;
using FrankenBloon;
using UnityEngine;

[assembly: MelonInfo(typeof(FrankensteinHero.FrankensteinHero), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace FrankensteinHero;

public class FrankensteinHero : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<FrankensteinHero>("Frankenstein Hero Mod loaded!");
    }
    public override void OnTowerSelected(Il2CppAssets.Scripts.Simulation.Towers.Tower tower)
    {
        if (tower.towerModel.name.Contains("FrankensteinHero"))
        {
            towerswitchUi.switchMenu.CreatePanel();
        }
    }
    public override void OnTowerDeselected(Il2CppAssets.Scripts.Simulation.Towers.Tower tower)
    {

        if (tower.towerModel.name.Contains("FrankensteinHero"))
        {
            if (towerswitchUi.switchMenu.instance != null)
            {
                towerswitchUi.switchMenu.instance?.Close();
            }
        }
    }
    public override void OnWeaponFire(Weapon weapon)
    {
        {
            if (weapon.attack.tower.towerModel.name.Contains("FrankensteinHero"))
            {
                if (weapon.weaponModel.projectile.maxPierce == 2137f)
                {
                    var Towers = (InGame.instance.GetAllTowerToSim("").FindAll(sim => sim.tower.towerModel.name.Contains("FrankensteinHero")));
                    foreach (var Tower in Towers)
                    {
                        var towerModel = Tower.tower.rootModel.Duplicate().Cast<TowerModel>();
                        if (Tower.cashEarned == 1)
                        {
                            towerModel.GetWeapon(0).isStunned = true;
                            towerModel.GetWeapon(1).isStunned = false;
                            towerModel.GetAttackModel(0).range = 0;
                            towerModel.GetAttackModel(1).range = 31;
                            towerModel.range = towerModel.GetAttackModel(1).range;
                            if (towerModel.appliedUpgrades.Contains("FrankensteinHero-FrankensteinHero Level 6"))
                            {
                                towerModel.GetWeapon(3).isStunned = true;
                            }
                            if (towerModel.appliedUpgrades.Contains("FrankensteinHero-FrankensteinHero Level 8"))
                            {
                                towerModel.GetAttackModel(1).range = 45;
                                towerModel.range = towerModel.GetAttackModel(1).range;
                            }
                            if (towerModel.appliedUpgrades.Contains("FrankensteinHero-FrankensteinHero Level 12"))
                            {
                                var bombaclat = towerModel.GetAttackModel("elebomb");
                                bombaclat.range = 0;
                                towerModel.GetWeapon(4).isStunned = true;
                            }
                            towerModel.range = towerModel.GetAttackModel(1).range;
                            if (towerModel.appliedUpgrades.Contains("FrankensteinHero-FrankensteinHero Level 7"))
                            {
                                towerModel.GetAbility(1).enabled = false;
                                towerModel.GetAbility(2).enabled = true;
                            }
                            Tower.tower.UpdateRootModel(towerModel);
                        }
                        else if (Tower.cashEarned == 0)
                        {
                            towerModel.GetWeapon(1).isStunned = true;
                            towerModel.GetWeapon(0).isStunned = false;
                            towerModel.GetAttackModel(1).range = 0;
                            towerModel.GetAttackModel(0).range = 65;
                            towerModel.range = towerModel.GetAttackModel(0).range;
                            if (towerModel.appliedUpgrades.Contains("FrankensteinHero-FrankensteinHero Level 6"))
                            {
                                towerModel.GetAttackModel(0).range = 70;
                                towerModel.range = towerModel.GetAttackModel(0).range;
                                towerModel.GetWeapon(3).isStunned = false;
                            }
                            if (towerModel.appliedUpgrades.Contains("FrankensteinHero-FrankensteinHero Level 12"))
                            {
                                towerModel.GetAttackModel(0).range = 85;
                                var bombaclat = towerModel.GetAttackModel("elebomb");
                                bombaclat.range = towerModel.GetAttackModel(0).range;
                                towerModel.range = towerModel.GetAttackModel(0).range;
                                towerModel.GetWeapon(4).isStunned = false;
                            }
                            if (towerModel.appliedUpgrades.Contains("FrankensteinHero-FrankensteinHero Level 7"))
                            {
                                towerModel.GetAbility(1).enabled = true;
                                towerModel.GetAbility(2).enabled = false;
                            }
                            Tower.tower.UpdateRootModel(towerModel);
                        }
                    }
                }
                if (weapon.weaponModel.projectile.maxPierce == 936)
                {
                    InGame.instance.SpawnBloons(ModContent.BloonID<FRANKENBLOON>(), 1, 0);
                }
                if (weapon.weaponModel.projectile.maxPierce == 396396)
                {
                    InGame.instance.SpawnBloons(ModContent.BloonID<FRANKENZOMG>(), 1, 0);
                }
                if (weapon.weaponModel.projectile.maxPierce == 936936)
                {
                    InGame.instance.SpawnBloons(ModContent.BloonID<FRANKENBAD>(), 1, 0);
                }
                if (weapon.weaponModel.projectile.maxPierce == 936936)
                {
                    InGame.instance.SpawnBloons(ModContent.BloonID<FRANKENBAD>(), 1, 0);
                }
                if (weapon.weaponModel.projectile.maxPierce == 489)
                {
                    weapon.attack.tower.Node.graphic.GetComponent<Animator>().SetTrigger("Shoot");
                }
                if (weapon.weaponModel.projectile.scale == 1.396f)
                {
                    weapon.attack.tower.Node.graphic.GetComponent<Animator>().SetTrigger("Punch");
                }
            }
        }
    }
}