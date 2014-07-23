using Daycareinator.Data.Entities;
using Daycareinator.Domain.Services;
using Daycareinator.Domain.Validation;
using Daycareinator.Models.Children;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Daycareinator.Controllers
{
    [Authorize]
    public class ChildApiController : ApiController
    {
        private IChildrenService _childrenService;
        private IChildModelBuilder _modelBuilder;
        private ICurrentUser _currentUser;

        public ChildApiController(IChildrenService childrenService, IChildModelBuilder modelBuilder, ICurrentUser currentUser)
        {
            _childrenService = childrenService;
            _modelBuilder = modelBuilder;
            _currentUser = currentUser;
        }

        [HttpGet]
        public ChildModel Get(int childId)
        {
            var validator = new UserAccessToChildValidator(_childrenService);
            if (!validator.Validate(_currentUser.GetCurrentUser, childId))
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            var child = _childrenService.GetById(childId);

           
            var model = _modelBuilder.Build(child);
            return model;



        }
    }
}
