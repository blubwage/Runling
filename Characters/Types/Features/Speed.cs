﻿using System.Collections;
using System.Collections.Generic;
using Drones.DroneTypes;
using Drones.Pattern;
using UnityEngine;

namespace Characters.Types.Features
{
    public class Speed
    {
        public int Points { get; private set; }
        
        public float Current
        {
            get { return _baseSpeed + _speedPointRatio * Points + _bonusSpeed; }
        }
        protected float _baseSpeed, _speedPointRatio;
        private float _bonusSpeed = 0F;

        public Speed(float baseSpeed, float speedPointRatio)
        {
            _baseSpeed = baseSpeed;
            _speedPointRatio = speedPointRatio;
        }

        public void IncrementPoints()
        {
            Points++;
        }

        public IEnumerator AddBonusSpeed(float bonusSpeed, float workingTime)
        {
            _bonusSpeed += bonusSpeed;
            yield return new WaitForSeconds(workingTime);
            _bonusSpeed -= bonusSpeed;
        }

        public void ActivateBoost(float boostSpeed) // probably need also bool variable to verify if is actiave already
        {
            _bonusSpeed += boostSpeed;
        }

        public void DeactivateBoost(float boostSpeed)
        {
            _bonusSpeed -= boostSpeed;
        }

    }


}