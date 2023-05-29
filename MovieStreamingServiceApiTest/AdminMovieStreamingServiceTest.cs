using Entity.Dto;
using Microsoft.AspNetCore.Mvc;
using Exception;
using Microsoft.AspNetCore.Http;

namespace MovieStreamingServiceApiTest;
public class AdminMovieStreamingServiceTest : AdminBaseTesting
{
    //Admin login test
    [Theory]
    [InlineData("AdminUser","Password@123",200)]
    [InlineData("Wrong@User.com","Nopassword@123",401)]
    public void LoginTesting(string userName, string password, int expectedCode)
    {

        LoginDto loginDto = new LoginDto()
        {
            UserName = userName,
            Password = password,
        };
        if(expectedCode == 200)
        {
            OkObjectResult result = (OkObjectResult)authenticationController.UserLoginApi(loginDto);
            TokenResponseDto tokenResponseDto = (TokenResponseDto)result.Value!;
            Assert.NotNull(result);
            Assert.Equal(expectedCode, result.StatusCode);
            Assert.NotNull(tokenResponseDto?.AccessToken);
        }
        else
        {
            BaseCustomException ex = Assert.Throws<BaseCustomException>(() => authenticationController.UserLoginApi(loginDto));
            Assert.Equal(expectedCode, ex.StatusCode);
        }
        
    }

   //Delete user by admin
    [Theory]
    [InlineData(1,200)]
    [InlineData(2,404)]
    [InlineData(3,400)]
    public void DeleteUserTesting(int n, int expectedCode)
    {
        if(n == 1)
        {
            OkObjectResult result = (OkObjectResult)adminController.DeleteUserApi(userId2);
            Assert.Equal(expectedCode, result.StatusCode);
        }
        else if(n == 2)
        {
            BaseCustomException ex = Assert.Throws<BaseCustomException>(() => adminController.DeleteUserApi(Guid.NewGuid()));
            Assert.Equal(expectedCode, ex.StatusCode);
        }
        else
        {
            BaseCustomException ex = Assert.Throws<BaseCustomException>(() => adminController.DeleteUserApi(userId));
            Assert.Equal(expectedCode, ex.StatusCode);
        }
    }

    //Get user by admin
    [Fact]
    public void GetAllUserTesting()
    {
        OkObjectResult result = (OkObjectResult)adminController.GetUserApi();
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<ProfileDto>>(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
    }
    
    //Get all movies by admin
    [Fact]
    public void GetMovieTesting()
    {
        OkObjectResult result = (OkObjectResult)movieController.GetMoviesApi();
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<MovieDto>>(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
    }

    //Get all subscriptions by admin
    [Fact]
    public void GetSubscriptionTesting()
    {
        OkObjectResult result = (OkObjectResult)subscriptionController.GetSubscriptionApi();
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<SubscriptionDto>>(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
    }

    //Update user by admin
    [Theory]
    [InlineData(1,200)]
    [InlineData(2,404)]
    [InlineData(3,400)]
    [InlineData(4,400)]
    public void UpdateUserTesting(int n, int expectedCode)
    {
        if( n == 1 )
        {
            OkObjectResult result = (OkObjectResult)userController.UpdateUserApi(userId2,source(1));
            Assert.Equal(expectedCode, result.StatusCode);
        }
        else if( n == 2 )
        {
            BaseCustomException ex = Assert.Throws<BaseCustomException>(() => userController.UpdateUserApi(Guid.NewGuid(),source(2)));
            Assert.Equal(expectedCode, ex.StatusCode);
        }
        else if( n == 3 )
        {
            Nullable<Guid> guid = null;
            BaseCustomException ex = Assert.Throws<BaseCustomException>(() => userController.UpdateUserApi(guid,source(2)));
            Assert.Equal(expectedCode, ex.StatusCode);
        }
        else
        {
            BaseCustomException ex = Assert.Throws<BaseCustomException>(() => userController.UpdateUserApi(userId,source(2)));
            Assert.Equal(expectedCode, ex.StatusCode);
        }
    }


    public ProfileDto source(int n)
    {
        ProfileDto profileDto = new ProfileDto();
        profileDto.PaymentDto = new List<ProfileDtoPaymentDto>();
        if( n == 1 )
        {
            profileDto.UserName = "new user one";
            profileDto.Password = "Password@123";
            profileDto.ConfirmPassword = "Password@123";
            profileDto.EmailAddress = "EmailAddress@email.com";
            profileDto.PhoneNumber = "+919876543210987";
            profileDto.SubscriptionPlan = "PREMIUM";
            profileDto.PaymentDto.Add(new ProfileDtoPaymentDto()
            {
                PaymentType = "UPI",
                UpiId = "userupi@bank",
                UpiApp = "gpay",
            });
            profileDto.PaymentDto.Add(new ProfileDtoPaymentDto()
            {
                PaymentType = "CREDIT/CARD",
                CardNumber = "987654321123450",
                CardHolderName = "card user name",
                ExpireDate = "01/30",
            });
        }
        else
        {
            profileDto.UserName = "new user two";
            profileDto.Password = "Password@123";
            profileDto.ConfirmPassword = "Password@123";
            profileDto.EmailAddress = "AddressEmail@email.com";
            profileDto.PhoneNumber = "+911234567890";
            profileDto.SubscriptionPlan = "STANDARD";
            profileDto.PaymentDto.Add(new ProfileDtoPaymentDto()
            {
                PaymentType = "UPI",
                UpiId = "upiuser@bank",
                UpiApp = "gpay",
            });
            profileDto.PaymentDto.Add(new ProfileDtoPaymentDto()
            {
                PaymentType = "DEBIT/CARD",
                CardNumber = "012345678909876",
                CardHolderName = "card user name two",
                ExpireDate = "11/29",
            });
        }
        return profileDto;
        
    }

}