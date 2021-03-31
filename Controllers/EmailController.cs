using GraphTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.Graph;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System;



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
        // MailFolder inbox = null;
        List<Email> MyMessages = null;
 

        IUserMessagesCollectionPage userMessages = null;


            
            
            public string GetPlainTextFromHtml(string htmlString)
            {
                string htmlTagPattern = "<.*?>";
                var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                htmlString = regexCss.Replace(htmlString, string.Empty);
                htmlString = Regex.Replace(htmlString, htmlTagPattern, string.Empty);
                htmlString = Regex.Replace(htmlString, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
                htmlString = htmlString.Replace("&nbsp;", string.Empty);




                // var html = Regex.Replace(htmlString, @"</?html(.+?)>", "");

                // var head = Regex.Replace(html, @"</?head>", "");

                // var meta = Regex.Replace(head, @"</?meta(.+?)>", "");

                // var body = Regex.Replace(meta, @"</?body>", "");

                return htmlString;
            }
            
            
            [AuthorizeForScopes(Scopes = new[] { "Mail.ReadWrite" })]
            public async  Task <IActionResult> Index(){

                        // graphClient = GraphClient;



                        userMessages = await _graphClient.Me.Messages.Request().Top(20)
                                                                        .Select("sender, from, subject, importance,body,receivedDateTime")
                                                                        .GetAsync();

                                        MyMessages = new List<Email>();

                                        
                                            
                          

                                            
                                        foreach (var message in userMessages)
                                        {

                                             var BodyText = GetPlainTextFromHtml(message.Body.Content);
                                            MyMessages.Add(new Email
                                            {

                                                Id = message.Id,
                                                ReceivedDateTime =message.ReceivedDateTime.ToString(),
                                                Body = BodyText,
                                                Sender = (message.Sender != null) ?
                                                        message.Sender.EmailAddress.Name :
                                                        "Unknown name",
                                                From = (message.Sender != null) ?
                                                        message.Sender.EmailAddress.Address :
                                                        "Unknown email",
                                                Subject = message.Subject ?? "No subject",
                                                Importance = message.Importance.ToString()
                                            });

                                            
                                            
                                            //  var fileName = Path.ChangeExtension(Path.GetTempFileName(), ".html");

                                            // var fs = System.IO.File.CreateText(fileName);
                                            // fs.Write(message.Body.Content);
                                            // fs.Flush();
                                            // fs.Close();

                                            // System.Diagnostics.Process.Start(fileName);

                                             

                                        }

                // return View();

                    return View (MyMessages);

                   }


              [HttpPost]
            [AuthorizeForScopes(Scopes = new[] { "Mail.Send" })]
                public async Task <IActionResult> SendMail()
                    {
                        var recipients = new List<Recipient>();
                        recipients.Add(new Recipient
                        {
                            EmailAddress = new EmailAddress
                            {
                                Address = "dk-webbdesign@outlook.com"
                            }
                        });


                        

                        var messageToSend = new Message
                        {
                            ToRecipients = recipients,
                        
                            Subject = "Urgent",
                            Body = new ItemBody
                            {
                                Content = "Call me immediately if you don't get this message!!",
                                ContentType = BodyType.Html
                            },
                        };

                        try
                        {
                            await _graphClient.Me.SendMail(messageToSend, true).Request().PostAsync();
                            
                        }
                        catch (ServiceException ex)
                        {
                            Console.WriteLine($"We could not send this message: {ex.Error.Message}");
                            
                        }
                        return View(messageToSend);
                    }


      


     }

}