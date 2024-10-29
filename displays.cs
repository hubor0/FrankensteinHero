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
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppSystem.IO;
using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2Cpp;
using BTD_Mod_Helper.Api;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using Il2CppAssets.Scripts.Models.Map;
using System.Collections.Generic;
using System.Linq;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using System.Runtime.CompilerServices;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity.Towers;
using FrankensteinHero;
using UnityEngine;
using System;
using System.IO;
using MelonLoader.Utils;


namespace FrankensteinHeroDisplays
{
    public class electricbomb : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }
    public class blaster : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }
    public class mbloon1 : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }
    public class mbloon2 : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }
    public class mbloon3 : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }
    public class mbloon4 : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }
    public class mash : ModDisplay
    {
        public override string BaseDisplay => "f5ecd5c90fb5d0240aaa7b75c980dffe";



    }
    public class nrgsplosion : ModDisplay
    {
        public override string BaseDisplay => "21f659bbb9e1d9441adf3239a773e224";
        public Dictionary<string, Color> psColor = new Dictionary<string, Color>()
        {
            { "Glow", new Color(0.1f, 0.7f, 1f, 0.409f) },
            { "Lightning", new Color(0.1f, 0.8f, 1f) },
            { "Pulse", new Color(0.1f, 0.5f, 1f, 0.518f) },
            { "PulseBig", new Color(0.1f, 0.5f, 1f, 0.518f) }
        };
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            foreach (ParticleSystem ps in node.GetComponentsInChildren<ParticleSystem>())
            {
                if (psColor.ContainsKey(ps.gameObject.name)) ps.startColor = psColor[ps.gameObject.name];
            }
        }
    }
    public class atomsmash : ModDisplay
    {
        public override string BaseDisplay => "88399aeca4ae48a44aee5b08eb16cc61";


        public override void ModifyDisplayNode(UnityDisplayNode node)
        {

            foreach (Renderer renderer in node.genericRenderers)
            {
                renderer.material.color = Color.cyan;
            }

        }
    }
    public class monsterbash : ModDisplay
    {
        public override string BaseDisplay => "0dbf845c78671364ab619d96d40696a5";



    }

    //3d models
    public class lvl1 : ModCustomDisplay
    {
        public override string AssetBundleName => "franknstan";
        public override string PrefabName => "frankensteinlvl1";

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.transform.GetChild(0).transform.localScale *= 108;
            foreach (var meshRenderer in node.GetMeshRenderers())
            {
                meshRenderer.ApplyOutlineShader();

                meshRenderer.SetOutlineColor(new Color(34 / 255f, 54 / 255f, 40 / 255f));

            }
        }
    }
    public class lvl3 : ModCustomDisplay
    {
        public override string AssetBundleName => "franknstan";
        public override string PrefabName => "lvl3";

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.transform.GetChild(0).transform.localScale *= 108;
            foreach (var meshRenderer in node.GetMeshRenderers())
            {
                meshRenderer.ApplyOutlineShader();

                meshRenderer.SetOutlineColor(new Color(34 / 255f, 54 / 255f, 40 / 255f));

            }
        }
    }
    public class lvl7 : ModCustomDisplay
    {
        public override string AssetBundleName => "franknstan";
        public override string PrefabName => "lvl7";

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.transform.GetChild(0).transform.localScale *= 108;
            foreach (var meshRenderer in node.GetMeshRenderers())
            {
                meshRenderer.ApplyOutlineShader();

                meshRenderer.SetOutlineColor(new Color(34 / 255f, 54 / 255f, 40 / 255f));

            }
        }
    }
    public class lvl10 : ModCustomDisplay
    {
        public override string AssetBundleName => "franknstan";
        public override string PrefabName => "lvl10";

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.transform.GetChild(0).transform.localScale *= 108;
            foreach (var meshRenderer in node.GetMeshRenderers())
            {
                meshRenderer.ApplyOutlineShader();

                meshRenderer.SetOutlineColor(new Color(34 / 255f, 54 / 255f, 40 / 255f));

            }
        }
    }
    public class lvl20 : ModCustomDisplay
    {
        public override string AssetBundleName => "franknstan";
        public override string PrefabName => "lvl20";

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.transform.GetChild(0).transform.localScale *= 110;
            foreach (var meshRenderer in node.GetMeshRenderers())
            {
                    meshRenderer.ApplyOutlineShader();

                    meshRenderer.SetOutlineColor(new Color(34 / 255f, 54 / 255f, 40 / 255f));
            }
        }
    }
    public class frankenbloon1 : ModCustomDisplay
    {
        public override string AssetBundleName => "franknstan";
        public override string PrefabName => "frankenbloon";

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.transform.GetChild(0).transform.localScale *= 60;
            foreach (var meshRenderer in node.GetMeshRenderers())
            {
                meshRenderer.ApplyOutlineShader();

                meshRenderer.SetOutlineColor(new Color(34 / 255f, 54 / 255f, 40 / 255f));

            }
        }
    }
    public class frankenbloon2 : ModCustomDisplay
    {
        public override string AssetBundleName => "franknstan";
        public override string PrefabName => "frankenbfb";

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.transform.GetChild(0).transform.localScale *= 77;
            foreach (var meshRenderer in node.GetMeshRenderers())
            {
                meshRenderer.ApplyOutlineShader();

                meshRenderer.SetOutlineColor(new Color(34 / 255f, 54 / 255f, 40 / 255f));

            }
        }
    }
    public class frankenbloon3 : ModCustomDisplay
    {
        public override string AssetBundleName => "franknstan";
        public override string PrefabName => "frankenbad";

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.transform.GetChild(0).transform.localScale *= 70;
            foreach (var meshRenderer in node.GetMeshRenderers())
            {
                meshRenderer.ApplyOutlineShader();

                meshRenderer.SetOutlineColor(new Color(34 / 255f, 54 / 255f, 40 / 255f));

            }
        }
    }
}