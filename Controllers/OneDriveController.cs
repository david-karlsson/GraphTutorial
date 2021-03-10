
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.Graph;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace GraphTutorial.Controllers{

 public class OneDriveController:Controller{

GraphServiceClient _graphClient{get;set;}

    public OneDriveController ( GraphServiceClient graphClient)
            
        {
            _graphClient = graphClient;
            
        }


             [HttpGet ]   
        [AuthorizeForScopes(Scopes = new[] { "Files.ReadWrite" })]
            public async Task<IActionResult> GetOneDrive(){


                            
            var drive = await _graphClient.Me.Drive.Root
                .Request()
                  .Select("items,id,list")
                .GetAsync();


                return View(drive);

            }



             [HttpGet ]   
        [AuthorizeForScopes(Scopes = new[] { "Files.ReadWrite" })]
            public async Task<IActionResult> GetFiles(){


             
                    var children = await _graphClient.Me.Drive.Root.Children
                .Request()
                .GetAsync();


                return View(children);

            }





 }
 
 }