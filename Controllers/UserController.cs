using GraphTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;

using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace GraphTutorial.Controllers{

 public class UserController:Controller{


public IActionResult Home(){


    return View();
}


public readonly static string[] UserScope =
        {
            "User.ReadWrite"};






GraphServiceClient _graphClient{get;set;}

    public UserController ( GraphServiceClient graphClient)
            
        {
            _graphClient = graphClient;
            
        }

                List<User>MyUsers = null;
                IGraphServiceUsersCollectionPage users = null;


    [HttpGet ]  
  [AuthorizeForScopes(Scopes = new[] { "User.Read" })]
            public async  Task <IActionResult> Index(){



                     users =  await _graphClient.Users.Request()
                                                                        .Select("displayName,mail,userPrincipalName,city")
                                                                        .GetAsync();

                                         MyUsers = new List<User>();

                                        
                                            
                        
                                        foreach (var u in users)
                                        {

                                            MyUsers.Add(new User
                                            {

                                                Id = u.Id,
                                                DisplayName = u.DisplayName,
                                                Mail = u.Mail,
                                                UserPrincipalName = u.UserPrincipalName,
                                                JobTitle =  u.JobTitle,
                                                City = u.City
                               

                                            });

                                            
                                        }
                return View(users);
            }



public IActionResult UserCreateForm(){

    return View();
}


[HttpPost]
  [AuthorizeForScopes(Scopes = new[] { "User.ReadWrite" })]
public async Task<IActionResult> CreateUser([Bind("DisplayName,UserPrincipalName")]User user){

    

// var user = new User
// {
// 	AccountEnabled = true,
// 	DisplayName = "Ariel Schnell",
// 	MailNickname = "AdeleS",
// 	UserPrincipalName = "Ariel@dkwebbdesign.onmicrosoft.com",
// 	PasswordProfile = new PasswordProfile
// 	{
// 		ForceChangePasswordNextSignIn = true,
// 		Password = "xWwvJ]6NMw+bWH-d"
// 	}
// };
        user.AccountEnabled = true;
        // var fullName = user.GivenName +" "+ user.Surname;
        // user.DisplayName = user.GivenName;
        // user.City="RequiredForCompatibility";
        user.MailNickname = "RequiredForCompatibility";
        user.PasswordProfile = new PasswordProfile
	{
		ForceChangePasswordNextSignIn = true,
		Password = "xWwvJ]6NMw+bWH-d"
	};


            if (ModelState.IsValid)
            {

                await _graphClient.Users
                    .Request()                 
                    .AddAsync(user);
            }
   
   
    return View(user);

}


    [HttpPatch]     
    [ValidateAntiForgeryToken]
  [AuthorizeForScopes(Scopes = new[] {  "User.ReadWrite.All", })]
            public async Task<IActionResult> UpdateUser( [Bind("JobTitle,DisplayName,UserPrincipalName")] User user){


                                
                // user.JobTitle ="Test..";

                await _graphClient.Users[user.Id]
                    .Request()
                    // .Select("jobTitle,mail,userPrincipalName")
                    .UpdateAsync(user);
                
                
                return View(user);
            }


       

public IActionResult UserDeleteForm(){

    return View();
}


   
    [HttpDelete ]     
    [ValidateAntiForgeryToken]
  [AuthorizeForScopes(Scopes = new[] { "User.ReadWrite" })]
            public async Task<IActionResult> DeleteUser( [FromBody] User user){


                                
 
               await _graphClient.Users[user.UserPrincipalName]
	                    .Request()
	                    .DeleteAsync();

                
                return View(user);
            }



 }
   
 
 }
 