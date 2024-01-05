using NSubstitute;
using Players.Domain.PlayerAggregate.Services;

namespace Players.SharedTestClasess.PlayerAggregate.Factories;

public static class PlayerDomainServicesTestFactory
{
    public static IDuplicateRegistrationCheckService CreateDuplicateRegistrationCheckServiceThatReturnsTrueResult()
    {
        var duplicateRegistrationCheckService = Substitute.For<IDuplicateRegistrationCheckService>();

        duplicateRegistrationCheckService.CheckAsync(Arg.Any<string>()).Returns(true);

        return duplicateRegistrationCheckService;
    }

    public static IDuplicateRegistrationCheckService CreateDuplicateRegistrationCheckServiceThatReturnsFalseResult()
    {
        var duplicateRegistrationCheckService = Substitute.For<IDuplicateRegistrationCheckService>();

        duplicateRegistrationCheckService.CheckAsync(Arg.Any<string>()).Returns(false);

        return duplicateRegistrationCheckService;
    }
}
