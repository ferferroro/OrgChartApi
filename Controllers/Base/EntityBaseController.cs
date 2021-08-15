using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;

namespace OrgChartApi.Controllers.Base {

    public class EntityBaseController : ControllerBase
    {

        public static void FilterEntityRequest<T>(ref IQueryable<T> list, dynamic filters)
        {
            // declare search filter variables
            long FindById;
            long[] FindByIdList;
            long FindByNotId;
            long[] FindByNotIdList;
            string FindByName;
            string FindByFirstName;
            string FindByLastName;
            long FindByCalendarId;
            long FindByPayrollId;
            long FindWorkStatusTemplateId;
            int FindByPageSize;
            int FindByPageNumber;
            string SortOrder;
            string SortField;

            // handle error and give default values
            try {
                FindById = filters.FindById;
            }
            catch(RuntimeBinderException) {
                FindById = 0;
            }

            try {
                FindByIdList = filters.FindByIdList;
            }
            catch(RuntimeBinderException) {
                FindByIdList = null;
            }

            try {
                FindByNotId = filters.FindByNotId;
            }
            catch(RuntimeBinderException) {
                FindByNotId = 0;
            }

            try {
                FindByNotIdList = filters.FindByNotIdList;
            }
            catch(RuntimeBinderException) {
                FindByNotIdList = null;
            }

            try {
                FindByName = filters.FindByName;
            }
            catch(RuntimeBinderException) {
                FindByName = null;
            }

            try {
                FindByFirstName = filters.FindByFirstName;
            }
            catch(RuntimeBinderException) {
                FindByFirstName = null;
            }

            try {
                FindByLastName = filters.FindByLastName;
            }
            catch(RuntimeBinderException) {
                FindByLastName = null;
            }

            try {
                FindByCalendarId = filters.FindByCalendarId;
            }
            catch(RuntimeBinderException) {
                FindByCalendarId = 0;
            }

            try {
                FindByPayrollId = filters.FindByPayrollId;
            }
            catch(RuntimeBinderException) {
                FindByPayrollId = 0;
            }

            try {
                FindWorkStatusTemplateId = filters.FindWorkStatusTemplateId;
            }
            catch(RuntimeBinderException) {
                FindWorkStatusTemplateId = 0;
            }

            try {
                FindByPageSize = filters.FindByPageSize;
            }
            catch(RuntimeBinderException) {
                FindByPageSize = 20;
            }

            try {
                FindByPageNumber = filters.FindByPageNumber;
            }
            catch(RuntimeBinderException) {
                FindByPageNumber = 1;
            }

            try {
                SortOrder = filters.SortOrder;
            }
            catch(RuntimeBinderException) {
                SortOrder = "asc";
            }

            try {
                SortField = filters.SortField;
            }
            catch(RuntimeBinderException) {
                SortField = null;
            }
            

            // Build the Query codes below:

            // add where clause
            if (FindById != 0) {
                list = list.Where(t => 
                    EF.Property<long>(t, "Id") == FindById
                );
            }
            else if (FindByIdList != null ) {
                list = list.Where(t => 
                    FindByIdList.Contains(EF.Property<long>(t, "Id"))
                );
            }
            else if (FindByNotId != 0) {
                list = list.Where(t => 
                    EF.Property<long>(t, "Id") != FindByNotId
                );
            }
            else if (FindByNotIdList != null) {
                list = list.Where(t => 
                    !FindByNotIdList.Contains(EF.Property<long>(t, "Id"))
                );                
            }
            else {
                if (FindByName != null) {
                    list = list.Where(t => 
                        EF.Property<string>(t, "Name").Contains(FindByName)
                    );
                }

                if (FindByFirstName != null) {
                    list = list.Where(t => 
                        EF.Property<string>(t, "FirstName").Contains(FindByFirstName)
                    );
                }

                if (FindByLastName != null) {
                    list = list.Where(t => 
                        EF.Property<string>(t, "LastName").Contains(FindByLastName)
                    );
                }

            } 

            // add sorting conditions 
            if (SortField != null) {

                if (SortOrder == "asc") {

                    if (SortField == "Id") {
                        list = list.OrderBy(t => 
                            EF.Property<long>(t, "Id")
                        );
                    }

                    if (SortField == "Name") {
                        list = list.OrderBy(t => 
                            EF.Property<string>(t, "Name")
                        );
                    }

                    if (SortField == "FirstName") {
                        list = list.OrderBy(t => 
                            EF.Property<string>(t, "FirstName")
                        );
                    }

                    if (SortField == "LastName") {
                        list = list.OrderBy(t => 
                            EF.Property<string>(t, "LastName")
                        );
                    }

                }

                if (SortOrder == "desc") {

                    if (SortField == "Id") {
                        list = list.OrderByDescending(t => 
                            EF.Property<long>(t, "Id")
                        );
                    }

                    if (SortField == "Name") {
                        list = list.OrderByDescending(t => 
                            EF.Property<string>(t, "Name")
                        );
                    }

                    if (SortField == "FirstName") {
                        list = list.OrderByDescending(t => 
                            EF.Property<string>(t, "FirstName")
                        );
                    }

                    if (SortField == "LastName") {
                        list = list.OrderByDescending(t => 
                            EF.Property<string>(t, "LastName")
                        );
                    }
                }
                
            }

            // pagination
            list = list
                .Skip(FindByPageSize * (FindByPageNumber - 1))
                .Take(FindByPageSize);

        }
    }
    
}