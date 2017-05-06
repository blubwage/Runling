﻿using System.Collections;
using Assets.Scripts.Launcher;
using Assets.Scripts.RLR.Levels;
using TMPro;
using UnityEngine;
using Assets.Scripts.UI.RLR_Menus;


namespace Assets.Scripts.RLR
{
    public class InitializeGameRLR : MonoBehaviour
    {

        // Attach scripts
        public LevelManagerRLR LevelManagerRLR;
        public ControlRLR ControlRLR;
        public InGameMenuManagerRLR InGameMenuManagerRLR;


        public GameObject PlayerPrefab;
        public GameObject LevelTextObject;
        public GameObject CountdownPrefab;
        public GameObject Player;
        public GameObject MainCamera;


        //set Spawnimmunity once game starts
        public void InitializeGame()
        {
            StartCoroutine(PrepareLevel());
        }

        IEnumerator PrepareLevel()
        {
            LevelManagerRLR.GenerateMap(GameControl.CurrentLevel);

            // Load drones and player
            var startPlatform = LevelManagerRLR.GenerateMapRLR.GetStartPlatform();
            var airColliderRange = LevelManagerRLR.GenerateMapRLR.GetAirColliderRange();
            Player = Instantiate(PlayerPrefab, new Vector3(startPlatform.x, 0, startPlatform.z), Quaternion.Euler(0, 90, 0));
            MainCamera.transform.position = new Vector3(Player.transform.localPosition.x, 40, Player.transform.localPosition.z);
            LevelManagerRLR.GenerateChasers(GameControl.CurrentLevel);
            GameControl.CameraRange = airColliderRange / 2.5f;
            GameControl.Dead = false;
            GameControl.IsInvulnerable = true;
            GameControl.IsImmobile = true;
            ControlRLR.StopUpdate = false;
            LevelManagerRLR.LoadDrones(GameControl.CurrentLevel);

            // Show current level
            var levelText = LevelTextObject.GetComponent<TextMeshProUGUI>();
            levelText.text = "Level " + GameControl.CurrentLevel;
            LevelTextObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            LevelTextObject.SetActive(false);
  
            yield return new WaitForSeconds(1f);
                   
            // Countdown
            for (var i = 0; i < 3; i++)
            {
                var countdown = Instantiate(CountdownPrefab, GameObject.Find("Canvas").transform);
                countdown.GetComponent<TextMeshProUGUI>().text = (3 - i).ToString();
                yield return new WaitForSeconds(1f);
                Destroy(countdown);
            }

            GameControl.IsInvulnerable = false;
            GameControl.IsImmobile = false;
        }
    }
}
