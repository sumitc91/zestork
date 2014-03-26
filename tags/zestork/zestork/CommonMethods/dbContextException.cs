using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SunPower.Common.Infrastructure.Logger;
using System.Reflection;
using System.Data.Entity.Validation;

namespace zestork.CommonMethods
{    
    public class dbContextException
    {
        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        public void logDbContextException(DbEntityValidationException e)
        {
            foreach (var eve in e.EntityValidationErrors)
            {
                logger.Error("Entity of type \"" + eve.Entry.Entity.GetType().Name + "\" in state \"" + eve.Entry.State + "\" has the following validation errors:",e);
                foreach (var ve in eve.ValidationErrors)
                {
                    logger.Error("- Property: \"" + ve.PropertyName + "\", Error: \"" + ve.ErrorMessage + "\"",e);
                }
            }
        }
    }
}