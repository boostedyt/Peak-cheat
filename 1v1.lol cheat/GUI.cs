using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DG.Tweening.Plugins.Core;
//using JustPlay.PsfLight.Events;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zorro.Core;
using Zorro.UI.Modal;
using Steamworks;
//using UnityEngine.UI.Extensions;
using Zorro.Core.Serizalization;
using static Item;
using UnityEngine.Rendering;
namespace _1v1.lol_cheat
{
    public class GUI : MonoBehaviour
    {
        //GUI Variables
        public static Rect GUIRect = new Rect(30, 30, 700, 600);
        public static int selected_tab = 0;
        public static string[] tabnames = { "Spawner", "Visual", "Self", "PlayerList", "Lobby", "Players" };

        //aim
        public static bool silentaim = false;
        public static float fov = 180;

        //fovcircle
        public static bool fovcircle = false;

        //godmode
        public static bool godmode = false;


        public static Vector2 itemScrollPos = Vector2.zero;
        public static bool clearStatuses = false;
        public static bool bodyesp = false;

        //infiniteammo
        public static bool infiniteammo = false;

        public static bool randomoutfits = false;

        public static bool setfieldofview = false;

        //rapidfire
        public static bool rapidfire = false;

        public static bool Unlockall = false;

        public static bool BingbongSpam = false;

        public static bool PassPortSpam = false;

        public static bool ScoutMaster = false;
        public static int rapidcooldown = 0;
        public static float fireratecooldown = 3;

        public static float ClimbSpeedForce = 1;
        public static float ClimbSpeed2 = 4;
        public static bool ClimbSpeed = false;

        public static float fieldofview = 35f;

        //crasher
        public static bool crasher = false;
        public static bool testting = false;

        //boxesp
        public static bool boxesp = false;
        public static bool ItemEsp = false;
        public static bool LuggageEsp = false;
        public static bool boxfix = false;


        public static bool spawnflags = false;

        public static Vector2 luggageScrollPos = Vector2.zero;

        private static string searchFilter = "";
        //speed
        public static bool speed = false;
        public static float speedmultiply = 5;
        public static bool flyMode = false;

        public static Dictionary<string, Character> playerDict = new Dictionary<string, Character>();
        public static string localPlayerName = "";
        public static UnityEngine.Vector2 playerScrollPos;
        public static int selectedPlayerIndex;

        public static string steamId = "";

        private static Vector2 scrollPos = Vector2.zero;

        public static string[] items = ItemDatabase.GetAllObjectNames();
        //  public static UnityEngine.Vector2 itemScrollPos;
        public static int selectedItemIndex;
        public static bool NoD;
        
        
        public static bool NoS;
        public static bool NoP;
        public static bool NoR;
        public static bool NoSL;
        public static bool NoDstry;

        public static bool AntiConsume;
        public static bool ICharge;
        public static bool InstantItemUse;

        private static string searchBuffer = "";

        public static Rect watermarkRect = new Rect(10, 10, 300, 30);
        public static bool showWatermark = true;
        public static bool showPing = true;
        public static bool spinX = false;
        public static bool spinY = true;
        public static bool spinZ = false;

        public static float spinSpeed = 120f;

        public static string cheatName = "Hyro";
        public static float timer = 0f;
        public static bool Noclip = false;
        public static bool Deaggro = false;
        public static bool PingWithAll = false;
        public static bool BlowdartShotGun = false;
        public static bool Tracers = false;
        public static bool PlayerTags = false;
        public static bool ClickToTp = false;
        public static float colorChangeSpeed = 1f;
        public static bool IncludeSelf;
        public static bool gravityGun = false;

        public static bool DrawItem = false;

        public static bool BCrashAll = false;

        public static bool SpinBOT = false;

        public static bool skeletonESP = true;
        private static Vector2 ScrollPos1;
        public static bool SpawnInHand = false;

        public static float ItemCookLevel;
        public static bool UseItemsInGameName;

        public static bool InstantThrow = false;

        public static string JoinLink;

        public static string SItemName;
        public static Item SItem;
        public static bool IJump;

        public static float ThrowPower = 1f;
        public static bool CThrowPower;

        private static string playerSearchBuffer = "";
        public static bool IRope;
        public static Character SelectedPlayer = null;

        public static Color topColor = Color.white;
        public static Color midColor = Color.white;
        public static Color bottomColor = Color.white;
        public static Color sunColor = Color.white;
        public static bool isSectionOpen = false;

        public static bool showTopPicker = false;
        public static bool showMidPicker = false;
        public static bool showBottomPicker = false;
        public static bool showSunPicker = false;

        private static Vector2 ScrollPos2;
        private static void Playermote(string emote)
        {
            if (IncludeSelf)
            {
                foreach (Character allCharacter in Character.AllCharacters)
                {
                    ((MonoBehaviourPun)allCharacter).photonView.RPC("RPCA_PlayRemove", (RpcTarget)0, new object[1] { emote });
                }
                return;
            }
            foreach (Character allCharacter2 in Character.AllCharacters)
            {
                if (!((MonoBehaviourPun)allCharacter2).photonView.IsMine)
                {
                    ((MonoBehaviourPun)allCharacter2).photonView.RPC("RPCA_PlayRemove", (RpcTarget)0, new object[1] { emote });
                }
            }
        }

        private static void StartCarry()
        {
            ((MonoBehaviourPun)Character.localCharacter).photonView.RPC("RPCA_StartCarry", (RpcTarget)0, new object[1] { ((MonoBehaviourPun)SelectedPlayer).photonView });
        }
        private static void Fling()
        {
            ((MonoBehaviourPun)SelectedPlayer).photonView.RPC("RPCA_Fall", (RpcTarget)0, new object[1] { 7f });
            ((MonoBehaviourPun)SelectedPlayer).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
            ((MonoBehaviourPun)SelectedPlayer).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
            ((MonoBehaviourPun)SelectedPlayer).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
            ((MonoBehaviourPun)SelectedPlayer).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
            ((MonoBehaviourPun)SelectedPlayer).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
            ((MonoBehaviourPun)SelectedPlayer).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
            ((MonoBehaviourPun)SelectedPlayer).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
            ((MonoBehaviourPun)SelectedPlayer).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
        }

        private static void Carry()
        {
            ((Component)Character.localCharacter.refs.carriying).GetComponent<PhotonView>().RPC("RPCA_StartCarry", (RpcTarget)0, new object[1] { ((MonoBehaviourPun)SelectedPlayer).photonView });
        }

        private static void RenderDead()
        {
            ((Component)SelectedPlayer.refs.customization).GetComponent<PhotonView>().RPC("CharacterDied", (RpcTarget)0, new object[0]);
        }

        private static void RenderPassedOut()
        {
            ((Component)SelectedPlayer.refs.customization).GetComponent<PhotonView>().RPC("CharacterPassedOut", (RpcTarget)0, new object[0]);
        }

        private static void DropHeldItem()
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            ((MonoBehaviourPun)SelectedPlayer.refs.items).photonView.RPC("DestroyHeldItemRpc", (RpcTarget)0, new object[0]);
        }

        private static void FullStam()
        {
            ((MonoBehaviourPun)SelectedPlayer).photonView.RPC("MoraleBoost", (RpcTarget)0, new object[2] { 1f, 0 });
        }

        private static void Jump()
        {
            ((MonoBehaviourPun)SelectedPlayer).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
        }

        private static void Fall()
        {
            ((MonoBehaviourPun)SelectedPlayer).photonView.RPC("RPCA_Fall", (RpcTarget)0, new object[1] { 7f });
        }


        private static void Kill()
        {
            
            ((MonoBehaviourPun)SelectedPlayer).photonView.RPC("RPCA_Die", (RpcTarget)0, new object[1] { SelectedPlayer.Center + Vector3.up * 0.2f + Vector3.forward * 0.1f });
        }

        private static void Slip()
        {
            
            BananaPeel val = UnityEngine.Object.FindFirstObjectByType<BananaPeel>();
            if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
            {
                val = PhotonNetwork.Instantiate("0_Items/Berrynana Peel Pink Variant", SelectedPlayer.Head, Quaternion.identity, (byte)0, (object[])null).GetComponent<BananaPeel>();
                //UI.NewDebugNotif("Spawned banana peel");
            }
            ((Component)val).GetComponent<PhotonView>().RPC("RPCA_TriggerBanana", (RpcTarget)0, new object[1] { SelectedPlayer.refs.view.ViewID });
        }


        public static void ChangeZoneColors(SpecialDayZone zone, Color topColor, Color midColor, Color bottomColor, Color sunColor, float fogDensity = 400f)
        {
            if (zone == null) return;

            zone.useCustomColorVals = true;
            zone.specialTopColor = topColor;
            zone.specialMidColor = midColor;
            zone.specialBottomColor = bottomColor;

            zone.overrideSun = true;
            zone.useCustomSun = true;
            zone.specialSunColor = sunColor;

            zone.overrideFog = true;
            zone.fogDensity = fogDensity;

            if (zone.specialLight != null)
            {
                zone.specialLight.color = sunColor;
            }

            Debug.Log("Zone colors updated via GUI");
        }

        private static Vector2 ScrollPos;

        public static void Begin()
        {
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
        }

        public static void End()
        {
            GUILayout.EndVertical();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            isSectionOpen = false;
        }

        public static void Separate()
        {
            GUILayout.EndVertical();
            GUILayout.EndVertical();
            GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
            isSectionOpen = false;
        }


        public static float NewSlider(string text, float value, float minimum, float maximum)
        {
          
            GUIStyle val = new GUIStyle()
            {
                fontSize = 12
            };
            GUILayout.Label(text + " : " + value, val, Array.Empty<GUILayoutOption>());
            return GUILayout.HorizontalSlider(value, minimum, maximum, Array.Empty<GUILayoutOption>());
        }

        public static void NewLabel(string text)
        {
            
            GUIStyle val = new GUIStyle
            {
                fontSize = 12
            };
            GUILayout.Label("<b><color=white>" + text + "</color></b>", val, Array.Empty<GUILayoutOption>());
        }


        public static void NewSection(string text)
        {
            if (isSectionOpen)
            {
                GUILayout.EndVertical();
                GUILayout.Space(4f);
            }

            float width = 285f;

            GUILayout.BeginVertical(UnityEngine.GUI.skin.box, GUILayout.Width(width));
            GUILayout.Box(text ?? "", GUILayout.ExpandWidth(true));

            isSectionOpen = true;
        }

        public static void NewButton(string text, Action callback)
        {

            if (callback == null)
            {
                Debug.LogWarning("NewButton callback is null.");
                return;
            }

            GUIStyle style = new GUIStyle(UnityEngine.GUI.skin.box)
            {
                fontSize = 12,
                richText = false
            };

            if (GUILayout.Button(text, style))
            {
                callback();
            }
       
        
        }
        public static List<string> ActiveModules = new List<string>();
        public static bool NewToggle(bool value, string label)
        {
            GUIStyle style = new GUIStyle(UnityEngine.GUI.skin.box)
            {
                fontSize = 12
            };

            // Ensure Visual.ActiveModules exists and is accessible
            if (value)
            {
                if (!ActiveModules.Contains(label))
                {
                    ActiveModules.Add(label);
                }

                style.normal.textColor = Color.green;
                label += " [ENABLED]";
            }
            else
            {
                if (ActiveModules.Contains(label))
                {
                    ActiveModules.Remove(label);
                }

                style.normal.textColor = Color.red;
                label += " [DISABLED]";
            }

            return GUILayout.Toggle(value, label, style);
        }

