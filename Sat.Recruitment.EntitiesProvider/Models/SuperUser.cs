using System;

namespace Sat.Recruitment.EntitiesProvider.Models
{
    public class SuperUser : User
    {
        public override void CalculateMoney()
        {
            if (Money > 100)
            {
                var percentage = Convert.ToDecimal(0.20);
                Money += Money * percentage;
            }
        }
    }
}
