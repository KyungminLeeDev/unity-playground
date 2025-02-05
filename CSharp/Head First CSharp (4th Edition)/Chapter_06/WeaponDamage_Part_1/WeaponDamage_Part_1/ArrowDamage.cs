using System;
using System.Collections.Generic;
using System.Text;

namespace WeaponDamage_Part_1
{
    internal class ArrowDamage : WeaponDamage
    {
        private const decimal BASE_MULTIPLIER = 0.35M;
        private const decimal MAGIC_MULTIPLIER = 2.5M;
        private const decimal FLAME_DAMAGE = 1.25M;


        /// <summary>
        /// 현재 속성들의 값을 기준으로 데미지를 계산합니다.
        /// </summary>
        protected override void CalculateDamage()
        {
            decimal baseDamage = Roll * BASE_MULTIPLIER;
            if (Magic) baseDamage *= MAGIC_MULTIPLIER;
            if (Flaming) Damage = (int)Math.Ceiling(baseDamage + FLAME_DAMAGE);
            else Damage = (int)Math.Ceiling(baseDamage);
        }

        public ArrowDamage(int startingRoll) : base(startingRoll) {}
    }
}