        private static int calculationofthestaminaverycoolyes()
        {
            return Mathf.RoundToInt(SelectedPlayer.data.currentStamina * 100f);
        }
        private static void SelectPlayer(Character plr)
        {
            SelectedPlayer = plr;
        }

        private static void ForceAllDone()
        {
            GameOverHandler val = UnityEngine.Object.FindFirstObjectByType<GameOverHandler>();
            if ((UnityEngine.Object)(object)val != (UnityEngine.Object)null)
            {
                ((Component)val).GetComponent<PhotonView>().RPC("ForceEveryPlayerDoneWithEndScreenRPC", (RpcTarget)0, new object[0]);
            }
        }

        public static void CherryName()
        {
            PhotonNetwork.NickName = "<size=25><color=#FF0033>H</color><color=#FF1A47>y</color><color=#FF3366>r</color><color=#FF4D7A>o</color><color=#FF6699>s</color><color=#FF80B3>s</color> <color=#FF4D7A>M</color><color=#FF3366>e</color><color=#FF1A47>n</color><color=#FF0033>u</color></size>";
            Cheat.RespawnCharacter();
        }

        private static void SpawnItem(string itemName)
        {
            if (Character.localCharacter == null || Character.localCharacter.refs.items == null)
                return;

            Character.localCharacter.refs.items.photonView.RPC("RPC_SpawnItemInHandMaster", RpcTarget.MasterClient, new object[]
            {
        itemName
            });

            Debug.Log($"[Cheat] Spawned item: {itemName}");
        }


        private static void beemod()
        {
            
            BeeSwarm component = PhotonNetwork.Instantiate("BeeSwarm", SelectedPlayer.Head, Quaternion.identity, (byte)0, (object[])null).GetComponent<BeeSwarm>();
            ((MonoBehaviourPun)component).photonView.RPC("SetBeesAngryRPC", (RpcTarget)0, new object[1] { true });
        }


        private static int SItemIndex;


        public static void SpawnItemOnEveryone()
        {
            if (string.IsNullOrEmpty(SItemName))
            {
                Debug.LogWarning("No item selected to spawn on everyone.");
                return;
            }

            foreach (Character plr in Character.AllCharacters)
            {
                if (plr == null || plr.player == null)
                    continue;

                Vector3 spawnPosition = plr.Center + Vector3.up * 3f;

                GameObject obj = PhotonNetwork.Instantiate(
                    "0_Items/" + SItemName,
                    spawnPosition,
                    Quaternion.identity,
                    0,
                    null
                );

                Item itemComponent = obj.GetComponent<Item>();
                if (itemComponent != null)
                {
                    itemComponent.Interact(plr);
                }
            }

            Debug.Log($"Spawned '{SItemName}' on all players.");
        }

        public static void SpawnItem2()
        {
            if (SpawnInHand)
            {
                ItemSlot[] itemSlots = Character.localCharacter.player.itemSlots;
                foreach (ItemSlot val in itemSlots)
                {
                    if ((UnityEngine.Object)(object)val.prefab == (UnityEngine.Object)null)
                    {
                        ItemSlot val2 = val;
                        val2.prefab = SItem;
                        val2.data = new ItemInstanceData(Guid.NewGuid());
                        ItemInstanceDataHandler.AddInstanceData(val.data);
                        byte[] array = IBinarySerializable.ToManagedArray<InventorySyncData>(new InventorySyncData(Character.localCharacter.player.itemSlots, Character.localCharacter.player.backpackSlot, Character.localCharacter.player.tempFullSlot));
                        ((MonoBehaviourPun)Character.localCharacter.player).photonView.RPC("SyncInventoryRPC", (RpcTarget)1, new object[2] { array, true });
                        break;
                    }
                }
            }
            else
            {
                Vector3 val3 = ((Component)Camera.main).transform.position + ((Component)Camera.main).transform.forward * 2f;
                PhotonNetwork.Instantiate("0_Items/" + SItemName, val3, Quaternion.identity, (byte)0, (object[])null);
            }
        }

        public static void SelectItem(int Index, string Name, Item Item)
        {
            SItemIndex = Index;
            SItemName = Name;
            SItem = Item;
        }


        public static void TeleportTo(Vector3 position, bool poof = true)
        {
            if (Character.localCharacter == null)
                return;

            Character.localCharacter.WarpPlayerRPC(position, poof);
        }





        private static void JumpAll()
        {
            if (IncludeSelf)
            {
                foreach (Character allCharacter in Character.AllCharacters)
                {
                    ((MonoBehaviourPun)allCharacter).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                }
                return;
            }
            foreach (Character allCharacter2 in Character.AllCharacters)
            {
                if (!((MonoBehaviourPun)allCharacter2).photonView.IsMine)
                {
                    ((MonoBehaviourPun)allCharacter2).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                }
            }
        }


        private static void FallAll()
        {
            if (IncludeSelf)
            {
                foreach (Character allCharacter in Character.AllCharacters)
                {
                    ((MonoBehaviourPun)allCharacter).photonView.RPC("RPCA_Fall", (RpcTarget)0, new object[1] { 7f });
                }
                return;
            }
            foreach (Character allCharacter2 in Character.AllCharacters)
            {
                if (!((MonoBehaviourPun)allCharacter2).photonView.IsMine)
                {
                    ((MonoBehaviourPun)allCharacter2).photonView.RPC("RPCA_Fall", (RpcTarget)0, new object[1] { 7f });
                }
            }
        }

        private static void PassOutAll()
        {
            if (IncludeSelf)
            {
                foreach (Character allCharacter in Character.AllCharacters)
                {
                    ((MonoBehaviourPun)allCharacter).photonView.RPC("RPCA_PassOut", (RpcTarget)0, new object[0]);
                }
                return;
            }
            foreach (Character allCharacter2 in Character.AllCharacters)
            {
                if (!((MonoBehaviourPun)allCharacter2).photonView.IsMine)
                {
                    ((MonoBehaviourPun)allCharacter2).photonView.RPC("RPCA_PassOut", (RpcTarget)0, new object[0]);
                }
            }
        }

        private static void FlingAll()
        {
            if (IncludeSelf)
            {
                foreach (Character allCharacter in Character.AllCharacters)
                {
                    ((MonoBehaviourPun)allCharacter).photonView.RPC("RPCA_Fall", (RpcTarget)0, new object[1] { 7f });
                    ((MonoBehaviourPun)allCharacter).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                    ((MonoBehaviourPun)allCharacter).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                    ((MonoBehaviourPun)allCharacter).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                    ((MonoBehaviourPun)allCharacter).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                    ((MonoBehaviourPun)allCharacter).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                    ((MonoBehaviourPun)allCharacter).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                    ((MonoBehaviourPun)allCharacter).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                    ((MonoBehaviourPun)allCharacter).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                }
                return;
            }
            foreach (Character allCharacter2 in Character.AllCharacters)
            {
                if (!((MonoBehaviourPun)allCharacter2).photonView.IsMine)
                {
                    ((MonoBehaviourPun)allCharacter2).photonView.RPC("RPCA_Fall", (RpcTarget)0, new object[1] { 7f });
                    ((MonoBehaviourPun)allCharacter2).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                    ((MonoBehaviourPun)allCharacter2).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                    ((MonoBehaviourPun)allCharacter2).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                    ((MonoBehaviourPun)allCharacter2).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                    ((MonoBehaviourPun)allCharacter2).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                    ((MonoBehaviourPun)allCharacter2).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                    ((MonoBehaviourPun)allCharacter2).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                    ((MonoBehaviourPun)allCharacter2).photonView.RPC("JumpRpc", (RpcTarget)0, new object[1] { false });
                }
            }
        }

        private static void FullStamAll()
        {
            if (IncludeSelf)
            {
                foreach (Character allCharacter in Character.AllCharacters)
                {
                    ((MonoBehaviourPun)allCharacter).photonView.RPC("MoraleBoost", (RpcTarget)0, new object[2] { 1f, 0 });
                }
                return;
            }
            foreach (Character allCharacter2 in Character.AllCharacters)
            {
                if (!((MonoBehaviourPun)allCharacter2).photonView.IsMine)
                {
                    ((MonoBehaviourPun)allCharacter2).photonView.RPC("MoraleBoost", (RpcTarget)0, new object[2] { 1f, 0 });
                }
            }
        }

        private static void UnPassOutAll()
        {
            if (IncludeSelf)
            {
                foreach (Character allCharacter in Character.AllCharacters)
                {
                    ((MonoBehaviourPun)allCharacter).photonView.RPC("RPCA_UnPassOut", (RpcTarget)0, new object[0]);
                }
                return;
            }
            foreach (Character allCharacter2 in Character.AllCharacters)
            {
                if (!((MonoBehaviourPun)allCharacter2).photonView.IsMine)
                {
                    ((MonoBehaviourPun)allCharacter2).photonView.RPC("RPCA_UnPassOut", (RpcTarget)0, new object[0]);
                }
            }
        }


        public static void StealHost()
        {

            if (!PhotonNetwork.InRoom || PhotonNetwork.OfflineMode)
            {
                Debug.LogWarning("You're not in a Photon room.");
                return;
            }


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("You are already the Master Client.");
                return;
            }


