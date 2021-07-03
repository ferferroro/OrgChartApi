using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrgChartApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class MenuListController : ControllerBase
    {
        // GET: v1/<controller>
        [HttpGet("{menu_type}")]
        public IEnumerable<string> Get(string menu_type)
        {
            string[] menu_list;

            switch(menu_type) 
            {
                case "company":
                    menu_list = new string[] {
                        "Add Department", 
                        "Add Employee", 
                        "Add Template",
                        "Assign Calendar Template",
                        "Assign Payroll Template",
                        "Assign Work Status Template",
                        "Assign Head",
                        "View Details"
                        };
                    break;
                case "dept":
                    menu_list = new string[] {
                        "dept menu 1", 
                        "dept menu 2", 
                        "dept menu 3"
                        };
                    break;
                default:
                    menu_list = new string[] {
                        "Unknown Menu Type"
                        };
                    break;
            }
            return menu_list;
        }
    }
}