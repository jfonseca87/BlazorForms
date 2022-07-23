using BlazorForms.Pages;
using Bunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlazorTests.Components
{
    public class CounterComponentTests
    {
        [Fact]
        public void RenderComponentSuccessfully()
        {
            // Arrange
            using var ctx = new TestContext();

            // Act 
            var component = ctx.RenderComponent<Counter>();

            // Assert
            Assert.NotNull(component);
            Assert.Equal("Click me", component.Find(".btn").TextContent);
        }

        [Fact]
        public void IncrementCounter()
        {
            // Arrange
            using var ctx = new TestContext();

            // Act 
            var component = ctx.RenderComponent<Counter>();
            var button = component.Find("button");
            button.Click();
            
            // Assert
            Assert.NotNull(component);
            string currentContent = component.Find("p").TextContent;
            Assert.Equal("Current count: 1", currentContent);
        }
    }
}
