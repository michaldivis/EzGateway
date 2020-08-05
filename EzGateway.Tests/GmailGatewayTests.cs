using EzGateway.Implementations;
using EzGateway.Tests.Config;
using EzGateway.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Mail;

namespace EzGateway.Tests
{
    [TestClass]
    public class GmailGatewayTests
    {
        #region Create

        [TestMethod]
        public void Create_SenderIsNull_ThrowsArgumentNullException()
        {
            var exceptionThrown = false;
            try
            {
                var gateway = GmailGateway.Create(new GmailSettings(null, "username", "password"));
            }
            catch (ArgumentNullException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void Create_SenderAddressIsNull_ThrowsArgumentNullException()
        {
            var exceptionThrown = false;
            try
            {
                var gateway = GmailGateway.Create(new GmailSettings(new MailAddress(null), "username", "password"));
            }
            catch (ArgumentNullException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void Create_UsernameIsNull_ThrowsArgumentNullException()
        {
            var exceptionThrown = false;
            try
            {
                var gateway = GmailGateway.Create(new GmailSettings(new MailAddress("example@example.com"), null, "password"));
            }
            catch (ArgumentNullException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void Create_PasswordIsNull_ThrowsArgumentNullException()
        {
            var exceptionThrown = false;
            try
            {
                var gateway = GmailGateway.Create(new GmailSettings(new MailAddress("example@example.com"), "username", null));
            }
            catch (ArgumentNullException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        #endregion

        #region SendMessage

        [TestMethod]
        public void SendMessage_RecipientIsNull_ThrowsArgumentNullException()
        {
            var exceptionThrown = false;
            try
            {
                var gateway = GmailGateway.Create(new GmailSettings(new MailAddress("example@example.com"), "username", "password"));
                gateway.SendMessage(null, "text", "title");
            }
            catch (ArgumentNullException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void SendMessage_RecipientIsInvalid_ThrowsException()
        {
            var exceptionThrown = false;
            try
            {
                var gateway = GmailGateway.Create(new GmailSettings(new MailAddress("example@example.com"), "username", "password"));
                gateway.SendMessage("NOT_AN_EMAIL_ADDRESS", "text", "title");
            }
            catch (Exception ex)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void SendMessage_TitleIsNull_ThrowsArgumentNullException()
        {
            var exceptionThrown = false;
            try
            {
                var gateway = GmailGateway.Create(new GmailSettings(new MailAddress("example@example.com"), "username", "password"));
                gateway.SendMessage("example@example.com", "text", null);
            }
            catch (ArgumentNullException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void SendMessage_TextIsNull_ThrowsException()
        {
            var exceptionThrown = false;
            try
            {
                var gateway = GmailGateway.Create(new GmailSettings(new MailAddress("example@example.com"), "username", "password"));
                gateway.SendMessage("example@example.com", null, "title");
            }
            catch (ArgumentNullException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void SendMessage_InputsAreValid_ThrowsNoExceptions()
        {
            var exceptionThrown = false;
            try
            {
                var gateway = GmailGateway.Create(new GmailSettings(new MailAddress(GmailTestConstants.SenderEmailAddress, GmailTestConstants.SenderAlias), GmailTestConstants.SenderEmailAddress, GmailTestConstants.SenderPassword));
                gateway.SendMessage(GmailTestConstants.RecipientEmailAddress, $"Testing {nameof(SendMessage_InputsAreValid_ThrowsNoExceptions)}", "Test message");
            }
            catch (ArgumentNullException)
            {
                exceptionThrown = true;
            }

            Assert.IsFalse(exceptionThrown);
        }

        #endregion
    }
}
