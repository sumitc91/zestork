using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SunPower.Common.Infrastructure.Logger;
using System.Reflection;

namespace zestork.CommonMethods
{
    public class sendAccountCreationValidationEmail
    {
        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));

        public String sendAccountCreationValidationEmailMessage(String toMail,String guid)
        {
            sendEmail sendEmail = new sendEmail();
            String retVal = sendEmail.sendEmailMessage(toMail,
                "donotreply",
                "Validate your Account",
                "click on the link below to validate your account " + guid + "",
                null,
                null,
                "Zestork - Place to boost your Carrer"
                );
            return retVal;
        }
    }
}