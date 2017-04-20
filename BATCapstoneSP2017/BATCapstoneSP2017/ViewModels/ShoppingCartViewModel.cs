using BATCapstoneSP2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BATCapstoneSP2017.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}