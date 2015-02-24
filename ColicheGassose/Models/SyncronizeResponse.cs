using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColicheGassose.Models
{
    public class SyncronizeResponse
    {
        public bool Error;
        public string Message;
        public object Data;
        public UserDataTransport UserData;

        public SyncronizeResponse(UserDataTransport userData = null,  bool error = false, string message = "No errors", object data = null)
        {
            UserData = userData;
            Error = error;
            Message = message;
            Data = data;
        }
    }
}