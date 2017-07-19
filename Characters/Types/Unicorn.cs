﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Characters.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace Characters.Types
{
    public class Unicorn : ACharacter
    {
        private Unicorn(CharacterDto characterDto): base(characterDto)
        {
        }

        public override void Initizalize(CharacterDto character)
        {
            InitiazlizeBase(character);
            // something about abilities or in awake
            //AbilityFirst = a1;
            //AbilitySecond = a2;
        }

        protected override void ActivateAbility1()
        {
            throw new NotImplementedException();
        }

        protected override void ActivateAbility2()
        {
            throw new NotImplementedException();
        }

        public static Unicorn Create(CharacterDto character)
        {
            return new Unicorn(character);
        }
    }
}
