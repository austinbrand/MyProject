using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BATCapstoneSP2017.Models;
using System.ComponentModel.DataAnnotations;

namespace BATCapstoneSP2017.ViewModels
{
    public class CategorieViewModel
    {
        [Key]
        int ID { get; set; }

        string CategoryName { get; set; }

    }
}