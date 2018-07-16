﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unsflash.Controls
{
    public class SaveColModel
    {
        private Size size;
        public Size Size
        {
            get { return size; }
            set { size = GetSize(width, height); }
        }
        private Size GetSize(int width, int height)
        {
            Size size = new Size(width, height);
            return size;
        }
        public double Scale { get; set; }
        public string id { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string urlsMedium { get; set; }
        public string urlsFull { get; set; }
    }
}