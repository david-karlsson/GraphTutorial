using Microsoft.Graph;

using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace GraphTutorial.Views.User{





    public class IndexViewComponent : ViewComponent{



                    GraphServiceClient _graphClient{get;set;}

                public IndexViewComponent ( GraphServiceClient graphClient)
                        
                    {
                        _graphClient = graphClient;
                        
                    }

        
                

             
                            public async Task<IViewComponentResult> InvokeAsync(){  
    
                                 IGraphServiceUsersCollectionPage users = null;
                List<Microsoft.Graph.User>MyUsers = null;

                     users =  await _graphClient.Users.Request()
                                                                        .Select("displayName,mail,userPrincipalName")
                                                                        .GetAsync();

                                         MyUsers = new List<Microsoft.Graph.User>();

                                        
                                            
                        
                                        foreach (var u in users)
                                        {

                                            MyUsers.Add(new Microsoft.Graph.User
                                            {

                                                Id = u.Id,
                                                DisplayName = u.DisplayName,
                                                Mail = u.Mail,
                                                UserPrincipalName = u.UserPrincipalName
                                            });

                                            
                                        }
                             return View(users);

   
                               
                
                                                       }


         }
    
    
    
    
    
    
    }
