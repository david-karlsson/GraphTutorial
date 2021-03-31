using Microsoft.Graph;

using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace GraphTutorial.Views.User{





    public class UserUpdateViewComponent : ViewComponent{



                    GraphServiceClient _graphClient{get;set;}

                public UserUpdateViewComponent ( GraphServiceClient graphClient)
                        
                    {
                        _graphClient = graphClient;
                        
                    }


                            public async Task<IViewComponentResult> InvokeAsync(){  
    


   
                                    return View();
                
                                                       }


         }
    
    
    
    
    
    
    }
