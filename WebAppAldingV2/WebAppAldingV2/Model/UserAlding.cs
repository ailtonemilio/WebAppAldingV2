namespace WebAppAldingV2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAlding")]
    public partial class UserAlding
    {
        public int UserAldingId { get; set; }

        [StringLength(255)]
        public string UserName { get; set; }

        [StringLength(12)]
        public string UserPassword { get; set; }

        public bool? UserActive { get; set; }

        [StringLength(20)]
        public string UserGender { get; set; }

        [StringLength(2)]
        public string UserProvince { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RegistrationDate { get; set; }
    }
}