            bool success = PhotonNetwork.CurrentRoom.SetMasterClient(PhotonNetwork.LocalPlayer);
            Debug.Log(success ? "Successfully stole host!" : "Failed to steal host.");
        }

        public static bool JoinRandomRoom()
        {
            return PhotonNetwork.JoinRandomRoom(null, 0, MatchmakingMode.FillRoom, null, null, null);
        }
        static string[] cachedPlayerKeys = new string[0];

        static string[] cachedKeys = new string[0];

        public static void RefreshPlayerDict()
        {
            playerDict = new Dictionary<string, Character>();

            foreach (Character character in GameObject.FindObjectsOfType<Character>())
            {
                if (character.player != null && character.player.photonView != null)
                {
                    string nickname = character.player.photonView.Owner.NickName;

                    if (!string.IsNullOrEmpty(nickname) && !playerDict.ContainsKey(nickname))
                    {
                        playerDict[nickname] = character;
                    }
                }
            }


           cachedPlayerKeys = playerDict.Keys.ToArray();
            cachedKeys = playerDict.Keys.ToArray();

            Debug.Log("Refreshed playerDict. Players found: " + playerDict.Count);
        }


        public static Character GetLocalPlayer()
        {
            if (global::Player.localPlayer == null)
                return null;

            return playerDict?.Values.FirstOrDefault(c =>
                c != null &&
                c.player == global::Player.localPlayer
            );
        } 
        
        public static Character GetLocalPlayerr()
        {
            if (global::Player.localPlayer == null)
                return null;

            return Character.AllCharacters.FirstOrDefault(c =>
                c != null &&
                c.player == global::Player.localPlayer
            );
        }




       

        private static void SpawnNetworkedJellyfish(Character charac)
        {
            // Create root GameObject with PhotonView and TriggerRelay
            GameObject root = new GameObject("JellyfishRoot");

            PhotonView pv = root.AddComponent<PhotonView>();
            pv.ViewID = GenerateFakePhotonViewID();
            pv.TransferOwnership(charac.photonView.ViewID);

            TriggerRelay relay = root.AddComponent<TriggerRelay>();

            GameObject child = new GameObject("SlipperyJellyfish");
            child.transform.SetParent(root.transform);

            var jelly = child.AddComponent<SlipperyJellyfish>();

            var col = child.AddComponent<SphereCollider>();
            col.isTrigger = true;

            root.transform.position = charac.refs.head.transform.position;

            pv.RPC("RPCA_TriggerWithTarget", RpcTarget.All, new object[]
            {
            root.transform.GetSiblingIndex(),
            charac.photonView.ViewID
            });

        }


        public static int GenerateFakePhotonViewID()
        {
            // WARNING: This is not safe in real multiplayer. Use actual PhotonNetwork.Instantiate() if possible.
            return UnityEngine.Random.Range(10000, 99999);
        }


        public static void CreatePlayersVerticalSelect()
        {
            if (playerDict == null || playerDict.Count == 0)
            {
                RefreshPlayerDict();
            }

            GUILayout.BeginVertical("box", GUILayout.Width(220));
            try
            {
                GUIStyle boldLabelStyle = new GUIStyle(UnityEngine.GUI.skin.label)
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 13,
                    alignment = TextAnchor.UpperLeft
                };
                GUILayout.Label("Players:", boldLabelStyle);

                GUIStyle searchStyle = new GUIStyle(UnityEngine.GUI.skin.textField) { fontSize = 12 };
                GUILayout.Space(4);
                playerSearchBuffer = GUILayout.TextField(playerSearchBuffer, searchStyle);
                GUILayout.Space(6);

                // Use cachedKeys here, not keys variable inside method!
                string[] filteredKeys = cachedKeys
                    .Where(name => string.IsNullOrEmpty(playerSearchBuffer) || name.ToLower().Contains(playerSearchBuffer.ToLower()))
                    .ToArray();

                if (filteredKeys.Length == 0)
                {
                    GUILayout.Label("No matching players.");
                }

                GUIStyle playerButtonStyle = new GUIStyle(UnityEngine.GUI.skin.button)
                {
                    fontSize = 12,
                    fixedHeight = 28,
                    alignment = TextAnchor.MiddleLeft,
                    margin = new RectOffset(4, 4, 2, 2)
                };

                playerScrollPos = GUILayout.BeginScrollView(playerScrollPos, GUILayout.Height(250));

                for (int i = 0; i < filteredKeys.Length; i++)
                {
                    string playerName = filteredKeys[i];
                    UnityEngine.GUI.backgroundColor = (playerName == cachedKeys.ElementAtOrDefault(selectedPlayerIndex)) ? Color.grey : Color.white;

                    if (GUILayout.Button(playerName, playerButtonStyle))
                    {
                        selectedPlayerIndex = Array.IndexOf(cachedKeys, playerName);
                    }
                }

                UnityEngine.GUI.backgroundColor = Color.white;
                GUILayout.EndScrollView();

                if (selectedPlayerIndex >= 0 && selectedPlayerIndex < cachedKeys.Length)
                {
                    GUIStyle infoBoxStyle = new GUIStyle(UnityEngine.GUI.skin.box)
                    {
                        fontSize = 11,
                        alignment = TextAnchor.MiddleLeft,
                        padding = new RectOffset(6, 6, 4, 4)
                    };

                    GUILayout.Space(5);
                    GUILayout.Label("Selected: " + cachedKeys[selectedPlayerIndex], infoBoxStyle);
                }
            }
            finally
            {
                GUILayout.EndVertical();
            }
        }


        public static void CreateItemsVerticalSelect()
        {
            GUILayout.BeginVertical("box", GUILayout.Width(220));
            try
            {
                GUIStyle boldLabelStyle = new GUIStyle(UnityEngine.GUI.skin.label)
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 13,
                    alignment = TextAnchor.UpperLeft
                };
                GUILayout.Label("Items:", boldLabelStyle);

                if (items == null || items.Length == 0)
                {
                    GUILayout.Label("No items available.");
                    return;
                }

                GUIStyle itemButtonStyle = new GUIStyle(UnityEngine.GUI.skin.button)
                {
                    fontSize = 12,
                    fixedHeight = 28,
                    alignment = TextAnchor.MiddleLeft,
                    margin = new RectOffset(4, 4, 2, 2)
                };

                int totalPages = Mathf.CeilToInt(items.Length / (float)itemsPerPage);
                int startIndex = currentPage * itemsPerPage;
                int endIndex = Mathf.Min(startIndex + itemsPerPage, items.Length);

                itemScrollPos = GUILayout.BeginScrollView(itemScrollPos, false, true, GUILayout.Height(250));

                for (int i = startIndex; i < endIndex; i++)
                {
                    string item = items[i];
                    UnityEngine.GUI.backgroundColor = (i == selectedItemIndex) ? Color.green : Color.white;

                    if (GUILayout.Button(item, itemButtonStyle))
                    {
                        selectedItemIndex = i;
                    }
                }

                UnityEngine.GUI.backgroundColor = Color.white;
                GUILayout.EndScrollView();

                if (selectedItemIndex >= 0 && selectedItemIndex < items.Length)
                {
                    GUIStyle infoBoxStyle = new GUIStyle(UnityEngine.GUI.skin.box)
                    {
                        fontSize = 11,
                        alignment = TextAnchor.MiddleLeft,
                        padding = new RectOffset(6, 6, 4, 4)
                    };

                    GUILayout.Space(5);
                    GUILayout.Label("Selected: " + items[selectedItemIndex], infoBoxStyle);
                }

                // Pagination controls
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("<", GUILayout.Width(25)) && currentPage > 0)
                    currentPage--;

                GUILayout.Label($"Page {currentPage + 1} / {Mathf.Max(1, totalPages)}", GUILayout.Width(100));

                if (GUILayout.Button(">", GUILayout.Width(25)) && (currentPage + 1) * itemsPerPage < items.Length)
                    currentPage++;
                GUILayout.EndHorizontal();
            }
            finally
            {
                GUILayout.EndVertical();
            }
        }





        public static void CreateLuggageVerticalSelect()
        {
            float fullWidth = Screen.width - 40f; 
            float halfWidth = fullWidth / 2f;

            GUILayout.BeginHorizontal();

            // LEFT PANEL: Luggage List & Controls
            GUILayout.BeginVertical(GUILayout.Width(halfWidth));
            {
                GUILayout.Label("Luggage List");
                GUILayout.Space(5);

                Cheat.EnsureLuggageListInitialized();

                GUILayout.BeginVertical("box");
                {
                    if (Cheat.luggageLabels == null || Cheat.luggageLabels.Count == 0)
                    {
                        GUILayout.Label("No luggage found.");
                        Cheat.selectedLuggageIndex = -1;
                    }
                    else
                    {
                        GUILayout.Label("Select Container:");
                        luggageScrollPos = GUILayout.BeginScrollView(luggageScrollPos, GUILayout.Height(150));

                        int newIndex = GUILayout.SelectionGrid(
                            Cheat.selectedLuggageIndex,
                            Cheat.luggageLabels.ToArray(),
                            1,
                            GUILayout.ExpandWidth(true)
                        );

                        if (newIndex != Cheat.selectedLuggageIndex)
                        {
                            Cheat.selectedLuggageIndex = newIndex;
                        }

                        GUILayout.EndScrollView();

                        GUILayout.Space(5);
                        GUILayout.Label("Selected: " + (Cheat.selectedLuggageIndex >= 0 && Cheat.selectedLuggageIndex < Cheat.luggageLabels.Count
                            ? Cheat.luggageLabels[Cheat.selectedLuggageIndex]
                            : "None"));
                    }

                    GUILayout.Space(5);
                    if (GUILayout.Button("Refresh Luggage List"))
                    {
                        Cheat.hasInitializedLuggageList = false;
                        Cheat.RefreshLuggageList();
                    }

                    GUILayout.Space(10);
                    if (GUILayout.Button("Open All Nearby Luggage"))
                    {
                        Cheat.OpenAllNearbyLuggage();
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndVertical();

            GUILayout.Space(10);

           
            GUILayout.BeginVertical(GUILayout.Width(halfWidth));
            {
                GUILayout.Label("Actions");
                GUILayout.Space(5);

                if (Cheat.selectedLuggageIndex >= 0 && Cheat.selectedLuggageIndex < Cheat.luggageObject.Count)
                {
                    GUILayout.Label("Luggage: " + Cheat.luggageLabels[Cheat.selectedLuggageIndex]);

                    if (GUILayout.Button("Warp To Luggage"))
                    {
                        Vector3 coords = Cheat.luggageObject[Cheat.selectedLuggageIndex].Center();
                        coords.y += 1.5f;
                        Cheat.TeleportToCoords(coords.x, coords.y, coords.z);
                    }

                    if (GUILayout.Button("Open Luggage"))
                    {
                        Cheat.OpenLuggage(Cheat.selectedLuggageIndex);
                    }
                }
                else
                {
                    GUILayout.Label("No luggage selected.");
                }
            }
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
        }








        public static void CreateSpawnItemButtons()
        {
            if (GUILayout.Button("Spawn item on player"))
            {
                var localPlayer = GetLocalPlayer();
                if (localPlayer == null)
                {
                    //RefreshPlayerDict();
                    Debug.LogWarning("No local player found for spawning item");
                    return;
                }

                PhotonNetwork.Instantiate(
                    "0_Items/" + items[selectedItemIndex],
                    localPlayer.Center + Vector3.up * 3f,
                    Quaternion.identity, 0, null
                ).GetComponent<Item>().Interact(localPlayer);
            }

            if (GUILayout.Button("Spawn item on everyone"))
            {
                foreach (var charac in playerDict.Values)
                {
                    if (charac == null) continue;

                    PhotonNetwork.Instantiate(
                        "0_Items/" + items[selectedItemIndex],
                        charac.Center + Vector3.up * 3f,
                        Quaternion.identity, 0, null
                    ).GetComponent<Item>().Interact(charac);
                }
            }
        }









        public static void KillSelectedPlayerWithTornado()
        {
            if (SelectedPlayer == null || SelectedPlayer.player == null || SelectedPlayer.photonView == null)
            {
                Debug.LogWarning("Selected player is null or missing required components.");
                return;
            }

           
            Vector3 spawnPosition = SelectedPlayer.Center + Vector3.up * 0.5f; 

            GameObject tornadoObj = PhotonNetwork.Instantiate("Tornado", spawnPosition, Quaternion.identity, 0);

            PhotonView tornadoView = tornadoObj.GetComponent<PhotonView>();
            if (tornadoView != null)
            {
                tornadoView.RPC("RPCA_InitTornado", RpcTarget.All, SelectedPlayer.photonView.ViewID);
            }
        }


        public static void KillPlayerWithTornado(string playerName)
        {
            if (!playerDict.ContainsKey(playerName))
            {
                Debug.LogWarning($"Player '{playerName}' not found in playerDict.");
                return;
            }

            Character targetCharacter = playerDict[playerName];
            PhotonView targetView = targetCharacter.player.photonView;

            if (targetView == null)
            {
                Debug.LogWarning($"PhotonView for player '{playerName}' is null.");
                return;
            }

           
            GameObject tornadoObj = PhotonNetwork.Instantiate("Tornado", targetCharacter.transform.position + Vector3.up * 2f, Quaternion.identity, 0);

            
            PhotonView tornadoView = tornadoObj.GetComponent<PhotonView>();
            tornadoView.RPC("RPCA_InitTornado", RpcTarget.All, targetView.ViewID);
        }


      












       


        private static Texture2D _whiteTex;

        private static void DrawRoundedBox(Rect rect, Color color, float borderRadius = 4f)
        {
            if (_whiteTex == null)
            {
                _whiteTex = new Texture2D(1, 1);
                _whiteTex.SetPixel(0, 0, Color.white);
                _whiteTex.Apply();
            }

            Color originalColor = UnityEngine.GUI.color;
            UnityEngine.GUI.color = color;
            UnityEngine.GUI.DrawTexture(rect, _whiteTex);
            UnityEngine.GUI.color = originalColor;
        }

        public static void DrawWatermark()
        {
            if (!showWatermark)
                return;

            // 🌈 Rainbow cheat name
            float r = Mathf.PingPong(timer * colorChangeSpeed, 1f);
            float g = Mathf.PingPong(timer * colorChangeSpeed + 0.33f, 1f);
            float b = Mathf.PingPong(timer * colorChangeSpeed + 0.66f, 1f);
            Color rainbow = new Color(r, g, b);

            string time = DateTime.Now.ToString("hh:mm:ss tt");
            string playerName = Photon.Pun.PhotonNetwork.LocalPlayer != null ? Photon.Pun.PhotonNetwork.LocalPlayer.NickName : "Unknown";

            string watermarkText = $"<b><color=#{ColorUtility.ToHtmlStringRGB(rainbow)}>{cheatName}</color></b> | {time}";

            if (showPing && Photon.Pun.PhotonNetwork.IsConnected)
            {
                int ping = Photon.Pun.PhotonNetwork.GetPing();
                watermarkText += $" | Ping: {ping}ms";
            }


            //if (showPing && Photon.Pun.PhotonNetwork.IsConnected)
            //{
            //    int ping = Photon.Pun.PhotonNetwork.GetPing();
            //    string serverAddress = Photon.Pun.PhotonNetwork.NetworkingClient != null
            //        ? Photon.Pun.PhotonNetwork.NetworkingClient.CurrentServerAddress
            //        : "<unknown>";

            //    string cloudRegion = (Photon.Pun.PhotonNetwork.NetworkingClient != null &&
            //                          Photon.Pun.PhotonNetwork.IsConnected &&
            //                          Photon.Pun.PhotonNetwork.Server != ServerConnection.NameServer)
            //        ? Photon.Pun.PhotonNetwork.NetworkingClient.CloudRegion
            //        : "<none>";

            //    watermarkText += $" | Ping: {ping}ms | Server: {serverAddress} | Region: {cloudRegion}";
            //}


            if (Photon.Pun.PhotonNetwork.IsConnected && Photon.Pun.PhotonNetwork.NetworkingClient != null)
            {
                int playersInRooms = Photon.Pun.PhotonNetwork.NetworkingClient.PlayersInRoomsCount;
                int playersOnMaster = Photon.Pun.PhotonNetwork.NetworkingClient.PlayersOnMasterCount;
                int totalPlayers = playersInRooms + playersOnMaster;
                int rooms = Photon.Pun.PhotonNetwork.NetworkingClient.RoomsCount;

                watermarkText += $" | Players: {playersInRooms} / {playersOnMaster} / {totalPlayers} | Rooms: {rooms}";

                // Master client
                var masterClient = Photon.Pun.PhotonNetwork.MasterClient;
                if (masterClient != null)
                {
                    string masterName = masterClient.NickName.Replace("\n", "").Replace("\r", "");
                    if (masterName.Length > 20)
                        masterName = masterName.Substring(0, 17) + "...";

                    watermarkText += $" | Master: {masterName}";
                }

                // Room (Game ID)
                if (Photon.Pun.PhotonNetwork.InRoom)
                {
                    string roomName = Photon.Pun.PhotonNetwork.CurrentRoom?.Name ?? "<unnamed>";
                    watermarkText += $" | Room: {roomName}";
                }

                // Lobby
                var currentLobby = Photon.Pun.PhotonNetwork.NetworkingClient.CurrentLobby;
                if (currentLobby != null && !string.IsNullOrEmpty(currentLobby.Name))
                {
                    watermarkText += $" | Lobby: {currentLobby.Name} ({currentLobby.Type})";
                }
                else
                {
                    watermarkText += $" | Lobby: <none>";
                }
            }

            GUIStyle style = new GUIStyle(UnityEngine.GUI.skin.label)
            {
                fontSize = 14,
                richText = true,
                wordWrap = false,  
                alignment = TextAnchor.MiddleLeft,
                clipping = TextClipping.Clip,
                normal = { textColor = Color.white }
            };

            GUIContent content = new GUIContent(watermarkText);

            // Calculate width and height
            float paddingX = 15f;
            float paddingY = 8f;
            float maxWidth = 900f;

            float width = Mathf.Min(style.CalcSize(content).x, maxWidth);
            float height = style.CalcHeight(content, width);

            Rect backgroundRect = new Rect(10, 10, width + paddingX * 2f, height + paddingY * 2f);
            Rect textRect = new Rect(10 + paddingX, 10 + paddingY, width, height);

            DrawRoundedBox(backgroundRect, new Color(0f, 0f, 0f, 0.6f), 6f);
            UnityEngine.GUI.Label(textRect, content, style);
        }



        private static string searchQuery = string.Empty;
        private static int itemsPerPage = 25;
        private static int currentPage = 0;
        public static void GUIMain(int o) 
        {
            GUILayout.BeginArea(new Rect(10, 10, GUIRect.width - 20, GUIRect.height - 20));
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            for (int i = 0; i < tabnames.Length; i++)
            {
                if (GUILayout.Toggle(selected_tab == i, tabnames[i], "Button"))
                {
                    selected_tab = i;
                }
            }

            GUILayout.EndHorizontal();


            // Vector2 itemScrollPos = Vector2.zero;
            switch (selected_tab)
            {


                case 0:

                   // string searchQuery = string.Empty;

                    Begin();
                    NewSection("Items");

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Search Items:");
                    searchQuery = GUILayout.TextField(searchQuery, GUILayout.Width(200f));
                    GUILayout.EndHorizontal();
                    ScrollPos = GUILayout.BeginScrollView(ScrollPos, (GUILayoutOption[])(object)new GUILayoutOption[2]
                    {
                GUILayout.Width(275f),
                GUILayout.Height(400f)
                    });




                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        
                        foreach (KeyValuePair<ushort, Item> item3 in SingletonAsset<ItemDatabase>.Instance.itemLookup)
                        {
                            Item item2 = item3.Value;
                            ushort id2 = item3.Key;

                            string itemName = (UseItemsInGameName) ? item2.GetItemName(null) : ((UnityEngine.Object)item2).name;

                           
                            if (itemName.ToLower().Contains(searchQuery.ToLower()))
                            {
                                NewButton($"{itemName} [{id2}]", delegate
                                {
                                    SelectItem(id2, ((UnityEngine.Object)item2).name, item2);
                                });
                            }
                        }
                    }
                    else
                    {
                       
                        if (!UseItemsInGameName)
                        {
                            foreach (KeyValuePair<ushort, Item> item3 in SingletonAsset<ItemDatabase>.Instance.itemLookup)
                            {
                                Item item2 = item3.Value;
                                ushort id2 = item3.Key;
                                NewButton($"{((UnityEngine.Object)item2).name} [{id2}]", delegate
                                {
                                    SelectItem(id2, ((UnityEngine.Object)item2).name, item2);
                                });
                            }
                        }
                        else
                        {
                            foreach (KeyValuePair<ushort, Item> item4 in SingletonAsset<ItemDatabase>.Instance.itemLookup)
                            {
                                Item item = item4.Value;
                                ushort id = item4.Key;
                                NewButton($"{item.GetItemName((ItemInstanceData)null)} [{id}]", delegate
                                {
                                    SelectItem(id, ((UnityEngine.Object)item).name, item);
                                });
                            }
                        }
                    }
                    //foreach (KeyValuePair<ushort, Item> item2 in SingletonAsset<ItemDatabase>.Instance.itemLookup)
                    //{
                    //    Item item = item2.Value;
                    //    ushort id = item2.Key;
                    //    NewButton($"{((UnityEngine.Object)item).name} [{id}]", delegate
                    //    {
                    //        SelectItem(id, ((UnityEngine.Object)item).name, item);
                    //    });
                    //}
                    GUILayout.EndScrollView();
                    NewButton("Spawn " + SItemName, SpawnItem2);
                    NewButton("Spawn " + SItemName + " on everyone", SpawnItemOnEveryone);
                   

                    
                    // SpawnInHand = NewToggle(SpawnInHand, "Spawn In Inventory");
                    //UseItemsInGameName = NewToggle(UseItemsInGameName, "Use Items In Game Name");
                    Separate();

                    NewSection("Others");
                    ScrollPos1 = GUILayout.BeginScrollView(ScrollPos1, (GUILayoutOption[])(object)new GUILayoutOption[2]
                    {
                GUILayout.Width(275f),
                GUILayout.Height(400f)
                    });
                    UseItemsInGameName = NewToggle(UseItemsInGameName, "Use Items In Game Name");
                   // NewButton("Remove All Items", RemoveItems);
                    NewButton("Remove My Items", Cheat.RemoveMyItems);
                   // NewButton("Remove All Flags", RemoveFlags);
                    SpawnInHand = NewToggle(SpawnInHand, "Spawn Item In Empty Slot");
                    GUI.gravityGun = NewToggle(GUI.gravityGun, "Gravity Gun F");
                    //FreezeItems = NewToggle(FreezeItems, "Freeze All Items");
                    DrawItem = NewToggle(DrawItem, "Draw With Selected Items");
                  //  SpinItems = NewToggle(SpinItems, "Spin All Items");
                    GUI.spawnflags = NewToggle(GUI.spawnflags, "Flag Gun  H/Key");
                   // SpinSpeed = Mathf.Round(NewSlider("Spin Speed", SpinSpeed, 0f, 100f));
                    ItemCookLevel = Mathf.Round(NewSlider("Item Cook Level", ItemCookLevel, 0f, 4f));
                    NewButton("Cook All Items", CookAllObjects);
                    NewButton("Finish Cooking All Items", FinishCooking);
                    NewButton("Enable Smoke On All Items", delegate
                    {
                        SmokeAllItems(v: true);
                    });


                    NewButton("Disable Smoke On All Items", delegate
                    {
                        SmokeAllItems(v: false);
                    });



                    GUILayout.EndScrollView();
                    End();

                    //var lookup = SingletonAsset<ItemDatabase>.Instance.itemLookup;
                    //var items = lookup.ToList();
                    //int totalPages = Mathf.CeilToInt(items.Count / (float)itemsPerPage);

                    //GUILayout.BeginVertical("box", GUILayout.Width(275));
                    //scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width(275), GUILayout.Height(400));

                    //int startIndex = currentPage * itemsPerPage;
                    //int endIndex = Mathf.Min(startIndex + itemsPerPage, items.Count);

                    //for (int i = startIndex; i < endIndex; i++)
                    //{
                    //    var kvp = items[i];
                    //    string itemName = ((UnityEngine.Object)kvp.Value).name;
                    //    ushort id = kvp.Key;
                    //    Item item = kvp.Value;

                    //    GUIStyle style = new GUIStyle(UnityEngine.GUI.skin.button);
                    //    if (SItemIndex == i)
                    //        style.normal.textColor = Color.green;

                    //    if (GUILayout.Button($"{itemName} [{id}]", style))
                    //    {
                    //        SelectItem(i, itemName, item);
                    //    }
                    //}

                    //GUILayout.EndScrollView();

                    //GUILayout.Space(5);
                    //GUILayout.BeginHorizontal();

                    //if (GUILayout.Button("<") && currentPage > 0)
                    //    currentPage--;

                    //GUILayout.Label($"Page {currentPage + 1} / {totalPages}");

                    //if (GUILayout.Button(">") && currentPage < totalPages - 1)
                    //    currentPage++;

                    //GUILayout.EndHorizontal();

                    //GUILayout.Space(10);
                    //if (SItemIndex >= 0 && !string.IsNullOrEmpty(SItemName))
                    //{
                    //    GUILayout.Label("Selected: " + SItemName);
                    //    if (GUILayout.Button("Spawn " + SItemName))
                    //    {
                    //        GUI.SpawnItem2(); // or SpawnItem()
                    //    }
                    //}
                    //else
                    //{
                    //    GUILayout.Label("No item selected.");
                    //}

                    //SpawnInHand = DrawToggleButton(SpawnInHand, "Spawn In Inventory");
                    //GUILayout.EndVertical();






                    // GUILayout.EndScrollView();
                    break;
                case 1:


                    Begin();
                    NewSection("ESP");

                    silentaim = DrawToggleButton(silentaim, "Silent Aim");
                    crasher = DrawToggleButton(crasher, "Crash Players in FOV Circle (Middle mouse click)");

                    testting = DrawToggleButton(testting, "Weird Feature press J");

                    boxesp = DrawToggleButton(boxesp, "Box ESP");

                    Tracers = DrawToggleButton(Tracers, "Tracers");
                    PlayerTags = DrawToggleButton(PlayerTags, "PlayerTags");
                    LuggageEsp = DrawToggleButton(LuggageEsp, "Luggage ESP");
                    ItemEsp = DrawToggleButton(ItemEsp, "Items ESP might be laggy lot of items");


                    GUILayout.Label("Username");
                    localPlayerName = GUILayout.TextField(localPlayerName);
                    if (GUILayout.Button("Save username"))
                    {
                        //File.WriteAllText(filePath, localPlayerName);

                        PhotonNetwork.NetworkingClient.LocalPlayer.NickName = localPlayerName;
                        // RefreshPlayerDict();
                        Cheat.RespawnCharacter();

                    }

                    if (GUILayout.Button("Hyro Name"))
                    {
                        //File.WriteAllText(filePath, localPlayerName);

                        CherryName();
                        Cheat.RespawnCharacter();

                    }




                    GUILayout.Label("Lobby ID");
                    steamId = GUILayout.TextField(steamId);
                    if (GUILayout.Button("Join Lobby"))
                    {
                        CSteamID lobbyId = new CSteamID(ulong.Parse(steamId));
                        Cheat.steamLobbyHandler.TryJoinLobby(lobbyId);
                    }



                    if (GUILayout.Button("Spawn Stove"))
                    {
                        PhotonNetwork.Instantiate("PortableStovetop_Placed", Camera.main.transform.position, Camera.main.transform.rotation);
                    }

                    if (GUILayout.Button("Spawn BeeSwarm"))
                    {
                        PhotonNetwork.Instantiate("BeeSwarm", Camera.main.transform.position, Camera.main.transform.rotation);
                    }

                    fovcircle = NewToggle(fovcircle, "FOV Circle");

                    GUILayout.Label("FOV: " + fov);
                    fov = GUILayout.HorizontalSlider(fov, 1.0f, 1440.0f);

                    Separate();
                    NewSection("Menu");

                    showWatermark = DrawToggleButton(showWatermark, "WaterMark");





                    End();


                    break;
                case 2:

                    Begin();
                    NewSection("Self");
                    ScrollPos = GUILayout.BeginScrollView(ScrollPos, (GUILayoutOption[])(object)new GUILayoutOption[2]
                    {
                GUILayout.Width(275f),
                GUILayout.Height(400f)
                    });

                    Cheat.thirdPersonEnabled = NewToggle(Cheat.thirdPersonEnabled, "Third Person Camera F7");

                    IJump = NewToggle(IJump, "Infinite Jump");
                    godmode = DrawToggleButton(godmode, "Inf Stamina");
                    randomoutfits = DrawToggleButton(randomoutfits, "Randomize Outifts glitchy");
                    infiniteammo = DrawToggleButton(infiniteammo, "Revive Yourself");

                    clearStatuses = DrawToggleButton(clearStatuses, "ClearStatus/godmode");

                    Unlockall = DrawToggleButton(Unlockall, "Unlock All");
                    InstantItemUse = NewToggle(InstantItemUse, "Instant Item Use");
                    ICharge = NewToggle(ICharge, "Infinite Item Charge");
                    IRope = NewToggle(IRope, "Infinite Rope");
                    AntiConsume = NewToggle(AntiConsume, "Sticky Fingers");

                    NoD = NewToggle(NoD, "Block Deaths");
                    NoS = NewToggle(NoS, "No Slip");
                    NoR = NewToggle(NoR, "No Ragdoll");
                    NoP = NewToggle(NoP, "No Passout");

                    NoSL = NewToggle(NoSL, "Test PeakAnticheat SoftLock");
                    NoDstry = NewToggle(NoDstry, "No Destroy");
                    /// GUILayout.Space(5);
                    // rapidfire = DrawToggleButton(rapidfire, "TimeModifier");




                    GUI.flyMode = NewToggle(GUI.flyMode, "Fly Mode");

                    ClimbSpeed = DrawToggleButton(ClimbSpeed, "ClimbSpeed");

                    if (ClimbSpeed)
                    {
                        ClimbSpeed2 = NewSlider("Climb Speed Base", ClimbSpeed2, 4.0f, 20.0f);
                        ClimbSpeedForce = NewSlider("Climb Speed Multiplier", ClimbSpeedForce, 1.0f, 5.0f);
                    }

                    speed = NewToggle(speed, "Speed");
                    speedmultiply = NewSlider("Speed", speedmultiply, 1.0f, 15.0f);

                    InstantThrow = DrawToggleButton(InstantThrow, "InstantThrow");

                    CThrowPower = DrawToggleButton(CThrowPower, "ThrowPower");

                    GUILayout.Label("Throw Power: " + ThrowPower.ToString("F1"));
                    ThrowPower = GUILayout.HorizontalSlider(ThrowPower, 0f, 100f);

                    GUI.Deaggro = DrawToggleButton(GUI.Deaggro, "Deaggro Bees");

                   


                    // GUI.gravityGun = DrawToggleButton(GUI.gravityGun, "GravityGun F");

                    //if (GUILayout.Button("Remove Items"))
                    //{
                    //    Cheat.RemoveMyItems();
                    //}

                    //GUI.DrawItem = DrawToggleButton(GUI.DrawItem, "Draw Item");



                   // NewSection("Special Day Zone Colors");

                   


                    GUI.Noclip = DrawToggleButton(GUI.Noclip, "Noclip");
                   


                    GUI.ClickToTp = DrawToggleButton(GUI.ClickToTp, "ClickTP");
                    GUI.PingWithAll = DrawToggleButton(GUI.PingWithAll, "Ping All");

                    BlowdartShotGun = NewToggle(BlowdartShotGun, "Shoot Blowdart On Mouse Click");



                  



                    GUILayout.Space(10);
                    GUILayout.Label("G Noclip F pickup items:");


                    GUILayout.EndScrollView();
                    Separate();
                    NewSection("Other");



                    topColor = DrawColorEdit3("Top Color", topColor, ref showTopPicker);
                    midColor = DrawColorEdit3("Mid Color", midColor, ref showMidPicker);
                    bottomColor = DrawColorEdit3("Bottom Color", bottomColor, ref showBottomPicker);
                    sunColor = DrawColorEdit3("Sun Color", sunColor, ref showSunPicker);

                    NewButton("Apply Colors to Zone", () =>
                    {
                        SpecialDayZone zone = UnityEngine.Object.FindObjectOfType<SpecialDayZone>();
                        if (zone != null)
                        {
                            ChangeZoneColors(zone, topColor, midColor, bottomColor, sunColor, 300f);
                        }
                    });



                    NewButton("Log Rpc", () =>
                    {
                        PhotonNetwork.PhotonServerSettings.RpcList.ToList().ForEach(r => Debug.Log(r));

                    });


                    End();




                    break;

                case 3:


                    Begin();
                    NewSection("Lobby");
                   // ScrollPos = GUILayout.BeginScrollView(ScrollPos, (GUILayoutOption[])(object)new GUILayoutOption[2]
                    //{
                //GUILayout.Width(275f),
                //GUILayout.Height(400f)
                  //  });
                    NewLabel("-- Join Handler Options --");
                    NewButton("[SM] Set Join Scene To Pretitle", delegate
                    {
                        SetJoinScene("Pretitle");
                    });
                    NewButton("[SM] Set Join Scene To Title", delegate
                    {
                        SetJoinScene("Title");
                    });
                    NewButton("[SM] Set Join Scene To MainMenu", delegate
                    {
                        SetJoinScene("MainMenu");
                    });
                    NewButton("[SM] Set Join Scene To WilIsland", delegate
                    {
                        SetJoinScene("WilIsland");
                    });
                    NewButton("[SM] Set Join Scene To Airport", delegate
                    {
                        SetJoinScene("Airport");
                    });
                    NewButton("[SM] Set Join Scene To Level_0", delegate
                    {
                        SetJoinScene("Level_0");
                    });
                    NewButton("[SM] Set Join Scene To Level_1", delegate
                    {
                        SetJoinScene("Level_1");
                    });
                    NewButton("[SM] Set Join Scene To Level_2", delegate
                    {
                        SetJoinScene("Level_2");
                    });
                    NewButton("[SM] Set Join Scene To Level_3", delegate
                    {
                        SetJoinScene("Level_3");
                    });
                    NewButton("[SM] Set Join Scene To Level_4", delegate
                    {
                        SetJoinScene("Level_4");
                    });
                    NewButton("[SM] Set Join Scene To Level_5", delegate
                    {
                        SetJoinScene("Level_5");
                    });
                    NewButton("[SM] Set Join Scene To Level_6", delegate
                    {
                        SetJoinScene("Level_6");
                    });
                    NewButton("[SM] Set Join Scene To Level_7", delegate
                    {
                        SetJoinScene("Level_7");
                    });
                    NewButton("[SM] Set Join Scene To Level_8", delegate
                    {
                        SetJoinScene("Level_8");
                    });
                    NewButton("[SM] Set Join Scene To Level_9", delegate
                    {
                        SetJoinScene("Level_9");
                    });
                    NewButton("[SM] Set Join Scene To Level_10", delegate
                    {
                        SetJoinScene("Level_10");
                    });
                    NewButton("[SM] Set Join Scene To Level_11", delegate
                    {
                        SetJoinScene("Level_11");
                    });
                    NewButton("[SM] Set Join Scene To Level_12", delegate
                    {
                        SetJoinScene("Level_12");
                    });
                    NewButton("[SM] Set Join Scene To Level_13", delegate
                    {
                        SetJoinScene("Level_13");
                    });
                    //VersionText = NewTextField(VersionText);
                   // NewButton("[SM] Set Room Version Requirement", SetJoinVers);
                    NewLabel("--------------------------");


                  //  GUILayout.EndScrollView();
                    //Separate();
                    //NewSection("Information");
                    //NewLabel("Count Of Rooms: " + PhotonNetwork.CountOfRooms);
                    //NewLabel("Players In Rooms: " + PhotonNetwork.CountOfPlayersInRooms);
                    //NewLabel("Ping: " + PhotonNetwork.GetPing());
                    End();

                    //DrawPlayerTab();


                    //if (GUILayout.Button("Acquire players"))
                    //{
                    //    playerDict.Clear();
                    //    foreach (Character charac in PlayerHandler.GetAllPlayerCharacters())
                    //    {
                    //        playerDict.Add(charac.player.photonView.Owner.NickName, charac);
                    //        if (charac.IsLocal)
                    //        {
                    //            localPlayerName = charac.player.photonView.Owner.NickName;
                    //        }
                    //    }

                    //    if (playerDict.Count > 0)
                    //    {

                    //    }
                    //    else
                    //    {

                    //    }

                    //}






                    break;


                case 4:

                   

                    Begin();
                    NewSection("Troll Lobby");

                    ScrollPos = GUILayout.BeginScrollView(ScrollPos, (GUILayoutOption[])(object)new GUILayoutOption[2]
                   {
                GUILayout.Width(275f),
                GUILayout.Height(400f)
                   });
                    GUILayout.Label("Troll Options Lobby:");

                    IncludeSelf = NewToggle(IncludeSelf, "Include Self");

                    GUILayout.Space(8);

                    // Player Actions
                    GUILayout.Label("Player Actions:");



                    NewButton("Warp Everyone to Me", () =>
                    {
                        WarpAllPlayersToMe();
                    });

                    NewButton("Revive Everyone", () =>
                    {
                        foreach (var character in Character.AllCharacters)
                        {
                            if (character != null && character.photonView != null)
                            {
                                Vector3 revivePos = character.Ghost != null
                                    ? character.Ghost.transform.position
                                    : character.Head;

                                Vector3 finalRevivePos = revivePos + new Vector3(0f, 4f, 0f);

                                character.photonView.RPC("RPCA_ReviveAtPosition", RpcTarget.All, new object[] { finalRevivePos, false });
                            }
                        }
                    });

                    NewButton("Explode All", ExplodeAll);
                    NewButton("Blowdart Shot All", BlowdartHitAll);
                    NewButton("Shake All Screen", ShakeAllScreen);
                    NewButton("Jump All", JumpAll);
                    NewButton("Slip All", SlipAll);
                    NewButton("Fling All", FlingAll);
                    NewButton("Pass Out All", PassOutAll);
                    NewButton("UnPass Out All", UnPassOutAll);
                    NewButton("Fall All", FallAll);
                    NewButton("Full Stamina All", FullStamAll);
                    NewButton("Swarm Bees All", beezall);

                    NewButton("Spawn SlipperyJellyfish on everyone", delegate
                    {
                        if (PlayerHandler.GetAllPlayerCharacters().Count == 0) return;
                        foreach (Character charac in PlayerHandler.GetAllPlayerCharacters())
                        {
                            SpawnNetworkedJellyfish(charac);
                        }
                    });
                   


                    

                    // Environment Actions
                    GUILayout.Label("Environment Actions:");
                    NewButton("Make All Icecicles Fall", Cheat.IcecicleFall);
                    NewButton("Make All Bridges Fall", Cheat.Bridge);
                    NewButton("Force All Ghost To Me", Cheat.ForceME);
                    NewButton("Clear All Status", Cheat.clearallthins);
                    NewButton("Start Moving Orb Fog", initfog);
                    NewButton("Render All Dead", RenderAllDead);


                    NewButton("Enable Wind", delegate
                    {
                        ToggleWind(true);
                    });
                    NewButton("Disable Wind", delegate
                    {
                        ToggleWind(false);
                    });
                    NewButton("Start Lava Rise", delegate
                    {
                        StartLavaRise();
                    });

                    Cheat.TimeOfDay = Mathf.Round(GUILayout.HorizontalSlider(Cheat.TimeOfDay, 1f, 10f));


                    GUILayout.Label("Time Slider: " + fireratecooldown.ToString("0"));


                    if (GUILayout.Button("Set Time Of Day"))
                    {
                        Cheat.SetTimeOfDay();
                    }

                    GUILayout.EndScrollView();
                    Separate();
                    NewSection("Other");

                    // Spam Toggles
                    GUILayout.Label("Spam Toggles:");
                    BingbongSpam = DrawToggleButton(BingbongSpam, "BingBong Spam");
                    PassPortSpam = DrawToggleButton(PassPortSpam, "PassPort Spam");

                    GUILayout.Space(20);

                    // Teleport Locations
                    GUILayout.Label("Teleport Locations:");

                    if (GUILayout.Button("Teleport to Spot 1"))
                        TeleportTo(new Vector3(-12.57f, 285.3f, 100.53f));

                    if (GUILayout.Button("Teleport to Spot 2"))
                        TeleportTo(new Vector3(-14.7f, 569.9f, 526f));

                    if (GUILayout.Button("Teleport to Spot 3"))
                        TeleportTo(new Vector3(0f, 861.8f, 1208.2f));

                    GUILayout.Space(20);

                    // Network Actions
                    GUILayout.Label("Network Actions:");
                   // NewButton("Join Random Room", JoinRandomRoom);
                    NewButton("Steal Host", StealHost);
                    NewButton("Force All Done End Screen", ForceAllDone);

                    NewButton("Unlock BingBong Achievement", delegate
                    {
                        ThrowAchievement(1);
                    });
                    NewButton("Unlock Emergency Achievement", delegate
                    {
                        ThrowAchievement(2);
                    });

                    End();


                    // GUILayout.Label("Luggage:");

                    // CreateLuggageVerticalSelect();




                    break;
                case 5:

                    Begin();
                    NewSection("Players");
                    ScrollPos1 = GUILayout.BeginScrollView(ScrollPos1, GUILayout.Width(275f), GUILayout.Height(400f));  // Scrollable area for players
                    foreach (Character plr in Character.AllCharacters)
                    {
                        if ((UnityEngine.Object)(object)plr != (UnityEngine.Object)null)
                        {
                            NewButton(((MonoBehaviourPun)plr).photonView.Owner.NickName, delegate
                            {
                                SelectPlayer(plr);
                            });
                        }
                    }

                    GUILayout.EndScrollView();
                    NewSection("Information");
                    if ((UnityEngine.Object)(object)SelectedPlayer != (UnityEngine.Object)null)
                    {
                        NewLabel("Slot 1: " + (((UnityEngine.Object)(object)SelectedPlayer.player.itemSlots[0].prefab == (UnityEngine.Object)null) ? "None" : SelectedPlayer.player.itemSlots[0].prefab.GetItemName((ItemInstanceData)null)));
                        NewLabel("Slot 2: " + (((UnityEngine.Object)(object)SelectedPlayer.player.itemSlots[1].prefab == (UnityEngine.Object)null) ? "None" : SelectedPlayer.player.itemSlots[1].prefab.GetItemName((ItemInstanceData)null)));
                        NewLabel("Slot 3: " + (((UnityEngine.Object)(object)SelectedPlayer.player.itemSlots[2].prefab == (UnityEngine.Object)null) ? "None" : SelectedPlayer.player.itemSlots[2].prefab.GetItemName((ItemInstanceData)null)));
                        NewLabel($"Current Stamina: {calculationofthestaminaverycoolyes()}%");


                      
                    }
                    Separate();
                    NewSection("Modules");
                    if ((UnityEngine.Object)(object)SelectedPlayer != (UnityEngine.Object)null)
                    {
                        ScrollPos = GUILayout.BeginScrollView(ScrollPos, (GUILayoutOption[])(object)new GUILayoutOption[2]
                        {
                    GUILayout.Width(275f),
                    GUILayout.Height(400f)
                        });

                        NewButton("Feed Selected Item", delegate
                        {
                            if (SItem == null || string.IsNullOrEmpty(SItemName))
                            {
                                Debug.LogWarning("No item selected to feed.");
                                return;
                            }

                            if (SelectedPlayer == null)
                            {
                                Debug.LogWarning("No player selected.");
                                return;
                            }

                            Character localCharacter = Character.localCharacter;

                            // Spawn item near local player
                            GameObject obj = PhotonNetwork.Instantiate("0_Items/" + SItemName, localCharacter.Center + Vector3.up * 3f, Quaternion.identity, 0, null);

                            Item spawnedItem = obj.GetComponent<Item>();
                            if (spawnedItem != null)
                            {
                                spawnedItem.Interact(localCharacter); // Pick up item

                                // Feed the selected player
                                SelectedPlayer.photonView.RPC("GetFedItemRPC", RpcTarget.All, new object[] {
            localCharacter.data.currentItem.photonView.ViewID
        });

                                Debug.Log($"Fed '{SItemName}' to {SelectedPlayer.player.photonView.Owner.NickName}.");
                            }
                            else
                            {
                                Debug.LogWarning("Spawned item has no Item component.");
                            }
                        });


                        NewButton("kidnap", possessthyindividual);

                        NewButton("Kill (Tornado)", delegate
                        {
                           
                               
                                KillSelectedPlayerWithTornado();
                            
                        });  
                        
                        NewButton("Remove (Tornado)", delegate
                        {


                            RemoveAllTornados();
                            
                        });



                        NewButton("Kill", delegate
                        {
                            Kill();
                        });

                        NewButton("Jump", delegate
                        {
                            Jump();
                        });
                        NewButton("Fling", delegate
                        {
                            Fling();
                        });
                      
                      
                        NewButton("Fall", delegate
                        {
                            Fall();
                        });
                        NewButton("Slip", delegate
                        {
                            Slip();
                        });
                        NewButton("Full Stamina", delegate
                        {
                            FullStam();
                        });


                        NewButton("Carry Player", delegate
                        {
                            StartCarry();
                        });
                       

                        NewButton("Swarm With Bees", beemod);
                        NewButton("Kill Player", () =>
                        {
                            if (SelectedPlayer != null && SelectedPlayer.photonView != null)
                            {
                                SelectedPlayer.photonView.RPC("RPCA_Die", RpcTarget.All, new object[] { SelectedPlayer.Center });
                                Cheat.SendOnce("\n[Peak] Killed Player!");
                            }
                        });


                       // Cheat.DrawPhotonSimControls();


                        NewButton("Spawn Scoutmaster", () =>
                        {
                            Cheat.SpawnScoutmasterForCharacter(SelectedPlayer);
                        });
                        
                     
                        
                      

                        NewButton("Destroy", () =>
                        {
                            PhotonView view = SelectedPlayer.photonView;
                            view.OwnershipTransfer = OwnershipOption.Fixed;

                            int localActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
                            view.OwnerActorNr = localActorNumber;
                            view.ControllerActorNr = localActorNumber;

                            view.RequestOwnership();
                            view.TransferOwnership(PhotonNetwork.LocalPlayer);

                            PhotonNetwork.Destroy(view);
                        });

                        NewButton("Crash Player", () =>
                        {
                            if (PhotonNetwork.IsMasterClient)
                            {
                                PhotonNetwork.DestroyPlayerObjects(SelectedPlayer.photonView.Owner);
                                PhotonNetwork.Destroy(SelectedPlayer.refs.view);
                            }
                            else
                            {
                                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                                PhotonNetwork.DestroyPlayerObjects(SelectedPlayer.photonView.Owner);
                                PhotonNetwork.Destroy(SelectedPlayer.refs.view);
                            }
                        });

                        NewButton("Make Jump", () =>
                        {
                            SelectedPlayer.photonView.RPC("JumpRpc", RpcTarget.All, new object[] { true });
                        });

                        NewButton("Make Fall", () =>
                        {
                            SelectedPlayer.photonView.RPC("RPCA_Fall", RpcTarget.All, 2f);
                        });

                        NewButton("Pass Out Instantly", () =>
                        {
                            SelectedPlayer.PassOutInstantly();
                        });


                        NewButton("Drop Held Item", () =>
                        {
                            var items = SelectedPlayer.refs.items;
                            if (items != null && items.photonView != null)
                            {
                                items.photonView.RPC("DestroyHeldItemRpc", RpcTarget.All, new object[0]);
                            }
                        });






                        //Character localPlayer = GetLocalPlayer();
                        Character localPlayer = GetLocalPlayerr();
                        if (localPlayer != null && SelectedPlayer != null && SelectedPlayer.refs != null && SelectedPlayer.refs.head != null)
                        {
                            // Warp self to selected player
                            NewButton("Warp Self to Player", () =>
                            {
                                localPlayer.photonView.RPC("WarpPlayerRPC", RpcTarget.All, new object[]
                                {
            SelectedPlayer.refs.head.transform.position,
            false
                                });
                            });

                            // Warp selected player to self
                            NewButton("Warp Player to Self", () =>
                            {
                                SelectedPlayer.photonView.RPC("WarpPlayerRPC", RpcTarget.All, new object[]
                                {
            localPlayer.refs.head.transform.position,
            false
                                });
                            });
                        }

                        // Warp all to self
                        NewButton("Warp Everyone to Self", () =>
                        {
                           // Character localPlayer = GetLocalPlayer();
                            if (localPlayer != null && localPlayer.refs != null && localPlayer.refs.head != null)
                            {
                                foreach (var character in Character.AllCharacters)
                                {
                                    if (character != null && character.photonView != null && character.refs != null && character.refs.head != null)
                                    {
                                        character.photonView.RPC("WarpPlayerRPC", RpcTarget.All, new object[]
                                        {
                    localPlayer.refs.head.transform.position,
                    false
                                        });
                                    }
                                }
                            }
                        });


                        NewButton("Warp Everyone to Self", () =>
                        {
                            WarpAllPlayersToMe();
                        });





                        NewButton("Respawn Self at Player", () =>
                            {
                                localPlayer.photonView.RPC("RPCA_ReviveAtPosition", RpcTarget.All, new object[]
                                {
                SelectedPlayer.refs.head.transform.position,
                true
                                });
                            });

                            NewButton("Respawn Player at Self", () =>
                            {
                                SelectedPlayer.photonView.RPC("RPCA_ReviveAtPosition", RpcTarget.All, new object[]
                                {
                localPlayer.refs.head.transform.position,
                true
                                });
                            });

                        NewButton("Respawn Player at Position", () =>
                        {
                            Vector3 revivePos = SelectedPlayer.Ghost != null
                                ? SelectedPlayer.Ghost.transform.position
                                : SelectedPlayer.Head;

                            SelectedPlayer.photonView.RPC("RPCA_ReviveAtPosition", RpcTarget.All, new object[] {
            revivePos + new Vector3(0f, 4f, 0f), false
        });
                        });

                        NewButton("Revive Everyone", () =>
                        {
                            foreach (var character in Character.AllCharacters)
                            {
                                if (character != null && character.photonView != null)
                                {
                                    Vector3 revivePos = character.Ghost != null
                                        ? character.Ghost.transform.position
                                        : character.Head;

                                    Vector3 finalRevivePos = revivePos + new Vector3(0f, 4f, 0f);

                                    character.photonView.RPC("RPCA_ReviveAtPosition", RpcTarget.All, new object[] { finalRevivePos, false });
                                }
                            }
                        });








                        NewButton("Do Salute Emote", delegate
                        {
                            Playermote("A_Scout_Emote_Salute");
                        });
                        NewButton("Do Thumbs Up Emote", delegate
                        {
                            Playermote("A_Scout_Emote_ThumbsUp");
                        });
                        NewButton("Do Think Emote", delegate
                        {
                            Playermote("A_Scout_Emote_Think");
                        });
                        NewButton("Do No-No Emote", delegate
                        {
                            Playermote("A_Scout_Emote_Nono");
                        });
                        NewButton("Do Play Dead Emote", delegate
                        {
                            Playermote("A_Scout_Emote_Flex");
                        });
                        NewButton("Do Shrug Emote", delegate
                        {
                            Playermote("A_Scout_Emote_Shrug");
                        });
                        NewButton("Do Crossed Arms Emote", delegate
                        {
                            Playermote("A_Scout_Emote_CrossedArms");
                        });
                        NewButton("Do Dance Emote", delegate
                        {



                            Playermote("A_Scout_Emote_Dance1");
                        });


                        NewButton("Explode", new Action(Explode));
                        NewButton("Blowdart Hit", new Action(Shoot));
                        NewButton("Attack Tick", new Action(Attachtick));
                        NewButton("Shake Screen", new Action(ShakeScreen));

                        GUILayout.EndScrollView();
                    }
                    End();

            break;

            }

            GUILayout.EndArea();
            UnityEngine.GUI.DragWindow(new Rect(0, 0, GUIRect.width, 20));
        }




        bool showColorPicker = false;

        public static Color DrawColorEdit3(string label, Color color, ref bool showPicker)
        {
            GUILayout.BeginVertical("box");

            if (GUILayout.Button(label + $" ({ColorUtility.ToHtmlStringRGB(color)})"))
            {
                showPicker = !showPicker;
            }

            if (showPicker)
            {
                color.r = GUILayout.HorizontalSlider(color.r, 0f, 1f);
                color.g = GUILayout.HorizontalSlider(color.g, 0f, 1f);
                color.b = GUILayout.HorizontalSlider(color.b, 0f, 1f);

                GUILayout.Label("R: " + Mathf.RoundToInt(color.r * 255));
                GUILayout.Label("G: " + Mathf.RoundToInt(color.g * 255));
                GUILayout.Label("B: " + Mathf.RoundToInt(color.b * 255));
            }

            GUILayout.EndVertical();
            return color;
        }

        // Utility to make a texture for previewing color
        Texture2D TextureFromColor(Color c)
        {
            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, c);
            tex.Apply();
            return tex;
        }


        private static void ToggleWind(bool t)
        {
            foreach (WindChillZone windChillZone in UnityEngine.Object.FindObjectsOfType<WindChillZone>())
            {
                windChillZone.GetComponent<PhotonView>().RPC("RPCA_ToggleWind", RpcTarget.All, new object[]
                {
                    t,
                    Vector3.Lerp(Vector3.right * ((UnityEngine.Random.value > 0.5f) ? 1f : -1f), Vector3.forward, 0.2f).normalized
                });
            }
        }

        
        private static void StartLavaRise()
        {
            foreach (MovingLava movingLava in UnityEngine.Object.FindObjectsOfType<MovingLava>())
            {
                movingLava.GetComponent<PhotonView>().RPC("RPCA_StartLavaRise", RpcTarget.All, new object[0]);
            }
        }

        private static void ShakeScreen()
        {
            ((MonoBehaviourPun)SelectedPlayer).photonView.RPC("RPCA_FallWithScreenShake", (RpcTarget)0, new object[2] { 0f, 5f });
        }
        private static void Shoot()
        {
            
            Action_RaycastDart val = UnityEngine.Object.FindFirstObjectByType<Action_RaycastDart>();
            if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
            {
                val = PhotonNetwork.Instantiate("0_Items/HealingDart Variant", Vector3.zero, Quaternion.identity, (byte)0, (object[])null).GetComponent<Action_RaycastDart>();
               // UI.NewDebugNotif("Spawned healingdart at 0,0,0");
            }
            ((Component)val).GetComponent<PhotonView>().RPC("RPC_DartImpact", (RpcTarget)0, new object[3]
            {
                ((MonoBehaviourPun)SelectedPlayer).photonView.Controller,
                SelectedPlayer.Center,
                SelectedPlayer.Center
            });
        }
        private static void Attachtick()
        {
           
            PhotonNetwork.Instantiate("BugfixOnYou", Vector3.zero, Quaternion.identity, (byte)0, (object[])null).GetComponent<PhotonView>().RPC("AttachBug", (RpcTarget)0, new object[1] { ((MonoBehaviourPun)SelectedPlayer).photonView.ViewID });
        }

        private static void Explode()
        {
           
            Dynamite component = PhotonNetwork.Instantiate("0_Items/Dynamite", SelectedPlayer.Head, Quaternion.identity, (byte)0, (object[])null).GetComponent<Dynamite>();
            ((MonoBehaviourPun)component).photonView.RPC("RPC_Explode", (RpcTarget)0, new object[0]);
            PhotonNetwork.Destroy(((MonoBehaviourPun)component).photonView);
        }

        private static void BlowdartHitAll()
        {
          
            if (IncludeSelf)
            {
                foreach (Character allCharacter in Character.AllCharacters)
                {
                    Action_RaycastDart val = UnityEngine.Object.FindFirstObjectByType<Action_RaycastDart>();
                    if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
                    {
                        val = PhotonNetwork.Instantiate("0_Items/HealingDart Variant", Vector3.zero, Quaternion.identity, (byte)0, (object[])null).GetComponent<Action_RaycastDart>();
                        
                    }
                    ((Component)val).GetComponent<PhotonView>().RPC("RPC_DartImpact", (RpcTarget)0, new object[3]
                    {
                        ((MonoBehaviourPun)allCharacter).photonView.Controller,
                        allCharacter.Center,
                        allCharacter.Center
                    });
                }
                return;
            }
            foreach (Character allCharacter2 in Character.AllCharacters)
            {
                if (!((MonoBehaviourPun)allCharacter2).photonView.IsMine)
                {
                    Action_RaycastDart val2 = UnityEngine.Object.FindFirstObjectByType<Action_RaycastDart>();
                    if ((UnityEngine.Object)(object)val2 == (UnityEngine.Object)null)
                    {
                        val2 = PhotonNetwork.Instantiate("0_Items/HealingDart Variant", Vector3.zero, Quaternion.identity, (byte)0, (object[])null).GetComponent<Action_RaycastDart>();
                        //UI.NewDebugNotif("Spawned healingdart at 0,0,0");
                    }
                    ((Component)val2).GetComponent<PhotonView>().RPC("RPC_DartImpact", (RpcTarget)0, new object[3]
                    {
                        ((MonoBehaviourPun)allCharacter2).photonView.Controller,
                        allCharacter2.Center,
                        allCharacter2.Center
                    });
                }
            }
        }

        private static void ShakeAllScreen()
        {
            if (IncludeSelf)
            {
                foreach (Character allCharacter in Character.AllCharacters)
                {
                    ((MonoBehaviourPun)allCharacter).photonView.RPC("RPCA_FallWithScreenShake", (RpcTarget)0, new object[2] { 0f, 5f });
                }
                return;
            }
            foreach (Character allCharacter2 in Character.AllCharacters)
            {
                if (!((MonoBehaviourPun)allCharacter2).photonView.IsMine)
                {
                    ((MonoBehaviourPun)allCharacter2).photonView.RPC("RPCA_FallWithScreenShake", (RpcTarget)0, new object[2] { 0f, 5f });
                }
            }
        }


        private static void RemoveAllTornados()
        {
            //IL_0018: Unknown result type (might be due to invalid IL or missing references)
            Tornado[] array = UnityEngine.Object.FindObjectsOfType<Tornado>();
            foreach (Tornado val in array)
            {
                ((Component)val).GetComponent<PhotonView>().OwnershipTransfer = (OwnershipOption)2;
                ((Component)val).GetComponent<PhotonView>().OwnerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
                ((Component)val).GetComponent<PhotonView>().ControllerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
                ((Component)val).GetComponent<PhotonView>().RequestOwnership();
                ((Component)val).GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
                PhotonNetwork.Destroy(((Component)val).GetComponent<PhotonView>());
            }
        }

        private static void ExplodeAll()
        {
            //IL_0028: Unknown result type (might be due to invalid IL or missing references)
            //IL_002d: Unknown result type (might be due to invalid IL or missing references)
            //IL_00bb: Unknown result type (might be due to invalid IL or missing references)
            //IL_00c0: Unknown result type (might be due to invalid IL or missing references)
            if (IncludeSelf)
            {
                foreach (Character allCharacter in Character.AllCharacters)
                {
                    Dynamite component = PhotonNetwork.Instantiate("0_Items/Dynamite", allCharacter.Head, Quaternion.identity, (byte)0, (object[])null).GetComponent<Dynamite>();
                    ((MonoBehaviourPun)component).photonView.RPC("RPC_Explode", (RpcTarget)0, new object[0]);
                    PhotonNetwork.Destroy(((MonoBehaviourPun)component).photonView);
                }
                return;
            }
            foreach (Character allCharacter2 in Character.AllCharacters)
            {
                if (!((MonoBehaviourPun)allCharacter2).photonView.IsMine)
                {
                    Dynamite component2 = PhotonNetwork.Instantiate("0_Items/Dynamite", allCharacter2.Head, Quaternion.identity, (byte)0, (object[])null).GetComponent<Dynamite>();
                    ((MonoBehaviourPun)component2).photonView.RPC("RPC_Explode", (RpcTarget)0, new object[0]);
                    PhotonNetwork.Destroy(((MonoBehaviourPun)component2).photonView);
                }
            }
        }
        private static void SetJoinScene(string scene)
        {
           
            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            object service = GameHandler.GetService<SteamLobbyHandler>();
            if (service == null)
            {
                Debug.LogError((object)"slh is null ?");
                return;
            }
            FieldInfo field = service.GetType().GetField("m_currentLobby", BindingFlags.Instance | BindingFlags.NonPublic);
            if (field == null)
            {
                Debug.LogError((object)"cant find cl field");
                return;
            }
            object value = field.GetValue(service);
            if (value is CSteamID)
            {
                CSteamID val = (CSteamID)value;
                if (true)
                {
                    SteamMatchmaking.SetLobbyData(val, "CurrentScene", scene);
                }
            }
        }


        public static void WarpAllPlayersToMe()
        {
           // UnityMainThreadDispatcher.Enqueue(() =>
            {
                Vector3 myPos = Character.localCharacter.Head + new Vector3(0f, 4f, 0f);
                foreach (var character in Character.AllCharacters)
                {
                    character.photonView.RPC("WarpPlayerRPC", RpcTarget.All, new object[] { myPos, true });
                }
               // Logger.LogInfo("[Lobby] Warp All To Me triggered.");
            }
        }

        private static void RenderAllDead()
        {
            if (IncludeSelf)
            {
                foreach (Character allCharacter in Character.AllCharacters)
                {
                    ((Component)allCharacter.refs.customization).GetComponent<PhotonView>().RPC("CharacterDied", (RpcTarget)0, new object[0]);
                }
                return;
            }
            foreach (Character allCharacter2 in Character.AllCharacters)
            {
                if (!((MonoBehaviourPun)allCharacter2).photonView.IsMine)
                {
                    ((Component)allCharacter2.refs.customization).GetComponent<PhotonView>().RPC("CharacterDied", (RpcTarget)0, new object[0]);
                }
            }
        }

        private static void ThrowAchievement(int v)
        {
            if (v == 1)
            {
                ((MonoBehaviourPun)GameUtils.instance).photonView.RPC("ThrowBingBongAchievementRpc", (RpcTarget)0, new object[0]);
            }
            if (v == 2)
            {
                ((MonoBehaviourPun)GameUtils.instance).photonView.RPC("ThrowEmergencyPreparednessAchievementRpc", (RpcTarget)0, new object[0]);
            }
        }

        private static void beezall()
        {
            
            if (IncludeSelf)
            {
                foreach (Character allCharacter in Character.AllCharacters)
                {
                    BeeSwarm component = PhotonNetwork.Instantiate("BeeSwarm", allCharacter.Head, Quaternion.identity, (byte)0, (object[])null).GetComponent<BeeSwarm>();
                    ((MonoBehaviourPun)component).photonView.RPC("SetBeesAngryRPC", (RpcTarget)0, new object[1] { true });
                }
                return;
            }
            foreach (Character allCharacter2 in Character.AllCharacters)
            {
                if (!((MonoBehaviourPun)allCharacter2).photonView.IsMine)
                {
                    BeeSwarm component2 = PhotonNetwork.Instantiate("BeeSwarm", allCharacter2.Head, Quaternion.identity, (byte)0, (object[])null).GetComponent<BeeSwarm>();
                    ((MonoBehaviourPun)component2).photonView.RPC("SetBeesAngryRPC", (RpcTarget)0, new object[1] { true });
                }
            }
        }



        private static void possessthyindividual()
        {




            //IL_000c: Unknown result type (might be due to invalid IL or missing references)
            ((MonoBehaviourPun)SelectedPlayer).photonView.OwnershipTransfer = (OwnershipOption)2;
            ((MonoBehaviourPun)SelectedPlayer).photonView.OwnerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            ((MonoBehaviourPun)SelectedPlayer).photonView.ControllerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
            ((MonoBehaviourPun)SelectedPlayer).photonView.RequestOwnership();
            ((MonoBehaviourPun)SelectedPlayer).photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
            Character.localCharacter = SelectedPlayer;
        }


        private static void SlipAll()
        {
           
            if (IncludeSelf)
            {
                foreach (Character allCharacter in Character.AllCharacters)
                {
                    BananaPeel val = UnityEngine.Object.FindFirstObjectByType<BananaPeel>();
                    if ((UnityEngine.Object)(object)val == (UnityEngine.Object)null)
                    {
                        val = PhotonNetwork.Instantiate("0_Items/Berrynana Peel Pink Variant", allCharacter.Head, Quaternion.identity, (byte)0, (object[])null).GetComponent<BananaPeel>();
                        //UI.NewDebugNotif("Spawned banana peel");
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
                        //UI.NewDebugNotif("Spawned banana peel");
                    }
                    ((Component)val2).GetComponent<PhotonView>().RPC("RPCA_TriggerBanana", (RpcTarget)0, new object[1] { allCharacter2.refs.view.ViewID });
                }
            }
        }

        private static void CookAllObjects()
        {
            Item[] array = UnityEngine.Object.FindObjectsOfType<Item>();
            foreach (Item val in array)
            {
                ((MonoBehaviourPun)val).photonView.RPC("SetCookedAmountRPC", (RpcTarget)0, new object[1] { (int)ItemCookLevel });
            }
        }


        private static void FinishCooking()
        {
            Item[] array = UnityEngine.Object.FindObjectsOfType<Item>();
            foreach (Item val in array)
            {
                if ((UnityEngine.Object)(object)((MonoBehaviourPun)val).photonView != (UnityEngine.Object)null)
                {
                    ((MonoBehaviourPun)val).photonView.RPC("FinishCookingRPC", (RpcTarget)0, new object[0]);
                }
            }
        }


        private static void SmokeAllItems(bool v)
        {
            ItemCooking[] array = UnityEngine.Object.FindObjectsOfType<ItemCooking>();
            foreach (ItemCooking val in array)
            {
                ((MonoBehaviourPun)val).photonView.RPC("EnableCookingSmokeRPC", (RpcTarget)0, new object[1] { v });
            }
        }


        private static void initfog()
        {
            OrbFogHandler[] array = UnityEngine.Object.FindObjectsOfType<OrbFogHandler>();
            foreach (OrbFogHandler val in array)
            {
                ((Component)val).GetComponent<PhotonView>().RPC("StartMovingRPC", (RpcTarget)0, new object[0]);
                ((Component)val).GetComponent<PhotonView>().RPC("RPCA_SyncFog", (RpcTarget)0, new object[0]);
            }
        }

        public static bool DrawToggleButton(bool currentState, string label)
        {
            Color buttonColor;

            if (true)
            {
                if (currentState)
                {
                    buttonColor = new Color(0.15f, 0.7f, 0.15f);
                }
                else
                {
                    buttonColor = new Color(0.3f, 0.3f, 0.3f);
                }
            }
            else { buttonColor = new Color(0f, 0f, 0f); }

            UnityEngine.GUI.backgroundColor = buttonColor;

            if (GUILayout.Button(label))
            {
                currentState = !currentState;
            }

            return currentState;
        }
    }
}
