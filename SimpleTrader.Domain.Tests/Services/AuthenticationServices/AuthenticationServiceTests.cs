using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading.Tasks;
using SimpleTrader.Domain.Services.AuthenticationServices;
using Moq;
using SimpleTrader.Domain.Services;
using Microsoft.AspNet.Identity;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Exceptions;

namespace SimpleTrader.Domain.Tests.Services.AuthenticationServices
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private Mock<IPasswordHasher>? mockPasswordHasher;
        private Mock<IAccountService>? mockAccountService;
        private AuthenticationService? authenticationService;

        [SetUp]
        public void SetUp()
        {
            mockAccountService = new Mock<IAccountService>();
            mockPasswordHasher = new Mock<IPasswordHasher>();
            authenticationService = new AuthenticationService(mockAccountService.Object, mockPasswordHasher.Object);

        }

        [Test]
        public async Task Login_WithCorrectPasswordForExistingUsername_ReturnsAccountForCorrectUsername()
        {
            //Arrange
            string expectedUsername = "testuser";
            string password = "testpassword";
           
            if (mockAccountService != null)
                mockAccountService.Setup(s => s.GetByUsername(expectedUsername)).
                ReturnsAsync(new Account()
                {
                    AccountHolder = new User()
                    {   
                        UserName = expectedUsername
                    }
                }); ;
          
            if (mockPasswordHasher != null)
                mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).Returns(PasswordVerificationResult.Success);   
                
            
            //Act
            Account account = await authenticationService.Login(expectedUsername, password);


            //Assert
            string actualUsername = account.AccountHolder.UserName;  
            Assert.AreEqual(expectedUsername,actualUsername);
        }

        [Test]
        public void Login_WithNonExistingUsername_ThrowsInvalidPasswordExceptionForUsername()
        {
            //Arrange
            string expectedUsername = "testuser";
            string password = "testpassword";
          
            
            if (mockPasswordHasher != null)
                mockPasswordHasher.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), password)).
                    Returns(PasswordVerificationResult.Failed);


            //Act
            UserNotFoundException exception = Assert.ThrowsAsync<UserNotFoundException>(() => authenticationService.Login(expectedUsername, password));


            //Assert
            string actualUsername = exception.Username;
            Assert.AreEqual(expectedUsername, actualUsername);
        }

        [Test]
        public async Task Register_WithPasswordsNotMatching_ReturnsPasswordsDoNotMatch()
        {
            string password = "testpassword";
            string confirmPassword = "confirmtestpassword";

            RegistrationResult expected = RegistrationResult.PasswordsDoNotMatch;

            RegistrationResult actual = await authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), password, confirmPassword);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingEmail_ReturnsEmailAlreadyExist()
        {
            string email = "test@gmail.com";
            mockAccountService.Setup(s => s.GetByEmail(email)).ReturnsAsync(new Account());
            RegistrationResult expected = RegistrationResult.EmailAlreadyExists;

            RegistrationResult actual = await authenticationService.Register(email, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithAlreadyExistingUsername_ReturnsUsernameAlreadyExist()
        {
            string username = "qz21";
            mockAccountService.Setup(s => s.GetByUsername(username)).ReturnsAsync(new Account());
            RegistrationResult expected = RegistrationResult.UsernameAlreadyExists;

            RegistrationResult actual = await authenticationService.Register( It.IsAny<string>(),username, It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Register_WithNonExistingUserAndMatchingPasswords_ReturnsSuccess()
        {          
            RegistrationResult expected = RegistrationResult.Success;

            RegistrationResult actual = await authenticationService.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expected, actual);
        }
    }
}
