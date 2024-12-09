using System;
using System.Collections.Generic;
using System.Text;

namespace AbilityScore
{
    internal class AbilityScoreCalculator
    {
        public int RollResult = 14;
        public double DivideBy = 1.75;
        public int AddAmount = 2;
        public int Minimum = 3;
        public int Score;
        public void CalculateAbilityScore()
        {
            // 굴리기 값을 DivideBy 필드 값으로 나누기
            double divided = RollResult / DivideBy;

            // AddAmount를 나눗셈 결과에 더하기
            int added = AddAmount + (int)divided;

            // 결과값이 너무 작으면 Minimum 값 사용
            if (added < Minimum)
            {
                Score = Minimum;
            }
            else
            {
                Score = added;
            }
        }
    }
}
