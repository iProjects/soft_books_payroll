using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class EditEmpNonCashBenefit
    {
        public DAL.EmpNonCashBenefit Benefit{get;set;}
        public Action Action{get;set;}

        public EditEmpNonCashBenefit() { }
        public EditEmpNonCashBenefit(EmpNonCashBenefit benefit, Action action)
        {
            Benefit = benefit;
            Action = action;
        }
    }

    public enum Action
    {
        Add,
        Edit,
        Delete,
        None
    }
}
