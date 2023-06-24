using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeBanHang.Models.EF
{
    [Table("tb_Post")]
    public class Post : CommonAbstract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CategoryID { get; set; }
        public string Title { get; set; }
        [StringLength(150)]
        public string Alias { get; set; }

        public string Description { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        [StringLength(150)]
        public string Image { get; set; }
        public bool IsActive { get; set; }
        [StringLength(250)]
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }

        public virtual Category Category { get; set; }
    }
}