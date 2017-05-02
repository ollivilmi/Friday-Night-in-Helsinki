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
        private System.Random random;

        void Start()
		{
            random = new System.Random();
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
                    EnterDoor(-270, -318, -238, -270);
				    break;
			    case "DoorMainHall1(Clone)":
                    EnterDoor(20, 0, 140, 20);
				    break;
			    case "DoorMainHall2(Clone)":
                    EnterDoor(-382, -458, -376, -382);
				    break;
			    case "DoorBar1(Clone)":
                    EnterDoor(-314, -318, -238, -314);
				    break;
                case "DoorMainHall3(Clone)":
                    EnterDoor(-150, -170, -162, -162);
                    break;
                case "DoorRWSCasino(Clone)":
                    EnterDoor(230, 195, 313, 230);
                    break;
                case "DoorRWSNightClub(Clone)":
                    EnterDoor(348, 368, 467, 368);
                    break;
                case "DoorMetroHelsinki1(Clone)":
                    EnterDoor(-242, -318, -238, -242);
                    break;
                case "DoorCasino(Clone)":
                    EnterDoor(135, 0, 140, 135);
                    break;
                case "DoorNightClub(Clone)":
                    EnterDoor(125, 0, 140, 125);
                    break;
                case "MetroHelsinki(Clone)":
                    if(player.money > 4.9)
                    {
                        EnterDoor(-545, -600, -545, -545);
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
                        EnterDoor(-458, -458, -376, -458);
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

        public void RandomLocation()
        {
            switch (random.Next(0, 6)) {
                case 0:
                    EnterDoor(20, 0, 140, 20);
                    break;
                case 1:
                    EnterDoor(230, 195, 313, 230);
                    break;
                case 2:
                    EnterDoor(-80, -105, -68, -80);
                    break;
                case 3:
                    EnterDoor(-150, -170, -161, -161);
                    break;
                case 4:
                    EnterDoor(348, 368, 467, 368);
                    break;
                case 5:
                    EnterDoor(-260, -250, -240, -250);
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
            events.ChangeTime(1);
            character.transform.position = new Vector2(xLocationPlayer, player.height);
            Camera.main.transform.position = new Vector3(xLocationCam, 2f, -10f);
            limits.xMin = xMin;
            limits.xMax = xMax;
        }

    }
}