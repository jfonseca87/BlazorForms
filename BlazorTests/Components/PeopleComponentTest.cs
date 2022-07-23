using BlazorForms.Pages;
using BlazorForms.Services;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace BlazorTests.Components
{
    public class PeopleComponentTest
    {
        [Fact]
        public void LoadPeopleComponentMockingPeopleServiceWithFakeData()
        {
            using var ctx = new TestContext();
            var mockPersonService = new Mock<IPersonService>();
            ctx.Services.AddSingleton<IPersonService>(mockPersonService.Object);
            mockPersonService.Setup(x => x.GetPeople())
                .ReturnsAsync(new BlazorForms.Models.ApiResponse<List<BlazorForms.Models.Person>>
                {
                    Status = BlazorForms.Utils.ResponseStatus.Success,
                    Data = new List<BlazorForms.Models.Person>
                    {
                        new BlazorForms.Models.Person(),
                        new BlazorForms.Models.Person()
                    }
                });

            var component = ctx.RenderComponent<People>();

            Assert.True(true);
        }
    }
}
