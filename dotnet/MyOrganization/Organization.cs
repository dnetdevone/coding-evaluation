using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;
        private int currentId;

        public Organization()
        {
            root = CreateOrganization();

            currentId = Convert.ToInt32(ConfigurationManager.AppSettings["InitialID"]);
        }

        protected abstract Position CreateOrganization();

        /// <summary>
        /// This method hires the given person as an employee in the position that has that title.
        /// Note: InitialID is set in the app.config file so that the number is not hard coded in case problems arise 
        /// and the number cannot be changed in the source code.
        /// </summary>
        /// <param name="person"></param>
        /// <param name="title"></param>
        /// <returns>Method returns the newly filled position or empty if no position has that title.</returns>
        public Position? Hire(Name person, string title)
        {            
            Employee employee = new Employee(++currentId, person);

            ////Note: this declartion in MyOrganization needs to call constructor with  Position("CEO", Employee employee) employee parameter.
            ///Position in MyOrganization has always null Employee because it is not set in this class.

            return new Position(title, employee);
        }

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "  "));
            }
            return sb.ToString();
        }
    }
}
