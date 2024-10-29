using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
using Il2CppAssets.Scripts.Unity;
using Il2CppNinjaKiwi.Common.ResourceUtils;
using BTD_Mod_Helper.Api.Components;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.Bloons;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Bloons;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppAssets.Scripts.Simulation.SMath;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using UnityEngine;
using static BTD_Mod_Helper.Api.ModContent;
using BTD_Mod_Helper.Api;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Bloons.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Behaviors;
using FrankenBloon;

namespace FrankenBloon
{
    public class FRANKENBLOON : ModBloon
    {
        public override string BaseBloon => BloonType.Zomg;
        public override string Name => "FRANKENBLOON";

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            var badImmunity = Game.instance.model.GetBloon("Bad").GetBehavior<BadImmunityModel>().Duplicate();
            bloonModel.leakDamage = 0;
            bloonModel.speed = 20f;
            bloonModel.maxHealth = 5500;
            bloonModel.disallowCosmetics = true;
            bloonModel.dontShowInSandbox = true;
            bloonModel.ApplyDisplay<FrankensteinHeroDisplays.frankenbloon1>();
            bloonModel.RemoveAllChildren();
            bloonModel.RemoveBehavior<DamageStateModel>();
            bloonModel.damageDisplayStates = bloonModel.damageDisplayStates.Empty();

            var hpTriggerModel = new HealthPercentTriggerModel("HealthPercentTrigger", true, new float[] { 0.01f }, new string[] { "KnockBack" }, false);
            bloonModel.AddBehavior(hpTriggerModel);
            bloonModel.AddBehavior(badImmunity);
        }
    }
    public class FRANKENZOMG : ModBloon
    {
        public override string BaseBloon => BloonType.Bad;
        public override string Name => "FRANKENZOMG";

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            var badImmunity = Game.instance.model.GetBloon("Bad").GetBehavior<BadImmunityModel>().Duplicate();
            bloonModel.leakDamage = 0;
            bloonModel.speed = 15f;
            bloonModel.maxHealth = 12500;
            bloonModel.disallowCosmetics = true;
            bloonModel.dontShowInSandbox = true;
            bloonModel.ApplyDisplay<FrankensteinHeroDisplays.frankenbloon2>();
            bloonModel.RemoveAllChildren();
            bloonModel.RemoveBehavior<DamageStateModel>();
            bloonModel.damageDisplayStates = bloonModel.damageDisplayStates.Empty();

            var hpTriggerModel = new HealthPercentTriggerModel("HealthPercentTrigger", true, new float[] { 0.01f }, new string[] { "KnockBack" }, false);
            bloonModel.AddBehavior(hpTriggerModel);
            bloonModel.AddBehavior(badImmunity);
        }
    }
    public class FRANKENBAD : ModBloon
    {
        public override string BaseBloon => BloonType.Bad;
        public override string Name => "FRANKENBAD";

        public override void ModifyBaseBloonModel(BloonModel bloonModel)
        {
            var badImmunity = Game.instance.model.GetBloon("Bad").GetBehavior<BadImmunityModel>().Duplicate();
            bloonModel.leakDamage = 0;
            bloonModel.speed = 10f;
            bloonModel.maxHealth = 24180;
            bloonModel.disallowCosmetics = true;
            bloonModel.dontShowInSandbox = true;
            bloonModel.ApplyDisplay<FrankensteinHeroDisplays.frankenbloon3>();
            bloonModel.RemoveAllChildren();
            bloonModel.RemoveBehavior<DamageStateModel>();
            bloonModel.damageDisplayStates = bloonModel.damageDisplayStates.Empty();

            var hpTriggerModel = new HealthPercentTriggerModel("HealthPercentTrigger", true, new float[] { 0.01f }, new string[] { "KnockBack" }, false);
            bloonModel.AddBehavior(hpTriggerModel);
            bloonModel.AddBehavior(badImmunity);
        }
    }
}
[HarmonyPatch]
public static class BloonPatches
{
    //credit = KayaGG - BonnieHero
    private static void CreateLightningProjectile(Tower tower, Vector3Boxed position, float amount)
    {
        var Projectile = Game.instance.model.GetTowerFromId("BoomerangMonkey-400").GetAttackModel(0).weapons[0].projectile.Duplicate();
        Projectile.pierce = 9999f;
        Projectile.display = new() { guidRef = "" };
        var createoncontact = Game.instance.model.GetTower(TowerType.BombShooter).GetWeapon().projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate();
        createoncontact.projectile = Game.instance.model.GetTowerFromId("Druid-200").GetAttackModel().weapons[1].projectile.Duplicate();
        CreateLightningEffectModel lightningEffect = new CreateLightningEffectModel("CreateLightningEffect_", 0.3f,
        new PrefabReference[]
{
                    new PrefabReference() { guidRef = "Lightning-LightningSmall1" },
                    new PrefabReference() { guidRef = "Lightning-LightningSmall2" },
                    new PrefabReference() { guidRef = "Lightning-LightningSmall3" },
                    new PrefabReference() { guidRef = "Lightning-LightningMedium1" },
                    new PrefabReference() { guidRef = "Lightning-LightningMedium2" },
                    new PrefabReference() { guidRef = "Lightning-LightningMedium3" },
                    new PrefabReference() { guidRef = "Lightning-LightningLarge1" },
                    new PrefabReference() { guidRef = "Lightning-LightningLarge2" },
                    new PrefabReference() { guidRef = "Lightning-LightningLarge3" },
},
        new float[]
{
                    17.962965f,
                    17.962965f,
                    17.962965f,
                    50.0000076f,
                    50.0000076f,
                    50.0000076f,
                    85.18519f,
                    85.18519f,
                    85.18519f
});
        createoncontact.projectile.RemoveBehavior<CreateLightningEffectModel>();
        createoncontact.projectile.AddBehavior(lightningEffect);
        createoncontact.projectile.pierce = 4;
        createoncontact.projectile.GetDamageModel().damage = 20;
        createoncontact.projectile.maxPierce = 4;
        createoncontact.projectile.GetBehavior<LightningModel>().splits = 1;
        Projectile.AddBehavior(createoncontact);
        var projectile = InGame.instance.GetMainFactory().CreateEntityWithBehavior<Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Projectile, ProjectileModel>(Projectile);
        projectile.Position.X = position.X;
        projectile.Position.Y = position.Y;
        projectile.Position.Z = 20;
        projectile.Direction.X = 0;
        projectile.Direction.Y = 0;
        projectile.Direction.Z = 0;
        projectile.owner = InGame.instance.GetUnityToSimulation().MyPlayerNumber;
        projectile.emittedFrom = new Il2CppAssets.Scripts.Simulation.SMath.Vector3(position.X, position.Y, 100);
        projectile.EmittedBy = tower;
        projectile.Lifespan = 9;
    }
    [HarmonyPatch(typeof(HealthPercentTrigger), nameof(HealthPercentTrigger.Trigger))]
    [HarmonyPostfix]
    public static void HealthPercentTrigger_Trigger(HealthPercentTrigger __instance)
    {
        if (__instance.bloon.bloonModel.name.Contains("FRANKEN"))
        {
            var hero = InGame.instance.GetTowers()
                .Find(tower => tower.towerModel.baseId == TowerID<FrankensteinHero.franknstan>());
            if (hero == null)
            {
                return;
            }
            var Projectile = Game.instance.model.GetTower("BombShooter", 2).GetAttackModel(0).weapons[0].projectile.Duplicate();
            CreateLightningProjectile(hero, __instance.bloon.Position, 3f);
        }
    }
}
