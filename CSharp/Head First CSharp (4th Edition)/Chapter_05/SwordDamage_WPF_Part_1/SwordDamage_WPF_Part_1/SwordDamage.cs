using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SwordDamage_WPF_Part_1
{
    internal class SwordDamage
    {
        private const int BASE_DAMAGE = 3;
        private const int FLAME_DAMAGE = 2;

        /// <summary>
        /// 계산된 데미지 값을 저장합니다.
        /// </summary>
        public int Damage { get; private set; }

        private int roll;

        /// <summary>
        /// 주사위 3개를 굴려서 나온 값을 설정하거나 변환합니다.
        /// </summary>
        public int Roll
        {
            get { return roll; }
            set
            {
                roll = value;
                CalculateDamage();
            }
        }

        private bool magic;

        /// <summary>
        /// 마법 칼이면 true, 아니면 false를 반환합니다.
        /// </summary>
        public bool Magic
        {
            get { return magic; }
            set
            {
                magic = value;
                CalculateDamage();
            }
        }

        private bool flaming;

        /// <summary>
        /// 불타는 칼이면 true, 아니면 false를 반환합니다.
        /// </summary>
        public bool Flaming
        {
            get { return flaming; }
            set
            {
                flaming = value;
                CalculateDamage();
            }
        }


        /// <summary>
        /// 현재 속성들의 값을 기준으로 데미지를 계산합니다.
        /// </summary>
        public void CalculateDamage()
        {
            decimal magicMultiplier = 1M;
            if (Magic) magicMultiplier = 1.75M;

            Damage = BASE_DAMAGE;
            Damage = (int)(Roll * magicMultiplier) + BASE_DAMAGE;
            if (flaming) Damage += FLAME_DAMAGE;
        }

        /// <summary>
        /// 생성자는 기본 Magic, Flaming 값과 주사위 3개를 굴려서 나온 값을 기준으로 데미지를 계산합니다.
        /// </summary>
        /// <param name="startingRoll">주사위 3개를 굴려서 나온 값</param>
        public SwordDamage(int startingRoll)
        {
            roll = startingRoll;
            CalculateDamage();
        }
    }
}
