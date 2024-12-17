using System;
using System.Collections.Generic;
using System.Text;

namespace BeehiveManagementSystem
{
    internal class Bee
    {
        public virtual float CostPerShift { get; }
        public string Job { get; private set; }
        public Bee(string job)
        {
            Job = job;
        }

        public void WorkTheNextShift()
        {
            if (HoneyVault.ConsumeHoney(CostPerShift))
            {
                DoJob();
            }
        }

        protected virtual void DoJob() { /* 하위 클래스에서 이 메서드 재정의 */ }

    }
}
