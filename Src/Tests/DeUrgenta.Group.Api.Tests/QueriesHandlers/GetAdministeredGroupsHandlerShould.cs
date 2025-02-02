﻿using System.Threading;
using System.Threading.Tasks;
using DeUrgenta.Common.Validation;
using DeUrgenta.Domain;
using DeUrgenta.Group.Api.Queries;
using DeUrgenta.Group.Api.QueryHandlers;
using DeUrgenta.Tests.Helpers;
using NSubstitute;
using Shouldly;
using Xunit;

namespace DeUrgenta.Group.Api.Tests.QueriesHandlers
{
    [Collection(TestsConstants.DbCollectionName)]
    public class GetAdministeredGroupsHandlerShould
    {
        private readonly DeUrgentaContext _dbContext;

        public GetAdministeredGroupsHandlerShould(DatabaseFixture fixture)
        {
            _dbContext = fixture.Context;
        }

        [Fact]
        public async Task Return_failed_result_when_validation_fails()
        {
            // Arrange
            var validator = Substitute.For<IValidateRequest<GetAdministeredGroups>>();
            validator
                .IsValidAsync(Arg.Any<GetAdministeredGroups>())
                .Returns(Task.FromResult(false));

            var sut = new GetAdministeredGroupsHandler(validator, _dbContext);

            // Act
            var result = await sut.Handle(new GetAdministeredGroups("a-sub"), CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
        }
    }
}