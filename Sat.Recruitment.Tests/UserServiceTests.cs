using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sat.Recruitment.EntitiesProvider.Commons;
using Sat.Recruitment.EntitiesProvider.Dtos;
using Sat.Recruitment.EntitiesProvider.Enums;
using Sat.Recruitment.EntitiesProvider.Mappers;
using Sat.Recruitment.EntitiesProvider.Models;
using Sat.Recruitment.Repositories.Interfaces;
using Sat.Recruitment.Services;
using Sat.Recruitment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private IUserService _userService;
        private Mock<IUserMapper> _userMaper;
        private Mock<IUserRepository> _userRepository;


        [TestInitialize]
        public void SetUp()
        {
            _userRepository = new Mock<IUserRepository>();
            _userMaper = new Mock<IUserMapper>();
            _userService = new UserService(_userRepository.Object, _userMaper.Object);
        }

        [TestMethod]
        public void CreateUser_OK()
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
            var userDto = new UserDto
            {
                Name = "IgorTest",
                Address = "Beruti 3372",
                Email = "igorovchinnikov33@gmail.com",
                Phone = "2216434567",
                Money = 100,
                UserType = "Normal"
            };

            _userRepository.Setup(x => x.GetUsers()).Returns(Task.FromResult<IEnumerable<User>>(new List<User>() { new User() }));
            _userRepository.Setup(x => x.Add(It.IsAny<User>())).Returns(Task.FromResult<User>(new NormalUser
            {
                Name = "IgorTest",
                Address = "Beruti 3372",
                Email = "igorovchinnikov33@gmail.com",
                Phone = "2216434567",
                Money = 100,
                UserType = UserType.Normal
            }));
            _userMaper.Setup(x => x.FromEntityToDto(It.IsAny<User>())).Returns(userDto);


            //act
            var result = _userService.CreateUser(user).ConfigureAwait(false).GetAwaiter().GetResult();

            //assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull((result as Result<UserDto>).Data);
        }

        [TestMethod]
        public void Create_AlreadyExists_SuperUser_Not_OK()
        {
            //init
            var user = new SuperUser
            {
                Name = "IgorTest",
                Address = "Beruti 3372",
                Email = "igorovchinnikov33@gmail.com",
                Phone = "2216434567",
                Money = 100,
                UserType = UserType.SuperUser
            };
            _userRepository.Setup(x => x.GetUsers()).Returns(Task.FromResult<IEnumerable<User>>(new List<User>() { user }));

            //act
            var result = _userService.CreateUser(user).ConfigureAwait(false).GetAwaiter().GetResult();

            //assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(result.Message, "User is duplicated");
        }

        [TestMethod]
        public void AddUser_OK()
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
            var normalUser = new NormalUser
            {
                Name = "IgorTest",
                Address = "Beruti 3372",
                Email = "igorovchinnikov33@gmail.com",
                Phone = "2216434567",
                Money = 100,
                UserType = UserType.Normal
            };
            _userRepository.Setup(x => x.Add(It.IsAny<User>())).Returns(Task.FromResult<User>(new NormalUser
            {
                Name = "IgorTest",
                Address = "Beruti 3372",
                Email = "igorovchinnikov33@gmail.com",
                Phone = "2216434567",
                Money = 100,
                UserType = UserType.Normal
            }));

            //act
            var result = _userService.Add(user).ConfigureAwait(false).GetAwaiter().GetResult();

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetType(), typeof(NormalUser));
        }

        [TestMethod]
        public void UserExists()
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
            _userRepository.Setup(x => x.GetUsers()).Returns(Task.FromResult<IEnumerable<User>>(new List<User>() { user }));

            //act
            var result = _userService.Exists(user).ConfigureAwait(false).GetAwaiter().GetResult();

            //assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CalculateMoney_By_UserType()
        {
            //init
            var userNormal1 = new User
            {         
                Money = 110,//expect 123.2
                UserType = UserType.Normal
            };
            var userNormal2 = new User
            {
                Money = 90,//expect 97.2
                UserType = UserType.Normal
            };
            var userNormal3 = new User
            {
                Money = 100,//expect 100
                UserType = UserType.Normal
            };
            var userSuper = new User
            {
                Money = 200,//expect 240
                UserType = UserType.SuperUser
            };
            var userPremium = new User
            {
                Money = 400,//expect 1200
                UserType = UserType.Premium
            };

            var list = new List<User>() { userNormal1, userNormal2, userNormal3, userSuper, userPremium };
            var usersList = new List<User>();
            
            //act
            list.ForEach(x =>
            {
                var newUser = x.GetUserByType();
                newUser.CalculateMoney();
                usersList.Add(newUser);
            });
              
            //assert
            Assert.AreEqual(UserType.Normal, usersList[0].UserType);
            Assert.AreEqual(UserType.SuperUser, usersList[3].UserType);
            Assert.AreEqual(UserType.Premium, usersList[4].UserType);
            Assert.AreEqual(Convert.ToDecimal(123.20), usersList[0].Money);
            Assert.AreEqual(Convert.ToDecimal(162), usersList[1].Money);
            Assert.AreEqual(Convert.ToDecimal(100), usersList[2].Money);
            Assert.AreEqual(Convert.ToDecimal(240), usersList[3].Money);
            Assert.AreEqual(Convert.ToDecimal(1200), usersList[4].Money);
        }


    }
}

