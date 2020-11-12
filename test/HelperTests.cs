using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Interfaces;
using Utils;
using Models;

namespace Test
{
    public class HelperTests
    {
        private IHelper _sut;
        private Mock<ILogger<Helper>> _logger;
        private Dictionary<Item, string> _dictionary;

        public HelperTests()
        {
            _logger = new Mock<ILogger<Helper>>();
            _sut = new Helper(_logger.Object);

            var initialCollection = new List<Item>()
            {
                new Item(){ FirstName = "James", SecondName = "Bond", Contact = "000 000 000", PostCode = "AB00 1AB" },
                new Item(){ FirstName = "Tom", SecondName = "Jones", Contact = "000 000 000", PostCode = "AB00 2AB"}
            };
            _dictionary = initialCollection.ToDictionary(x => x, x => x.FirstName, new Item.ItemEqualityComparer());
        }

        [Fact]
        public void TestConvertRequestBody()
        {
            var request = "{ items : [{ firstName:'James', secondName: 'Bond', contact: '000 000 000', postcode: 'AB00 1AB' }], item:{ firstName: 'Tom', secondName: 'Jones', contact: '000 000 000', postcode: 'AB00 2AB'} }";
            var defaultType = new RequestBody();
            var result = _sut.Convert<RequestBody>(request, defaultType);
            Assert.Equal("James", result.Items[0].FirstName);
            Assert.Equal("Bond", result.Items[0].SecondName);
            Assert.Equal("000 000 000", result.Items[0].Contact);
            Assert.Equal("AB00 1AB", result.Items[0].PostCode);

            Assert.Equal("Tom", result.Item.FirstName);
            Assert.Equal("Jones", result.Item.SecondName);
            Assert.Equal("000 000 000", result.Item.Contact);
            Assert.Equal("AB00 2AB", result.Item.PostCode);
        }

        [Fact]
        public void TestConvertRequestBodyInvalid()
        {
            var request = "{ i : [{ f:'James', s: 'Bond', c: '000 000 000', postcode: 'AB00 1AB' }]";
            var defaultType = new RequestBody();
            var result = _sut.Convert<RequestBody>(request, defaultType);
            VerifyLogger(LogLevel.Error, "There was a problem converting.");
            Assert.Empty(result.Items);
        }

        [Fact]
        public void TestConvertRequestBodyNullCheck()
        {
            var defaultType = new RequestBody();
            var result = _sut.Convert<RequestBody>(null, defaultType);
            Assert.Empty(result.Items);
        }

        [Fact]
        public void TestCanItemBeAddedNotMatching()
        {
            var item = new Item(){ FirstName = "Tom", SecondName = "Smith", Contact = "000 000 000", PostCode = "AB00 3AB" };
            var result = _sut.CanItemBeAdded(_dictionary, item);

            Assert.True(result);
        }

        [Fact]
        public void TestCanItemBeAddedPartialMatch()
        {
            var item = new Item(){ FirstName = "Tommy", SecondName = "Jones", Contact = "000 000 000", PostCode = "AB00 2AB" };
            var result = _sut.CanItemBeAdded(_dictionary, item);
            VerifyLogger(LogLevel.Information, "You cannot add duplicate items.");
            Assert.False(result);
        }

        [Fact]
        public void TestCanItemBeAddedMatchingSecondNameDifferentElse()
        {
            var item = new Item(){ FirstName = "Tommy", SecondName = "Jones", Contact = "111 111 111", PostCode = "AB00 4AB" };
            var result = _sut.CanItemBeAdded(_dictionary, item);

            Assert.True(result);
        }

        [Fact]
        public void TestCanItemBeAddedToEmptyDictionary()
        {
            var initialCollection = new List<Item>();
            var item = new Item(){ FirstName = "Karen", SecondName = "Jones", Contact = "000 000 000", PostCode = "AB00 1AB" };
            var dictionary = initialCollection.ToDictionary(x => x, x => x.FirstName, new Item.ItemEqualityComparer());
            var result = _sut.CanItemBeAdded(dictionary, item);
            Assert.True(result);
        }


        private void VerifyLogger(LogLevel expectedLogLevel, string expectedMessage = "")
        {
            _logger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == expectedLogLevel),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => String.IsNullOrEmpty(expectedMessage) ? true : v.ToString() == expectedMessage),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
        }
    }
}