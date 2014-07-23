using Daycareinator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace Daycareinator.Membership
{
    public interface ISimpleMembershipInitializer
    {
        void Initialize();
    }
    public class SimpleMembershipInitializer : ISimpleMembershipInitializer
    {
        public void Initialize()
        {

            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DaycareinatorContext", "Users", "UserId", "EmailAddress", true);
        }
    }
}