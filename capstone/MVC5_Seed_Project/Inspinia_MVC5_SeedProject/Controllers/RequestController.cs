using System;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Xml;
using Inspinia_MVC5_SeedProject.Controllers.Requests;
using Inspinia_MVC5_SeedProject.Models;
using SuperXML;


namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class RequestController : Controller
    {

        public ReturnType AssessmentOrderRequest(Client client, Candidates candidate, string reqId, string callBackUri)
        {
            // Send request and get XML response
            var xml = GenerateAssessmentOrderRequestXml(client, candidate, reqId, callBackUri);
            // Response from Select Server
            //var responseStr = PostXmlData("http://localhost:5001/SelectServer/ReceiveXmlData", xml); //mac
            var responseStr = PostXmlData("http://localhost:59328/SelectServer/ReceiveXmlData", xml); //windows

            // Parse XML response to check state and parse returned urls
            var doc = new XmlDocument();
            doc.LoadXml(responseStr);
            var receiptId = doc.GetElementsByTagName("ProviderId");
            var internetWebAddress = doc.GetElementsByTagName("InternetWebAddress");
            var description = doc.GetElementsByTagName("Description");
            
            var returnType = new ReturnType();
            if (receiptId.Count >= 1 && internetWebAddress.Count >= 1)
            {
                returnType.ResponseStatus = true;
                
                returnType.ReceiptId = receiptId[0].InnerText;
                returnType.Url = internetWebAddress[0].InnerText;
                returnType.Description = description[0].InnerText;
            }
            return returnType;
        }
        
        
        [HttpGet]
        public void TestAssessmentOrderRequest()
        {
            var client = new Client
            {
                ClientCode = "001",
                ProviderKey = "Provider_Coke",
                CustomerNumber = "Coke",
            };

            var candidate = new Candidates
            {
                UserID = "tcao",
                Requestor = "Coke",
                LastName = "Cao",
                FirstName = "Tianyi",
                Email = "caoty0303@outlook.com"
            };

            var urls = AssessmentOrderRequest(client, candidate, "Test pool 1", "None call back");
            Console.Write("finish");
        }
        
        
        private string GenerateAssessmentOrderRequestXml(Client client, Candidates candidate, string requisitionId, string callBackUri)
        {
            var compiler = new Compiler()
                .AddKey("ClientCode", candidate.Requestor)
                .AddKey("ProviderKey", client.ProviderKey)
                .AddKey("CustomerNumber", client.CustomerNumber)
                .AddKey("RequisitionId", requisitionId)
                .AddKey("EmployeeNumber", candidate.UserID) // same to UniqueIdentifier
                .AddKey("CallBackUri", callBackUri)
                .AddKey("Requestor", candidate.Requestor)
                .AddKey("LastName", candidate.LastName)
                .AddKey("FirstName", candidate.FirstName)
                .AddKey("CandidateEmail", candidate.Email);

            //var path = Directory.GetCurrentDirectory() + "/Controllers/Requests/AssessmentOrderTemplate.xml"; //mac
            //var path = "C:\\Users\\zirui\\capstone\\Capstone-MISM\\MVC5_Seed_Project\\Inspinia_MVC5_SeedProject\\Controllers\\Requests\\AssessmentOrderTemplate.xml";
            var path = "C:\\Users\\lenovo\\Desktop\\1129\\Capstone-MISM\\MVC5_Seed_Project\\Inspinia_MVC5_SeedProject\\Controllers\\Requests\\AssessmentOrderTemplate.xml";
            var compliedXml = compiler.CompileXml(path);
            return compliedXml;
        }


        public ReturnType AssessmentStatusRequest(Client client, string receiptId)
        {
            // Send request and get XML response
            var xml = GenerateAssessmentStatusRequest(client, receiptId);
            // Response from Select Server
            var responseStr = PostXmlData("http://localhost:5001/SelectServer/ReceiveXmlData", xml);
            
            // Parse XML response to check state and parse returned urls
            var doc = new XmlDocument();
            doc.LoadXml(responseStr);

            var returnType = new ReturnType();
            var status = doc.GetElementsByTagName("Status");
            var statusDate = doc.GetElementsByTagName("InternetWebAddress");
            if (status.Count >= 1 && statusDate.Count >= 1)
            {
                // Flag of valid response or not
                returnType.ResponseStatus = true;
                
                returnType.Status = status[0].InnerText;
                returnType.StatusDate = statusDate[0].InnerText;
            }
            return returnType;
        }
        
        
        [HttpGet]
        public void TestAssessmentStatusRequest()
        {
            var client = new Client
            {
                ClientCode = "001",
                ProviderKey = "Provider_Coke",
                CustomerNumber = "Coke",
            };
            string receiptId = "Receipt:001";
            ReturnType returnType = AssessmentStatusRequest(client, receiptId);
        }

        
        private string GenerateAssessmentStatusRequest(Client client, string receiptId)
        {
            var compiler = new Compiler()
                .AddKey("ClientCode", client.ClientCode)
                .AddKey("ProviderKey", client.ProviderKey)
                .AddKey("CustomerNumber", client.CustomerNumber)
                .AddKey("ReceiptKey", receiptId);

            //var path = Directory.GetCurrentDirectory() + "/Controllers/Requests/AssessmentStatusTemplate.xml";
            var path = "C:\\Users\\lenovo\\Desktop\\1129\\Capstone-MISM\\MVC5_Seed_Project\\Inspinia_MVC5_SeedProject\\Controllers\\Requests\\AssessmentStatusTemplate.xml";
            var result = compiler.CompileXml(path);
            return result;
        }

        
        public string PostXmlData(string destinationUrl, string requestXml)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(destinationUrl);
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            var response = (HttpWebResponse) request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            return null;
        }

    }
}