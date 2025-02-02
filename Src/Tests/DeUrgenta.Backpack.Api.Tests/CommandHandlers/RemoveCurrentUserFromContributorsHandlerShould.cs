﻿using System;
using System.Threading;
using System.Threading.Tasks;
using DeUrgenta.Backpack.Api.CommandHandlers;
using DeUrgenta.Backpack.Api.Commands;
using DeUrgenta.Common.Validation;
using DeUrgenta.Domain;
using DeUrgenta.Tests.Helpers;
using NSubstitute;
using Shouldly;
using Xunit;

namespace DeUrgenta.Backpack.Api.Tests.CommandHandlers
{
    [Collection(TestsConstants.DbCollectionName)]
    public class RemoveCurrentUserFromContributorsHandlerShould
    {
        private readonly DeUrgentaContext _dbContext;

        public RemoveCurrentUserFromContributorsHandlerShould(DatabaseFixture fixture)
        {
            _dbContext = fixture.Context;
        }

        [Fact]
        public async Task Return_failed_result_when_validation_fails()
        {
            // Arrange
            var validator = Substitute.For<IValidateRequest<RemoveCurrentUserFromContributors>>();
            validator
                .IsValidAsync(Arg.Any<RemoveCurrentUserFromContributors>())
                .Returns(Task.FromResult(false));

            var sut = new RemoveCurrentUserFromContributorsHandler(validator, _dbContext);

            // Act
            var result = await sut.Handle(new RemoveCurrentUserFromContributors("a-sub", Guid.NewGuid()), CancellationToken.None);

            // Assert
            result.IsFailure.ShouldBeTrue();
        }
    }
}