using BepInEx;
using HarmonyLib;
using System;
using UnityEngine;

namespace MonsterSanctuary_DokodemoDoor
{
    [BepInPlugin("scp.plugins.monstersanctuary.dokodemodoor", "DokodemoDoor", "1.0.0")]
    [BepInProcess("Monster Sanctuary.exe")]
    public class DokodemoDoor : BaseUnityPlugin
    {
        static Harmony instance;
        static BepInEx.Configuration.KeyboardShortcut KeyR;
        static BepInEx.Configuration.KeyboardShortcut KeyT;

        void Awake()
        {
            Logger.LogInfo("DokodemoDoor loaded.");
            instance = HarmonyLib.Harmony.CreateAndPatchAll(typeof(DokodemoDoor));
        }

        void Start()
        {
            KeyR = new BepInEx.Configuration.KeyboardShortcut(KeyCode.R);
            KeyT = new BepInEx.Configuration.KeyboardShortcut(KeyCode.T);
        }
        void Update()
        {
            if (!PlayerController.Instance.InputAllowed()) return;
            if (KeyR.IsDown())
            {
                GameStateManager.Instance.SetState(GameStateManager.GameStates.IngameMenu, 0f);
                UIController.Instance.Minimap.Hide(true);
                UIController.Instance.IngameMenu.Map.Open(MapMenu.EOpenType.Teleporting);

            }
            if (KeyT.IsDown())
            {
                Vector2 pos = CalculateMousePosRelativeToPlayer();
                Vector3 curPos = PlayerController.Instance.PlayerPosition;
                Vector3 target = new Vector3();
                target.x = pos.x;
                target.y = pos.y;
                target.z = curPos.z;
                PlayerController.Instance.Physics.SetPlayerPosition(target, true, 0f);
            }
        }
        [HarmonyReversePatch]
        [HarmonyPatch(typeof(PlayerController), "CalculateMousePosRelativeToPlayer")]
#pragma warning disable Harmony001 // Harmony patch missing 'static' member modifier   WTF?
        public Vector2 CalculateMousePosRelativeToPlayer()
        {
            throw new NotImplementedException("It's a stub");
        }
    }
}
