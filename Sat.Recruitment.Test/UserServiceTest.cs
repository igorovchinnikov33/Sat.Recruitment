using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sat.Recruitment.EntitiesProvider.Enums;
using Sat.Recruitment.EntitiesProvider.Mappers;
using Sat.Recruitment.EntitiesProvider.Models;
using Sat.Recruitment.Repositories.Interfaces;
using Sat.Recruitment.Services;
using Sat.Recruitment.Services.Interfaces;
using System;

namespace Sat.Recruitment.Test
{
    [TestClass]
    public class UserServiceTest
    {
        private IUserService _userService;        
        private Mock<IUserRepository> _userRepository;
        private Mock<IUserMapper> _userMaper;

        [TestInitialize]
        public void SetUp()
        {
            _userRepository = new Mock<IUserRepository>();
            _userMaper = new Mock<IUserMapper>();
            _userService = new UserService(_userRepository.Object, _userMaper.Object);
        }

        [TestMethod]
        public void CreateNormalUserOK()
        {
            try
            {
                //init
                var user = new User
                {
                    Name = "IgorTest",
                    Address = "Beruti 3372",
                    Email = "igorovchinnikov33@gmail.com",
                    Phone = "2216434567",
                    Money = 100,
                    UserType = UserType.Normal
                };

                //act
                var result = _userService.CreateUser(user).ConfigureAwait(false).GetAwaiter().GetResult();

                //assert
                Assert.AreEqual(result.GetType(), typeof(NormalUser));
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        [TestMethod]
        public async void CreateNormalUserDuplicateERROR()
        {
            //init
            var user = new User
            {
                Name = "IgorTest",
                Address = "Beruti 3372",
                Email = "igorovchinnikov33@gmail.com",
                Phone = "2216434567",
                Money = 100,
                UserType = UserType.Normal
            };

            //act
            var result = await _userService.CreateUser(user);

            //assert
            Assert.AreEqual(result.GetType(), typeof(NormalUser));
        }

        //[TestMethod]
        //public void CreateNormalUserOK()
        //{
        //    //init
        //    var user = new User
        //    {
        //        Name = "Igor",
        //        Address = "Beruti 3372",
        //        Email = "igorovchinnikov33@gmail.com",
        //        Phone = "2216434567",
        //        Money = 100,
        //        UserType = UserType.Normal
        //    };

        //    //act
        //    var result = user.GetUserByType();

        //    //assert
        //    Assert.AreEqual(result.GetType(), typeof(NormalUser));
        //}
    }
}
