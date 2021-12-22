using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace BusinessLayer
{
    public class BLSMS
    {
        public static string SendSMS2(string User, string password, string senderid, string Mobile_Number, string Message)
        {
            // use the API URL here  
            string strUrl = "http://wiztechsms.com/http-api.php?username=" + User + "&password=" + password + "&senderid=" + senderid + "&route=2&number=" + Mobile_Number + "&message=" + Message;        // Create a request object  
            WebRequest request = HttpWebRequest.Create(strUrl);
            // Get the response back  
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();
            return dataString;
        }
        public static string SendSMSNew(string Mobile_Number, string Message)
        {
            // use the API URL here  
            //string strUrl = "http://smsw.co.in/API/WebSMS/Http/v1.0a/index.php?username=dharaworld&password=123456&sender=DHARAW&to=8052949381&message=" + 
            //    User + "&password=" + password + "&senderid=" + senderid + "&route=2&number=" + Mobile_Number + "&message=" + Message;        // Create a request object  

            string strUrl = "http://smsw.co.in/API/WebSMS/Http/v1.0a/index.php?username=Afluex&password=f6b7c7-970d7&sender=AFLUEX&to=" + Mobile_Number + "&message=" + Message + "& reqid = 1 & format ={ json}&route_id = 39 & callback =#&unique=0&sendondate='" + DateTime.Now.ToString() + " '";

            WebRequest request = HttpWebRequest.Create(strUrl);
            // Get the response back  
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();
            return dataString;
        }
        static public string ForgetPassword(string MemberName, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["ForgetPassword"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[Password]", Password);
            return Message;
        }
        static public string Registration(string MemberName, string LoginId, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["REGISTRATION"].ToString();
            Message = Message.Replace("[Name]", MemberName);
            Message = Message.Replace("[LoginId]", LoginId);
            Message = Message.Replace("[Password]", Password);

            return Message;
        }
        static public string OTP(string MemberName, string OTP)
        {
            string Message = ConfigurationSettings.AppSettings["OTP"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[OTP]", OTP);

            return Message;
        }
        public static string sendSMS(string Message, string MobileNo)
        {
            String message = HttpUtility.UrlEncode(Message);
            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , "ZjVhMWJlNGUxMjkxMTY0MGQyYzQ0MjBkMGVhMWZiMDc="},
                {"numbers" , MobileNo   },
                {"message" , message},
                {"sender" , "DOSTIN"}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                return result;
            }
        }
        public static string sendSMSUpdated(string msg, string mobileNo)
        {
            String result;
            string apiKey = "ZjVhMWJlNGUxMjkxMTY0MGQyYzQ0MjBkMGVhMWZiMDc=";
            string numbers = mobileNo; // in a comma seperated list
            string message = msg;
            string sender = "DOSTIN";

            String url = "https://api.textlocal.in/send/?apikey=" + apiKey + "&numbers=" + numbers + "&message=" + message + "&sender=" + sender;
            //refer to parameters to complete correct url string

            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

            objRequest.Method = "POST";
            objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(url);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                myWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                // Close and clean up the StreamReader
                sr.Close();
            }
            return result;
        }
    }
}
