//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ColicheGassose
{
    using System;
    using System.Collections.Generic;
    
    public partial class WebUser
    {
        public WebUser()
        {
            this.Admin = false;
        }
    
        public int ID { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
    }
}
