using Xunit;
using Microsoft.EntityFrameworkCore;
using Backend.Infrastructure.Repositories;
using Backend.Infrastructure.Data;
using Backend.Domain.Entities;
using Backend.Domain.Enums;

namespace Backend.Infrastructure.Test.Repositories
{
    public class BaseRepositoryAsyncTest
    {
        private readonly Sample01DbContext _Sample01DbContext;
        private readonly UnitOfWork _unitOfWork;

        public BaseRepositoryAsyncTest()
        {
            var options = new DbContextOptionsBuilder<Sample01DbContext>().UseInMemoryDatabase(databaseName: "Sample01Db").Options;
            _Sample01DbContext = new Sample01DbContext(options);
            _unitOfWork = new UnitOfWork(_Sample01DbContext);
        }

        [Fact]
        public async void Given_ValidData_When_AddAsync_Then_SuccessfullyInsertData()
        {
            var user = new User
            {
                FirstName = "Nilav",
                LastName = "Patel",
                EmailId = "nilavpatel1992@gmail.com",
                Password = "Test123",
                Status = UserStatus.Active,
                CreatedBy = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.UtcNow
            };

            var result = await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            Assert.Equal(result, _Sample01DbContext.Users.Find(result.Id));
        }
    }
}