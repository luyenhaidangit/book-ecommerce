﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thegioididong.Model.ViewModels.CMS.Galleries
{
    public class GalleryImageProductDetailPageViewModel
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public class GalleryProductDetailPageViewModel
        {
            public string Name { get; set; }

            public string Image { get; set; }

            public List<GalleryImageProductDetailPageViewModel> GalleryImages { get; set; }
        }
    }
}
