using AutoMapper;
using Daycareinator.Data.Entities;
using Daycareinator.Domain;
using Daycareinator.Domain.Services;
using Daycareinator.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using WebMatrix.WebData;

namespace Daycareinator.Controllers.api
{
    [Authorize(Roles="Admin")]
    public class UserController : ApiController
    {
        private IUsersService _usersService;
        private IInvitationService _invitationService;
        private ICurrentUser _currentUser;

        public UserController(IUsersService usersService, IInvitationService invitationService, ICurrentUser currentUser)
        {
            _usersService = usersService;
            _invitationService = invitationService;
            _currentUser = currentUser;
        }


        public IEnumerable<UserModel> Get()
        {
            var models = new List<UserModel>();
            var users = _usersService.GetUsers(User.Identity.Name);
            Mapper.CreateMap<User, UserModel>();

            foreach (var user in users)
            {
                models.Add(Mapper.Map<User, UserModel>(user));
            }

            return models;
        }

        public HttpResponseMessage Post(UserModel user)
        {
            var currentUser = _currentUser.GetCurrentUser;
            try
            {
                //simple membership doesn't really support adding users by invitation.
                //as a work around i'm creating the account and using the confirmation token for the invitation.
                //using guid as placeholder password until account is confirmed (password is irrelevant now because 
                //they can't log in until it they register using the invitation link).  
                string token = WebSecurity.CreateUserAndAccount(user.EmailAddress, new Guid().ToString(), new { ClientId = currentUser.ClientId }, requireConfirmationToken: true);
                _invitationService.SendInvitation(user.EmailAddress, token);
            }
            catch (MembershipCreateUserException ex)
            {
                var errorResult = ValidationResult.ErrorMessage(String.Format("{0} is already in use.", HttpUtility.HtmlEncode(user.EmailAddress)));
                return Request.CreateResponse<ValidationResult>(HttpStatusCode.OK, errorResult);
            }
            catch (Exception ex)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                var errorResult = ValidationResult.ErrorMessage("An error occurred while sending the invitation.");
                return Request.CreateResponse<ValidationResult>(HttpStatusCode.OK, errorResult);
            }

            var result = ValidationResult.SuccessMessage(String.Format("Invitation was sent to {0}", HttpUtility.HtmlEncode(user.EmailAddress)));
            return Request.CreateResponse<ValidationResult>(HttpStatusCode.OK, result);

           
        }

        public HttpResponseMessage Delete(int id)
        {
            var currentUser = _currentUser.GetCurrentUser;

            try
            {
                _usersService.Delete(id, currentUser.ClientId);
                var result = ValidationResult.SuccessMessage("User was deleted successfully.");
                return Request.CreateResponse<ValidationResult>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                var errorResult = ValidationResult.ErrorMessage("An error occured while deleting the user.");
                return Request.CreateResponse<ValidationResult>(HttpStatusCode.OK, errorResult);
            }
            

            
        }

    }

}
