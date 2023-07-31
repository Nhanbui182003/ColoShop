using Microsoft.Owin.BuilderProperties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace WeBanHang.Common
{
    public class Common
    {
        private static string password = ConfigurationManager.AppSettings["PasswordEmail"];
        private static string email = ConfigurationManager.AppSettings["Email"];
        public static bool SendMail(string name, string subject, string content,string toMail)
        {

            try
            {
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(email, password)
                };
                using (var message = new MailMessage(email, toMail)
                {
                    Subject = subject,
                    Body = content,
                    IsBodyHtml= true
                })
                {
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception ex) {
                // Log the exception for debugging purposes.
                Console.WriteLine("Error while sending email: " + ex.Message);
                return false;
            }
        }
        public static string FormatNumber(object value, int SoSauDauPhay = 2)
        {
            bool isNumber = IsNumberic(value);
            decimal GT = 0;
            if (isNumber)
            {
                GT = Convert.ToDecimal(value);
            }
            string str = "";
            string thapphan = "";
            for (int i = 0; i < SoSauDauPhay; i++)
            {
                thapphan += "#";
            }
            if (thapphan.Length>0) thapphan = "." + thapphan;
            string snumformat = string.Format("0:#,##0{0}", thapphan);
            str= string.Format("{" + snumformat+ "}", GT);
            return str;
        }
        public static bool IsNumberic(object value)
        {
            return value is sbyte || value is byte || value is ushort || value is int || value is uint || value is long || value is ulong || value is float || value is double || value is decimal;
        }
    }
}