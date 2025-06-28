using DG.Tweening.Plugins.Core;
using JustPlay.PsfLight.Events;
using Photon.Pun;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using Zorro.ControllerSupport;
using Zorro.Core;
using static AppConstants.Localizations;


namespace _1v1.lol_cheat
{
    /* Base Took From OUTSPECT Peak Source Lewis
      * CODED BY OUTSPECT AND FXZH
      * I am not going to put comments everywhere.
      * I might update this in the future.
      * 
     */

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

        //Variable to keep targets
        public static List<Character> targets = new List<Character>();

        //material for esp and fov circle
        public static Material mat = new Material(Shader.Find("GUI/Text Shader"));
        //public static string[] items = ItemDatabase.GetAllObjectNames();
        //watermark (now be happy this is open source lmao)
        private float colorChangeSpeed = 1f;
        private float timer = 0f;

        public static string[] items;

        public static string localPlayerName = "";

        public static Dictionary<string, Character> playerDict = new Dictionary<string, Character>();

        private void Start()
        {

            localPlayerName = PhotonNetwork.NickName;
            SceneManager.sceneLoaded += (_, __) => RefreshPlayerDict();

            StartCoroutine(UpdateTargets());
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


        }



        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            RefreshPlayerDict();
            InitializeItems();
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
                targets = Utils.GetTargets(); //get the list of targets from the util
                yield return new WaitForSeconds(1f);
            }
        }

       

        private void OnGUI()
        {
            float r = Mathf.PingPong(timer * colorChangeSpeed, 1f);
            float g = Mathf.PingPong(timer * colorChangeSpeed + 0.33f, 1f);
            float b = Mathf.PingPong(timer * colorChangeSpeed + 0.66f, 1f);

            UnityEngine.GUI.color = new Color(r, g, b);

            UnityEngine.GUI.Label(new Rect(10, 10, 400, 40), "https://github.com/outspect 1v1.lol cheat");

            UnityEngine.GUI.color = Color.white;

            timer += Time.deltaTime;

            if (GUI.fovcircle)
            {
                FOVCircle((int)GUI.fov);
            }

            if (toggled)
            {
                GUI.GUIRect = UnityEngine.GUI.Window(69, GUI.GUIRect, GUI.GUIMain, "outspect 1v1.lol cheat | FPS: " + 1.0f / Time.deltaTime + " | Toggle: INSERT");
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

        private void SilentAim()
        {
            short result = GetAsyncKeyState(0x01);
            bool leftmousebutton = (result & 0x8000) != 0;

            if (leftmousebutton)
            {
                Transform aimpos = null;

                float closest = float.PositiveInfinity;

                foreach (Character ctrl in targets)
                {
                    //fxzh type shit
                    Vector3 w2s = CameraManager.Instance.MainCamera.WorldToScreenPoint(ctrl.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.position);
                    float abs = Vector2.Distance(new Vector2(w2s.x, Screen.height - w2s.y), new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f));
                    if ( w2s.z >= 0f && abs <= GUI.fov && abs < closest)
                    {
                        closest = abs;
                        aimpos = ctrl.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform; //pretty straight forward
                    }
                }

                if (aimpos != null && closest != float.PositiveInfinity)
                {
                    CameraManager.Instance.MainCamera.gameObject.transform.LookAt(aimpos); //1v1lol camera works really weird but this is a silent aim
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
                    Vector3 w2s = CameraManager.Instance.MainCamera.WorldToScreenPoint(ctrl.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.position);
                    float abs = Vector2.Distance(new Vector2(w2s.x, Screen.height - w2s.y), new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f));
                    if ( w2s.z >= 0f && abs <= GUI.fov && abs < closest)
                    {
                        closest = abs;
                        PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer); //sets yourself as master client
                        PhotonNetwork.DestroyPlayerObjects(ctrl.photonView.Controller); //destroys player objects (will make them invisible and unable to do anything)
                    }
                }
            }
        }



        private static void BodyESP(bool enable)
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (Character playerObjs in UnityEngine.Object.FindObjectsOfType<Character>())
                {
                    if (playerObjs != null)
                    {
                        if (playerObjs.gameObject.GetComponentInChildren<SkinnedMeshRenderer>())
                        {
                            playerObjs.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.shader = Shader.Find("GUI/Text Shader");
                            playerObjs.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.Lerp(Color.cyan, Color.green, Mathf.PingPong(Time.time / 2f, 1f));
                        }
                    }
                }
            }
        }

        public static void DisableBodyESP()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (PlayerController playerObjs in UnityEngine.Object.FindObjectsOfType<PlayerController>())
                {
                    if (playerObjs != null)
                    {
                        if (playerObjs.gameObject.GetComponentInChildren<SkinnedMeshRenderer>())
                        {
                            playerObjs.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.shader = Shader.Find("PlayerToonShader");
                            playerObjs.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
                        }
                    }
                }
            }
        }


        private static void BoxESP(bool enable)
        {
            if (enable)
            {
                if (!PhotonNetwork.InRoom) return;

                foreach (Character sss in targets)
                {


                    Vector3 first = sss.transform.position;

                    Vector3 start = new Vector3(first.x, first.y + 1.75f, first.z);
                    Vector3 end = new Vector3(first.x, first.y, first.z);

                    LineRenderer lineRenderer;
                    if (!sss.gameObject.GetComponent<LineRenderer>())
                    {
                        lineRenderer = sss.gameObject.AddComponent<LineRenderer>();
                    }
                    else
                    {
                        lineRenderer = sss.gameObject.GetComponent<LineRenderer>();
                    }

                    Material material = new Material(Shader.Find("GUI/Text Shader"));
                    material.color = new UnityEngine.Color(1f, 1f, 1f, 0.75f);

                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(0, start);
                    lineRenderer.SetPosition(1, end);
                    lineRenderer.material = material;
                    lineRenderer.enabled = true;

                    float hue = Time.time * 0.5f;
                    UnityEngine.Color headColor = UnityEngine.Color.HSVToRGB(hue % 1f, 1f, 1f);
                    lineRenderer.startColor = new UnityEngine.Color(headColor.r, headColor.g, headColor.b, 0.75f);
                    lineRenderer.endColor = new UnityEngine.Color(headColor.r, headColor.g, headColor.b, 0.75f);

                    GUI.boxfix = true;
                }
            }
            else
            {
                if (GUI.boxfix)
                {
                    foreach (Character sss in targets)
                    {
                        UnityEngine.Object.Destroy(sss.gameObject.GetComponent<LineRenderer>());
                    }
                    GUI.boxfix = false;
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
                Debug.Log("Clearing status: " + statustype.ToString());
                if (statustype != CharacterAfflictions.STATUSTYPE.Weight && (!excludeCurse || statustype != CharacterAfflictions.STATUSTYPE.Curse) && statustype != CharacterAfflictions.STATUSTYPE.Crab)
                {
                    Debug.Log(string.Format("Current: {0}, amount {1}", statustype, Character.localCharacter.refs.afflictions.GetCurrentStatus(statustype)));
                    Debug.Log(string.Format("SetStatus status: {0}", statustype));
                    Character.localCharacter.refs.afflictions.SetStatus(statustype, 0f);
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

        



        private void HandleFlying()
        {
            if (!isFlying || Character.localCharacter == null || Character.localCharacter.refs?.ragdoll == null)
                return;

            var character = Character.localCharacter;

           
            character.data.isGrounded = true;
            character.data.sinceGrounded = 0f;
            character.data.sinceJump = 0f;

           
            Vector3 input = character.input.movementInput;
            Vector3 forward = character.data.lookDirection_Flat.normalized;
            Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;

            Vector3 direction = forward * input.y + right * input.x;

            if (character.input.jumpIsPressed)
                direction += Vector3.up;
            if (character.input.crouchIsPressed)
                direction += Vector3.down;

            
            flyVelocity = Vector3.Lerp(flyVelocity, direction.normalized * flySpeed, Time.deltaTime * acceleration);

           
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


        private void Update()
        {
            GUIToggleCheck();

            if (GUI.silentaim)
            {
                SilentAim();
            }

            if (GUI.boxesp)
            {
                BoxESP(true);
            }
            else { BoxESP(false); }


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

            HandleFlying();

            
            if (UnityEngine.Input.GetKeyDown(KeyCode.G))
            {
                GUI.flyMode = !GUI.flyMode; 
            }


            if (GUI.boxesp)
            {
                BodyESP(true);
            }
            else { BodyESP(false); }

            if (toggled != lastToggledState)
            {
                if (isMainScene)
                {
                    Cursor.visible = toggled;
                    Cursor.lockState = toggled ? CursorLockMode.None : CursorLockMode.Locked;
                }

                lastToggledState = toggled; 
            }


            //maxpalyers not sure
            if (_newMaxPlayers == 0)
            {
                _newMaxPlayers = 1;
            }
            else if (_newMaxPlayers > 30)
            {
                _newMaxPlayers = 30;
            }
            NetworkConnector.MAX_PLAYERS = _newMaxPlayers;


            GravityGun();

            if (GUI.clearStatuses)
            {
                ClearAllStatus(); 
            }

            // noclip
            //if (gui.boxesp)
            //{
            //    noclipupdate();
            //}

            //if (GUI.boxesp)
            //{
            //    BoneESP();
            //} else { BoneESP(); }

            if (GUI.crasher)
            {
                Crasher();

                
            }




            if (GUI.godmode)
            {


                //Mine
                if (!Character.localCharacter.photonView.IsMine)
                {
                    return;
                }
                Character.localCharacter.data.currentStamina = 999;
                GUIManager.instance.bar.ChangeBar();
                Character.localCharacter.AddExtraStamina(100);
                Character.InfiniteStamina();


                Character.localCharacter.refs.climbing.climbSpeed = 10;
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




            if (GUI.BingbongSpam)
            {
                BingBongSpam();

            }
            else
            {

            }


            if (GUI.Unlockall)
            {
                PassportManager.instance.testUnlockAll = true;

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

            if (GUI.speed)
            {

                PlayerHandler.GetPlayer(Character.localCharacter.photonView.Owner);
                Character.localCharacter.refs.movement.movementModifier = GUI.speedmultiply;
                // Character.localCharacter.refs.movement.

                //      PlayerController.Mine.PlayerSpeedMultiplier = GUI.speedmultiply;
            }
            else
            {
                Character.localCharacter.refs.movement.movementModifier = 1; 
            }
        }
    }
}
