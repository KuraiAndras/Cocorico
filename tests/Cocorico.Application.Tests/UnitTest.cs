using Cocorico.Shared.Dtos.Authentication;
using Xunit;

namespace Cocorico.Application.Tests
{
    public class UnitTest
    {
        [Fact]
        public void Test1()
        {
            var sut = new UserClaimRequest();

            Assert.NotNull(sut);
        }
    }
}
