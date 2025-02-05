using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using WeaponDamage_Part_1;

namespace SwordDamage_WPF_Part_1
{
    internal class SwordDamage : WeaponDamage
    {
        private const int BASE_DAMAGE = 3;
        private const int FLAME_DAMAGE = 2;

        
        /// <summary>
        /// 현재 속성들의 값을 기준으로 데미지를 계산합니다.
        /// </summary>
        protected override void CalculateDamage()
        {
            decimal magicMultiplier = 1M;
            if (Magic) magicMultiplier = 1.75M;

            Damage = BASE_DAMAGE;
            Damage = (int)(Roll * magicMultiplier) + BASE_DAMAGE;
            if (Flaming) Damage += FLAME_DAMAGE;
        }

        public SwordDamage(int startingRoll) : base(startingRoll) {}
    }
}
