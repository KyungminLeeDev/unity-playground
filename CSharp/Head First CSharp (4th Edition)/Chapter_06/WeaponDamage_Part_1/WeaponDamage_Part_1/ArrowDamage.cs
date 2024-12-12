using System;
using System.Collections.Generic;
using System.Text;

namespace WeaponDamage_Part_1
{
    internal class ArrowDamage
    {
        private const decimal BASE_MULTIPLIER = 0.35M;
        private const decimal MAGIC_MULTIPLIER = 2.5M;
        private const decimal FLAME_DAMAGE = 1.25M;

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
            decimal baseDamage = Roll * BASE_MULTIPLIER;
            if (Magic) baseDamage *= MAGIC_MULTIPLIER;
            if (flaming) Damage = (int)Math.Ceiling(baseDamage + FLAME_DAMAGE);
            else Damage = (int)Math.Ceiling(baseDamage);
        }

        /// <summary>
        /// 생성자는 기본 Magic, Flaming 값과 주사위 3개를 굴려서 나온 값을 기준으로 데미지를 계산합니다.
        /// </summary>
        /// <param name="startingRoll">주사위 3개를 굴려서 나온 값</param>
        public ArrowDamage(int startingRoll)
        {
            roll = startingRoll;
            CalculateDamage();
        }
    }
}
