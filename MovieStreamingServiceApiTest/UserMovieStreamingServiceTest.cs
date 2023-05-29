using Entity.Dto;
using Microsoft.AspNetCore.Mvc;
using Exception;
using Microsoft.AspNetCore.Http;
namespace MovieStreamingServiceApiTest;
public class UserMovieStreamingServiceTest : UserBaseTesting
{   
    //User login test
    [Theory]
    [InlineData("UserUser","Propel@123",200)]
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

    //User delete test
    [Fact]
    public void DeleteUserTesting()
    {
        OkObjectResult result = (OkObjectResult)adminController.DeleteUserApi(userId2);
        Assert.Equal(200, result.StatusCode);  
    }

    //Get user by user
    [Fact]
    public void GetUserTesting()
    {
        OkObjectResult result = (OkObjectResult)adminController.GetUserApi();
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<ProfileDto>>(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
    }

    //Get subscribed movie by user
    [Fact]
    public void GetMovieTesting()
    {
        OkObjectResult result = (OkObjectResult)movieController.GetMoviesApi();
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<MovieDto>>(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
    }

    //Get subscription details by user
    [Fact]
    public void GetSubscriptionTesting()
    {
        OkObjectResult result = (OkObjectResult)subscriptionController.GetSubscriptionApi();
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<SubscriptionDto>>(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
    }

    //Update user by admin
    [Fact]
    public void UpdateUserTesting()
    {
        OkObjectResult result = (OkObjectResult)userController.UpdateUserApi(userId2,source(1));
        Assert.Equal(200, result.StatusCode);
    }

    //Register user
    [Theory]
    [InlineData(1,201)]
    [InlineData(2,400)]
    [InlineData(3,409)]
    [InlineData(4,409)]
    [InlineData(5,409)]
    [InlineData(6,404)]
    [InlineData(7,409)]
    public void RegisterUserTesting(int n, int expectedCode)
    {
        ProfileDto profileDto = source(2);
        if( n == 1 )
        {
            IActionResult result = userController.UserRegisterApi(profileDto);
            CreatedAtActionResult createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            ResponseIdDto responseIdDto = Assert.IsType<ResponseIdDto>(createdAtActionResult.Value);
            Assert.NotNull(responseIdDto);
            
        }
        else if( n == 2 )
        {
            profileDto.Password = "Hello@123";
            profileDto.ConfirmPassword = "Hihello@123";
        }
        else if( n == 3 )
        {
            profileDto.UserName = "UserUser";
        }
        else if ( n == 4)
        {
            profileDto.PhoneNumber = "+919876543210";
        }
        else if( n == 5 )
        {
            profileDto.EmailAddress = "user@outlook.com";
        }
        else if ( n == 6 )
        {
            profileDto.SubscriptionPlan = "ULTIMATE";
        }
        else if ( n == 7 )
        {
            profileDto.PaymentDto![1].CardNumber = "12345678909876";
        }
        if(n != 1)
        {
            BaseCustomException ex = Assert.Throws<BaseCustomException>(() => userController.UserRegisterApi(profileDto));
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
        else if( n == 2 )
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