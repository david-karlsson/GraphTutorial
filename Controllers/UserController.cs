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

  [AuthorizeForScopes(Scopes = new[] { "User.ReadWrite" })]
            public async  Task <IActionResult> Index(){

//  userMessages =

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
                return View(MyUsers);
            }



 }
   
 
 }
 