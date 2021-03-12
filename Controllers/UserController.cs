using GraphTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.Graph;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace GraphTutorial.Controllers{

 public class UserController:Controller{

GraphServiceClient _graphClient{get;set;}

    public UserController ( GraphServiceClient graphClient)
            
        {
            _graphClient = graphClient;
            
        }

                List<User>MyUsers = null;
                IGraphServiceUsersCollectionPage users = null;


    [HttpGet ]  
  [AuthorizeForScopes(Scopes = new[] { "User.ReadWrite.All" })]
            public async  Task <IActionResult> Index(){



                     users =  await _graphClient.Users.Request()
                                                                        .Select("displayName,mail,userPrincipalName")
                                                                        .GetAsync();

                                         MyUsers = new List<User>();

                                        
                                            
                        
                                        foreach (var u in users)
                                        {

                                            MyUsers.Add(new User
                                            {

                                                Id = u.Id,
                                                DisplayName = u.DisplayName,
                                                Mail = u.Mail,
                                                UserPrincipalName = u.UserPrincipalName
                                            });

                                            
                                        }
                return View(users);
            }



[HttpPost]
  [AuthorizeForScopes(Scopes = new[] { "User.ReadWrite" })]

public async Task<IActionResult> CreateUser(){

    

var user = new User
{
	AccountEnabled = true,
	DisplayName = "Adele Vance",
	MailNickname = "AdeleV",
	UserPrincipalName = "AdeleV@contoso.onmicrosoft.com",
	PasswordProfile = new PasswordProfile
	{
		ForceChangePasswordNextSignIn = true,
		Password = "xWwvJ]6NMw+bWH-d"
	}
};

await _graphClient.Users
	.Request()
	.AddAsync(user);

    return View(user);

}


    [HttpPatch ]     
  [AuthorizeForScopes(Scopes = new[] { "User.ReadWrite" })]
            public async Task<IActionResult> UpdateUser(){


                                var user = new User
                {
                 GivenName ="Test name"
                };

                await _graphClient.Me
                    .Request()
                    .UpdateAsync(user);
                
                
                return View(user);
            }


       

   

 }
   
 
 }
 