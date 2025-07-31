using DG.Tweening.Plugins.Core;
using ExitGames.Client.Photon;
//using JustPlay.PsfLight.Events;
using Photon.Pun;
using Photon.Realtime;
using pworld.Scripts;
using pworld.Scripts.Extensions;
using SCPE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using Zorro.ControllerSupport;
using Zorro.Core;
using Zorro.Settings;
//using static AppConstants.Localizations;
using static HelperFunctions;
using static Item;
using static Mono.Security.X509.X520;



using System.Diagnostics;
using System.Linq;
using System.Numerics;

using System.Reflection.Emit;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using HarmonyLib.Tools;

using Photon.Realtime.Demo;
using Steamworks;

using Zorro.UI.Modal;
using Debug = UnityEngine.Debug;
using static Mono.Security.X509.X520;
using Loading;
using static Zorro.ControllerSupport.Rumble.RumbleClip;
using static UnityEngine.Rendering.STP;
using static Character;
using Photon.Voice.Unity;
using Sirenix.OdinInspector;
using static UnityEngine.UI.CanvasScaler;
using static UnityEngine.Rendering.DebugUI;
using Zorro.UI;
using Unity.Services.Lobbies.Models;

//using pworld.Scripts;
namespace _1v1.lol_cheat
{
    /* Baee Took From OUTSPECT Peak Source Lewis
     * CODED BY OUTSPECT AND FXZH
     * I am not going to put comments everywhere.
     * I might update this in the future.
     * 
    */






   


   

        [HarmonyPatch(typeof(MainCameraMovement), "LateUpdate")]
    class MainCameraMovement_LateUpdate_Patch
    {
        static void Postfix(MainCameraMovement __instance)
        {
            if (UnityEngine.Input.GetKeyDown(Cheat.toggleKeys))
            {
                Cheat.thirdPersonEnabled = !Cheat.thirdPersonEnabled;
            }

            if (Cheat.thirdPersonEnabled)
            {
                float scroll = UnityEngine.Input.mouseScrollDelta.y;
                if (Mathf.Abs(scroll) > 0.01f)
                {
                    Cheat.currentDistance = Mathf.Clamp(Cheat.currentDistance - scroll * Cheat.zoomSpeed, Cheat.minDistance, Cheat.maxDistance);
                }
            }
        }
    }


    [HarmonyPatch(typeof(RopeSpool), "set_RopeFuel")]
    public class SetRopeFuelPatch
    {
        // Token: 0x06000108 RID: 264 RVA: 0x00010DF4 File Offset: 0x0000EFF4
        private static bool Prefix(float value)
        {
            bool irope = GUI.IRope;
            return !irope;
        }
    }

