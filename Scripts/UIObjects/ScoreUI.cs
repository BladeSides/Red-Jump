using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Scripts.UIObjects
{
    public static class ScoreUI
    {
            public static int CurrentScore { get; set; } = 0;

        public static void Update(int scoreToAdd)
        {
            CurrentScore += scoreToAdd;
        }
    }
}
