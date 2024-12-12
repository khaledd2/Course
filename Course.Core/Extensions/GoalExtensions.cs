using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Extensions
{
    public static class GoalExtensions
    {
        public static void ConvertGoalsFromStringToList(this ICollection<Goal> goals, string stringGoals, int maxId)
        {
            // To convert goals from string to list and add it to Goals Table
            foreach (string goal in stringGoals.Split("-"))
            {
                var newGoal = new Goal()
                {
                    Id = ++maxId,
                    Name = goal
                };
                goals.Add(newGoal);
            }
        }
    }
}
