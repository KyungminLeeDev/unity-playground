using System;
using System.Collections.Generic;
using System.Text;

namespace WeaponDamage_Part_1
{
    internal class WeaponDamage
    {
        public int Damage { get; protected set; }
        private int roll;
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
        public bool Flaming
        {
            get { return flaming; }
            set
            {
                flaming = value;
                CalculateDamage();
            }
        }


        protected virtual void CalculateDamage() { /* 하위 클래스에서 재정의합니다.  */}
        public WeaponDamage(int startingRoll)
        {
            roll = startingRoll;
            CalculateDamage();
        }

    }
}
