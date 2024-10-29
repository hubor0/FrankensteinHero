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
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.Stats;
using Il2CppAssets.Scripts.Simulation.Input;
using BTD_Mod_Helper.Api.Components;
using HarmonyLib;
using Il2CppAssets.Scripts.Simulation.Behaviors;
using Il2CppAssets.Scripts.Simulation.Display;
using Il2CppAssets.Scripts.Simulation.SMath;
using FrankensteinHero;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppNinjaKiwi.Common.ResourceUtils;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using UnityEngine;

namespace FrankensteinHerolevels
{
        public class Lv2 : ModHeroLevel<franknstan>
      {
        public override string Description => "FRANK's mash attacks deal +1 damage, and can pop all Bloon types";
        public override int Level => 2;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetAttackModel(1).weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().damage = 3;
            towerModel.GetAttackModel(1).weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
        }
      }
        public class Lv3 : ModHeroLevel<franknstan>
    {
        public override string Description => "Lightning Conduit Ability: Send 3 waves of powerful chained lightning attack which follow your targeting priority and deal insane damage to MOAB class Bloons.";
        public override int Level => 3;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<FrankensteinHeroDisplays.lvl3>();
            AttackModel[] attack = { Game.instance.model.GetTowerFromId("Druid-200").GetBehavior<AttackModel>().Duplicate() };
            attack[0].weapons[0].Rate = 9999f;
            attack[0].weapons[0].projectile.RemoveBehavior<DamageModel>();
            attack[0].weapons[0].projectile.GetBehavior<TravelStraitModel>().Lifespan = 0.0000001f;
            attack[0].weapons[0].animateOnMainAttack = false;
            attack[0].weapons[0].animation = 0;
            attack[0].weapons[0].projectile.display = new() { guidRef = "" };
            attack[0].weapons[0].name = "DUPA";
            attack[0].weapons[1].projectile.UpdateCollisionPassList();
            attack[0].range = 140f;
            attack[0].weapons[1].projectile.pierce = 3;
            attack[0].weapons[1].projectile.maxPierce = 3;
            attack[0].weapons[1].projectile.GetBehavior<LightningModel>().splits = 1;
            attack[0].weapons[1].projectile.GetDamageModel().damage = 30;
            attack[0].weapons[1].projectile.AddBehavior(new DamageModifierForTagModel("Moabsdmg", "Moabs", 1, 90, false, false));
            attack[0].weapons[1].projectile.AddBehavior(new DamageModifierForTagModel("Bossdmg", "Boss", 5, 1, false, true));
            attack[0].weapons[1].Rate = 2f;
            attack[0].weapons[1].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
            attack[0].weapons[1].projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter").GetWeapon().projectile.GetBehavior<CreateEffectOnContactModel>().Duplicate());
            attack[0].weapons[1].projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-500").GetWeapon().projectile.GetBehavior<CreateSoundOnProjectileCollisionModel>().Duplicate());
            var Ability = Game.instance.model.GetTowerFromId("Mermonkey-040").GetAbility().Duplicate();
            Ability.GetBehavior<ActivateAttackModel>().attacks = attack;
            Ability.maxActivationsPerRound = 9999999;
            Ability.cooldown = 200;
            Ability.name = "Lightning Conduit";
            Ability.AddBehavior(Game.instance.model.GetTowerFromId("EngineerMonkey-040").GetBehavior<AbilityModel>().GetBehavior<CreateSoundOnAbilityModel>());
            Ability.displayName = "Lightning Conduit";
            Ability.GetBehavior<ActivateAttackModel>().Lifespan = 5f;
            Ability.icon = GetSpriteReference("conduit");
            CreateLightningEffectModel lightningEffect = new CreateLightningEffectModel("CreateLightningEffect_", 0.3f,
               new PrefabReference[]
               {
                    new PrefabReference() { guidRef = "Lightning-LightningSmall1xl" },
                    new PrefabReference() { guidRef = "Lightning-LightningSmall2xl" },
                    new PrefabReference() { guidRef = "Lightning-LightningSmall3xl" },
                    new PrefabReference() { guidRef = "Lightning-LightningMedium1xl" },
                    new PrefabReference() { guidRef = "Lightning-LightningMedium2xl" },
                    new PrefabReference() { guidRef = "Lightning-LightningMedium3xl" },
                    new PrefabReference() { guidRef = "Lightning-LightningLarge1xl" },
                    new PrefabReference() { guidRef = "Lightning-LightningLarge2xl" },
                    new PrefabReference() { guidRef = "Lightning-LightningLarge3xl" },
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
                    85.18519f,
               });
            attack[0].weapons[1].projectile.RemoveBehavior<CreateLightningEffectModel>();
            attack[0].weapons[1].projectile.AddBehavior(lightningEffect);
            towerModel.AddBehavior(Ability);
        }
    }
        public class Lv4 : ModHeroLevel<franknstan>
    {
        public override string Description => "Increases the pierce and power of Stan's blaster";
        public override int Level => 4;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetAttackModel(0).weapons[0].projectile.pierce = 4;
            towerModel.GetAttackModel(0).weapons[0].projectile.GetDamageModel().damage = 4;
        }
    }
        public class Lv5 : ModHeroLevel<franknstan>
    {
        public override string Description => "Increases the damage of FRANK's mash attack by 2";
        public override int Level => 5;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetAttackModel(1).weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().damage = 5;
        }
    }
        public class Lv6 : ModHeroLevel<franknstan>
    {
        public override string Description => "Experimental Ressurection: every 5s Stan summons an small Monster-Bloon. Stan gains Camo detection, increased range and can pop any Bloon type.";
        public override int Level => 6;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetDescendants<FilterInvisibleModel>().ForEach(model => model.isActive = false);
            towerModel.GetAttackModel(0).weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
            towerModel.GetAttackModel(0).weapons[0].projectile.SetHitCamo(true);
            towerModel.GetAttackModel(0).RemoveFilter<FilterInvisibleModel>();
            towerModel.RemoveBehaviors<NecromancerZoneModel>();
            var summon = Game.instance.model.GetTowerFromId("WizardMonkey-004").GetAttackModel(2).Duplicate();
            summon.name = "MBLOON";
            summon.weapons[0].emission = new NecromancerEmissionModel("BaseDeploy_", 1, 1, 1, 1, 1, 1, 0, null, null, null, 1, 1, 1, 1, 2);
            summon.weapons[0].rate = 12f;
            summon.range = 100000;
            summon.weapons[0].projectile.GetBehavior<TravelAlongPathModel>().disableRotateWithPathDirection = true;
            summon.weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Lead;
            summon.weapons[0].projectile.RemoveBehavior<CreateEffectOnExhaustedModel>();
            summon.weapons[0].projectile.GetBehavior<TravelAlongPathModel>().lifespan = 99999f;
            summon.weapons[0].projectile.GetBehavior<TravelAlongPathModel>().lifespanFrames = 0;
            summon.weapons[0].projectile.pierce = 9;
            summon.weapons[0].projectile.ApplyDisplay<FrankensteinHeroDisplays.mbloon1>();
            summon.weapons[0].projectile.GetDamageModel().damage = 2;
            summon.weapons[0].projectile.scale *= 1.3f;

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
            createoncontact.projectile.pierce = 2;
            createoncontact.projectile.GetDamageModel().damage = 1;
            createoncontact.projectile.maxPierce = 2;
            createoncontact.projectile.GetBehavior<LightningModel>().splits = 1;
            summon.weapons[0].projectile.AddBehavior(createoncontact);
            towerModel.AddBehavior(summon);
        }
    }
        public class Lv7 : ModHeroLevel<franknstan>
    {
        public override string Description => "Depending on your chosen character you get different abilities. \n" +
        "Atom Smash Ability: Stan Launches a slow Energy Bomb towards the strongest Bloon\n" +
        "Monster Bash Ability: FRANK vigorously mashes all nearby Bloons, heavily damaging, and slowing them."; 
        public override int Level => 7;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<FrankensteinHeroDisplays.lvl7>();

            AttackModel[] abilitystat = { Game.instance.model.GetTowerFromId("DartMonkey").GetBehavior<AttackModel>().Duplicate() };
            var Ability1 = Game.instance.model.GetTowerFromId("Mermonkey-040").GetAbility().Duplicate();
            var splosion = Game.instance.model.GetTowerFromId("MonkeySub-040").GetAbility().GetBehavior<ActivateAttackModel>().attacks[0].weapons[0].projectile.GetBehavior<CreateProjectileOnExpireModel>().Duplicate();
            splosion.projectile.GetDamageModel().damage = 12000;
            splosion.projectile.pierce = 700;
            splosion.projectile.GetDamageModel().distributeToChildren = true;
            splosion.projectile.radius = 4000;
            splosion.projectile.maxPierce = 750;
            abilitystat[0].range = 9999;
            abilitystat[0].weapons[0].projectile.RemoveFilter<FilterAllExceptTargetModel>();
            abilitystat[0].weapons[0].Rate = 2f;
            abilitystat[0].weapons[0].projectile.AddBehavior(new TrackTargetModel("TrackTargetModel_", 2000f, true, false, 180f, true, 360f, false, false));
            abilitystat[0].weapons[0].projectile.AddBehavior(splosion);
            var dupafx = Game.instance.model.GetTowerFromId("MonkeyAce-030").GetAttackModel(1).weapons[0].projectile.GetBehavior<CreateEffectOnExhaustFractionModel>().Duplicate();
            dupafx.effectModel.ApplyDisplay<FrankensteinHeroDisplays.nrgsplosion>();
            abilitystat[0].weapons[0].projectile.AddBehavior(dupafx);
           abilitystat[0].weapons[0].projectile.GetBehavior<TravelStraitModel>().speed = 6f;
            abilitystat[0].weapons[0].projectile.pierce = 999999;
            abilitystat[0].weapons[0].projectile.maxPierce = 999999;
            abilitystat[0].weapons[0].projectile.ApplyDisplay<FrankensteinHeroDisplays.atomsmash>();
            abilitystat[0].weapons[0].projectile.AddBehavior(new DamageModel("nrgballdmg", 2, 2, false, false, false, BloonProperties.Purple, BloonProperties.Purple, false));
            abilitystat[0].weapons[0].projectile.GetBehavior<TravelStraitModel>().Lifespan = 2.3f;
            Ability1.name = "Atom Smash";
            Ability1.GetBehavior<ActivateAttackModel>().attacks = abilitystat;
            Ability1.GetBehavior<ActivateAttackModel>().lifespan = 1.5f;
            Ability1.displayName = "Atom Smash";
            Ability1.icon = GetSpriteReference("atomsmash");
            Ability1.Cooldown = 270f;
            towerModel.AddBehavior(Ability1);

            AttackModel[] attack = { Game.instance.model.GetTowerFromId("BombShooter-500").GetBehavior<AttackModel>().Duplicate() };
            var Ability = Game.instance.model.GetTowerFromId("DartlingGunner-040").GetAbility(0).Duplicate();
            var mash = Game.instance.model.GetTowerFromId("MortarMonkey-040").GetAbility().GetBehavior<TurboModel>().Duplicate();
            mash.Lifespan = 5f;
            mash.multiplier = 0.3f;
            Ability.AddBehavior(mash);
            Ability.GetBehavior<ActivateAttackModel>().attacks = attack;  
            attack[0].weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.RemoveBehavior<PushBackModel>();
            attack[0].weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<SlowModel>().multiplier = 0.5f;
            attack[0].weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().damage = 10;
            attack[0].weapons[0].projectile.GetBehavior<CreateEffectOnContactModel>().effectModel.ApplyDisplay<FrankensteinHeroDisplays.monsterbash>();
            attack[0].weapons[0].projectile.display = new() { guidRef = "" };
            attack[0].weapons[0].rate = 3f;
            Ability.name = "monster bash";
            Ability.displayName = "Monster Bash";
            Ability.Cooldown = 100f;
            Ability.icon = GetSpriteReference("monsterbash");
            Ability.GetBehavior<ActivateAttackModel>().Lifespan = 7f;
            towerModel.AddBehavior(Ability);

            var Abilitylooks = Game.instance.model.GetTowerFromId("BombShooter-005").GetAbility().Duplicate();
            Abilitylooks.icon = GetSpriteReference("zl7");
            Abilitylooks.addedViaUpgrade = "FrankensteinHero-FrankensteinHero Level 7";
            Abilitylooks.RemoveBehavior<ActivateAttackModel>();
            Abilitylooks.RemoveBehaviors<CreateEffectOnAbilityModel>();
            towerModel.AddBehavior(Abilitylooks);
        }
    }
        public class Lv8 : ModHeroLevel<franknstan>
    {
        public override string Description => "Stan now fires 25% faster. FRANK now has 30% more range, attacks faster and deals increased damage to Ceramic and MOAB class Bloons";
        public override int Level => 8;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetAttackModel(0).weapons[0].Rate = .6f;
            towerModel.GetAttackModel(1).weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("Ceramicdmgbonus", "Ceramic", 1, 3, false, false));
            towerModel.GetAttackModel(1).weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("Moabdmgfrank", "Moabs", 1, 20, false, false));
            towerModel.GetAttackModel(1).weapons[0].Rate = 1.25f;
        }
    }
        public class Lv9 : ModHeroLevel<franknstan>
    {
        public override string Description => "Lightning Conduit gains another 3 waves of lightning attacks and Heavily Increased damage.";
        public override int Level => 9;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetAbility(0).GetBehavior<ActivateAttackModel>().lifespan = 11f;
            towerModel.GetAbility(0).GetBehavior<ActivateAttackModel>().attacks[0].weapons[1].projectile.GetDamageModel().damage = 90;
            towerModel.GetAbility(0).GetBehavior<ActivateAttackModel>().attacks[0].weapons[1].projectile.AddBehavior(new DamageModifierForTagModel("Moabsdmg", "Moabs", 1, 150, false, false));
        }
    }
        public class Lv10 : ModHeroLevel<franknstan>
    {
        public override string Description => "Glory to Science Ability: Focus all your power to summon the \"FRANKENBLOON\". The more damage it takes, the more Bloons nearby it shocks. Can only be used when there's a MOAB class Bloon on the screen.";
        public override int Level => 10;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<FrankensteinHeroDisplays.lvl10>();
            AttackModel[] attack = { Game.instance.model.GetTowerFromId("SniperMonkey").GetBehavior<AttackModel>().Duplicate() };
            var Ability = Game.instance.model.GetTowerFromId("BombShooter-040").GetAbility().Duplicate();
            Ability.name = "frankenbloon_summon";
            Ability.displayName = "Glory To Science";
            attack[0].weapons[0].projectile.maxPierce = 936;
            attack[0].weapons[0].fireWithoutTarget = true;
            attack[0].weapons[0].Rate = 260f;
            attack[0].weapons[0].projectile.GetBehavior<AgeModel>().lifespan = 0.00001f;
            attack[0].range = 999999f;
            Ability.GetBehavior<ActivateAttackModel>().attacks = attack;
            Ability.addedViaUpgrade = "FrankensteinHero-FrankensteinHero Level 10";
            Ability.GetBehavior<ActivateAttackModel>().lifespan = 0.5f;
            Ability.icon = GetSpriteReference("frankenbloon");
            Ability.Cooldown = 200f;
            towerModel.AddBehavior(Ability);
        }
    }
    public class Lv11 : ModHeroLevel<franknstan>
    {
        public override string Description => "Experimental Ressurection's Monster-Bloons are now faster and can withstand more enemy Bloons. Frank attacks 15% faster.";
        public override int Level => 11;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var mbloon = towerModel.GetAttackModel("MBLOON");
            mbloon.weapons[0].projectile.pierce = 20;
            mbloon.weapons[0].projectile.GetBehavior<TravelAlongPathModel>().speed /= 1.3f;
            mbloon.weapons[0].projectile.ApplyDisplay<FrankensteinHeroDisplays.mbloon2>();
            towerModel.GetAttackModel(1).weapons[0].Rate -= 0.3f;
        }
    }
    public class Lv12 : ModHeroLevel<franknstan>
    {
        public override string Description => "Stan's blaster has increased pierce and attacks 30% faster. Every 4s Stan will fire an powerful electricity bomb which stuns Bloons for 1s";
        public override int Level => 12;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetAttackModel(0).weapons[0].projectile.pierce = 8;
            towerModel.GetAttackModel(0).weapons[0].Rate = 0.3f;
            var elebomb = Game.instance.model.GetTowerFromId("BoomerangMonkey-402").GetAttackModel().Duplicate();
            elebomb.weapons[0].projectile.pierce = 5;
            elebomb.weapons[0].projectile.maxPierce = 5;
            var splode = Game.instance.model.GetTowerFromId("BombShooter-500").GetWeapon().projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate();
            splode.projectile.GetBehavior<SlowModel>().lifespan = 0.3f;
            splode.projectile.GetBehavior<SlowModel>().lifespanFrames = 40;
            splode.projectile.RemoveBehavior<PushBackModel>();
            splode.projectile.RemoveBehavior<DamageModel>();
            elebomb.weapons[0].Rate = 4f;
            elebomb.weapons[0].projectile.SetHitCamo(true);
            elebomb.weapons[0].projectile.RemoveFilter<FilterInvisibleModel>();
            elebomb.RemoveFilter<FilterInvisibleModel>();
            splode.projectile.SetHitCamo(true);
            elebomb.RemoveFilter<FilterInvisibleModel>();
            elebomb.weapons[0].projectile.AddBehavior(splode);
            elebomb.weapons[0].projectile.ApplyDisplay<FrankensteinHeroDisplays.electricbomb>();
            elebomb.weapons[0].projectile.GetBehavior<TravelStraitModel>().speed /= .7f;
            elebomb.weapons[0].projectile.GetBehavior<TravelStraitModel>().Lifespan = 10f;
            elebomb.range = towerModel.GetAttackModel(0).range;
            elebomb.name = "elebomb";
            elebomb.weapons[0].name = "elebomb";
            towerModel.AddBehavior(elebomb);
        }
    }
    public class Lv13 : ModHeroLevel<franknstan>
    {
        public override string Description => "Lightning Conduit now casts 9 waves of chain lightning and deals even more damage. FRANK's mash attacks deal double damage, more MOAB damage, and extra damage to fortified and stunned Bloons";
        public override int Level => 13;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetAttackModel(1).weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().damage = 10;
            towerModel.GetAttackModel(1).weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("Moabdmgfrank2", "Moabs", 1, 55, false, false));
            towerModel.GetAttackModel(1).weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("Fortifieddmgfrank", "Fortified", 1, 2, false, false));
            towerModel.GetAttackModel(1).weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("stundmg", "Stun", 1, 2, false, false));

            towerModel.GetAbility(0).GetBehavior<ActivateAttackModel>().lifespan = 8.5f;
            towerModel.GetAbility(0).GetBehavior<ActivateAttackModel>().attacks[0].weapons[1].Rate = 1f;
            towerModel.GetAbility(0).GetBehavior<ActivateAttackModel>().attacks[0].weapons[1].projectile.GetDamageModel().damage = 300;
            towerModel.GetAbility(0).GetBehavior<ActivateAttackModel>().attacks[0].weapons[1].projectile.AddBehavior(new DamageModifierForTagModel("Moabsdmg", "Moabs", 1, 150, false, false));
        }
    }
    public class Lv14 : ModHeroLevel<franknstan>
    {
        public override string Description => "Monster-Bloons gain increased pierce and power.";
        public override int Level => 14;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var mbloon = towerModel.GetAttackModel("MBLOON");
            mbloon.weapons[0].projectile.pierce = 25;
            mbloon.weapons[0].projectile.GetDamageModel().damage = 5;
            mbloon.weapons[0].projectile.ApplyDisplay<FrankensteinHeroDisplays.mbloon3>();
        }
    }
    public class Lv15 : ModHeroLevel<franknstan>
    {
        public override string Description => "FRANK's mash attacks create weak chain electric attacks on hit.";
        public override int Level => 15;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var createoncontact = Game.instance.model.GetTower(TowerType.BombShooter).GetWeapon().projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate();
            createoncontact.projectile = Game.instance.model.GetTowerFromId("Druid-200").GetAttackModel().weapons[1].projectile.Duplicate();
            createoncontact.projectile.AddBehavior(new DamageModifierForTagModel("Ceramicdmgbonusltng", "Ceramic", 1, 3, false, false));
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
            createoncontact.projectile.maxPierce = 4;
            createoncontact.projectile.GetBehavior<LightningModel>().splits = 1;
            towerModel.GetAttackModel(1).weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(createoncontact);
        }
    }
    public class Lv16 : ModHeroLevel<franknstan>
    {
        public override string Description => "Glory to Science's FRANKENBLOON is now significantly stronger and slower";
        public override int Level => 16;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetAbility(4).GetBehavior<ActivateAttackModel>().attacks[0].weapons[0].projectile.maxPierce = 396396f;
        }
    }
    public class Lv17 : ModHeroLevel<franknstan>
    {
        public override string Description => "Experimental Ressurection's Monster-Bloons now have increased pierce and damage. Stan's electricity bomb can now stun for longer.";
        public override int Level => 17;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var bomb = towerModel.GetAttackModel("elebomb");
            bomb.weapons[0].projectile.pierce = 9;
            bomb.weapons[0].projectile.maxPierce = 9;
            bomb.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<SlowModel>().lifespan = 1.5f;
            bomb.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetBehavior<SlowModel>().lifespanFrames = 90;

            var mbloon = towerModel.GetAttackModel("MBLOON");
            mbloon.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().damage = 15;
            mbloon.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("zapceram", "Ceramic", 2, 5, false, false));
            mbloon.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(new DamageModifierForTagModel("zapfort", "Fortified", 1, 5, false, false));
            mbloon.weapons[0].projectile.ApplyDisplay<FrankensteinHeroDisplays.mbloon4>();
            mbloon.weapons[0].projectile.pierce = 30;

        }
    }
    public class Lv18 : ModHeroLevel<franknstan>
    {
        public override string Description => "both Monster Bash and Atom Smash cooldowns reduced by 20%. Monster Bash lasts 2s more";
        public override int Level => 18;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetAbility(1).cooldown -= 20;
            towerModel.GetAbility(2).cooldown -= 40;
            towerModel.GetAbility(2).GetBehavior<TurboModel>().lifespan = 7;
        }
    }
    public class Lv19 : ModHeroLevel<franknstan>
    {
        public override string Description => "Monster-Bloons zaps have now increased damage and pierce. FRANK's mash attacks deals increased damage. ";
        public override int Level => 19;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetAttackModel(1).weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().damage = 20;

            var mbloon = towerModel.GetAttackModel("MBLOON");
            mbloon.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().damage = 7;
            mbloon.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.pierce = 5;
        }
    }
    public class Lv20 : ModHeroLevel<franknstan>
    {
        public override string Description => "Glory to Science now summons The FRANKENBAD...  Lightning Conduit's lightning wave amount increased to 20. ";
        public override int Level => 20;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.ApplyDisplay<FrankensteinHeroDisplays.lvl20>();

            towerModel.GetAbility(0).GetBehavior<ActivateAttackModel>().lifespan = 10f;
            towerModel.GetAbility(0).GetBehavior<ActivateAttackModel>().attacks[0].weapons[1].Rate = 0.5f;

            towerModel.GetAbility(4).GetBehavior<ActivateAttackModel>().attacks[0].weapons[0].projectile.maxPierce = 936936f;
        }
    }
}

