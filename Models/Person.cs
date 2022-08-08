using System;
using System.ComponentModel.DataAnnotations;

namespace MarleysGoldendoodles.Models
{
    public class Person
    {
        public int ID { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd_HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Updated Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd_HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedDate { get; set; }
    }
}

