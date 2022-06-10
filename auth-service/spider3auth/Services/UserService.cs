using CommonUtilities.Repository;
using Microsoft.Extensions.Options;
using spider3auth.Authorization;
using spider3auth.Entities;
using spider3auth.Helpers;
using spider3auth.Models.Users;
using spider3auth.Repository;
using System;

namespace spider3auth.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
        void RevokeToken(string token, string ipAddress);
        public IEnumerable<User> getAllUsers();
        User GetById(string id);
    }
    public class UserService : IUserService
    {

        private readonly IMongoRepository<User> _userRepository;
        private readonly IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public UserService(IMongoRepository<User> userRepository, IJwtUtils jwtUtils, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            //var newUser = new User
            //{
            //    CustomerAccountID="belan",
            //    Username=ipAddress,
            //    Description="",
            //    IsActive=true,
            //    GroupItemList= new List<GroupItem>(),
            //    Password="",
            //    RunTimeCustomerAccountItem=null,
            //    UserType=Common.UserTypeEnum.ADMINISTRATOR,
            //    RefreshTokens = new List<RefreshToken>() {new RefreshToken
            //    {
            //        Created = DateTime.Now,
            //        CreatedByIp = ipAddress,
            //        Expires = DateTime.Now,
            //        Id = 123,
            //        ReasonRevoked = null,
            //        ReplacedByToken = "",
            //        Revoked = DateTime.Now,
            //        RevokedByIp = ipAddress,
            //        Token = "df;lsafjld;askjfads"
            //    }, }
            //};

           

            var user = _userRepository.FindOneAsync(x => x.Username.ToUpper() == model.Username.ToUpper()).Result;
            if (user == null)
                throw new AppException("Username is incorrect !!");
            if(user.Password != model.Password)
                throw new AppException("Password is incorrect !!");
            var jwtToken = _jwtUtils.GenerateJwtToken(user);
            var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            if(user.RefreshTokens != null)
            {
                user.RefreshTokens.Add(refreshToken);
                removeOldRefreshTokens(user);
            }            
            _userRepository.ReplaceOneAsync(user);
            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }


        public IEnumerable<User> getAllUsers()
        {
            return _userRepository.AsQueryable();
        }

        public User GetById(string id)
        {
            var user = _userRepository.FindById(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;

        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public void RevokeToken(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }

        private void removeOldRefreshTokens(User user)
        {
            // remove old inactive refresh tokens from user based on TTL in app settings
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }
    }
}
