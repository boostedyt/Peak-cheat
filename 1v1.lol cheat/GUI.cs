using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening.Plugins.Core;
using JustPlay.PsfLight.Events;
using Photon.Pun;
using UnityEngine;
using Zorro.Core;

namespace _1v1.lol_cheat
{
    public class GUI : MonoBehaviour
    {
        //GUI Variables
        public static Rect GUIRect = new Rect(30, 30, 700, 600);
        public static int selected_tab = 0;
        public static string[] tabnames = { "Spawner", "Visual", "Other", "Test" };

        //aim
        public static bool silentaim = false;
        public static float fov = 180;

        //fovcircle
        public static bool fovcircle = false;

        //godmode
        public static bool godmode = false;


        public static Vector2 itemScrollPos = Vector2.zero;
        public static bool clearStatuses = false;

        //infiniteammo
        public static bool infiniteammo = false;

        public static bool randomoutfits = false;

        public static bool setfieldofview = false;

        //rapidfire
        public static bool rapidfire = false;

        public static bool Unlockall = false;

        public static bool BingbongSpam = false;
        public static int rapidcooldown = 0;
        public static float fireratecooldown = 3;

        public static float fieldofview = 35f;

        //crasher
        public static bool crasher = false;

        //boxesp
        public static bool boxesp = false;
        public static bool boxfix = false;

        //speed
        public static bool speed = false;
        public static float speedmultiply = 5;
        public static bool flyMode = false;

        public static Dictionary<string, Character> playerDict = new Dictionary<string, Character>();
        public static string localPlayerName = "";
        public static UnityEngine.Vector2 playerScrollPos;
        public static int selectedPlayerIndex;



        public static string[] items = ItemDatabase.GetAllObjectNames();
        //public static string[] items = ItemDatabase.GetAllObjectNames();
        //  public static UnityEngine.Vector2 itemScrollPos;
        public static int selectedItemIndex;

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




        public static void TeleportTo(Vector3 position, bool poof = true)
        {
            if (Character.localCharacter == null)
                return;

            Character.localCharacter.WarpPlayerRPC(position, poof);
        }



        public static void MakeEveryoneJump()
        {
            foreach (Character guy in Resources.FindObjectsOfTypeAll<Character>())
            {
                if (guy != null && guy.photonView != null)
                {
                    if (!guy.photonView.IsMine)
                    {
                        guy.photonView.RPC("JumpRpc", Photon.Pun.RpcTarget.All, new object[]
                        {
                    true
                        });
                    }
                }
            }

            Debug.Log("[Cheat] Made all other players jump!");
        }


        public static void MakeAllFall()
        {


            foreach (Character q in Character.AllCharacters)
            {
                if (!q.photonView.IsMine)
                {
                    q.refs.view.RPC("RPCA_Fall", RpcTarget.All, new object[]
                         {
                         q, 5
                           });
                }
            }


            Debug.Log("[Cheat] Made all other players Fall!");
        }


        public static void MakeAllPassout()
        {
            foreach (Character guy in Resources.FindObjectsOfTypeAll<Character>())
            {
                if (guy != null && guy.photonView != null)
                {


                    guy.refs.view.RPC("RPCA_PassOut", RpcTarget.All, new object[]
                      {
                         guy, 5
                        });



                }
            }

            Debug.Log("[Cheat] Made all other players Fall!");
        }






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

            Debug.Log("Refreshed playerDict. Players found: " + playerDict.Count);
        }



        public static void CreatePlayersVerticalSelect()
        {
            if (playerDict == null || playerDict.Count == 0)
            {
                RefreshPlayerDict();
            }

            var keys = playerDict.Keys.ToArray();

            GUILayout.BeginVertical("box", GUILayout.Width(200));
            try
            {
                GUILayout.Label("Players:");

                if (keys.Length == 0)
                {
                    GUILayout.Label("No players found");
                    return;
                }

                playerScrollPos = GUILayout.BeginScrollView(playerScrollPos, GUILayout.Height(150), GUILayout.Width(200));

                // Clamp the selection index
                if (selectedPlayerIndex < 0 || selectedPlayerIndex >= keys.Length)
                {
                    selectedPlayerIndex = 0;
                }

                selectedPlayerIndex = GUILayout.SelectionGrid(selectedPlayerIndex, keys, 1);

                GUILayout.EndScrollView();

                GUILayout.Label("Selected: " + keys[selectedPlayerIndex]);
            }
            finally
            {
                GUILayout.EndVertical();
            }
        }


