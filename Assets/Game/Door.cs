using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Interface;

namespace Game
{
    public class Door : MonoBehaviour
    {
        private GameObject character;
        private CameraFollow limits;
        private Cutscene cutscene;
        public Player.Player player { get; set; }
        public GameEvents events { get; set; }

        void Start()
		{
			character = GameObject.Find("Player");
			limits = FindObjectOfType<CameraFollow> ();
            cutscene = FindObjectOfType<Cutscene>();      
		}

		public void Enter(NPC.Collision teleporter)
        {
			string name = teleporter.GetName ();
			switch (name)
			{
			    case "DoorRWS1(Clone)":
                    EnterDoor(-60, -105, -68, -68);
				    break;
			    case "DoorMainHall1(Clone)":
                    EnterDoor(20, 0, 140, 20);
				    break;
			    case "DoorMainHall2(Clone)":
                    EnterDoor(-140, -182, -161, -161);
				    break;
			    case "DoorBar1(Clone)":
                    EnterDoor(-80, -105, -68, -80);
				    break;
                case "DoorMainHall3(Clone)":
                    EnterDoor(-225, -250, -240, -240);
                    break;
                case "DoorMetroHelsinki1(Clone)":
                    EnterDoor(-125, -105, -68, -105);
                    break;
                case "MetroHelsinki(Clone)":
                    if(player.money > 4.9)
                    {
                        EnterDoor(-300, -320, -315, -315);
                        player.useMoney(-5);
                        events.ChangeTime(10);
                        StartCoroutine(cutscene.CutsceneMetro());
                    }
                    else
                    {
                        //not enough money
                    }
                    break;
                case "MetroSornainen(Clone)":
                    if (player.money > 4.9)
                    {
                        EnterDoor(-260, -250, -240, -250);
                        player.useMoney(-5);
                        events.ChangeTime(10);
                        StartCoroutine(cutscene.CutsceneMetro());
                    }
                    else
                    {
                        //not enough money
                    }
                    break;
            }
        }
        /// <summary>
        /// Transports player and camera to the next area and sets the cameras borders in that area.
        /// </summary>
        /// <param name="xLocationPlayer"> Player moves to this location </param>
        /// <param name="xMin"> Cameras left border in next area </param>
        /// <param name="xMax"> Cameras right border in next area </param>
        /// <param name="xLocationCam"> Camera moves to this locatioin </param>
        private void EnterDoor(float xLocationPlayer, float xMin, float xMax, float xLocationCam)
        {
            character.transform.position = new Vector2(xLocationPlayer, -8f);
            Camera.main.transform.position = new Vector3(xLocationCam, 3f, -10f);
            limits.xMin = xMin;
            limits.xMax = xMax;
        }

    }
}