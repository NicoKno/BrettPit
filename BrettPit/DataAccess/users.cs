//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BrettPit.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public users()
        {
            this.log_admin = new HashSet<log_admin>();
            this.log_admin1 = new HashSet<log_admin>();
            this.log_pwchange = new HashSet<log_pwchange>();
            this.log_pwchange1 = new HashSet<log_pwchange>();
            this.pairings = new HashSet<pairings>();
            this.pairings1 = new HashSet<pairings>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public bool isadmin { get; set; }
        public System.DateTime last_login { get; set; }
        public string loginid { get; set; }
    
        public virtual eloes eloes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<log_admin> log_admin { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<log_admin> log_admin1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<log_pwchange> log_pwchange { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<log_pwchange> log_pwchange1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pairings> pairings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pairings> pairings1 { get; set; }
    }
}
