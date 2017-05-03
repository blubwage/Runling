﻿using Assets.Scripts.Drones;
using UnityEngine;

namespace Assets.Scripts.RLR.Levels
{
    public class Level4RLR : ALevelRLR
    {
        public Level4RLR(LevelManagerRLR manager) : base(manager)
        {
        }

        public override void CreateDrones()
        {
            Area[] laneArea = Manager.GenerateMapRLR.GetDroneSpawnArea();

            // Spawn red drones
            DroneFactory.SpawnDrones(new RandomFlyingBouncingDrone(5f, 1f, Color.red), 100, area: laneArea[0]);
        }
    }
}