    [HarmonyPatch(typeof(RopeSpool), "get_RopeFuel")]
    public class GetRopeFuelPatch
    {
        // Token: 0x06000106 RID: 262 RVA: 0x00010DC0 File Offset: 0x0000EFC0
        private static bool Prefix(ref float __result)
        {
            bool irope = GUI.IRope;
            bool result;
            if (irope)
            {
                __result = float.MaxValue;
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
    }

    [HarmonyPatch(typeof(RopeSpool), "get_IsOutOfRope")]
    public class OutOfRopePatch
    {
        // Token: 0x06000104 RID: 260 RVA: 0x00010D90 File Offset: 0x0000EF90
        private static bool Prefix(ref bool __result)
        {
            bool irope = GUI.IRope;
            bool result;
            if (irope)
            {
                __result = false;
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
    }

    [HarmonyPatch(typeof(RopeSpool), "DefaultFuel")]
    public class RopeFuelPatch
    {
        // Token: 0x0600010A RID: 266 RVA: 0x00010E20 File Offset: 0x0000F020
        private static bool Prefix(ref FloatItemData __result)
        {
            bool irope = GUI.IRope;
            bool result;
            if (irope)
            {
                __result = new FloatItemData
                {
                    Value = float.MaxValue
                };
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
    }


    [HarmonyPatch(typeof(MainCameraMovement), "CharacterCam")]
    class MainCameraMovement_CharacterCam_Patch
    {
        static bool Prefix(MainCameraMovement __instance)
        {
            if (!Cheat.thirdPersonEnabled || Character.localCharacter == null)
                return true; // fall back to original

            CharacterRefs refs = Character.localCharacter.refs;
            if (refs == null || refs.head == null)
                return true;

            Transform head = refs.head.transform;

            Vector3 lookDir = Character.localCharacter.data.lookDirection;
            if (lookDir == Vector3.zero) lookDir = head.forward;

            Vector3 desiredPos = head.position + Vector3.up * Cheat.height - lookDir.normalized * Cheat.currentDistance;
            Vector3 dir = (desiredPos - head.position).normalized;
            float dist = Vector3.Distance(desiredPos, head.position);

            RaycastHit hit;
            if (Physics.SphereCast(head.position, Cheat.clipRadius, dir, out hit, dist, LayerMask.GetMask("Default", "Terrain")))
            {
                desiredPos = hit.point - dir * Cheat.clipBuffer;
            }

            Transform camTransform = __instance.transform;

            camTransform.position = Vector3.Lerp(camTransform.position, desiredPos, Time.deltaTime * Cheat.lerpRate);
            camTransform.rotation = Quaternion.RotateTowards(
                camTransform.rotation,
                Quaternion.LookRotation(head.position - camTransform.position),
                Time.deltaTime * Cheat.turnSpeed
            );

            return false; // Skip original
        }
    }

        [HarmonyPatch(typeof(GameOverHandler), "BeginIslandLoadRPC")]
    public class SoftLockPatch
    {
        private static bool Prefix()
        {
            //UI.NewDebugNotif("Patched Character.rpca_fall");
            return !GUI.NoSL;
        }
    }


    [HarmonyPatch(typeof(Character), "RPCA_Fall")]
    public class FallPatch
    {
        private static bool Prefix()
        {
            //UI.NewDebugNotif("Patched Character.rpca_fall");
            return !GUI.NoR;
        }
    }
    [HarmonyPatch(typeof(Character), "RPCA_PassOut")]
    public class PassOutPatch
    {
        private static bool Prefix()
        {
           // UI.NewDebugNotif("Patched Character.rpca_passout");
            return !GUI.NoP;
        }
    }
    [HarmonyPatch(typeof(Character), "PassOut")]
    public class PassOutPatch1
    {
        private static bool Prefix()
        {
          //  UI.NewDebugNotif("Patched Character.passout");
            return !GUI.NoP;
        }
    }

    [HarmonyPatch(typeof(Character), "RPCA_Die")]
    public class DiePatch1
    {
        private static bool Prefix()
        {
            //UI.NewDebugNotif("Patched Character.rpca_die");
            return !GUI.NoD;
        }
    }


    [HarmonyPatch(typeof(Character), "Die")]
    public class DiePatch2
    {
        private static bool Prefix()
        {
            //UI.NewDebugNotif("Patched Character.die");
            return !GUI.NoD;
        }
    }


    [HarmonyPatch(typeof(SlipperyJellyfish), "Trigger")]
    public class SlipPatch1
    {
        private static bool Prefix()
        {
           // UI.NewDebugNotif("Patched SlipperyJellyfish.trigger");
            return !GUI.NoS;
        }
    }


    [HarmonyPatch(typeof(BananaPeel), "RPCA_TriggerBanana")]
    public class SlipPatch2
    {
        private static bool Prefix()
        {
           // UI.NewDebugNotif("Patched BananaPeel.rpca_triggerbanana");
            return !GUI.NoS;
        }
    }


    [HarmonyPatch(typeof(Item), "Consume")]
    public class ConsumePatch
    {
        private static bool Prefix(Item __instance)
        {
            if (GUI.AntiConsume && ((MonoBehaviourPun)__instance.holderCharacter).photonView.IsMine)
            {
                return false;
            }
            return true;
        }
    }


    [HarmonyPatch(typeof(Action_ReduceUses), "ReduceUsesRPC")]
    public class ReduceUsesPatch
    {
        private static bool Prefix()
        {
            if (GUI.ICharge)
            {
                return false;
            }
            if (GUI.AntiConsume)
            {
                return false;
            }
            return true;
        }
    }





    [HarmonyPatch(typeof(MagicBugle), "StartToot")]
    public class BuglePatch
    {
        private static bool Prefix(MagicBugle __instance)
        {
            if (GUI.ICharge)
            {
                Debug.Log((object)"Started toot infmod");
                FieldInfo field = ((object)__instance).GetType().GetField("tooting", BindingFlags.Instance | BindingFlags.NonPublic);
                field.SetValue(((object)__instance).GetType(), true);
                return false;
            }
            return true;
        }
    }
    [HarmonyPatch(typeof(Lantern), "UpdateFuel")]
    public class UpdateFuelPatch
    {
        private static bool Prefix(Lantern __instance)
        {
            return !GUI.ICharge;
        }
    }


    [HarmonyPatch(typeof(Item), "ContinueUsePrimary")]
    public class CUsePrimaryPatch
    {
        private static bool Prefix(Item __instance)
        {
            if (!GUI.InstantItemUse)
                return true;

            if (__instance.isUsingSecondary)
                __instance.CancelUseSecondary();

            if (__instance.isUsingPrimary && !__instance.finishedCast)
            {
                var feedback = __instance.GetComponent<ItemUseFeedback>();
                if (feedback != null)
                {
                    // Stop animation
                    var animator = __instance.holderCharacter?.refs?.animator;
                    if (animator != null && !string.IsNullOrEmpty(feedback.useAnimation))
                        animator.SetBool(feedback.useAnimation, false);

                    // Play SFX
                    if (feedback.sfxUsed != null)
                        feedback.sfxUsed.Play(__instance.transform.position);
                }

                __instance.finishedCast = true;
                __instance.lastFinishedCast = Time.time;

                __instance.OnPrimaryFinishedCast?.Invoke();
            }

            __instance.OnPrimaryHeld?.Invoke();

            return false; // Skip original method
        }
    }

    [HarmonyPatch(typeof(Item), "ContinueUseSecondary")]
    public class CUseSecondaryPatch
    {
        private static bool Prefix(Item __instance)
        {
            if (GUI.InstantItemUse)
            {
                if (__instance.isUsingPrimary)
                {
                    return false;
                }
                if (__instance.isUsingSecondary)
                {
                    if (__instance.OnSecondaryHeld != null)
                    {
                        __instance.OnSecondaryHeld();
                    }
                    if (!__instance.finishedCast)
                    {
                        __instance.FinishCastSecondary();
                        return false;
                    }
                }
                return false;
            }
            return true;
        }
    }


    [HarmonyPatch(typeof(SteamLobbyHandler), "OnLobbyEnter")]
    public class OnLobbyEnter_Patch
    {
        static bool Prefix(SteamLobbyHandler __instance, LobbyEnter_t param)
        {
            FieldInfo m_isHostingField = typeof(SteamLobbyHandler).GetField("m_isHosting", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo m_currentLobby = typeof(SteamLobbyHandler).GetField("m_currentLobby", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo m_tryingToFetchLobbyDataAttempts = typeof(SteamLobbyHandler).GetField("tryingToFetchLobbyDataAttempts", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo m_currentlyWaitingForRoomID = typeof(SteamLobbyHandler).GetField("m_currentlyWaitingForRoomID", BindingFlags.NonPublic | BindingFlags.Instance);

            MethodInfo LeaveLobbyMethod = typeof(SteamLobbyHandler).GetMethod("LeaveLobby", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            MethodInfo JoinLobbyMethod = typeof(SteamLobbyHandler).GetMethod("JoinLobby", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);


            bool m_isHosting = (bool)m_isHostingField.GetValue(__instance);

            if (m_isHosting)
            {
                m_isHostingField.SetValue(__instance, false);
                return false;
            }
            if (param.m_EChatRoomEnterResponse != 1U)
            {
                // Plugin.ShowNotification("Trying to join ...");
            }

            m_currentLobby.SetValue(__instance, new CSteamID(param.m_ulSteamIDLobby));

            string lobbyData = SteamMatchmaking.GetLobbyData((CSteamID)m_currentLobby.GetValue(__instance), "PhotonRegion");
            string lobbyData2 = SteamMatchmaking.GetLobbyData((CSteamID)m_currentLobby.GetValue(__instance), "CurrentScene");


            if (!string.IsNullOrEmpty(lobbyData))
            {
                m_tryingToFetchLobbyDataAttempts.SetValue(__instance, Optionable<int>.None);
                m_currentlyWaitingForRoomID.SetValue(__instance, Optionable<ValueTuple<CSteamID, string, string>>.Some(new ValueTuple<CSteamID, string, string>((CSteamID)m_currentLobby.GetValue(__instance), lobbyData2, lobbyData)));
                return false;
            }
            if (((Optionable<int>)m_tryingToFetchLobbyDataAttempts.GetValue(__instance)).IsNone)
            {
                m_tryingToFetchLobbyDataAttempts.SetValue(__instance, Optionable<int>.Some(1));
            }
            else
            {
                m_tryingToFetchLobbyDataAttempts.SetValue(__instance, Optionable<int>.Some(((Optionable<int>)m_tryingToFetchLobbyDataAttempts.GetValue(__instance)).Value + 1));
                // Plugin.ShowNotification("Trying to fetch lobby data again ..." + ((Optionable<int>)m_tryingToFetchLobbyDataAttempts.GetValue(__instance)).Value);
            }
            if (((Optionable<int>)m_tryingToFetchLobbyDataAttempts.GetValue(__instance)).Value < 5)
            {
                LeaveLobbyMethod.Invoke(__instance, null);
                JoinLobbyMethod.Invoke(__instance, new object[] { (CSteamID)param.m_ulSteamIDLobby });
                return false;
            }
            LeaveLobbyMethod.Invoke(__instance, null);
            Modal.OpenModal(new DefaultHeaderModalOption("Joining failed", "Sadly my shit didn't work, something went wrong, unlucky!"), new ModalButtonsOption(new ModalButtonsOption.Option[]
            {
        new ModalButtonsOption.Option("God dammit", null)
            }), null);

            return false;
        }
    }

    public class PlayerCallbackHandler : MonoBehaviourPunCallbacks
    {



        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            if (((Dictionary<object, object>)(object)newPlayer.CustomProperties).ContainsKey((object)"CherryUser"))
            {
                Cheat.SendOnce("<color=#8765ca>[Hyro] An Cherry user joined.</color>");
            }
            if (((Dictionary<object, object>)(object)newPlayer.CustomProperties).ContainsKey((object)"AtlUser"))
            {
                Cheat.SendOnce("<color=#8765ca>[Hyro] An Atlas user joined.</color>");
            }

            if (((Dictionary<object, object>)(object)newPlayer.CustomProperties).ContainsKey((object)"AtlOwner"))
            {
                Cheat.SendOnce("<color=#8765ca>[Hyro] An Atlas Owner joined.</color>");
            }
            if (((Dictionary<object, object>)(object)newPlayer.CustomProperties).ContainsKey((object)"CherryOwner"))
            {
                Cheat.SendOnce("<color=#8765ca>[Hyro] An Cherry Owner joined.</color>");
            }
        }


        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            string name = otherPlayer.NickName;

            if (otherPlayer.CustomProperties.ContainsKey("CherryUser"))
                Cheat.SendOnce($"<color=#ff69b4>[Hyro]</color> Cherry user <b>{name}</b> left.");
            else if (otherPlayer.CustomProperties.ContainsKey("AtlUser"))
                Cheat.SendOnce($"<color=#ff69b4>[Hyro]</color> Atlas user <b>{name}</b> left.");
            else if (otherPlayer.CustomProperties.ContainsKey("AtlOwner"))
                Cheat.SendOnce($"<color=#ff69b4>[Hyro]</color> Atlas Owner <b>{name}</b> left.");
            else if (otherPlayer.CustomProperties.ContainsKey("CherryOwner"))
                Cheat.SendOnce($"<color=#ff69b4>[Hyro]</color> Cherry Owner <b>{name}</b> left.");
            else
                Cheat.SendOnce($"<color=#ff69b4>[Hyro]</color> <b>{name}</b> left the lobby.");
        }

        public override void OnJoinedRoom()
        {
            foreach (Photon.Realtime.Player val in PhotonNetwork.PlayerListOthers)
            {
                string name = val.NickName;

                if (val.CustomProperties.ContainsKey("CherryUser"))
                    Cheat.SendOnce($"<color=#ff69b4>[Hyro]</color> Cherry user <b>{name}</b> is in the lobby.");
                else if (val.CustomProperties.ContainsKey("AtlUser"))
                    Cheat.SendOnce($"<color=#ff69b4>[Hyro]</color> Atlas user <b>{name}</b> is in the lobby.");
                else if (val.CustomProperties.ContainsKey("AtlOwner"))
                    Cheat.SendOnce($"<color=#ff69b4>[Hyro]</color> Atlas Owner <b>{name}</b> is in the lobby.");
                else if (val.CustomProperties.ContainsKey("CherryOwner"))
                    Cheat.SendOnce($"<color=#ff69b4>[Hyro]</color> Cherry Owner user <b>{name}</b> is in the lobby.");
                else
                    Cheat.SendOnce($"<color=#ff69b4>[Hyro]</color> <b>{name}</b> is in the lobby.");
            }
        }

        public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
        {
            Cheat.SendOnce($"<color=#ff69b4>[Hyro]</color> New master client: <b>{newMasterClient.NickName}</b>");
        }
    }



    public class SkeletonESP : MonoBehaviour
    {
        private struct BonePair
        {
            public HumanBodyBones start;
            public HumanBodyBones end;

            public BonePair(HumanBodyBones s, HumanBodyBones e)
            {
                start = s;
                end = e;
            }
        }

        private static readonly BonePair[] bonePairs = new BonePair[]
        {
        new BonePair(HumanBodyBones.Head, HumanBodyBones.Neck),
        new BonePair(HumanBodyBones.Neck, HumanBodyBones.UpperChest),
        new BonePair(HumanBodyBones.UpperChest, HumanBodyBones.Chest),
        new BonePair(HumanBodyBones.Chest, HumanBodyBones.Spine),
        new BonePair(HumanBodyBones.Spine, HumanBodyBones.Hips),
        new BonePair(HumanBodyBones.LeftShoulder, HumanBodyBones.LeftUpperArm),
        new BonePair(HumanBodyBones.LeftUpperArm, HumanBodyBones.LeftLowerArm),
        new BonePair(HumanBodyBones.LeftLowerArm, HumanBodyBones.LeftHand),
        new BonePair(HumanBodyBones.RightShoulder, HumanBodyBones.RightUpperArm),
        new BonePair(HumanBodyBones.RightUpperArm, HumanBodyBones.RightLowerArm),
        new BonePair(HumanBodyBones.RightLowerArm, HumanBodyBones.RightHand),
        new BonePair(HumanBodyBones.LeftUpperLeg, HumanBodyBones.LeftLowerLeg),
        new BonePair(HumanBodyBones.LeftLowerLeg, HumanBodyBones.LeftFoot),
        new BonePair(HumanBodyBones.LeftFoot, HumanBodyBones.LeftToes),
        new BonePair(HumanBodyBones.RightUpperLeg, HumanBodyBones.RightLowerLeg),
        new BonePair(HumanBodyBones.RightLowerLeg, HumanBodyBones.RightFoot),
        new BonePair(HumanBodyBones.RightFoot, HumanBodyBones.RightToes),
        new BonePair(HumanBodyBones.LeftEye, HumanBodyBones.Head),
        new BonePair(HumanBodyBones.RightEye, HumanBodyBones.Head),
        new BonePair(HumanBodyBones.Jaw, HumanBodyBones.Head)
        };



        public static void DrawSkeleton(Animator animator, Color color)
        {
            if (animator == null || animator.avatar == null || !animator.avatar.isHuman || Camera.main == null)
                return;

            foreach (var pair in bonePairs)
            {
                Transform start = animator.GetBoneTransform(pair.start);
                Transform end = animator.GetBoneTransform(pair.end);

                if (start == null || end == null)
                    continue;

                Vector3 startPos = Camera.main.WorldToScreenPoint(start.position);
                Vector3 endPos = Camera.main.WorldToScreenPoint(end.position);

                if (startPos.z < 0 || endPos.z < 0) continue;

                startPos.y = Screen.height - startPos.y;
                endPos.y = Screen.height - endPos.y;

                DrawLine(new Vector2(startPos.x, startPos.y), new Vector2(endPos.x, endPos.y), color);
            }
        }


        private static Texture2D lineTex;

        private static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width = 1f)
        {
            if (lineTex == null)
            {
                lineTex = new Texture2D(1, 1);
                lineTex.SetPixel(0, 0, Color.white);
                lineTex.Apply();
            }

            Matrix4x4 matrix = UnityEngine.GUI.matrix;
            Color savedColor = UnityEngine.GUI.color;

            float angle = Mathf.Atan2(pointB.y - pointA.y, pointB.x - pointA.x) * Mathf.Rad2Deg;
            float length = Vector2.Distance(pointA, pointB);

            UnityEngine.GUI.color = color;
            GUIUtility.RotateAroundPivot(angle, pointA);
            UnityEngine.GUI.DrawTexture(new Rect(pointA.x, pointA.y, length, width), lineTex);
            UnityEngine.GUI.matrix = matrix;
            UnityEngine.GUI.color = savedColor;
        }
    }

        public static class MyPluginInfo
    {
        public const string PLUGIN_GUID = "com.yourname.yourplugin";
        public const string PLUGIN_NAME = "Your Plugin Name";
        public const string PLUGIN_VERSION = "1.0.0";
    }

    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Cheat : MonoBehaviour
    {
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey); //input handling
        private static int _newMaxPlayers;
        //GUI Toggling (45 = insert)
        public int toggleKey = 45;
        public float toggleDelay = 0.5f;
        private bool toggled = true;
        private float lastToggleTime;


        private float spawnCooldown = 0.05f;

        private float spawnTimer = 0f;

        //Variable to keep targets
        public static List<Character> targets = new List<Character>();

        //material for esp and fov circle
        public static Material mat = new Material(Shader.Find("GUI/Text Shader"));
        public static string[] items;
        //watermark (now be happy this is open source lmao)
        private float colorChangeSpeed = 1f;
        private float timer = 0f;
        public static Dictionary<string, Character> playerDict = new Dictionary<string, Character>();

        public static string localPlayerName = "";

        public static bool sswsw = false;
        public static GameObject q = null;
        public static float cc = 0.5f;
        public static float ccc = 0.5f;


        public static bool teleportToPingEnabled = false;
        public static float teleportX = 0f;
        public static float teleportY = 0f;
        public static float teleportZ = 0f;

        public static int selectedLuggageIndex = -1;
        public static List<string> luggageLabels = new List<string>();
        public static List<Luggage> luggageObject = new List<Luggage>();
        public static List<Luggage> allOpenedLuggage = new List<Luggage>();

        public static bool thirdPersonEnabled = false;
        public static float currentDistance = 5.0f;
        public static float defaultDistance = 5.0f;
        public static float zoomSpeed = 1.5f;
        public static float minDistance = 2.0f;
        public static float maxDistance = 12.0f;

        public static float height = 1.7f;  // Camera height from head
        public static float clipRadius = 0.2f;
        public static float clipBuffer = 0.2f;
        public static float lerpRate = 10.0f;
        public static float turnSpeed = 360f;

        public static bool settingsInitialized = false;
        public static KeyCode toggleKeys = KeyCode.F7;

        public static GameObject atlas;
        public static GUIStyle tabStyle;

        private static Dictionary<Character, LineRenderer> lineRenderers = new Dictionary<Character, LineRenderer>();

        public static List<Character> allPlayers = new List<Character>();
        // public static List<Character> allPlayers = new List<Character>();
        public static List<string> playerNames = new List<string>();
        public static int selectedPlayer = -1;
        public static bool excludeSelfFromAllActions = true;


        public static List<Item> itemObjects = new List<Item>();
        public static bool setobjs = false;

        public static string yourName = "";
        private int i = 0;


        public static SteamLobbyHandler steamLobbyHandler = new SteamLobbyHandler();

        public static PhotonPeer Peer { get; set; }

       // public static PhotonPeer Peer; // also make this static if not already

        public static void DrawPhotonSimControls()
        {
            if (Peer == null)
            {
                GUILayout.Label("No peer to communicate with.");
                return;
            }

            GUILayout.Label($"Photon RTT: {Peer.RoundTripTime} (±{Peer.RoundTripTimeVariance})");

            bool simEnabled = Peer.IsSimulationEnabled;
            bool newSimEnabled = GUILayout.Toggle(simEnabled, "Enable Simulation");
            if (newSimEnabled != simEnabled)
                Peer.IsSimulationEnabled = newSimEnabled;

            float lag = Peer.NetworkSimulationSettings.IncomingLag;
            GUILayout.Label($"Lag: {lag}");
            lag = GUILayout.HorizontalSlider(lag, 0, 500);
            Peer.NetworkSimulationSettings.IncomingLag = (int)lag;
            Peer.NetworkSimulationSettings.OutgoingLag = (int)lag;

            float jitter = Peer.NetworkSimulationSettings.IncomingJitter;
            GUILayout.Label($"Jitter: {jitter}");
            jitter = GUILayout.HorizontalSlider(jitter, 0, 100);
            Peer.NetworkSimulationSettings.IncomingJitter = (int)jitter;
            Peer.NetworkSimulationSettings.OutgoingJitter = (int)jitter;

            float loss = Peer.NetworkSimulationSettings.IncomingLossPercentage;
            GUILayout.Label($"Loss: {loss}%");
            loss = GUILayout.HorizontalSlider(loss, 0, 10);
            Peer.NetworkSimulationSettings.IncomingLossPercentage = (int)loss;
            Peer.NetworkSimulationSettings.OutgoingLossPercentage = (int)loss;
        }

        private CSteamID spoofedSteamID;

                                                                       
      

        private void Start()
        {



           // SteamUser.GetSteamID();
           

            //Peer = PhotonNetwork.NetworkingClient.LoadBalancingPeer;

            //localPlayerName = PhotonNetwork.NickName;
            //SceneManager.sceneLoaded += (_, __) => RefreshPlayerDict();





            // SceneManager.sceneLoaded += OnSceneLoaded;
            // items = ItemDatabase.GetAllObjectNames();
            //StartCoroutine(UpdateTargets());
            // StartCoroutine(AutoRefreshItemList());
        }



        private void Awake()
        {

           // toggleKey = Config.Bind<KeyCode>("Camera", "ToggleKey", KeyCode.V, "Toggle third-person camera").Value;
            currentDistance = defaultDistance;

            var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

            harmony.PatchAll();

            Start();
        }


       


        private IEnumerator AutoRefreshItemList()
        {
            while (true)
            {
                RefreshItemList();
                yield return new WaitForSeconds(2f); // Update every second
            }
        }


        public static void OpenAllNearbyLuggage()
        {

            {
                int opened = 0;

                for (int i = 0; i < luggageObject.Count; i++)
                {
                    var luggage = luggageObject[i];
                    if (luggage == null) continue;

                    var view = luggage.GetComponent<PhotonView>();
                    if (view != null)
                    {
                        view.RPC("OpenLuggageRPC", RpcTarget.All, new object[] { true });
                        opened++;
                    }
                }


            }
        }

        public static void OpenLuggage(int index)
        {
            if (index < 0 || index >= luggageObject.Count)
                return;

            var luggage = luggageObject[index];
            if (luggage == null)
                return;


            {
                try
                {
                    PhotonView view = luggage.GetComponent<PhotonView>();
                    if (view != null)
                    {
                        view.RPC("OpenLuggageRPC", RpcTarget.All, new object[] { true });

                    }
                }
                catch (Exception ex)
                {

                }
            }
        }


        public static void TeleportToCoords(float x, float y, float z)
        {
            // UnityMainThreadDispatcher.Enqueue(() =>
            {
                try
                {
                    Character localCharacter = Character.localCharacter;
                    if (localCharacter == null || localCharacter.data.dead)
                    {

                        return;
                    }

                    PhotonView photonView = localCharacter.photonView;
                    if (photonView == null)
                        return;

                    Vector3 target = new Vector3(x, y, z);
                    photonView.RPC("WarpPlayerRPC", RpcTarget.All, new object[]
                    {
                target, true
                    });


                }
                catch (Exception ex)
                {

                }
            }
        }

        public static bool hasInitializedLuggageList = false;

        public static void EnsureLuggageListInitialized()
        {
            if (!hasInitializedLuggageList)
            {
                hasInitializedLuggageList = true;
                RefreshLuggageList();
            }
        }

        public static void RefreshLuggageList()
        {
            luggageLabels.Clear();
            luggageObject.Clear();
            selectedLuggageIndex = -1;

            var allLuggage = new List<(Luggage lug, float distance)>();

            foreach (var lug in Luggage.ALL_LUGGAGE)
            {
                if (lug == null) continue;

                float distance = Vector3.Distance(Character.localCharacter.Head, lug.Center());
                if (distance <= 300)
                {
                    allLuggage.Add((lug, distance));
                }
            }

           
            allLuggage.Sort((a, b) => a.distance.CompareTo(b.distance));

            foreach (var (lug, distance) in allLuggage)
            {
                string name = lug.displayName ?? "Unnamed";
                luggageLabels.Add($"{name} [{distance:F1}m]");
                luggageObject.Add(lug);
            }


        }




        public static void RefreshPlayerDict()
        {

            {
                try
                {
                    allPlayers.Clear();
                    playerNames.Clear();
                    selectedPlayer = -1;

                    foreach (var character in Character.AllCharacters)
                    {
                        //if (excludeSelfFromAllActions && character.NickName == PhotonNetwork.NickName)
                        //  continue;

                        allPlayers.Add(character);
                        playerNames.Add(character.characterName);
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }










        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {


           // RefreshPlayerDict();
            //GUI.RefreshPlayerDict();
            //InitializeItems();
        }


        public static void RefreshItemList()
        {
            itemObjects.Clear();

            foreach (var item in GameObject.FindObjectsOfType<Item>())
            {
                if (item == null) continue;

                float distance = Vector3.Distance(Character.localCharacter.Head, item.transform.position);
                if (distance <= 300)
                {
                    itemObjects.Add(item);
                }
            }

            Debug.Log($"[ESP] Refreshed item list. Found {itemObjects.Count} items in range.");
        }



        private void InitializeItems()
        {
            if (SceneManager.GetActiveScene().name == "GameScene")
            {
                items = ItemDatabase.GetAllObjectNames();
            }
        }

        private IEnumerator UpdateTargets()
        {
            while (true)
            {
                targets = Utils.GetTargets();
                yield return new WaitForSeconds(1f);
            }
        }

        private Texture2D buttonTex;
        private GUIStyle buttonStyle;

       
        private Texture2D buttonHoverTex;

        private Texture2D buttonActiveTex;
        private Texture2D dividerTex;
        public static Color buttonColor = new Color(0.5f, 0.7f, 3f);
        private Texture2D transparentTex;
        public Font menuFont = null;

        private void InitTextures()
        {
           
            buttonTex = MakeTex(buttonColor * 4f);
            buttonHoverTex = MakeTex(buttonColor * 3f);
            buttonActiveTex = MakeTex(buttonColor * 2f);
            dividerTex = MakeTex(new Color(0.15f, 0.15f, 0.15f));
            transparentTex = MakeTex(new Color(0f, 0f, 0f, 0f));
        }

        private Texture2D MakeTex(Color col)
        {
           
            Texture2D val = new Texture2D(1, 1);
            val.SetPixel(0, 0, col);
            val.Apply();
            return val;
        }


        private GUIStyle windowStyle;
        
        private void InitStyles()
        {
            if (menuFont == null)
            {
                menuFont = Font.CreateDynamicFontFromOSFont("Calibri", 16);
            }

            // Define core colors
            Color purple = new Color(0.5f, 0.0f, 0.5f, 1.0f);
            Color darkPurple = new Color(0.3f, 0.0f, 0.3f, 1.0f);
            Color transparent = new Color(0f, 0f, 0f, 0f);
            Color black = new Color(0.05f, 0.05f, 0.05f, 1.0f);
            Color semiTransparentBlack = new Color(0f, 0f, 0f, 0.5f);

            // BUTTON STYLE
            GUIStyle val = new GUIStyle(UnityEngine.GUI.skin.button);
            val.fontSize = 14;
            val.alignment = TextAnchor.MiddleCenter;
            val.padding = new RectOffset(8, 8, 4, 4);
            val.font = menuFont;

            val.normal.background = MakeTex(black);
            val.hover.background = MakeTex(purple);
            val.active.background = MakeTex(darkPurple);
            val.onNormal.background = MakeTex(darkPurple);
            val.onHover.background = MakeTex(purple);
            val.onActive.background = MakeTex(purple);

            val.normal.textColor = Color.white;
            val.hover.textColor = Color.white;
            val.active.textColor = Color.white;
            val.onNormal.textColor = Color.white;
            val.onHover.textColor = Color.white;
            val.onActive.textColor = Color.white;

            buttonStyle = val;

            // TAB STYLE (clone of buttonStyle with tweaks)
            tabStyle = new GUIStyle(buttonStyle)
            {
                fontSize = 13,
                fixedHeight = 30f,
                font = menuFont
            };

            // WINDOW STYLE
            GUIStyle val2 = new GUIStyle(UnityEngine.GUI.skin.window);
            val2.border = new RectOffset(0, 0, 0, 0);
            val2.font = menuFont;

            val2.normal.background = MakeTex(semiTransparentBlack);
            val2.hover.background = MakeTex(semiTransparentBlack);
            val2.active.background = MakeTex(semiTransparentBlack);
            val2.onNormal.background = MakeTex(semiTransparentBlack);
            val2.onHover.background = MakeTex(semiTransparentBlack);
            val2.onActive.background = MakeTex(semiTransparentBlack);

            val2.normal.textColor = Color.white;
            val2.hover.textColor = Color.white;
            val2.active.textColor = Color.white;
            val2.onNormal.textColor = Color.white;
            val2.onHover.textColor = Color.white;
            val2.onActive.textColor = Color.white;

            windowStyle = val2;
        }

        private bool texturesInitialized = false;
        private float themeLerpT = 1f;
        private bool stylesInitialized = false;

        private float animT = 0f;
        private void OnGUI()
        {
          
                //InitTextures();
                //texturesInitialized = true;
            
                //InitStyles();
                //stylesInitialized = true;
           

            if (UnityEngine.Input.GetKeyDown(KeyCode.F2))
            {
                isCursorOn = !isCursorOn;
            }

            // Cursor management:
            if (toggled || isCursorOn)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {

            }

            float r = Mathf.PingPong(timer * colorChangeSpeed, 1f);
            float g = Mathf.PingPong(timer * colorChangeSpeed + 0.33f, 1f);
            float b = Mathf.PingPong(timer * colorChangeSpeed + 0.66f, 1f);

            UnityEngine.GUI.color = new Color(r, g, b);

            // UnityEngine.GUI.Label(new Rect(10, 10, 400, 40), "Peak");

            UnityEngine.GUI.color = Color.white;

            timer += Time.deltaTime;








            if (GUI.PlayerTags)
            {
                Vector2 val29 = default(Vector2);
                foreach (Character allCharacter3 in Character.AllCharacters)
                {
                    if (((MonoBehaviourPun)allCharacter3).photonView.IsMine)
                    {
                        continue;
                    }
                    Vector3 head = allCharacter3.Head;
                    head.y += 0.5f;
                    Vector3 val25 = ToScreen(head);
                    if (IsBehindCamera(val25))
                    {
                        continue;
                    }
                    string text;
                    if (((Dictionary<object, object>)(object)allCharacter3.refs.view.Owner.CustomProperties).ContainsKey((object)"AtlUser"))
                    {
                        text = "Name [" + allCharacter3.refs.view.Owner.NickName + "] [Atlas]";
                    }
                    else
                    {
                        text = "Name [" + allCharacter3.refs.view.Owner.NickName + "]";
                    }
                    text = ((!((Dictionary<object, object>)(object)allCharacter3.refs.view.Owner.CustomProperties).ContainsKey((object)"CherryUser")) ? ("Name [" + allCharacter3.refs.view.Owner.NickName + "]") : ("Name [" + allCharacter3.refs.view.Owner.NickName + "] <color=#c53637>[Cherry]</color>"));

                    Vector3 val26 = Character.localCharacter.Head - allCharacter3.Head;
                    string text2 = "Distance [" + Math.Round(val26.magnitude, 1) + "]";

                    GUIStyle val27 = new GUIStyle
                    {
                        fontSize = 10
                    };
                    GUIStyle val28 = new GUIStyle
                    {
                        fontSize = 10
                    };

                    float num8 = 0f;
                    int num9 = 0;
                    if (num9 % 2 == 0)
                    {
                        num8 += 0.01f;
                    }
                    num9++;
                    val27.normal.textColor = Color.white;
                    val28.normal.textColor = GetRainbow(num8);

                    Vector2[] array11 = new Vector2[2]
                    {
            val27.CalcSize(new GUIContent(text)),
            val27.CalcSize(new GUIContent(text2))
                    };
                    val29 = Vector2.zero;
                    foreach (Vector2 val30 in array11)
                    {
                        if (val30.x > val29.x)
                        {
                            val29.x = val30.x;
                        }
                        if (val30.y > val29.y)
                        {
                            val29.y = val30.y;
                        }
                    }

                    float x = val25.x;
                    float num11 = val25.y + (val29.y + 5f);
                    float width = val29.x + 10f;
                    float height = val29.y * array11.Length + 5f;

                    DrawBox(new Color(0.15f, 0.15f, 0.15f), new Vector2(x, num11), width, height);

                    int num12 = 1;
                    if (((Dictionary<object, object>)(object)allCharacter3.refs.view.Owner.CustomProperties).ContainsKey((object)"AtlUser"))
                    {
                        UnityEngine.GUI.Label(new Rect(val25.x - val29.x / 2f, val25.y + val29.y / 2f * num12, val29.x, val29.y * 1.5f), text, val28);
                        num12 += 2;
                        UnityEngine.GUI.Label(new Rect(val25.x - val29.x / 2f, val25.y + val29.y / 2f * num12, val29.x, val29.y * 1.5f), text2, val27);
                    }
                    else
                    {
                        UnityEngine.GUI.Label(new Rect(val25.x - val29.x / 2f, val25.y + val29.y / 2f * num12, val29.x, val29.y * 1.5f), text, val27);
                        num12 += 2;
                        UnityEngine.GUI.Label(new Rect(val25.x - val29.x / 2f, val25.y + val29.y / 2f * num12, val29.x, val29.y * 1.5f), text2, val27);
                    }


                    //Animator animator = allCharacter3.GetComponentInChildren<Animator>();
                    //if (animator != null)
                    //{
                    //    SkeletonESP.DrawSkeleton(animator, Color.cyan); 
                    //}

                }



            }






            if (GUI.showWatermark)
            {
                GUI.DrawWatermark();
            }








            if (GUI.Tracers)
            {
                foreach (Character allCharacter2 in Character.AllCharacters)
                {
                    if (((MonoBehaviourPun)allCharacter2).photonView.IsMine)
                    {
                        continue;
                    }
                    Vector3 val24 = ToScreen(allCharacter2.Head);
                    if (!IsBehindCamera(val24))
                    {
                        float num6 = 0f;
                        int num7 = 0;
                        if (num7 % 2 == 0)
                        {
                            num6 += 0.01f;
                        }
                        num7++;
                        DrawLine(GetRainbow(num6), new Vector3((float)(Screen.width / 2), (float)Screen.height), val24);
                    }
                }
            }







            if (GUI.boxesp)
            {
                foreach (Character character in Character.AllCharacters)
                {
                    if (character == null || character.refs.mainRenderer == null) continue;

                    Bounds bounds = character.refs.mainRenderer.bounds;
                    Vector3 center = bounds.center;
                    Vector3 extents = bounds.extents;

                    Vector3[] corners = new Vector3[8]
                    {
            center + new Vector3(-extents.x, -extents.y, -extents.z),
            center + new Vector3(extents.x, -extents.y, -extents.z),
            center + new Vector3(-extents.x, -extents.y, extents.z),
            center + new Vector3(extents.x, -extents.y, extents.z),
            center + new Vector3(-extents.x, extents.y, -extents.z),
            center + new Vector3(extents.x, extents.y, -extents.z),
            center + new Vector3(-extents.x, extents.y, extents.z),
            center + new Vector3(extents.x, extents.y, extents.z)
                    };

                    Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
                    Vector2 max = new Vector2(float.MinValue, float.MinValue);

                    bool isVisible = true;
                    foreach (Vector3 corner in corners)
                    {
                        Vector3 screenPoint = Camera.main.WorldToScreenPoint(corner);
                        if (screenPoint.z < 0f)
                        {
                            isVisible = false;
                            break;
                        }

                        Vector2 screenPos = new Vector2(screenPoint.x, Screen.height - screenPoint.y);
                        min = Vector2.Min(min, screenPos);
                        max = Vector2.Max(max, screenPos);
                    }

                    if (isVisible)
                    {
                        float width = max.x - min.x;
                        float height = max.y - min.y;

                        Color color = character.refs.customization.PlayerColor;


                        RectFilled(min.x, min.y, width, 1f, color);
                        RectFilled(min.x, min.y, 1f, height, color);
                        RectFilled(min.x + width, min.y, 1f, height, color);
                        RectFilled(min.x, min.y + height, width, 1f, color);
                    }
                }
            }


            if (GUI.fovcircle)
            {
                FOVCircle((int)GUI.fov);
            }

            if (GUI.LuggageEsp)
            {
                foreach (Luggage lug in luggageObject)
                {
                    if (lug == null) continue;

                    Vector3 worldPos = lug.transform.position + Vector3.up * 2.0f;
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

                    if (screenPos.z > 0)
                    {
                        string name = lug.displayName ?? "Unnamed";
                        float distance = Vector3.Distance(Character.localCharacter.Head, lug.Center());
                        string label = $"{name} [{distance:F1}m]";

                        GUIStyle boldStyle = new GUIStyle(UnityEngine.GUI.skin.label)
                        {
                            fontStyle = FontStyle.Bold,
                            normal = { textColor = Color.yellow },
                            alignment = TextAnchor.UpperCenter,
                            richText = false
                        };

                        Vector2 size = boldStyle.CalcSize(new GUIContent(label));
                        Rect labelRect = new Rect(
                            screenPos.x - size.x / 2f,
                            Screen.height - screenPos.y - size.y,
                            size.x,
                            size.y
                        );

                        UnityEngine.GUI.Label(labelRect, label, boldStyle);
                    }
                }
            }








            DrawItemESP();






            if (toggled)
            {




              
                //Cursor.lockState = CursorLockMode.None;
                //Cursor.visible = true;

                GUI.GUIRect = UnityEngine.GUI.Window(69, GUI.GUIRect, GUI.GUIMain, "Hryo Peak by Lewis| FPS: " + 1.0f / Time.deltaTime + " | Toggle: INSERT"); //windowStyle
            }
            else
            {
               
                // Cursor.lockState = CursorLockMode.Locked;
                //Cursor.visible = true;
            }
        }

        private void GUIToggleCheck()
        {
            if (GetAsyncKeyState(toggleKey) < 0)
            {
                if (Time.time - lastToggleTime >= toggleDelay)
                {
                    toggled = !toggled;
                    lastToggleTime = Time.time;
                }
            }
        }

        private void FOVCircle(int radius)
        {
            mat.SetPass(0);
            GL.PushMatrix();
            GL.LoadPixelMatrix(0, Screen.width, Screen.height, 0);
            GL.Begin(2);
            GL.Color(Color.yellow);

            for (int i = 0; i <= 360; i++)
            {
                float angle = i * (2f * (float)Math.PI / 360f);
                float x = Mathf.Cos(angle) * radius + Screen.width / 2;
                float y = Mathf.Sin(angle) * radius + Screen.height / 2;
                GL.Vertex(new Vector3(x, y, 0));
            }

            GL.End();
            GL.PopMatrix();
        }

        private static Item[] cachedItems;
        private static float lastItemScanTime;
        private static float itemScanInterval = 3f;

        private static void UpdateCachedItems()
        {
            if (Time.time - lastItemScanTime > itemScanInterval)
            {
                cachedItems = UnityEngine.Object.FindObjectsOfType<Item>();
                lastItemScanTime = Time.time;
            }
        }

        private static GUIStyle boldStyle;

        public static void DrawItemESP()
        {
            if (!GUI.ItemEsp) return;

            UpdateCachedItems();

            if (boldStyle == null)
            {
                boldStyle = new GUIStyle(UnityEngine.GUI.skin.label);
                boldStyle.fontStyle = FontStyle.Bold;
                boldStyle.normal.textColor = Color.cyan;
            }

            foreach (Item item in cachedItems)
            {
                if (item == null) continue;

                Vector3 worldPos = item.transform.position + Vector3.up * 0.5f;
                Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

                if (screenPos.z <= 0) continue;

                float distance = Vector3.Distance(Character.localCharacter.Head, item.transform.position);
                if (distance > 200f) continue;

                // Remove "(Clone)" from the item name
                string cleanName = item.name.Replace("(Clone)", "").Trim();

                string label = $"{cleanName} [{distance:F1}m]";

                Vector2 labelSize = boldStyle.CalcSize(new GUIContent(label));
                Rect labelRect = new Rect(
                    screenPos.x - labelSize.x / 2f,
                    Screen.height - screenPos.y - labelSize.y,
                    labelSize.x, labelSize.y
                );

                UnityEngine.GUI.Label(labelRect, label, boldStyle);
            }
        }
















        private static void BoxESPItems(bool enable)
        {
            if (enable)
            {
                if (!PhotonNetwork.InRoom) return;

                foreach (Luggage sss in luggageObject)
                {
                    Vector3 first = sss.transform.position;
                    Vector3 start = new Vector3(first.x, first.y + 1.75f, first.z);
                    Vector3 end = new Vector3(first.x, first.y, first.z);

                    //LineRenderer lineRenderer;
                    //if (!sss.gameObject.GetComponent<LineRenderer>())
                    //    lineRenderer = sss.gameObject.AddComponent<LineRenderer>();
                    //else
                    //    lineRenderer = sss.gameObject.GetComponent<LineRenderer>();

                    //Material material = new Material(Shader.Find("GUI/Text Shader"));
                    //material.color = new UnityEngine.Color(1f, 1f, 1f, 0.75f);

                    //lineRenderer.positionCount = 2;
                    //lineRenderer.SetPosition(0, start);
                    //lineRenderer.SetPosition(1, end);
                    //lineRenderer.material = material;
                    //lineRenderer.enabled = true;

                    float hue = Time.time * 0.5f;
                    UnityEngine.Color headColor = UnityEngine.Color.HSVToRGB(hue % 1f, 1f, 1f);
                    //lineRenderer.startColor = new UnityEngine.Color(headColor.r, headColor.g, headColor.b, 0.75f);
                    // lineRenderer.endColor = new UnityEngine.Color(headColor.r, headColor.g, headColor.b, 0.75f);

                    GUI.boxfix = true;
                }
            }
            else
            {
                if (GUI.boxfix)
                {
                    foreach (Luggage sss in luggageObject)
                    {
                        UnityEngine.Object.Destroy(sss.gameObject.GetComponent<LineRenderer>());
                    }
                    GUI.boxfix = false;
                }
            }
        }



        private static void Crasher()
        {
            int VK_MBUTTON = 4;
            short state = GetAsyncKeyState(VK_MBUTTON);
            if (GUI.crasher && (state & 0x8001) != 0)
            {
                float closest = float.PositiveInfinity;
                foreach (Character ctrl in targets)
                {




                    Vector3 w2s = Camera.main.WorldToScreenPoint(ctrl.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.position);
                    // Vector3 w2s = CameraManager.Instance.MainCamera.WorldToScreenPoint(ctrl.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.position);
                    float abs = Vector2.Distance(new Vector2(w2s.x, Screen.height - w2s.y), new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f));
                    if (ctrl.photonView.IsMine == false && !ctrl.IsLocal && w2s.z >= 0f && abs <= GUI.fov && abs < closest)
                    {
                        closest = abs;
                        PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer); 
                        PhotonNetwork.DestroyPlayerObjects(ctrl.photonView.Controller);
                    }
                }
            }
        }









        private List<string> spawnableItems = new List<string>
{
    "ropeshooteranti",
    "anti-rope spool",
    "bugle_magic",
    "Cure-all",
    "Lantern_faerie",
    "pandorasbox",
    "scouteffigy",
    "bugle_scoutmaster variant"

};
        private int currentItemIndex = 0;
        private bool hasSpawned = false;
        private bool lastToggledState = false;

        private bool isMainScene = false;



        public void ClearAllStatus(bool excludeCurse = true)
        {
            int num = Enum.GetNames(typeof(CharacterAfflictions.STATUSTYPE)).Length;

            for (int i = 0; i < num; i++)
            {
                CharacterAfflictions.STATUSTYPE statustype = (CharacterAfflictions.STATUSTYPE)i;

                // Exclude specific statuses
                if (statustype != CharacterAfflictions.STATUSTYPE.Weight &&
                    (!excludeCurse || statustype != CharacterAfflictions.STATUSTYPE.Curse) &&
                    statustype != CharacterAfflictions.STATUSTYPE.Crab)
                {
                    float currentAmount = Character.localCharacter.refs.afflictions.GetCurrentStatus(statustype);

                    // Only clear if it's actually applied
                    if (currentAmount > 0f)
                    {
                        Character.localCharacter.refs.afflictions.SetStatus(statustype, 0f);

                        // Optional: remove or wrap this in a debug flag
                        // Debug.Log($"Cleared status: {statustype} (was {currentAmount})");
                    }
                }
            }
        }





        public static float laserRange = 100f;
        public static Item GrabbedItem;
        public static void GravityGun() // credits to synq. for this
        {

            if (UnityEngine.Input.GetKey(KeyCode.F))
            {


                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, laserRange))
                {
                    Debug.Log("ray hit: " + hit.collider.name);

                    Item itm = hit.collider.GetComponentInParent<Item>();
                    if (itm != null && itm.photonView != null)
                    {
                        Debug.Log("im nut");
                        GrabbedItem = itm;
                        itm.photonView.RPC("SetKinematicRPC", RpcTarget.All, new object[]
                        {
                true,
                Camera.main.transform.position + Camera.main.transform.forward * 6f,
                Quaternion.identity
                        });
                    }
                    else
                    {
                        Debug.Log("no itm or pv");
                    }
                }
                else
                {
                    Debug.Log("hit nothing");
                }

                if (GrabbedItem != null)
                {
                    GrabbedItem.photonView.RPC("SetKinematicRPC", RpcTarget.All, new object[]
                    {
            true,
            Camera.main.transform.position + Camera.main.transform.forward * 6f,
            Quaternion.identity
                    });
                }
            }
        }






        private static CharacterCustomization aa;
        public static float cooldown = 0.07f;
        public static void RandomCosmeticstest()
        {
            if (Time.time > cooldown)
            {
                if (aa == null && Character.localCharacter != null)
                {
                    aa = Character.localCharacter.GetComponent<CharacterCustomization>();
                }

                aa?.RandomizeCosmetics();
                cooldown = Time.time + 0.07f;
            }
        }


        private static bool isFlying = false;

        private static Vector3 flyVelocity = Vector3.zero;

        public static float flySpeed = 58f;

        private static float acceleration = 200f;

        private static void Postfix(Character __instance)
        {
            if (!__instance.IsLocal || __instance.refs?.ragdoll == null || !isFlying)
                return;

            // Prevent the player from being considered grounded so gravity doesn't kick in
            __instance.data.isGrounded = true;
          //  __instance.data.sinceGrounded = 0f;
            __instance.data.sinceJump = 0f;

            // Movement logic
            Vector3 input = __instance.input.movementInput;
            Vector3 forward = __instance.data.lookDirection_Flat.normalized;
            Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;

            Vector3 direction = forward * input.y + right * input.x;

            if (__instance.input.jumpIsPressed)
                direction += Vector3.up;
            if (__instance.input.crouchIsPressed)
                direction += Vector3.down;

            flyVelocity = Vector3.Lerp(flyVelocity, direction.normalized * flySpeed, Time.deltaTime * acceleration);

            foreach (Bodypart part in __instance.refs.ragdoll.partList)
            {
                if (part?.Rig != null)
                {
                    part.Rig.linearVelocity = flyVelocity;
                }
            }
        }






        private void HandleFlying()
        {
            if (!isFlying || Character.localCharacter == null || Character.localCharacter.refs?.ragdoll == null)
                return;

            var character = Character.localCharacter;

            character.data.isGrounded = true;
           // character.data.sinceGrounded = 0f;
            character.data.sinceJump = 0f;

            Vector3 input = character.input.movementInput;
            Vector3 forward = character.data.lookDirection_Flat.normalized;
            Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;

            Vector3 direction = forward * input.y + right * input.x;

            if (character.input.jumpIsPressed)
                direction += Vector3.up;
            if (character.input.crouchIsPressed)
                direction += Vector3.down;

            // Modify this section to increase speed when Shift is held
            float currentFlySpeed = flySpeed;

            // --- Option 1: Using Unity's Input system ---
            if (UnityEngine.Input.GetKey(KeyCode.LeftShift))
                currentFlySpeed *= 2f;


            
           
            //if (character.input.sprintIsPressed) 
            //    currentFlySpeed *= 2f;

            flyVelocity = Vector3.Lerp(flyVelocity, direction.normalized * currentFlySpeed, Time.deltaTime * acceleration);

            foreach (Bodypart part in character.refs.ragdoll.partList)
            {
                if (part?.Rig != null)
                {
                    part.Rig.useGravity = false;
                    part.Rig.velocity = flyVelocity;

                }
            }
        }






        public static void BotSpam()
        {
            var in1 = PhotonNetwork.Instantiate("Character", Camera.main.transform.position, Camera.main.transform.rotation);
            var in2 = PhotonNetwork.Instantiate("Player", Camera.main.transform.position, Camera.main.transform.rotation);
            if (in1 && in2 != null)
            {
                in1.GetComponent<Character>().enabled = false;
                in1.GetComponent<CharacterMovement>().enabled = false;
            }
        }




        public static void BingBongSpam()
        {
            PhotonNetwork.Instantiate("0_Items/BingBong", Camera.main.transform.position, Camera.main.transform.rotation);
        }

        public static void PassportSpam()
        {
            PhotonNetwork.Instantiate("0_Items/Passport", Camera.main.transform.position, Camera.main.transform.rotation);
        }


        public static void ScoutMaster()
        {
            PhotonNetwork.Instantiate("Character_Scoutmaster", Camera.main.transform.position, Camera.main.transform.rotation);
        }









        public static void TestTing()
        {
            if (!sswsw && q == null)
            {
                q = PhotonNetwork.Instantiate("0_Items/Flag_Plantable_Seagull", Vector3.zero, Quaternion.identity);
                sswsw = true;
            }
            if (q == null) return;
            if (UnityEngine.Input.GetKey(KeyCode.H))
            {
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, 50f))
                {
                    if (Time.time >= cc)
                    {
                        Constructable e = q.GetComponent<Constructable>();
                        if (e != null && e.photonView != null)
                        {
                            e.photonView.RPC("CreatePrefabRPC", RpcTarget.All, new object[]
                            {
                                hit.point,
                                Quaternion.identity
                            });
                        }

                        cc = Time.time + ccc;
                    }
                }
            }
        }

      


        private static IEnumerator SetScoutTargetDelayed(GameObject scoutObj, Character target)
        {
            yield return new WaitForSeconds(0.25f); 

            var scoutmaster = scoutObj.GetComponent<Scoutmaster>();
            if (scoutmaster != null)
            {
                var method = typeof(Scoutmaster).GetMethod("SetCurrentTarget", BindingFlags.Instance | BindingFlags.NonPublic);
                if (method != null)
                {
                    method.Invoke(scoutmaster, new object[] { target, 15f });
                }
            }
        }


        public static void SpawnScoutmasterForCharacter(Character targetCharacter)
        {
            if (!PhotonNetwork.IsMasterClient)
                return;

            if (targetCharacter == null)
                return;

            Vector3 targetPos = targetCharacter.Center;
            Vector3 spawnOrigin = targetPos + new Vector3(UnityEngine.Random.Range(-10f, 10f), 25f, UnityEngine.Random.Range(-10f, 10f));
            Vector3 down = Vector3.down;

            if (Physics.Raycast(spawnOrigin, down, out RaycastHit hit, 100f, ~0))
            {
                Vector3 spawnPoint = hit.point + Vector3.up * 1f;
                Quaternion rotation = Quaternion.identity;

                GameObject scoutObj = PhotonNetwork.InstantiateRoomObject("Character_Scoutmaster", spawnPoint, rotation, 0, null);
                var character = scoutObj.GetComponent<Character>();
                if (character != null)
                    character.data.spawnPoint = character.transform;

               
                scoutObj.GetComponent<MonoBehaviour>().StartCoroutine(SetScoutTargetDelayed(scoutObj, targetCharacter));
            }
        }



        
      





        private static Texture2D drawingTex;
        private static Color lastTexColour = Color.clear;


        private static Material overlayMaterial = null;
        internal static void Init()
        {

            if ((UnityEngine.Object)(object)overlayMaterial == (UnityEngine.Object)null)
            {
                overlayMaterial = new Material(Shader.Find("Hidden/Internal-Colored"))
                {
                    hideFlags = (HideFlags)5
                };
                overlayMaterial.SetInt("_SrcBlend", 5);
                overlayMaterial.SetInt("_DstBlend", 10);
                overlayMaterial.SetInt("_Cull", 0);
                overlayMaterial.SetInt("_ZTest", 8);
                overlayMaterial.SetInt("_ZWrite", 0);
                overlayMaterial.SetColor("_Color", Color.white);
            }
        }

        internal static bool IsBehindCamera(Vector3 screenPosition)
        {

            return screenPosition.z < 0f;
        }

        internal static Vector3 ToScreen(Vector3 worldPosition)
        {

            Vector3 val = Camera.main.WorldToScreenPoint(worldPosition);
            val.y = (float)Screen.height - val.y;
            return val;
        }


        internal static Color GetRainbow(float offset = 0f)
        {

            float num = (Time.time + offset) * 2f;
            float num2 = Mathf.Sin(num) * 0.5f + 0.5f;
            float num3 = Mathf.Sin(num + 2f) * 0.5f + 0.5f;
            float num4 = Mathf.Sin(num + 4f) * 0.5f + 0.5f;
            return new Color(num2, num3, num4);
        }
        public static Font gamefont;
        private static bool IsOnScreen(Vector2 position)
        {
            return position.x >= 0 && position.y >= 0 &&
                   position.x <= Screen.width && position.y <= Screen.height;
        }
        private static GUIStyle __outlineStyle = new GUIStyle();
        private static GUIStyle __style = new GUIStyle();
        internal static void DrawString(Vector2 pos, string text, Color color, bool center = true, int size = 12, FontStyle fontStyle = FontStyle.Bold, int depth = 1)

        {

            if ((UnityEngine.Object)(object)gamefont == (UnityEngine.Object)null)
            {
                gamefont = ((TMP_Text)UnityEngine.Object.FindObjectOfType<PlayerConnectionLog>().text).font.sourceFontFile;
            }
            __style.fontSize = size;
            __style.richText = true;
            __style.font = gamefont;
            __style.normal.textColor = color;
            __style.fontStyle = fontStyle;
            __outlineStyle.fontSize = size;
            __outlineStyle.richText = true;
            __outlineStyle.font = gamefont;
            __outlineStyle.normal.textColor = new Color(0f, 0f, 0f, 1f);
            __outlineStyle.fontStyle = fontStyle;
            GUIContent val = new GUIContent(text);
            GUIContent val2 = new GUIContent(text);
            if (center)
            {
                pos.x -= __style.CalcSize(val).x / 2f;
            }
            switch (depth)
            {
                case 0:
                    UnityEngine.GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), val, __style);
                    break;
                case 1:
                    UnityEngine.GUI.Label(new Rect(pos.x + 1f, pos.y + 1f, 300f, 25f), val2, __outlineStyle);
                    UnityEngine.GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), val, __style);
                    break;
                case 2:
                    UnityEngine.GUI.Label(new Rect(pos.x + 1f, pos.y + 1f, 300f, 25f), val2, __outlineStyle);
                    UnityEngine.GUI.Label(new Rect(pos.x - 1f, pos.y - 1f, 300f, 25f), val2, __outlineStyle);
                    UnityEngine.GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), val, __style);
                    break;
                case 3:
                    UnityEngine.GUI.Label(new Rect(pos.x + 1f, pos.y + 1f, 300f, 25f), val2, __outlineStyle);
                    UnityEngine.GUI.Label(new Rect(pos.x - 1f, pos.y - 1f, 300f, 25f), val2, __outlineStyle);
                    UnityEngine.GUI.Label(new Rect(pos.x, pos.y - 1f, 300f, 25f), val2, __outlineStyle);
                    UnityEngine.GUI.Label(new Rect(pos.x, pos.y + 1f, 300f, 25f), val2, __outlineStyle);
                    UnityEngine.GUI.Label(new Rect(pos.x, pos.y, 300f, 25f), val, __style);
                    break;
            }
        }
        internal static void DrawLine(Color color, Vector3 origin, Vector3 destination)
        {

            Init();
            GL.PushMatrix();
            overlayMaterial.SetPass(0);
            GL.LoadPixelMatrix(0f, (float)Screen.width, (float)Screen.height, 0f);
            GL.Begin(2);
            GL.Color(color);
            GL.Vertex(new Vector3(origin.x, origin.y));
            GL.Vertex(new Vector3(destination.x, destination.y));
            GL.End();
            GL.PopMatrix();
        }


        internal static void DrawBox(Color color, Vector2 position, float width, float height)
        {

            Init();
            width /= 2f;
            height /= 2f;
            GL.PushMatrix();
            overlayMaterial.SetPass(0);
            GL.LoadPixelMatrix(0f, (float)Screen.width, (float)Screen.height, 0f);
            GL.Begin(7);
            GL.Color(color);
            GL.TexCoord2(position.x + width, position.y + height);
            GL.Vertex(new Vector3(position.x + width, position.y + height));
            GL.TexCoord2(position.x + width, position.y - height);
            GL.Vertex(new Vector3(position.x + width, position.y - height));
            GL.TexCoord2(position.x - width, position.y - height);
            GL.Vertex(new Vector3(position.x - width, position.y - height));
            GL.TexCoord2(position.x - width, position.y + height);
            GL.Vertex(new Vector3(position.x - width, position.y + height));
            GL.End();
            GL.PopMatrix();
        }

        internal static void RectFilled(float x, float y, float width, float height, Color color)
        {
            if (drawingTex == null)
            {
                drawingTex = new Texture2D(1, 1);
                drawingTex.wrapMode = TextureWrapMode.Clamp;
                drawingTex.filterMode = FilterMode.Point;
            }

            if (color != lastTexColour)
            {
                drawingTex.SetPixel(0, 0, color);
                drawingTex.Apply();
                lastTexColour = color;
            }

            UnityEngine.GUI.DrawTexture(new Rect(x, y, width, height), drawingTex);
        }



        public static void RespawnCharacter()
        {

            PhotonNetwork.Instantiate("Character", ((Component)Camera.main).transform.position, ((Component)Camera.main).transform.rotation, (byte)0, (object[])null);
            PhotonNetwork.Instantiate("Player", ((Component)Camera.main).transform.position, ((Component)Camera.main).transform.rotation, (byte)0, (object[])null);
            SendOnce("\n[Peak] Character respawned successfully!");
        }


        private static readonly HashSet<string> sentMessages = new HashSet<string>();

        public static void SendOnce(string message)
        {
            if (!sentMessages.Contains(message) && TrySend(message))
            {
                sentMessages.Add(message);
            }
        }

        public static void Reset(string message)
        {
            sentMessages.Remove(message);
        }

        public static void ResetAll()
        {
            sentMessages.Clear();
        }

        private static bool TrySend(string message)
        {
            PlayerConnectionLog val = UnityEngine.Object.FindFirstObjectByType<PlayerConnectionLog>();
            if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
            {
                return false;
            }
            MethodInfo method = typeof(PlayerConnectionLog).GetMethod("AddMessage", BindingFlags.Instance | BindingFlags.NonPublic);
            if (method != null)
            {
                method.Invoke(val, new object[1] { message });
                return true;
            }
            Debug.LogWarning((object)"AddMessage method not found on PlayerConnectionLog.");
            return false;
        }



        public static void SlipAll()
        {

            if (GUI.IncludeSelf)
            {
                foreach (Character allCharacter in Character.AllCharacters)
                {
                    BananaPeel val = UnityEngine.Object.FindFirstObjectByType<BananaPeel>();
                    if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
                    {
                        val = PhotonNetwork.Instantiate("0_Items/Berrynana Peel Pink Variant", allCharacter.Head, Quaternion.identity, (byte)0, (object[])null).GetComponent<BananaPeel>();

                    }
                    ((Component)val).GetComponent<PhotonView>().RPC("RPCA_TriggerBanana", (RpcTarget)0, new object[1] { allCharacter.refs.view.ViewID });
                }
                return;
            }
            foreach (Character allCharacter2 in Character.AllCharacters)
            {
                if (!((MonoBehaviourPun)allCharacter2).photonView.IsMine)
                {
                    BananaPeel val2 = UnityEngine.Object.FindFirstObjectByType<BananaPeel>();
                    if ((UnityEngine.Object)(object)val2 == (UnityEngine.Object)null)
                    {
                        val2 = PhotonNetwork.Instantiate("0_Items/Berrynana Peel Pink Variant", allCharacter2.Head, Quaternion.identity, (byte)0, (object[])null).GetComponent<BananaPeel>();

                    }
                    ((Component)val2).GetComponent<PhotonView>().RPC("RPCA_TriggerBanana", (RpcTarget)0, new object[1] { allCharacter2.refs.view.ViewID });
                }
            }
        }




        public static void RemoveMyItems()
        {
            PhotonView[] allViews = UnityEngine.Object.FindObjectsOfType<PhotonView>();

            foreach (PhotonView view in allViews)
            {
                Item itemComponent = view.GetComponent<Item>();

                // Skip if no Item component or not owned by this client
                if (itemComponent == null || !view.IsMine)
                    continue;

                PhotonNetwork.Destroy(view);
            }
        }

        public static float TimeOfDay = 1f;
        public static void SetTimeOfDay()
        {
            DayNightManager[] array = UnityEngine.Object.FindObjectsOfType<DayNightManager>();
            foreach (DayNightManager val in array)
            {
                ((Component)val).GetComponent<PhotonView>().RPC("RPCA_SyncTime", (RpcTarget)0, new object[1] { TimeOfDay });
            }
        }


        public static void IcecicleFall()
        {
            ShakyIcicleIce2[] array = UnityEngine.Object.FindObjectsOfType<ShakyIcicleIce2>();
            foreach (ShakyIcicleIce2 val in array)
            {
                ((Component)val).GetComponent<PhotonView>().RPC("Fall_Rpc", (RpcTarget)0, new object[0]);
            }
        }

        public static void Bridge()
        {
            BreakableBridge[] array = UnityEngine.Object.FindObjectsOfType<BreakableBridge>();
            foreach (BreakableBridge val in array)
            {
                ((MonoBehaviourPun)val).photonView.RPC("Fall_Rpc", (RpcTarget)0, new object[0]);
            }
        }

        public static void clearallthins()
        {
            if (GUI.IncludeSelf)
            {
                foreach (Character allCharacter in Character.AllCharacters)
                {
                    float[] array = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
                    ((MonoBehaviourPun)allCharacter).photonView.RPC("ApplyStatusesFromFloatArrayRPC", (RpcTarget)0, new object[1] { array });
                }
                return;
            }
            foreach (Character allCharacter2 in Character.AllCharacters)
            {
                if (!((MonoBehaviourPun)allCharacter2).photonView.IsMine)
                {
                    float[] array2 = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
                    ((MonoBehaviourPun)allCharacter2).photonView.RPC("ApplyStatusesFromFloatArrayRPC", (RpcTarget)0, new object[1] { array2 });
                }
            }
        }


        public static void ForceME()
        {
            PlayerGhost[] array = UnityEngine.Object.FindObjectsOfType<PlayerGhost>();
            foreach (PlayerGhost val in array)
            {
                val.m_view.RPC("RPCA_SetTarget", (RpcTarget)0, new object[1] { ((MonoBehaviourPun)Character.localCharacter).photonView });
            }
        }


        private void HandleThrowMod()
        {
            if (Character.localCharacter == null || Character.localCharacter.refs == null)
                return;

            if (GUI.InstantThrow)
            {
                Character.localCharacter.refs.items.throwChargeLevel = 1f;
                GUI.CThrowPower = false;
            }

            if (GUI.CThrowPower)
            {
                Character.localCharacter.refs.items.throwChargeLevel = GUI.ThrowPower;
            }
        }



        private void AcquireServices()
        {
            steamLobbyHandler = GameHandler.GetService<SteamLobbyHandler>();
        }



        private bool loaded;
        private bool isCursorOn = false;
        private void Update()
        {

            // join notis
            //if (!loaded)
            //{
            //    atlas = new GameObject("atlasobj");
            //    //atlas.AddComponent<UI>();
            //    atlas.AddComponent<PlayerCallbackHandler>();
            //    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object)(object)atlas);

            //    loaded = true;
            //}


            if ((GetAsyncKeyState(0x2E) & 1) != 0)
            {
                Loader.Unload();
                //StopCoroutine(Instance.CollectObjects());
            }

            GUIToggleCheck();




            //UpdateCachedItems();

            if (GUI.spawnflags)
            {
                TestTing();
            }


            if (GUI.silentaim)
            {
                // SilentAim();
            }


            HandleThrowMod();





            bool deaggroTriggered = false;

            if (GUI.Deaggro && !deaggroTriggered)
            {
                deaggroTriggered = true; 

                BeeSwarm[] array4 = UnityEngine.Object.FindObjectsOfType<BeeSwarm>();
                foreach (BeeSwarm beeSwarm in array4)
                {
                    if (beeSwarm.currentAggroCharacter == Character.localCharacter)
                    {
                        beeSwarm.photonView.RPC("DisperseRPC", RpcTarget.All, new object[0]);
                    }
                }
            }



            RaycastHit val = default(RaycastHit);
            if (GUI.ClickToTp && UnityEngine.Input.GetMouseButtonDown(0) && PExt.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out val, HelperExtensions.ToLayerMask((LayerType)1), -1f))
            {
                Character.localCharacter.photonView.RPC("WarpPlayerRPC", RpcTarget.All, new object[2]
                {
        val.point,
        true
                });
            }

            RaycastHit val2 = default(RaycastHit);
            if (GUI.PingWithAll && UnityEngine.Input.GetMouseButton(2) && PExt.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out val2, HelperExtensions.ToLayerMask((LayerType)1), -1f))
            {
                PointPinger[] array = UnityEngine.Object.FindObjectsOfType<PointPinger>();
                foreach (PointPinger val3 in array)
                {
                    val3.GetComponent<PhotonView>().RPC("ReceivePoint_Rpc", RpcTarget.All, new object[2]
                    {
            val2.point,
            val2.normal
                    });
                }
            }

            bool blowdartShotGun = GUI.BlowdartShotGun;
            if (blowdartShotGun)
            {
                bool mouseButton3 = UnityEngine.Input.GetMouseButton(0);
                if (mouseButton3)
                {
                    Ray ray2 = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                    RaycastHit raycastHit4;
                    bool flag10 = Physics.Raycast(ray2, out raycastHit4, Cheat.laserRange);
                    if (flag10)
                    {
                        Action_RaycastDart action_RaycastDart = UnityEngine.Object.FindFirstObjectByType<Action_RaycastDart>();
                        bool flag11 = action_RaycastDart == null;
                        if (flag11)
                        {
                            action_RaycastDart = PhotonNetwork.Instantiate("0_Items/HealingDart Variant", Vector3.zero, Quaternion.identity, 0, null).GetComponent<Action_RaycastDart>();
                           
                        }
                        action_RaycastDart.GetComponent<PhotonView>().RPC("RPC_DartImpact", RpcTarget.All, new object[]
                        {
                    null,
                    raycastHit4.point,
                    raycastHit4.point
                        });
                    }
                }
            }







            //if (GUI.boxesp)
            //{
            //    BoxESP(true);
            //} else { BoxESP(false); }

            if (GUI.LuggageEsp)
            {
                BoxESPItems(true);
            }
            else
            {
                BoxESPItems(false);
            }


            //luggagelist
             RefreshLuggageList();





            if (UnityEngine.Input.GetKeyDown((KeyCode)32) && (UnityEngine.Object)(object)Character.localCharacter != (UnityEngine.Object)null && GUI.IJump)
            {
                ((MonoBehaviourPun)Character.localCharacter).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
            }


            if (isFlying != GUI.flyMode)
            {
                isFlying = GUI.flyMode;
                Debug.Log("[Cheat] Flight mode: " + (isFlying ? "Enabled" : "Disabled"));

                if (Character.localCharacter != null && Character.localCharacter.refs?.ragdoll != null)
                {
                    foreach (Bodypart part in Character.localCharacter.refs.ragdoll.partList)
                    {
                        if (part?.Rig != null)
                            part.Rig.useGravity = !isFlying;
                    }
                }
            }



            if (GUI.flyMode)
            {
                HandleFlying();
            }




            if (GUI.gravityGun)
            {
                GravityGun();
            }



            if (UnityEngine.Input.GetKeyDown(KeyCode.G))
            {
                GUI.flyMode = !GUI.flyMode;
            }







            //maxpalyers not sure
            //if (_newMaxPlayers == 0)
            //{
            //    _newMaxPlayers = 1;
            //}
            //else if (_newMaxPlayers > 30)
            //{
            //    _newMaxPlayers = 30;
            //}
            //NetworkConnector.MAX_PLAYERS = _newMaxPlayers;




            if (GUI.clearStatuses)
            {


                ClearAllStatus();
            }



            if (GUI.crasher)
            {
                Crasher();

                //MakeNameEmojis();
            }



            if (GUI.ClimbSpeed)
            {


                Character.localCharacter.refs.climbing.climbSpeed = GUI.ClimbSpeed2;
                Character.localCharacter.refs.climbing.climbSpeedMod = GUI.ClimbSpeedForce;
                Character.localCharacter.refs.vineClimbing.climbSpeedMod = GUI.ClimbSpeedForce;
                Character.localCharacter.refs.ropeHandling.climbSpeedMod = GUI.ClimbSpeedForce;
                // Character.localCharacter.refs.climbing.climbSpeed = 10;

            }
            else
            {
                Character.localCharacter.refs.climbing.climbSpeed = 4;
                Character.localCharacter.refs.climbing.climbSpeedMod = 1;
                Character.localCharacter.refs.vineClimbing.climbSpeedMod = 1;
                Character.localCharacter.refs.ropeHandling.climbSpeedMod = 1;
            }







            if (GUI.godmode)
            {


                //Mine
                if (!Character.localCharacter.photonView.IsMine)
                {
                    return;
                }
                Character.localCharacter.data.currentStamina = 100;
                // GUIManager.instance.bar.ChangeBar();
                Character.localCharacter.AddExtraStamina(100);
                // Character.InfiniteStamina();



                // Character.localCharacter.refs.customization.RandomizeCosmetics();

                // Character.localCharacter.WarpPlayerRPC
                // Character.localCharacter.photonView.RPC("RPCEndGame_ForceWin", RpcTarget.All, Array.Empty<object>());


            }
            else
            {


            }

            if (GUI.infiniteammo)
            {

                // BingBongSpam();
                //Mine.Weapons.CurrentWeapon
                // string objName = "ropeshooteranti";
                //Character.localCharacter.refs.items.photonView.RPC("RPC_SpawnItemInHandMaster", RpcTarget.MasterClient, new object[]{
                //    objName
                //  });




                // Character.localCharacter.data.dead, Character.localCharacter.data.fullyPassedOut);
                if (Character.localCharacter.data.dead || Character.localCharacter.data.fullyPassedOut)
                {
                    Character.localCharacter.photonView.RPC("RPCA_Revive", RpcTarget.All, new object[]
                    {
                true
                    });
                }



            }



           


            if (GUI.randomoutfits)
            {


                RandomCosmeticstest();
                // Character.localCharacter.refs.customization.RandomizeCosmetics();


                //Mine.Weapons.CurrentWeapon



                // Character.localCharacter.data.dead, Character.localCharacter.data.fullyPassedOut);




            }


            if (GUI.setfieldofview)
            {
                //MainCamera.instance.SetCameraOverride(CameraOverride.fov);
                //CameraOverride.Instantiate(Camera.main);
                //Camera.main.fieldOfView = 120;


                //Mine.Weapons.CurrentWeapon



                // Character.localCharacter.data.dead, Character.localCharacter.data.fullyPassedOut);




            }
            else
            {
                // Camera.main.fieldOfView = 35;
            }


            if (GUI.testting && UnityEngine.Input.GetKeyDown((KeyCode.J)))
            {
                if (((Behaviour)((Component)Character.localCharacter).GetComponent<CharacterMovement>()).enabled)
                {
                    ((Behaviour)((Component)Character.localCharacter).GetComponent<CharacterMovement>()).enabled = false;
                }
                else
                {
                    ((Behaviour)((Component)Character.localCharacter).GetComponent<CharacterMovement>()).enabled = true;
                }
            }



            if (GUI.BingbongSpam)
            {
                BingBongSpam();

            }
            else
            {

            }

            if (GUI.PassPortSpam)
            {
                PassportSpam();


            }
            else
            {

            }

            if (GUI.ScoutMaster)
            {


            }
            else
            {

            }











            if (GUI.Unlockall)
            {
                PassportManager.instance.testUnlockAll = true;
                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);

            }
            else
            {
                PassportManager.instance.testUnlockAll = false;
            }

            if (GUI.rapidfire)
            {
                GUI.rapidcooldown++;
                if (GUI.rapidcooldown > GUI.fireratecooldown)
                {

                    DayNightManager.instance.setTimeOfDay(GUI.fireratecooldown);
                    // DayNightManager.instance.specialTopColor = Color.red;
                    //DayNightManager.instance.specialMidColor = Color.red;

                    //if (UnityEngine.Input.GetMouseButton(0))
                    //{
                    //    PlayerController.Mine.Weapons.photonView.RPC("FireWeaponRemote", RpcTarget.All, new object[]
                    //    {
                    //    null,
                    //    true,
                    //    1
                    //    });
                    //}
                    GUI.rapidcooldown = 0;
                }
            }


          



            if (GUI.DrawItem)
            {
                if (UnityEngine.Input.GetMouseButton(0))
                {
                    spawnTimer -= Time.deltaTime;
                    if (spawnTimer <= 0f)
                    {
                        spawnTimer = spawnCooldown;
                        Vector3 val14 = ((Component)Camera.main).transform.position + ((Component)Camera.main).transform.forward * 2f;
                        Item component = PhotonNetwork.Instantiate("0_Items/" + GUI.SItemName, val14, Quaternion.identity, (byte)0, (object[])null).GetComponent<Item>();
                        ((MonoBehaviourPun)component).photonView.RPC("SetKinematicRPC", (RpcTarget)0, new object[3]
                        {
                            true,
                            ((Component)component).transform.position,
                            ((Component)component).transform.rotation
                        });
                    }
                }
                else
                {
                    spawnTimer = 0f;
                }
            }


            if (GUI.speed)
            {

                PlayerHandler.GetPlayer(Character.localCharacter.photonView.Owner);
                Character.localCharacter.refs.movement.movementModifier = GUI.speedmultiply;
                // Character.localCharacter.refs.movement.

                //      PlayerController.Mine.PlayerSpeedMultiplier = GUI.speedmultiply;
            }
            else
            {
                Character.localCharacter.refs.movement.movementModifier = 1; //should be the default value
            }


        }
    }
}
