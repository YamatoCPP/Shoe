using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project3
{
    public enum RoleEnum
    {
        CLIENT,
        MANAGER,
        ADMIN,
        GUEST
    }

    internal class Manager 
    {
        public static string Name;
        public static RoleEnum role;
        public static User user;
        public static Frame MainFrame { get; set; }
    }
}
