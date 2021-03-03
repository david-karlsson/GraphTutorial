using GraphTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// using System.Net.Http;


namespace GraphTutorial.Controllers{

 public class EmailController:Controller{
 
//  static HttpClient httpClient {get;set;}



                // GraphServiceClient graphClient = SDKHelper.GetMicrosoftAuthenticatedClient(accessToken);

GraphServiceClient _graphClient{get;set;}

    public EmailController ( GraphServiceClient graphClient)
            
        {
            _graphClient = graphClient;
            
        }



// public GraphServiceClient graphClient {get;set;}
        MailFolder inbox = null;
        List<Models.Message> MyMessages = null;
 

        IUserMessagesCollectionPage userMessages = null;


            
            
            
            
            [AuthorizeForScopes(Scopes = new[] { "Mail.ReadWrite" })]
            public async  Task <IActionResult> Index(){

                        // graphClient = GraphClient;



                        userMessages = await _graphClient.Me.Messages.Request().Top(20)
                                                                        .Select("sender, from, subject, importance,body,receivedDateTime")
                                                                        .GetAsync();

                                        MyMessages = new List<Models.Message>();

                                        

                                        foreach (var message in userMessages)
                                        {
                                            MyMessages.Add(new Models.Message
                                            {

                                                Id = message.Id,
                                                ReceivedDateTime =message.ReceivedDateTime.ToString(),
                                                Body = message.Body.ToString(),
                                                Sender = (message.Sender != null) ?
                                                        message.Sender.EmailAddress.Name :
                                                        "Unknown name",
                                                From = (message.Sender != null) ?
                                                        message.Sender.EmailAddress.Address :
                                                        "Unknown email",
                                                Subject = message.Subject ?? "No subject",
                                                Importance = message.Importance.ToString()
                                            });



                                        }

                // return View();

                    return View(MyMessages);

                   }
              }



        }