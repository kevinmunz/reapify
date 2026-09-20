using Microsoft.AspNetCore.Mvc.Rendering;

namespace Advertisements.Models.TransactionModels
{
    public class CreateTransactionVM : Transaction
    {
        public IEnumerable<SelectListItem> EntityNames { get; set; }
        public IEnumerable<SelectListItem> ProductIds { get; set; }
    }
}
