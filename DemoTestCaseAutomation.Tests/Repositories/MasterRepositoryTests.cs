using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoTestCaseAutomation.Domain.Entities;
using DemoTestCaseAutomation.Infrastructure.Data;
using DemoTestCaseAutomation.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DemoTestCaseAutomation.Tests.Repositories
{
    public class MasterRepositoryTests
    {
        private ApplicationDbContext CreateDbContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task GetCitiesAsync_ReturnsAllCities()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using (var context = CreateDbContext(dbName))
            {
                context.Cities.AddRange(
                    new City { Id = 1, Name = "City 1", StateId = 1 },
                    new City { Id = 2, Name = "City 2", StateId = 2 }
                );
                await context.SaveChangesAsync();
            }

            // Act
            IEnumerable<City> result;
            using (var context = CreateDbContext(dbName))
            {
                var repository = new MasterRepository(context);
                result = await repository.GetCitiesAsync();
            }

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetStatesAsync_ReturnsAllStates()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using (var context = CreateDbContext(dbName))
            {
                context.States.AddRange(
                    new State { Id = 1, Name = "State 1" },
                    new State { Id = 2, Name = "State 2" }
                );
                await context.SaveChangesAsync();
            }

            // Act
            IEnumerable<State> result;
            using (var context = CreateDbContext(dbName))
            {
                var repository = new MasterRepository(context);
                result = await repository.GetStatesAsync();
            }

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetCitiesByStateIdAsync_WhenStateIdIsTwoOrLess_ReturnsEmptyOrDefaultList(int stateId)
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using (var context = CreateDbContext(dbName))
            {
                context.Cities.AddRange(
                    new City { Id = 1, Name = "City 1", StateId = 1 },
                    new City { Id = 2, Name = "City 2", StateId = 2 }
                );
                await context.SaveChangesAsync();
            }

            // Act
            IEnumerable<City> result;
            using (var context = CreateDbContext(dbName))
            {
                var repository = new MasterRepository(context);
                result = await repository.GetCitiesByStateIdAsync(stateId);
            }

            // Assert
            Assert.True(result == null || !result.Any(), $"Expected no cities returned for StateId {stateId} when stateId <= 2.");
        }

        [Fact]
        public async Task GetCitiesByStateIdAsync_WhenStateIdGreaterThanTwo_ReturnsMatchingCities()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            int targetStateId = 3;

            using (var context = CreateDbContext(dbName))
            {
                context.Cities.AddRange(
                    new City { Id = 1, Name = "City A", StateId = 2 },
                    new City { Id = 2, Name = "City B", StateId = 3 },
                    new City { Id = 3, Name = "City C", StateId = 3 }
                );
                await context.SaveChangesAsync();
            }

            // Act
            IEnumerable<City> result;
            using (var context = CreateDbContext(dbName))
            {
                var repository = new MasterRepository(context);
                result = await repository.GetCitiesByStateIdAsync(targetStateId);
            }

            // Assert
            Assert.NotNull(result);
            var cityList = result.ToList();
            Assert.Equal(2, cityList.Count);
            Assert.All(cityList, c => Assert.Equal(targetStateId, c.StateId));
        }
    }
}