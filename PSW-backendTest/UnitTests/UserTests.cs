using Microsoft.AspNetCore.Mvc;
using Moq;
using PSW_backend.Adapters;
using PSW_backend.Controllers;
using PSW_backend.Dtos;
using PSW_backend.Enums;
using PSW_backend.Models;
using PSW_backend.Repositories.Interfaces;
using PSW_backend.Services;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PSW_backendTest
{
    public class UserTests
    {
        #region Variables
        private readonly Mock<IUserRepository> _stubUserRepository;
        private UserService _userService;
        private UserController _userController;
        private List<User> _users;
        private List<UserDto> _userDtos;
        #endregion Variables

        public UserTests()
        {
            _stubUserRepository = new Mock<IUserRepository>();

            _users = new List<User>();
            _userDtos = new List<UserDto>();
        }

        #region AdapterTests
        [Fact]
        public void Adapts_user_Dto_to_user()
        {
            //Arange
            UserDto userDto = CreateUserDto();

            //Act
            User user = UserAdapter.UserDtoToUser(userDto);

            //Assert
            user.ShouldNotBeNull();
            user.ShouldBeOfType(typeof(User));
        }

        [Fact]
        public void Adapts_user_to_user_Dto()
        {
            //Arange
            User user = CreateUser();

            //Act
            UserDto userDto = UserAdapter.UserToUserDto(user);

            //Assert
            userDto.ShouldNotBeNull();
            userDto.ShouldBeOfType(typeof(UserDto));
        }
        #endregion AdapterTests

        #region LoginTests
        [Fact]
        public void Checks_if_username_is_valid_true()
        {
            //Arange
            User user = CreateUser();
            LoginDto loginDto = new LoginDto { Username = user.Username, Password = user.Password };
            ArrangeForCheckingIfUsernameOrPasswordExistsTests(loginDto);

            //Act
            User ValidUser = _userService.CheckIfUsernameIsValid(loginDto.Username);

            //Assert
            ValidUser.ShouldNotBeNull();
            ValidUser.ShouldBeOfType<User>();
        }

        [Fact]
        public void Checks_if_username_is_valid_false()
        {
            //Arange
            LoginDto loginDto = new LoginDto { Username = "nonExistingUsername", Password = "123" };
            ArrangeForCheckingIfUsernameOrPasswordExistsTests(loginDto);

            //Act
            User ValidUser = _userService.CheckIfUsernameIsValid(loginDto.Username);

            //Assert
            ValidUser.ShouldBeNull();
        }

        [Fact]
        public void Checks_if_password_is_valid_true()
        {
            //Arange
            User user = CreateUser();
            LoginDto loginDto = new LoginDto { Username = user.Username, Password = user.Password };
            ArrangeForCheckingIfUsernameOrPasswordExistsTests(loginDto);

            //Act
            bool IsPasswordValid = _userService.CheckIfPasswordIsValid(user.Password, loginDto.Password);

            //Assert
            IsPasswordValid.ShouldBeTrue();
        }

        [Fact]
        public void Checks_if_password_is_valid_false()
        {
            //Arange
            User user = CreateUser();
            LoginDto loginDto = new LoginDto { Username = user.Username, Password = "nonExistingPassword" };
            ArrangeForCheckingIfUsernameOrPasswordExistsTests(loginDto);

            //Act
            bool IsPasswordValid = _userService.CheckIfPasswordIsValid(user.Password, loginDto.Password);

            //Assert
            IsPasswordValid.ShouldBeFalse();
        }

        [Fact]
        public void Login_controller()
        {
            //Arange
            UserDto userDto = CreateUserDto();
            LoginDto loginDto = CreateLoginDto();
            _stubUserRepository.Setup(x => x.GetAll()).Returns(CreateUsers());
            _stubUserRepository.Setup(x => x.GetUserByUsername(loginDto.Username)).Returns(CreateUsers().Find(user => user.Username == loginDto.Username));
            _userService = new UserService(_stubUserRepository.Object);
            _userController = new UserController(_userService);

            //Act
            var actionResult = _userController.Login(loginDto);

            //Assert
            ((actionResult as OkObjectResult).Value as UserDto).ShouldBeEquivalentTo(userDto);
        }
        #endregion loginTests

        #region HelperFunctions
        private UserDto CreateUserDto()
        {
            return new UserDto
            {
                Id = 1,
                Name = "Milica",
                Surname = "Brujic",
                Username = "mb",
                Email = "mb@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                Role = "Patient",
                IsBlocked = false,
                IsMalicious = false
            };
        }
        private User CreateUser()
        {
            return new User
            {
                Id = 1,
                Name = "Milica",
                Surname = "Brujic",
                Username = "mb",
                Email = "mb@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                Role = Roles.Patient,
                IsBlocked = false,
                IsMalicious = false,
            };
        }

        private List<User> CreateUsers()
        {
            _users.Add(new User
            {
                Id = 1,
                Name = "Milica",
                Surname = "Brujic",
                Username = "mb",
                Email = "mb@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                Role = Roles.Patient,
                IsBlocked = false,
                IsMalicious = false,
            });

            _users.Add(new Patient
            {
                Id = 2,
                Name = "Ivan",
                Surname = "Ivanovic",
                Username = "ii",
                Email = "ii@gmail.com",
                Password = "123",
                Address = "Laze Teleckog 1",
                PhoneNumber = "060000000",
                Role = Roles.Patient,
                IsBlocked = false,
                IsMalicious = false,
            });

            return _users;
        }

        private LoginDto CreateLoginDto()
        {
            return new LoginDto
            {
                Username = "mb",
                Password = "123",
            };
        }

        private void ArrangeForCheckingIfUsernameOrPasswordExistsTests(LoginDto loginDto)
        {
            _stubUserRepository.Setup(x => x.GetUserByUsername(loginDto.Username)).Returns(CreateUsers().Find(user => user.Username == loginDto.Username));
            _stubUserRepository.Setup(x => x.GetUserByPassword(loginDto.Password)).Returns(CreateUsers().Find(user => user.Password == loginDto.Password));
            _userService = new UserService(_stubUserRepository.Object);
        }
        #endregion HelperFunctions
    }
}
