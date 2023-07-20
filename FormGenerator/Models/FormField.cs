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
    
    public partial class FormField
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FormField()
        {
            this.UserSubmissionValues = new HashSet<UserSubmissionValue>();
        }
    
        public long Id { get; set; }
        public string Caption { get; set; }
        public string TextBefore { get; set; }
        public string TextAfter { get; set; }
        public byte FieldTypeId { get; set; }
        public Nullable<int> SelectListId { get; set; }
        public byte FieldOrder { get; set; }
        public int FormId { get; set; }
        public bool IsActive { get; set; }
        public string MaxValue { get; set; }
        public string MinValue { get; set; }
        public Nullable<int> MaxLength { get; set; }
        public Nullable<int> MinLength { get; set; }
        public string DefaultValue { get; set; }
    
        public virtual FieldType FieldType { get; set; }
        public virtual Form Form { get; set; }
        public virtual SelectList SelectList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserSubmissionValue> UserSubmissionValues { get; set; }
    }
}
