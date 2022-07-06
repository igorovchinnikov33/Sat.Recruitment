using Sat.Recruitment.EntitiesProvider.Enums;

namespace Sat.Recruitment.EntitiesProvider.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public decimal Money { get; set; }

        public virtual void CalculateMoney() { }

        public User GetUserByType()
        {
            var user = new User();
            switch (UserType)
            {
                case UserType.SuperUser:
                    user = new SuperUser {
                        Address = Address,
                        Email = Email, 
                        Name = Name, 
                        Phone = Phone, 
                        Money = Money, 
                        UserType = UserType.SuperUser 
                    };
                    break;

                case UserType.Normal:
                    user = new NormalUser { 
                        Address = Address, 
                        Email = Email, 
                        Name = Name, 
                        Phone = Phone, 
                        Money = Money, 
                        UserType = UserType.Normal 
                    };
                    break;

                case UserType.Premium:
                    user = new PremiumUser { 
                        Address = Address, 
                        Email = Email, 
                        Name = Name, 
                        Phone = Phone, 
                        Money = Money, 
                        UserType = UserType.Premium
                    };
                    break;

                default:
                    break;
            }
            return user;

        }
    }
}
