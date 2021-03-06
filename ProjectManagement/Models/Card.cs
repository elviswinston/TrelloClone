//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Card
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Card()
        {
            this.Tasks = new HashSet<Task>();
        }
    
        public int CardID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public Nullable<System.DateTime> ExpiryDay { get; set; }
        public int ListID { get; set; }
    
        public virtual List List { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
