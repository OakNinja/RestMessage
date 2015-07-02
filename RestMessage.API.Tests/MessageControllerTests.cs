using System.Linq;
using System.Threading;
using NUnit.Framework;
using RestMessage.App;
using RestMessage.App.Repositories;
using RestMessage.API.Controllers;
using RestMessage.API.Facades;
using RestMessage.API.Interfaces;

namespace RestMessage.API.Tests
{
    [TestFixture]
    public class MessageControllerTests
    {
        [SetUp]
        public void SetUp()
        {
            _repository = new InMemoryMessageRepository();
            _facade = new MessageFacade(_repository);
            _controller = new MessageController(_facade);
            _defaultUserName = "John";
            _defaultMessage = "Message";
        }

        private IMessageRepository<IMessage> _repository;
        private IMessageFacade _facade;
        private MessageController _controller;
        private string _defaultUserName;
        private string _defaultMessage;

        [TestCase]
        public void GetUnread_ExpectOneNewMessage()
        {
            _controller.Post(_defaultMessage, _defaultUserName);

            var messages = _controller.GetUnread(_defaultUserName);

            Assert.AreEqual(1, messages.Count);
        }

        [TestCase]
        public void GetUnread_TwoRequests_ExpectZeroNewMessagesOnSecondGet()
        {
            _controller.Post(_defaultMessage, _defaultUserName);
            //Discard first get
            _controller.GetUnread(_defaultUserName);
            var messages = _controller.GetUnread(_defaultUserName);

            Assert.AreEqual(0, messages.Count);
        }

        [TestCase]
        public void Post_SendMessageToRecipient_ExpectMessageAdded()
        {
            _controller.Post(_defaultMessage, _defaultUserName);

            var messages = _controller.GetUnread(_defaultUserName);

            Assert.AreEqual(1, messages.Count);
        }

        [TestCase]
        public void GetAll_GetPreviouslyReadMessages_ExpectOneMessage()
        {
            _controller.Post(_defaultMessage, _defaultUserName);

            _controller.GetUnread(_defaultUserName);

            var messages = _controller.GetAll(_defaultUserName);

            Assert.AreEqual(1, messages.Count);
        }

        [TestCase]
        public void Delete_DeleteOneMessage_ExpectDeleted()
        {
            _controller.Post(_defaultMessage, _defaultUserName);

            var messages = _controller.GetAll(_defaultUserName);
            var messageIds = messages.Select(m => m.Id).ToArray();

            _controller.Delete(messages[0].UserName, messageIds);

            Assert.AreEqual(0, _controller.GetAll(_defaultUserName).Count);
        }

        [TestCase]
        public void Delete_DeleteThreeMessages_ExpectDeleted()
        {
            _controller.Post(_defaultMessage, _defaultUserName);
            _controller.Post(_defaultMessage, _defaultUserName);
            _controller.Post(_defaultMessage, _defaultUserName);

            var messages = _controller.GetAll(_defaultUserName);
            var messageIds = messages.Select(m => m.Id).ToArray();

            _controller.Delete(messages[0].UserName, messageIds);

            Assert.AreEqual(0, _controller.GetAll(_defaultUserName).Count);
        }

        [TestCase]
        public void GetRange_GetMessagesWithIdsBetweenXAndY_ExpectSortedByDateDescending()
        {
            _controller.Post(_defaultMessage, _defaultUserName);
            //Sleep to ensure dates will differ
            Thread.Sleep(100);
            //Add one message from another user to validate only messages from the requested user is returned
            _controller.Post(_defaultMessage, "Jane");
            _controller.Post(_defaultMessage, _defaultUserName);

            var messages = _controller.GetRange(_defaultUserName, 0, 2);

            Assert.AreEqual(2, messages.Count);
            Assert.Greater(messages[0].Created, messages[1].Created);
        }
    }
}