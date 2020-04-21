using System;
using System.Collections.Generic;
using Xunit;

namespace dotnetcore_closest_matching_string.Tests
{
    public class NameMatchingHelper_GetSpecificElementWithName_Should
    {
        private readonly NameMatchingHelper<MockObject> _nameMatchingHelper;
        private List<MockObject> _elements;

        public NameMatchingHelper_GetSpecificElementWithName_Should()
        {
            _nameMatchingHelper = new NameMatchingHelper<MockObject>(new NameMatchingSettings
            {
                AcceptedNumberOfErrors = 2,
                AcceptMultipleResults = false
            });

            _elements = new List<MockObject>
            {
                new MockObject{ComparableString = "Karoline Magdalena"},
                new MockObject{ComparableString = "Espen Karen Majken"},
                new MockObject{ComparableString = "Henning Bernhard Maren"},
                new MockObject{ComparableString = "Alf Dagmar Johannes Christer"},
                new MockObject{ComparableString = "Julius Maren Karsten"},
                new MockObject{ComparableString = "Inger Svante Nikolaj"},
                new MockObject{ComparableString = "Poul Vita"},
                new MockObject{ComparableString = "Filip Kris Herman Ansgar"},
                new MockObject{ComparableString = "Aina Maximilian Theresa"},
                new MockObject{ComparableString = "Aina Theresa"},
                new MockObject{ComparableString = "Aina Broesa"},
                new MockObject{ComparableString = "Eskil Cecilia Emma Aksel"},
                new MockObject{ComparableString = "Inge Jytte Olav Dorit"},
                new MockObject{ComparableString = "Valentin Olga Alfred Agnes"},
            };
        }

        [Fact]
        public void GetSpecificElementWithName_FullNameIsExactMatch_ReturnTrue()
        {
            var name = "Aina Maximilian Theresa";
            var result = _nameMatchingHelper.GetSpecificWithName(name, _elements);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetSpecificElementWithName_FullNameWithOneErrorIsMatch_ReturnTrue()
        {
            var name = "Aina Maximlian Theresa";
            var result = _nameMatchingHelper.GetSpecificWithName(name, _elements);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetSpecificElementWithName_FullNameWithTwoErrorsIsMatch_ReturnTrue()
        {
            var name = "Aina Maximiian Theesa";
            var result = _nameMatchingHelper.GetSpecificWithName(name, _elements);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetSpecificElementWithName_FullNameWithThreeErrorsIsMatch_ReturnFalse()
        {
            var name = "Aia Maxmilian Thersa";
            var result = _nameMatchingHelper.GetSpecificWithName(name, _elements);
            Assert.Null(result);
        }

        [Fact]
        public void GetSpecificElementWithName_ShortenedNameWithExactMatch_ReturnTrue()
        {
            var name = "Aina  Theresa";
            var result = _nameMatchingHelper.GetSpecificWithName(name, _elements);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetSpecificElementWithName_ShortenedNameWithOneErrorIsMatch_ReturnTrue()
        {
            var name = "Aina Theesa";
            var result = _nameMatchingHelper.GetSpecificWithName(name, _elements);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetSpecificElementWithName_ShortenedNameWithTwoErrorsIsMatch_ReturnTrue()
        {
            var name = "Aina eresa";
            var result = _nameMatchingHelper.GetSpecificWithName(name, _elements);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetSpecificElementWithName_ShortenedNameWithThreeErrorsIsMatch_ReturnFalse()
        {
            var name = "Laura randrup";
            var result = _nameMatchingHelper.GetSpecificWithName(name, _elements);
            Assert.Null(result);
        }

        [Fact]
        public void GetSpecificElementWithName_ShortenedNameWithFourErrorsIsMatch_ReturnFalse()
        {
            var name = "Lara Brandrup";
            var result = _nameMatchingHelper.GetSpecificWithName(name, _elements);
            Assert.Null(result);
        }

        [Fact]
        public void GetSpecificElementWithName_ShortenedNameWithFourInLastnameErrorsIsMatch_ReturnFalse()
        {
            var name = "Lara dstrup";
            var result = _nameMatchingHelper.GetSpecificWithName(name, _elements);
            Assert.Null(result);
        }

        [Fact]
        public void GetSpecificElementWithName_ShortenedNameToLastTwoNamesWithExactMatch_ReturnTrue()
        {
            var name = "Maximilian Theresa";
            var result = _nameMatchingHelper.GetSpecificWithName(name, _elements);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetSpecificElementWithName_ShortenedNameToLastTwoNamesWithOneErrorIsMatch_ReturnTrue()
        {
            var name = "Maximilan Theresa";
            var result = _nameMatchingHelper.GetSpecificWithName(name, _elements);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetSpecificElementWithName_ShortenedNameToLastTwoNamesWithFourErrorsIsMatch_ReturnFalse()
        {
            var name = "Maximlian Theea";
            var result = _nameMatchingHelper.GetSpecificWithName(name, _elements);
            Assert.Null(result);
        }
    }
}
