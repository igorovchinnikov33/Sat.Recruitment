using System;

namespace Sat.Recruitment.EntitiesProvider.Models
{
    public class NormalUser : User
    {
        public override void CalculateMoney()
        {
            if (Money > 100)
            {
                var percentage = Convert.ToDecimal(0.12);
                Money += Money * percentage;
            }

            if (Money < 100 && Money > 10)
            {
                var percentage = Convert.ToDecimal(0.8);
                Money += Money * percentage;
            }
        }
    }
}
