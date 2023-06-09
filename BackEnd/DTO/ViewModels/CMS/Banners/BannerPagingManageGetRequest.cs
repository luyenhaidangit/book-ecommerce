﻿using DTO.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Thegioididong.Model.ViewModels.Common;

namespace Thegioididong.Model.ViewModels.CMS.Banners
{
    public class BannerPagingManageGetRequest : PagingRequestBase
    {
        public string? Name { get; set; }

        public string? Page { get; set; }

        public string? Position { get; set; }

        public bool? Published { get; set; }

        public string? SortBy { get; set; }

        public string? OrderBy { get; set; }
    }
}
