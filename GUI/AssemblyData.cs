using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    static class AssemblyData
    {
        private static string _title = null;
        private static string _version = null;
        private static string _description = null;
        private static string _product = null;
        private static string _copyright = null;
        private static string _company = null;

        #region Assembly Attribute Accessors

        public static string Title
        {
            get
            {
                if (_title != null)
                {
                    return _title;
                }

                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (!string.IsNullOrEmpty(titleAttribute.Title))
                    {
                        _title = titleAttribute.Title;
                        return _title;
                    }
                }
                _title = System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
                return _title;
            }
        }

        public static string Version
        {
            get
            {
                if (_version != null)
                {
                    return _version;
                }

                _version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                return _version;
            }
        }

        public static string Description
        {
            get
            {
                if (_description != null)
                {
                    return _description;
                }

                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    _description = "";
                    return _description;
                }
                _description = ((AssemblyDescriptionAttribute)attributes[0]).Description;
                return _description;
            }
        }

        public static string Product
        {
            get
            {
                if (_product != null)
                {
                    return _product;
                }

                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    _product = "";
                    return _product;
                }
                _product = ((AssemblyProductAttribute)attributes[0]).Product;
                return _product;
            }
        }

        public static string Copyright
        {
            get
            {
                if (_copyright != null)
                {
                    return _copyright;
                }

                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    _copyright = "";
                    return _copyright;
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string Company
        {
            get
            {
                if (_company != null)
                {
                    return _company;
                }

                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    _company = "";
                    return _company;
                }
                _company = ((AssemblyCompanyAttribute)attributes[0]).Company;
                return _company;
            }
        }
        #endregion
    }
}
