using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Unity;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using System.Linq;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.Stats;
using Il2CppAssets.Scripts.Simulation.Input;
using BTD_Mod_Helper.Api.Components;
using HarmonyLib;
using Il2CppAssets.Scripts.Simulation.Behaviors;
using Il2CppAssets.Scripts.Simulation.Display;
using Il2CppAssets.Scripts.Simulation.SMath;
using BTD_Mod_Helper.Api;
using UnityEngine;
using FrankensteinHero;
using Il2CppAssets.Scripts.Simulation.SimulationBehaviors;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using static BTD_Mod_Helper.Api.ModContent;
using FrankenBloon;

namespace FrankensteinHero
{
    public class franknstan : ModHero
    {
        public override string BaseTower => TowerType.DartMonkey;
        public override string Name => "FrankensteinHero";
        public override int Cost => 1465;
        public override string DisplayName => "FRANK & Stan";
        public override string Title => "Doctor and Monster";
        public override string Level1Description => "Change between powering the Scientist or Monster for different specializations. Stan zaps Bloons with a long range blaster, and FRANK mashes Bloons for a powerful close range attack.";
        public override string Description => "Legendary Doctor Stanislaw Viktor Monkeystein, and his ultimate creation FRANK. A Monstrous duo born from mad genius and crackling lightning. \"IT'S ALIVEE!!\" ";
        public override string Icon => "heroportrait";
        public override string Portrait => "heroportrait";
        public override string Square => "squareicon";
        public override string Button => "Icon";
        public override string NameStyle => TowerType.PatFusty;
        public override string BackgroundStyle => TowerType.Ezili;
        public override string GlowStyle => TowerType.PatFusty;
        public override int MaxLevel => 20;
        public override float XpRatio => 1.71f;

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<FrankensteinHeroDisplays.lvl1>();
            towerModel.range = 65;
            towerModel.radius = Game.instance.model.GetTower(TowerType.BananaFarm).radius;
            var attackModel = towerModel.GetAttackModel();
            attackModel.range = 65;
            attackModel.weapons[0].Rate = 0.9f;
            attackModel.weapons[0].projectile.pierce = 2;
            attackModel.weapons[0].projectile.maxPierce = 489;
            attackModel.weapons[0].projectile.GetDamageModel().damage = 3;
            attackModel.weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Purple;
            attackModel.weapons[0].projectile.GetBehavior<TravelStraitModel>().Lifespan = 10f;
            attackModel.weapons[0].projectile.GetBehavior<TravelStraitModel>().Speed /= 0.5f;
            attackModel.weapons[0].projectile.ApplyDisplay<FrankensteinHeroDisplays.blaster>();
            attackModel.name = "blaster";
            attackModel.weapons[0].projectile.name = "blaster";
            var fistmash = Game.instance.model.GetTowerFromId("BombShooter-200").GetAttackModel().Duplicate();
            fistmash.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Lead;
            fistmash.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().damage = 2;
            fistmash.weapons[0].projectile.RemoveBehavior<CreateSoundOnProjectileCollisionModel>();
            fistmash.weapons[0].projectile.GetBehavior<CreateEffectOnContactModel>().effectModel.ApplyDisplay<FrankensteinHeroDisplays.mash>();
            fistmash.weapons[0].projectile.display = new() { guidRef = "" };
            fistmash.name = "fistmash";
            fistmash.weapons[0].projectile.scale = 1.396f;
            fistmash.weapons[0].name = "fistmash";
            fistmash.weapons[0].Rate = 1.3f;
            fistmash.weapons[0].projectile.GetBehavior<TravelStraitModel>().Lifespan = 10f;
            fistmash.weapons[0].projectile.GetBehavior<TravelStraitModel>().Speed /= 0.7f;
            towerModel.AddBehavior(fistmash);

            var timertick = Game.instance.model.GetTowerFromId("MonkeySub-300").GetAttackModel(1).Duplicate();
            timertick.weapons[0].rate = 1f;
            timertick.weapons[0].projectile.maxPierce = 2137f;
            timertick.weapons[0].projectile.radius = 0.00001f;
            towerModel.AddBehavior(timertick);


        }
    }
    public static class patches
    {


        [HarmonyPatch(typeof(Factory.__c__DisplayClass21_0), "_CreateAsync_b__0")]
        public class FactoryCreateAsync_Patch
        {
            // CREDIT: Hoo-Knows - TeslaCoil
            [HarmonyPrefix]
            public static bool Prefix(ref Factory.__c__DisplayClass21_0 __instance, ref UnityDisplayNode prototype)
            {
                if (__instance.objectId.guidRef.Split('-')[0] == "Lightning")
                {
                    GameObject go = new GameObject(__instance.objectId.guidRef);
                    go.transform.position = new UnityEngine.Vector3(-3000f, 0f, 0f);

                    SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                    sr.sprite = ModContent.GetSprite<FrankensteinHero>(__instance.objectId.guidRef.Split('-')[1]);
                    // These are just values from the original lightning
                    sr.sortingGroupID = 1048575;
                    sr.sortingLayerID = -4022049;
                    sr.renderingLayerMask = 4294967295;

                    go.AddComponent<UnityDisplayNode>();
                    prototype = go.GetComponent<UnityDisplayNode>();

                    __instance.__4__this.active.Add(prototype);
                    __instance.onComplete.Invoke(prototype);
                    return false;
                }
                return true;
            }
        }
        [HarmonyPatch(typeof(Il2CppAssets.Scripts.Simulation.SimulationBehaviors.NecroData), nameof(NecroData.RbePool))]
        internal static class Necro_RbePool
        {
            [HarmonyPrefix]
            private static bool Postfix(NecroData __instance, ref int __result)
            {
                var tower = __instance.tower;
                __result = 9999;
                return false;
            }
        }
        [HarmonyPatch(typeof(Ability), nameof(Ability.Activate))]
        [HarmonyPostfix]
        private static void Ability_Activate(Ability __instance)
        {
            InGame.instance.SpawnBloons(ModContent.BloonID<FRANKENBLOON>(), 1, 0);
        }
    }
}