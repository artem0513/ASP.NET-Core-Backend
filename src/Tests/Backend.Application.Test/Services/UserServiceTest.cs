using Xunit;
using Sample01.Application.Models.Requests;
using Moq;
using Backend.Application.Services;
using Backend.Application.Core.Services;
using Backend.Domain.Core.Specifications;
using Backend.Domain.Core.Repositories;
using Backend.Domain.Entities;
using Backend.Domain.Enums;
using Backend.Domain.Exceptions;

namespace Backend.Application.Test.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<ILoggerService> _loggerMock = new Mock<ILoggerService>();
        private readonly UserService _userService;

        public UserServiceTest()
        {
            _userService = new UserService(_unitOfWorkMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async void Given_WithValidData_When_CreateUser_Then_SuccessfullyCreateUser()
        {
            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(x => x.Repository<User>().AddAsync(It.IsAny<User>()))
                .ReturnsAsync(new User
                {
                    Id = id,
                    FirstName = "Nilav",
                    LastName = "Patel",
                    EmailId = "nilavpatel1992@gmail.com",
                    Password = "Test123",
                    Status = UserStatus.Active
                });
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var result = await _userService.CreateUser(new CreateUserReq
            {
                FirstName = "Nilav",
                LastName = "Patel",
                EmailId = "nilavpatel1992@gmail.com",
                Password = "Test123",
                Status = UserStatus.Active
            });

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal(id, result.Data.Id);
        }

        [Fact]
        public async void Given_UserNotExist_When_ValidateUser_Then_ThrowException()
        {
            _unitOfWorkMock.Setup(x => x.Repository<User>().FirstOrDefaultAsync(It.IsAny<ISpecification<User>>()))
                .ReturnsAsync((User)null);

            await Assert.ThrowsAsync<UserNotFoundException>(async () => await _userService.ValidateUser(new ValidateUserReq
            {
                EmailId = "nilavpatel1992@gmail.com",
                Password = "Test123"
            }));
        }

        [Fact]
        public async void Given_UserIsNotActive_When_ValidateUser_Then_ThrowException()
        {
            var id = Guid.NewGuid();
            User user = new User
            {
                Id = id,
                FirstName = "Nilav",
                LastName = "Patel",
                EmailId = "nilavpatel1992@gmail.com",
                Password = "Test123",
                Status = UserStatus.InActive
            };

            _unitOfWorkMock.Setup(x => x.Repository<User>().FirstOrDefaultAsync(It.IsAny<ISpecification<User>>()))
                .ReturnsAsync(user);

            await Assert.ThrowsAsync<UserIsNotActiveException>(async () => await _userService.ValidateUser(new ValidateUserReq
            {
                EmailId = "nilavpatel1992@gmail.com",
                Password = "Test123"
            }));
        }

        [Fact]
        public async void Given_ValidData_When_ValidateUser_Then_ReturnsResult()
        {
            var id = Guid.NewGuid();
            User user = new User
            {
                Id = id,
                FirstName = "Nilav",
                LastName = "Patel",
                EmailId = "nilavpatel1992@gmail.com",
                Password = "Test123",
                Status = UserStatus.Active
            };

            _unitOfWorkMock.Setup(x => x.Repository<User>().FirstOrDefaultAsync(It.IsAny<ISpecification<User>>()))
                .ReturnsAsync(user);

            var result = await _userService.ValidateUser(new ValidateUserReq
            {
                EmailId = "nilavpatel1992@gmail.com",
                Password = "Test123"
            });

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }
    }
}