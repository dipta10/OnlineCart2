using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineCart2.Models {
    public class ok {
        public static List <cartitem> c = new List<cartitem>();
    }
    public class cartitem {
        public int iid;
        public int iqty;
        public void set(int id, int qty) {
            iid = id;
            iqty = qty;
        }
    }
}