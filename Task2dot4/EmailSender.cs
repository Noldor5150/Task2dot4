using System;
using MailKit.Net.Smtp;
using MimeKit;

namespace Task2dot4
{
    public static class EmailSender
    {
        private const string EMAIL_FROM = "someemail";

        private const string PASSWORD = "somepassword";

		private const int SSL_PORT = 465;

		private const string EMAIL_SMTP = "smtp.gmail.com";

		private const string REFERING_TO = "Dear Admin";

		private const string MESSAGE_CONTENT = @"Yor site is not working!!!";

		private const string MESSAGE_SUBJECT = "SITE ISSUES";
        public static  void SendEmail( string emailTo )
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("Tester", EMAIL_FROM));
			message.To.Add(new MailboxAddress(REFERING_TO, emailTo));
			message.Subject = MESSAGE_SUBJECT;

			message.Body = new TextPart("plain")
			{
				Text = MESSAGE_CONTENT
			};
			object locker = new object();
			lock (locker)
			{
				using ( var client = new SmtpClient() )
				{
					try
					{
						client.Connect(EMAIL_SMTP, SSL_PORT, true);
						client.Authenticate(EMAIL_FROM, PASSWORD);
						client.Send(message);
					}
					catch ( Exception ex )
					{
						Console.WriteLine( ex.Message );
					}
					finally
					{
						client.Disconnect(true);
						client.Dispose();
					}
				}
			}
		}
	}
}
