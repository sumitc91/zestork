//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace zestork.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ThirdPartyLogin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FacebookId { get; set; }
        public string FacebookAccessToken { get; set; }
        public string GoogleId { get; set; }
        public string GoogleAccessToken { get; set; }
    }
}
