using EzGateway.Implementations;
using EzGateway.Tests.Config;
using EzGateway.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EzGateway.Tests
{
    [TestClass]
    public class TwilioSmsGatewatTests
    {
        [TestCleanup]
        public void Cleanup()
        {
            Twilio.TwilioClient.Invalidate();
        }

        #region Create

        [TestMethod]
        public void Create_AccountSidIsNull_ThrowsArgumentNullException()
        {
            var argumentExceptionThrown = false;
            try
            {
                var gateway = TwilioSmsGateway.Create(new TwilioSettings(null, "apiToken", "from"));
            }
            catch (ArgumentNullException)
            {
                argumentExceptionThrown = true;
            }

            Assert.IsTrue(argumentExceptionThrown);
        }

        [TestMethod]
        public void Create_ApiTokenIsNull_ThrowsArgumentNullException()
        {
            var argumentExceptionThrown = false;
            try
            {
                var gateway = TwilioSmsGateway.Create(new TwilioSettings("accountSid", null, "from"));
            }
            catch (ArgumentNullException)
            {
                argumentExceptionThrown = true;
            }

            Assert.IsTrue(argumentExceptionThrown);
        }

        [TestMethod]
        public void Create_FromIsNull_ThrowsArgumentNullException()
        {
            var argumentExceptionThrown = false;
            try
            {
                var gateway = TwilioSmsGateway.Create(new TwilioSettings("accountSid", "apiToken", null));
            }
            catch (ArgumentNullException)
            {
                argumentExceptionThrown = true;
            }

            Assert.IsTrue(argumentExceptionThrown);
        }

        #endregion

        #region SendMessage

        [TestMethod]
        public void SendMessage_AccountSidInvalid_ThrowsException()
        {
            var exceptionThrown = false;
            try
            {
                var gateway = TwilioSmsGateway.Create(new TwilioSettings("accountSid", TwilioTestConstants.ApiToken, TwilioTestConstants.SenderPhoneNumber));
                gateway.SendMessage(TwilioTestConstants.RecipientPhoneNumber, $"Testing {nameof(SendMessage_AccountSidInvalid_ThrowsException)}");
            }
            catch (Exception)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void SendMessage_ApiTokenInvalid_ThrowsException()
        {
            var exceptionThrown = false;
            try
            {
                var gateway = TwilioSmsGateway.Create(new TwilioSettings(TwilioTestConstants.AccountSid, "apiToken", TwilioTestConstants.SenderPhoneNumber));
                gateway.SendMessage(TwilioTestConstants.RecipientPhoneNumber, $"Testing {nameof(SendMessage_ApiTokenInvalid_ThrowsException)}");
            }
            catch (Exception)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void SendMessage_SenderIsInvalid_ThrowsException()
        {
            var exceptionThrown = false;
            try
            {
                var gateway = TwilioSmsGateway.Create(new TwilioSettings(TwilioTestConstants.AccountSid, TwilioTestConstants.ApiToken, "from"));
                gateway.SendMessage(TwilioTestConstants.RecipientPhoneNumber, $"Testing {nameof(SendMessage_SenderIsInvalid_ThrowsException)}");
            }
            catch (Exception)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void SendMessage_RecipientIsNull_ThrowsArgumentNullException()
        {
            var argumentNullExceptionThrown = false;
            try
            {
                var gateway = TwilioSmsGateway.Create(new TwilioSettings(TwilioTestConstants.AccountSid, TwilioTestConstants.ApiToken, TwilioTestConstants.SenderPhoneNumber));
                gateway.SendMessage(null, "messageText");
            }
            catch (ArgumentNullException)
            {
                argumentNullExceptionThrown = true;
            }

            Assert.IsTrue(argumentNullExceptionThrown);
        }

        [TestMethod]
        public void SendMessage_RecipientIsInvalid_ThrowsException()
        {
            var exceptionThrown = false;
            try
            {
                var gateway = TwilioSmsGateway.Create(new TwilioSettings(TwilioTestConstants.AccountSid, TwilioTestConstants.ApiToken, TwilioTestConstants.SenderPhoneNumber));
                gateway.SendMessage("THISISNOTAREALPHONENUMBER", "messageText");
            }
            catch (Exception)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void SendMessage_MessageTextIsNull_ThrowsArgumentNullException()
        {
            var argumentNullExceptionThrown = false;
            try
            {
                var gateway = TwilioSmsGateway.Create(new TwilioSettings(TwilioTestConstants.AccountSid, TwilioTestConstants.ApiToken, TwilioTestConstants.SenderPhoneNumber));
                gateway.SendMessage(TwilioTestConstants.RecipientPhoneNumber, null);
            }
            catch (ArgumentNullException)
            {
                argumentNullExceptionThrown = true;
            }

            Assert.IsTrue(argumentNullExceptionThrown);
        }

        [TestMethod]
        public void SendMessage_TooLongMessage_ThrowsArgumentOutOfRangeException()
        {
            var argumentOutOfRangeExceptionThrown = false;
            try
            {
                var gateway = TwilioSmsGateway.Create(new TwilioSettings(TwilioTestConstants.AccountSid, TwilioTestConstants.ApiToken, TwilioTestConstants.SenderPhoneNumber));
                gateway.SendMessage(TwilioTestConstants.RecipientPhoneNumber, new string('x', 1601));
            }
            catch (ArgumentOutOfRangeException)
            {
                argumentOutOfRangeExceptionThrown = true;
            }

            Assert.IsTrue(argumentOutOfRangeExceptionThrown);
        }

        [TestMethod]
        public void SendMessage_InputsAreValid_ThrownsNoExceptions()
        {
            var exceptionThrown = false;
            try
            {
                var gateway = TwilioSmsGateway.Create(new TwilioSettings(TwilioTestConstants.AccountSid, TwilioTestConstants.ApiToken, TwilioTestConstants.SenderPhoneNumber));
                gateway.SendMessage(TwilioTestConstants.RecipientPhoneNumber, $"Testing {nameof(SendMessage_InputsAreValid_ThrownsNoExceptions)}");
            }
            catch (ArgumentOutOfRangeException)
            {
                exceptionThrown = true;
            }

            Assert.IsFalse(exceptionThrown);
        }

        #endregion
    }
}
