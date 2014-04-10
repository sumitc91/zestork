using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SunPower.Common.Infrastructure.Logger;
using System.Reflection;
using System.Text;

namespace zestork.CommonMethods
{
    public class sendAccountCreationValidationEmail
    {
        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));

        public String sendAccountCreationValidationEmailMessage(String toMail,String guid, HttpRequestBase Request)
        {
            sendEmail sendEmail = new sendEmail();
            String retVal = sendEmail.sendEmailMessage(toMail,
                "donotreply",
                "Validate your Account",
                CreateAccountEmailBodyContent(Request.Url.Authority, toMail, guid),
                null,
                null,
                "Zestork - Place to boost your Carrer"
                );
            return retVal;
        }
       

        private string CreateAccountEmailBodyContent(String Request_Url_Authority,String toMail,String guid)
        {
            StringBuilder HtmlBody = new StringBuilder();

            HtmlBody.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#368ee0\">");
		        HtmlBody.Append("<tr>");
			        HtmlBody.Append("<td align=\"center\">");
				        HtmlBody.Append("<center>");
					        HtmlBody.Append("<table border=\"0\" width=\"600\" cellpadding=\"0\" cellspacing=\"0\">");
						        HtmlBody.Append("<tr>");
							        HtmlBody.Append("<td style=\"color:#ffffff !important; font-size:24px; font-family: Arial, Verdana, sans-serif; padding-left:10px;\" height=\"40\"></td>");
							        HtmlBody.Append("<td align=\"right\" width=\"50\" height=\"45\"></td>");
						        HtmlBody.Append("</tr>");
					        HtmlBody.Append("</table>");
				        HtmlBody.Append("</center>");
			        HtmlBody.Append("</td>");
		        HtmlBody.Append("</tr>");
	        HtmlBody.Append("</table>");

	        HtmlBody.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#ffffff\">");
		        HtmlBody.Append("<tr>");
			        HtmlBody.Append("<td align=\"center\">");
				        HtmlBody.Append("<center>");
					        HtmlBody.Append("<table border=\"0\" width=\"600\" cellpadding=\"0\" cellspacing=\"0\">");
						        HtmlBody.Append("<tr>");
							        HtmlBody.Append("<td style=\"color:#333333 !important; font-size:20px; font-family: Arial, Verdana, sans-serif; padding-left:10px;\" height=\"40\">");
								        HtmlBody.Append("<h3 style=\"font-weight:normal; margin: 20px 0;\">Account verification</h3>");
								        HtmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
									        HtmlBody.Append("Message for User. <br /><br />");
									        HtmlBody.Append("Email: "+toMail+"");
								        HtmlBody.Append("</p>");
								        HtmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
                                        HtmlBody.Append("<a href=\"http://" + Request_Url_Authority + "/#/" + "validate/" + toMail + "/" + guid + "\"> Click here to validate your account </a>");
								        HtmlBody.Append("</p>");
							        HtmlBody.Append("</td>");
						        HtmlBody.Append("</tr>");
					        HtmlBody.Append("</table>");
				        HtmlBody.Append("</center>");
			        HtmlBody.Append("</td>");
		        HtmlBody.Append("</tr>");
	        HtmlBody.Append("</table>");
	        HtmlBody.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#ffffff\">");
		        HtmlBody.Append("<tr>");
			        HtmlBody.Append("<td align=\"center\">");
				        HtmlBody.Append("<center>");
					        HtmlBody.Append("<table border=\"0\" width=\"600\" cellpadding=\"0\" cellspacing=\"0\">");
						        HtmlBody.Append("<tr>");
							        HtmlBody.Append("<td style=\"color:#333333 !important; font-size:20px; font-family: Arial, Verdana, sans-serif; padding-left:10px;\" height=\"40\">");
								        HtmlBody.Append("<h3 style=\"font-weight:normal; margin: 20px 0;\">Security</h3>");
								        HtmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
									        HtmlBody.Append("Some details for user<br />");
									        HtmlBody.Append("<br />");
									        HtmlBody.Append("<br />More details for user.");
								        HtmlBody.Append("</p>");
								        HtmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
									        HtmlBody.Append("<a href=\"#\">Check security settings</a>");
								        HtmlBody.Append("</p>");
							       HtmlBody.Append(" </td>");
						        HtmlBody.Append("</tr>");
					        HtmlBody.Append("</table>");
				        HtmlBody.Append("</center>");
			        HtmlBody.Append("</td>");
		        HtmlBody.Append("</tr>");
	        HtmlBody.Append("</table>");

	        HtmlBody.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" bgcolor=\"#368ee0\">");
		        HtmlBody.Append("<tr>");
			        HtmlBody.Append("<td align=\"center\">");
				        HtmlBody.Append("<center>");
					        HtmlBody.Append("<table border=\"0\" width=\"600\" cellpadding=\"0\" cellspacing=\"0\">");
						        HtmlBody.Append("<tr>");
							        HtmlBody.Append("<td style=\"color:#ffffff !important; font-size:20px; font-family: Arial, Verdana, sans-serif; padding-left:10px;\" height=\"40\">");
								        HtmlBody.Append("<center>");
									        HtmlBody.Append("<p style=\"font-size:12px; line-height:18px;\">");
									        HtmlBody.Append("If you don't want to get system emails from FLAT please change your email settings.");
									        HtmlBody.Append("<br />");
									        HtmlBody.Append("<a href=\"#\" style=\"color:#ffffff !important;\">Click here to change email settings</a>");
								        HtmlBody.Append("</p>");
								        HtmlBody.Append("</center>");
							        HtmlBody.Append("</td>");
						        HtmlBody.Append("</tr>");
					        HtmlBody.Append("</table>");
				        HtmlBody.Append("</center>");
			        HtmlBody.Append("</td>");
		        HtmlBody.Append("</tr>");
	        HtmlBody.Append("</table>");


            return HtmlBody.ToString();
        }
    }
}