using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestApp.Fundamentals.xUnitUnitTests
{
    public class MarkdownFormatterTests
    {
        private const string DoubleAsterix = "**";

        private const string ContentEncloseDoubleAsterixPattern = @"^\*{2}[a-zA-Z ]*\*{2}$";
        
        private readonly MarkdownFormatter markdownFormatter;

        public MarkdownFormatterTests()
        {
            markdownFormatter = new MarkdownFormatter();
        }

        [Theory]
        [InlineData("a")]
        [InlineData("abc")]
        [InlineData("Lorem ipsum")]
        public void FormatAsBold_ValidContent_ShouldReturnsContentEncloseDoubleAsterix(string content)
        {
            // Act
            var result = markdownFormatter.FormatAsBold(content);

            // Assert
            // Assert.Equal($"**abc**", result); // <-- szczegółowy test
           
            // test ogólny
            Assert.StartsWith(DoubleAsterix, result); 
            Assert.Contains(content, result);
            Assert.EndsWith(DoubleAsterix, result);

            result.Should()
                .StartWith(DoubleAsterix)
                .And.Contain(content)
                .And.EndWith(DoubleAsterix);

            
            // Weryfikacja z użyciem wyrażeń regularnych
            Assert.Matches(ContentEncloseDoubleAsterixPattern, result);
        }

        [Fact]
        public void FormatAsBold_EmptyContent_ShouldThrowFormatException()
        {            
            // Act
            Action act = ()=>markdownFormatter.FormatAsBold(string.Empty);

            // Assert
            Assert.Throws<FormatException>(act);
        }
    }
}