        public static void CreateItemsVerticalSelect()
        {
            GUILayout.BeginVertical("box", GUILayout.Width(200));
            try
            {
                GUILayout.Label("Items:");

                itemScrollPos = GUILayout.BeginScrollView(itemScrollPos, GUILayout.Height(150), GUILayout.Width(200));

                if (items == null || items.Length == 0)
                {

                    RefreshPlayerDict();
                    GUILayout.Label("No items available");
                    selectedItemIndex = -1;  // reset invalid index
                }
                else
                {
                    if (selectedItemIndex < 0 || selectedItemIndex >= items.Length)
                        selectedItemIndex = 0;

                    selectedItemIndex = GUILayout.SelectionGrid(selectedItemIndex, items, 1);

                    GUILayout.Label("Selected: " + items[selectedItemIndex]);
                }

                GUILayout.EndScrollView();
            }
            finally
            {
                GUILayout.EndVertical();
            }
        }



        public static void CreateSpawnItemButtons()
        {
            if (GUILayout.Button("Spawn item on player"))
            {
                var localPlayer = GetLocalPlayer();
                if (localPlayer == null)
                {
                    RefreshPlayerDict();
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


        public static void CreateWarpButtons()
        {
            Character localPlayer = GetLocalPlayer();

            if (localPlayer == null)
            {
                GUILayout.Label("Local player not ready");
                return;
            }

            var keys = playerDict?.Keys.ToArray();
            if (keys == null || keys.Length == 0 || selectedPlayerIndex < 0 || selectedPlayerIndex >= keys.Length)
            {
                GUILayout.Label("No players found or invalid selection");
                return;
            }

            Character selectedPlayer = playerDict[keys[selectedPlayerIndex]];

            if (GUILayout.Button("Warp self to player"))
            {
                if (selectedPlayer != null)
                {
                    localPlayer.photonView.RPC("WarpPlayerRPC", RpcTarget.All, new object[] {
                selectedPlayer.refs.head.transform.position, false
            });
                }
            }

            if (GUILayout.Button("Warp player to self"))
            {
                if (selectedPlayer != null)
                {
                    selectedPlayer.photonView.RPC("WarpPlayerRPC", RpcTarget.All, new object[] {
                localPlayer.refs.head.transform.position, false
            });
                }
            }

            if (GUILayout.Button("Warp everyone to self"))
            {
                foreach (var character in playerDict.Values)
                {
                    if (character != null && character.photonView != null)
                    {
                        character.photonView.RPC("WarpPlayerRPC", RpcTarget.All, new object[] {
                    localPlayer.refs.head.transform.position, false
                });
                    }
                }
            }
        }

        public static void CreateRespawnButtons()
        {
            try
            {
                if (playerDict == null || playerDict.Count == 0)
                {
                    GUILayout.Label("No players found");
                    return;
                }

                var keys = playerDict.Keys.ToArray();

                if (selectedPlayerIndex < 0 || selectedPlayerIndex >= keys.Length)
                {
                    GUILayout.Label("Invalid player selection");
                    return;
                }

                Character localPlayer = playerDict.Values.FirstOrDefault(x => x.IsLocal || x.player.photonView.IsMine);
                if (localPlayer == null)
                {
                    GUILayout.Label("Local player not found");
                    return;
                }

                Character selectedPlayer = playerDict[keys[selectedPlayerIndex]];
                if (selectedPlayer == null)
                {
                    GUILayout.Label("Selected player not found");
                    return;
                }

                if (GUILayout.Button("Respawn self at player"))
                {
                    localPlayer.photonView.RPC("RPCA_ReviveAtPosition", RpcTarget.All, new object[]
                    {
                selectedPlayer.refs.head.transform.position,
                true
                    });
                }

                if (GUILayout.Button("Respawn player at self"))
                {
                    selectedPlayer.photonView.RPC("RPCA_ReviveAtPosition", RpcTarget.All, new object[]
                    {
                localPlayer.refs.head.transform.position,
                true
                    });
                }





                if (GUILayout.Button("Respawn player at Position"))
                {

                    Vector3 revivePos = selectedPlayer.Ghost != null ? selectedPlayer.Ghost.transform.position : selectedPlayer.Head;
                    selectedPlayer.photonView.RPC("RPCA_ReviveAtPosition", RpcTarget.All, new object[] {
                revivePos + new Vector3(0f, 4f, 0f), false

                    });
                }




            }
            catch (Exception ex)
            {
                GUILayout.Label("Respawn error: " + ex.Message);
            }
        }


        public static void CreateKillPlayerButton()
        {
            if (GUILayout.Button("Kill player"))
            {
                if (playerDict.Count == 0)
                {
                    // ShowNotification("Aqcuire players first");
                    return;
                }
                Character selectedCharacter = playerDict.Values.First(x => x.player.photonView.Owner.NickName == playerDict.Keys.ToArray()[selectedPlayerIndex]);
                selectedCharacter.photonView.RPC("RPCA_Die", RpcTarget.All, new object[] { selectedCharacter.Center });

            }






        }

        public static void DrawPlayerTab()
        {
            GUILayout.BeginHorizontal();


            GUILayout.BeginVertical("box", GUILayout.Width(250));


            CreatePlayersVerticalSelect();
            CreateKillPlayerButton();
            CreateRespawnButtons();
            CreateWarpButtons();

            GUILayout.EndVertical();



            GUILayout.BeginVertical("box", GUILayout.Width(250));
            //  GUILayout.Label("Item Spawning", EditorStyles.boldLabel);




            CreateItemsVerticalSelect();

            if (items != null && items.Length > 0 && selectedItemIndex >= 0 && selectedItemIndex < items.Length)
            {
                CreateSpawnItemButtons();
            }
            else
            {
                GUILayout.Label("No item selected or item list empty.");
            }

          

            GUILayout.EndVertical();




            GUILayout.EndHorizontal(); // End layout row
        }



        private static Character GetLocalPlayer()
        {
            if (global::Player.localPlayer == null)
                return null;

            return playerDict?.Values.FirstOrDefault(c =>
                c != null &&
                c.player == global::Player.localPlayer
            );
        }




      

       


        public static void GUIMain(int o) //required for the gui.window thing
        {
            GUILayout.BeginArea(new Rect(10, 10, GUIRect.width - 20, GUIRect.height - 20));
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            for (int i = 0; i < tabnames.Length; i++)
            {
                if (GUILayout.Toggle(selected_tab == i, tabnames[i], "Button"))
                {
                    selected_tab = i; //set the tab
                }
            }

            GUILayout.EndHorizontal();


            // Vector2 itemScrollPos = Vector2.zero;
            switch (selected_tab)
            {


                case 0:
                    itemScrollPos = GUILayout.BeginScrollView(itemScrollPos, GUILayout.Height(400));


                    if (GUILayout.Button("Spawn Rope Shooter Anti"))
                        SpawnItem("ropeshooteranti");

                    if (GUILayout.Button("Spawn Spool"))
                        SpawnItem("anti-rope spool");

                    if (GUILayout.Button("Spawn Bugle Magic"))
                        SpawnItem("bugle_magic");

                    if (GUILayout.Button("Spawn Cure-All"))
                        SpawnItem("Cure-all");

                    if (GUILayout.Button("Spawn Lantern Faerie"))
                        SpawnItem("Lantern_faerie");

                    if (GUILayout.Button("Spawn Pandoras Box"))
                        SpawnItem("pandorasbox");

                    if (GUILayout.Button("Spawn Scout Effigy"))
                        SpawnItem("scouteffigy");

                    if (GUILayout.Button("Spawn Bugle Scoutmaster"))
                        SpawnItem("bugle_scoutmaster variant");


                    if (GUILayout.Button("Spawn Bandages"))
                        SpawnItem("bandages");

                    if (GUILayout.Button("Spawn backpack"))
                        SpawnItem("backpack");

                    if (GUILayout.Button("Spawn Sports Drink"))
                        SpawnItem("sports drink");

                    if (GUILayout.Button("Spawn FirstAid"))
                        SpawnItem("firstaidkit");

                    if (GUILayout.Button("Spawn Lollipop"))
                        SpawnItem("lollipop");

                    if (GUILayout.Button("Spawn HeatPack"))
                        SpawnItem("Heat Pack");


                    if (GUILayout.Button("Spawn HoneyComb"))
                        SpawnItem("Item_Honeycomb");

                    if (GUILayout.Button("Spawn MedicinalRoot"))
                        SpawnItem("MedicinalRoot");

                    if (GUILayout.Button("Spawn BounceShroom"))
                        SpawnItem("BounceShroom");

                    if (GUILayout.Button("Spawn ChainShooter"))
                        SpawnItem("ChainShooter");

                    if (GUILayout.Button("Spawn ClimbingSpike"))
                        SpawnItem("ClimbingSpike");

                    if (GUILayout.Button("Spawn PortableStovetopItem"))
                        SpawnItem("PortableStovetopItem");

                    if (GUILayout.Button("Spawn RopeShooter"))
                        SpawnItem("RopeShooter");


                    if (GUILayout.Button("Spawn HealingDart Variant"))
                        SpawnItem("HealingDart Variant");

                    if (GUILayout.Button("Spawn CursedSkull"))
                        SpawnItem("Cursed Skull");

                    if (GUILayout.Button("Spawn Kingberry Green"))
                        SpawnItem("Kingberry Green");

                    if (GUILayout.Button("Spawn Kingberry Purple"))
                        SpawnItem("Kingberry Purple");

                    if (GUILayout.Button("Spawn PepperBerry"))
                        SpawnItem("Pepper Berry");

                    if (GUILayout.Button("Spawn AirplaneFood"))
                        SpawnItem("Airplane Food");

                    if (GUILayout.Button("Spawn Coconut"))
                        SpawnItem("Item_Coconut_half");

                    if (GUILayout.Button("Spawn Egg"))
                        SpawnItem("Egg");


                    if (GUILayout.Button("Spawn ScoutCookies"))
                        SpawnItem("ScoutCookies");

                    if (GUILayout.Button("Spawn Antidote"))
                        SpawnItem("Antidote");

                    if (GUILayout.Button("Spawn Binoculars"))
                        SpawnItem("Binoculars");



                    GUILayout.EndScrollView();
                    break;
                case 1:

                    silentaim = DrawToggleButton(silentaim, "Silent Aim");
                    crasher = DrawToggleButton(crasher, "Crash Players in FOV Circle (Middle mouse click)");

                    boxesp = DrawToggleButton(boxesp, "Box ESP");
                    fovcircle = DrawToggleButton(fovcircle, "FOV Circle");

                    GUILayout.Label("Username");
                    localPlayerName = GUILayout.TextField(localPlayerName);
                    if (GUILayout.Button("Save username"))
                    {
                        //File.WriteAllText(filePath, localPlayerName);

                        PhotonNetwork.NetworkingClient.LocalPlayer.NickName = localPlayerName;
                        RefreshPlayerDict();

                    }


                    GUILayout.Space(20);
                    GUILayout.Label("Teleport Locations:");



                    if (GUILayout.Button("Teleport to Spot 1"))
                        TeleportTo(new Vector3(-12.57f, 285.3f, 100.53f));

                    if (GUILayout.Button("Teleport to Spot 2"))
                        TeleportTo(new Vector3(-14.7f, 569.9f, 526f));

                    if (GUILayout.Button("Teleport to Spot 3"))
                        TeleportTo(new Vector3(0f, 861.8f, 1208.2f));


                    GUILayout.Space(10);
                    GUILayout.Label("Troll Options:");

                    if (GUILayout.Button("Make All Other Players Jump"))
                    {
                        MakeEveryoneJump();
                    }

                    if (GUILayout.Button("Make All Other Players Fall"))
                    {
                        MakeAllFall();
                    }

                    if (GUILayout.Button("Make All Other Players Passout"))
                    {
                        MakeAllPassout();
                    }




                    BingbongSpam = DrawToggleButton(BingbongSpam, "BingBong Spamm");




                    // setfieldofview = DrawToggleButton(setfieldofview, "Field Of View");


                    GUILayout.Label("FOV: " + fov);
                    fov = GUILayout.HorizontalSlider(fov, 1.0f, 1440.0f);

                    break;
                case 2:
                    godmode = DrawToggleButton(godmode, "Inf Stamina");
                    randomoutfits = DrawToggleButton(randomoutfits, "Randomize Outifts glitchy");
                    infiniteammo = DrawToggleButton(infiniteammo, "Revive Yourself");

                    clearStatuses = DrawToggleButton(clearStatuses, "ClearStatus/godmode");

                    Unlockall = DrawToggleButton(Unlockall, "Unlock All");
                    rapidfire = DrawToggleButton(rapidfire, "TimeModifier");
                    GUILayout.Label("Time Slider: " + fireratecooldown);
                    fireratecooldown = GUILayout.HorizontalSlider(fireratecooldown, 0f, 48f);
                    speed = DrawToggleButton(speed, "Speed");
                    GUILayout.Label("Speed: " + speedmultiply);
                    speedmultiply = GUILayout.HorizontalSlider(speedmultiply, 1.0f, 15.0f);

                    GUI.flyMode = DrawToggleButton(GUI.flyMode, "Fly Mode");

                   

                    GUILayout.Space(10);
                    GUILayout.Label("G Noclip F pickup items:");










                    break;

                case 3:




                    DrawPlayerTab();






                    break;

            }

            GUILayout.EndArea();
            UnityEngine.GUI.DragWindow(new Rect(0, 0, GUIRect.width, 20));
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

