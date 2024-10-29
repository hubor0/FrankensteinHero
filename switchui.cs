using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using System.Collections.Generic;
using UnityEngine;
using BTD_Mod_Helper.Extensions;
using MelonLoader;
using BTD_Mod_Helper.Api.Components;
using BTD_Mod_Helper.Api;
using Il2CppAssets.Scripts.Unity;
using System.Collections;
using UnityEngine.UI;
using BTD_Mod_Helper;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using System;
using Il2CppAssets.Scripts;
using FrankensteinHero;
using Il2CppAssets.Scripts.Models.Towers;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.TowerSelectionMenu;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppInterop.Runtime;
using Il2CppNinjaKiwi.Common;
using Il2CppSystem.Reflection;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Events;
using System.Threading;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using Il2CppTMPro;
using Il2CppAssets.Scripts.Models;
using Object = UnityEngine.Object;
using Il2CppSystem;
using Boolean = Il2CppSystem.Boolean;
using Action = System.Action;
using Math = System.Math;



namespace towerswitchUi
{
    [RegisterTypeInIl2Cpp(false)]
    public class switchMenu : MonoBehaviour
    {
        public static switchMenu? instance = null;
        public ModHelperInputField? input;
        // private ModHelperImage? image;
        public ModHelperButton? Prestige;
        public static Il2CppAssets.Scripts.Simulation.Towers.Tower? selectedTower = null;

        private static Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();





        public void Close()
        {
            if (gameObject)
            {
                gameObject.Destroy();
            }
        }
        public static void CreatePanel()
        {
            if (InGame.instance != null)
            {
                RectTransform rect = InGame.instance.uiRect;
                var panel = rect.gameObject.AddModHelperPanel(new("Panelinvis", 0, 0, 0, 0), VanillaSprites.BrownInsertPanel);
                panel = GameObject.Find("TowerElements").AddModHelperPanel(new Info("switchpanel", -230, 690, 500, 500), ModContent.GetTextureGUID<FrankensteinHero.FrankensteinHero>("cable"));
                instance = panel.AddComponent<switchMenu>();
                var button = panel.AddButton(new("Button_", 40, 160, 150, 150), ModContent.GetTextureGUID<FrankensteinHero.FrankensteinHero>("button"), new Action(() =>
                {

                    var Towers = InGame.instance.GetAllTowerToSim().FindAll(sim => sim.tower.towerModel.name.Contains("FrankensteinHero"));
                    foreach (var Tower in Towers)
                    {
                                var towerModel = Tower.tower.rootModel.Duplicate().Cast<TowerModel>();
                        Tower.tower.cashEarned = 1;
                        Tower.tower.UpdateRootModel(towerModel);
                    }

                }));
                var button2 = panel.AddButton(new("Button_", -150, 160, 150, 150), ModContent.GetTextureGUID<FrankensteinHero.FrankensteinHero>("button"), new Action(() =>
                {
                    var Towers = InGame.instance.GetAllTowerToSim().FindAll(sim => sim.tower.towerModel.name.Contains("FrankensteinHero"));
                    foreach (var Tower in Towers)
                    {

                                var towerModel = Tower.tower.rootModel.Cast<TowerModel>();
                        Tower.tower.cashEarned = 0;
                        Tower.tower.UpdateRootModel(towerModel);
                    }
                }));
                button.AddImage(new Info("monstericon", 0, 0, 130, 130), ModContent.GetTextureGUID<FrankensteinHero.FrankensteinHero>("frankicon"));
                button2.AddImage(new Info("doctoricon", 0, 0, 130, 130), ModContent.GetTextureGUID<FrankensteinHero.FrankensteinHero>("stanicon"));
            }
        }
    }
}