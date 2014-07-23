using Daycareinator.Data.Entities;
using Daycareinator.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Domain.Validation
{
    public class UserAccessToChildValidator
    {
        private IChildrenService _childrenService;
        private IUsersService _usersService;


        public UserAccessToChildValidator(IChildrenService childrenService)
        {
            _childrenService = childrenService;
            
        }

        public bool Validate(User user, int childId)
        {
            var child = _childrenService.GetById(childId);

            if (user != null & child != null)
            {
                return user.ClientId == child.ClientId;
            }



            return false;

        }
    }
}
