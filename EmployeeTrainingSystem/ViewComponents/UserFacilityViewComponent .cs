using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTrainingSystem.ViewComponents
{
    public class UserFacilityViewComponent:ViewComponent
    {
        private readonly EntityDbContext _entityDbContext;
        public UserFacilityViewComponent(EntityDbContext entityDbContext)
        {
            _entityDbContext = entityDbContext;
        }

        public IViewComponentResult Invoke()
        {
          var member = _entityDbContext.Member.SingleOrDefault(x => x.Username == User.Identity.Name);
            ViewBag.member = member;
            return View();
        }
    }
}
