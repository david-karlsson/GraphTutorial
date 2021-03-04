using Microsoft.Graph;

using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace GraphTutorial.Views.Email{





    public class EmailViewComponent : ViewComponent{

        IUserMessagesCollectionPage userMessages = null;

            GraphServiceClient _graphClient{get;set;}

                public EmailViewComponent ( GraphServiceClient graphClient)
                        
                    {
                        _graphClient = graphClient;
                        
                    }


            public string GetPlainTextFromHtml(string htmlString)
            {
                string htmlTagPattern = "<.*?>";
                var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                htmlString = regexCss.Replace(htmlString, string.Empty);
                htmlString = Regex.Replace(htmlString, htmlTagPattern, string.Empty);
                htmlString = Regex.Replace(htmlString, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
                htmlString = htmlString.Replace("&nbsp;", string.Empty);

                return htmlString;
            }                        public async Task<IViewComponentResult> InvokeAsync(){
                
                                
                                userMessages = await _graphClient.Me.Messages.Request().Top(1)
                                                                                    .Select("body")
                                                                                    .GetAsync();


                                    var ComponentMessage = new List<Models.Message>();


                                        foreach (var message in userMessages){



                                                  var BodyText = GetPlainTextFromHtml(message.Body.Content);
                                                       ComponentMessage.Add(new Models.Message{
                                                                    Body= BodyText
                                             
                                                       });

                                                
                                           
                                            
                                            }



                            return View(ComponentMessage);


                        }

        }





}

