//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SecureVault.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vaults
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vaults()
        {
            this.Keys = new HashSet<Keys>();
        }
    
        public int U_ID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public System.DateTime DateCreated { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Keys> Keys { get; set; }
        public virtual Users Users { get; set; }
    }
}