using System;
using System.IO;
using System.Web.Mvc;
using System.Xml;
using Inspinia_MVC5_SeedProject.Controllers.Requests;
using Inspinia_MVC5_SeedProject.Models;
using SuperXML;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class SelectServerController : Controller
    {
        Dictionary<string,int> _myHashMap = new Dictionary<string,int>();

        [HttpPost]
        public string updateProgress()
        {
            var ReciptID = "";
            using(System.IO.StreamReader reader = new System.IO.StreamReader(Request.InputStream))
            {
                while (reader.Peek() >= 0)
                {    
                    ReciptID += reader.ReadLine();
                }
            }
            var status = "50";
            if (_myHashMap.ContainsKey(ReciptID))
            {
                status = "100";
            }
            else
            {
                _myHashMap.Add(ReciptID,1);
            }
            var root = new 
            { 
                assessment_progress = new 
                { 
                    reciptID = ReciptID, 
                    progress = status
                }
            };
            string json = JsonConvert.SerializeObject(root);
            return json;
        }
        
        [HttpPost]
        public ContentResult ReceiveXmlData()
        {
            var strmContents="";
            using(System.IO.StreamReader reader = new System.IO.StreamReader(Request.InputStream))
            {
                while (reader.Peek() >= 0)
                {    
                    strmContents += reader.ReadLine();
                }
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strmContents);
            XmlElement root = doc.DocumentElement;
            String requestType = root.Name;
            Console.Write(requestType);
            string clientCode = "";
            string providerKey = "";
            string customerNumber = "";
            XmlNodeList clientId = doc.GetElementsByTagName("ClientId");
            if(clientId.Count >= 1)
            {
                // The tag could be found!
                clientCode= clientId[0].InnerText;
                Console.Write(clientCode);
            }
            
            XmlNodeList providerId = doc.GetElementsByTagName("ProviderId");
            if(providerId.Count >= 1)
            {
                // The tag could be found!
                providerKey= providerId[0].InnerText;
                Console.Write(providerKey);
            }
            
            XmlNodeList CustomerId = doc.GetElementsByTagName("ClientOrderId");
            if(CustomerId.Count >= 1)
            {
                // The tag could be found!
                customerNumber = CustomerId[0].InnerText;
                Console.Write(customerNumber);
            }


            Client client = new Client
            {
                ClientCode = clientCode,
                ProviderKey = providerKey,
                CustomerNumber = customerNumber
            };
            
            String xml = "";
            if (requestType.Equals("AssessmentOrderRequest"))
            {
                xml = SendAcknowledgementResponse(client, true);
            }
            else
            {
                xml = SendAcknowledgementResponse(client, false);
            }
            return Content(xml);

        }
        
        
        public static String GetTimestamp(DateTime value) {
            return value.ToString("yyyyMMddHHmmssffff");
        }
        
       
        public string SendAcknowledgementResponse(Client client, bool flag)
        {
            String timeStamp = GetTimestamp(DateTime.Now);
            
            // True for register, False for progress
            var acknowledgement1 = new Acknowledgement()
            {
                ReceiptId = "Receipt-" + timeStamp,
                AssessmentUrl = "http://assessment_url/",
                Description =  "Software Engineer, Entry-Level Challenge I",
                Status = "0", //0-100
                StatusDate =  "Noverber 15, 2017"
            };
            var acknowledgement2 = new Acknowledgement()
            {
                Status = "80", //0-100
                StatusDate =  "Noverber 20, 2017"
            };
            var xml = "";
            if (flag)
            {
                xml = GenerateAcknowledgementResponseXml(client, acknowledgement1);
            }
            else
            {
                xml = GenerateAcknowledgementResponseXml(client, acknowledgement2);
            }
            return xml;
        }

        public string GenerateAcknowledgementResponseXml(Client client, Acknowledgement acknowledgement)
        {
            var compiler = new Compiler();
            compiler.AddKey("ClientCode", client.ClientCode)
                .AddKey("ProviderKey", client.ProviderKey)
                .AddKey("ReceiptId", "Receipt:001")
                .AddKey("CustomerNumber", client.CustomerNumber)
                .AddKey("AssessmentUrl", acknowledgement.AssessmentUrl)
                .AddKey("Description", acknowledgement.Description)
                .AddKey("Status", acknowledgement.Status)
                .AddKey("StatusDate", acknowledgement.StatusDate);

            //var path = Directory.GetCurrentDirectory() + "/Controllers/Requests/AcknowledgementTemplate.xml";
            var path = "C:\\Users\\lenovo\\Desktop\\1129\\Capstone-MISM\\MVC5_Seed_Project\\Inspinia_MVC5_SeedProject\\Controllers\\Requests\\AcknowledgementTemplate.xml";
            var result = compiler.CompileXml(path);
            return result;
        }

    }
    
}