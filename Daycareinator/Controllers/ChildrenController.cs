using Daycareinator.Data.Entities;
using Daycareinator.Domain;
using Daycareinator.Domain.Services;
using Daycareinator.Domain.Validation;
using Daycareinator.Models.Children;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Daycareinator.Controllers
{
    [Authorize]
    public class ChildrenController :BaseController
    {
        private IChildrenService _childrenService;
        private IRecordsService _recordsService;
private  IUsersService _usersService;
private ICurrentUser _currentUser;
        public ChildrenController(IChildrenService childrenService, IRecordsService recordsService, ICurrentUser currentUser)
        {
            _childrenService = childrenService;
            _recordsService = recordsService;
            _currentUser = currentUser;
        }

        public ActionResult Index()
        {
            var children = _childrenService.GetChildren(_currentUser.GetCurrentUser.ClientId);
            var records = _recordsService.GetRecordsByType(RecordType.Child);
            var builder = new ChildGridModelBuilder(records);

            var childModels = new List<ChildGridModel>();
            foreach (var child in children)
            {
                childModels.Add(builder.Build(child));
            }

            return View(childModels);

        }

        

    }
}
