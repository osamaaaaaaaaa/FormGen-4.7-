//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FormGenerator.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserSubmissionValue
    {
        public long SubmissionId { get; set; }
        public long FieldId { get; set; }
        public string SubmittedValue { get; set; }
    
        public virtual FormField FormField { get; set; }
        public virtual UserSubmission UserSubmission { get; set; }
    }
}