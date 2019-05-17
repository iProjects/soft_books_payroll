using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Criteria
{
    public class CriteriaBuilder
    {
        List<CriterionItem> criteriaList = new List<CriterionItem>();
        public void AddCriterionItem(CriterionItem cr)
        {
            criteriaList.Add(cr);
        }
        public void Remove(CriterionItem cr)
        {
            criteriaList.Remove(cr);
        }
        public List<CriterionItem> CriterionItemList()
        {
            return criteriaList;
        }
        public bool IsFirstItem()
        {
            return criteriaList.Count() == 0;
        }
        public string CriteriaString()
        {
            return string.Join(" ", criteriaList.Select(c => c.ToString()));
        }

    }

    public class CriterionItem
    {
        public string Id { get; set; }
        public Criterion Criterion { get; set; }

        public CriterionItem(string id, Criterion ct)
        {
            this.Id = id;
            Criterion = ct;
        }

        public override string ToString ()
        {

                return Criterion.ToString();
            
        }
    }

    public class Criterion
    {
        private conjuction Conjuction;
        private string Field;
        private Op Op;
        private string Value;
        
       public Criterion()
       { }

       public Criterion(conjuction cj, string fd, Op op, string val)
       {
           Conjuction = cj;
           Field = fd;
           Op = op;
           Value = val;
       }
       public string FieldName
       {
           get { return Field; }
       }
       public Op Opr
       {
           get { return Op; }
       }
       public string FValue {
           get { return Value; }
       }

       public override string ToString()
       {
           if (Conjuction == conjuction.nil )
               return string.Format("{0} {1} {2}", Field, Op.Symbol.ToString(), Value);
           else
               return string.Format("{0} {1} {2} {3}",Conjuction.ToString(), Field, Op.Symbol.ToString(), Value);
       }

       public List<Criterion> Parse(string Str)
       {
           List<Criterion> cr = new List<Criterion>();
           string[] sep = new string[] { "AND","OR"};
           string[] strList = Str.ToUpper().Split(sep,StringSplitOptions.None);

           return cr;
 
       }
    }

   public class Op
    {
        private string name;
        private string description;
        private string symbol;

        public Op(string Name, string Des, string sym)
        {
            name = Name;
            description = Des;
            symbol = sym;
        }

        public string Name {
            get { return name; }
        }

        public string Description
        {
            get { return description; }
        }
       
        public string Symbol {
            get {
                return symbol;
            }
        }

        static public List<Op> GetList()
        {
            List<Op> oplist = new List<Op>();
            oplist.Add(new Op("equal","Equals", "="));
            oplist.Add(new Op("greatorthan","Greator Than", ">"));
            oplist.Add(new Op("lessthan", "Less Than","<"));
            oplist.Add(new Op("notequal", "Not Equal to","<>"));
            oplist.Add(new Op("greatorthanorequal", "Greator Than or Equal to",">="));
            oplist.Add(new Op("lessthanorequal", "Less Than or Equal to", "<="));
            oplist.Add(new Op("startswith","Starts With", "LIKE"));
            oplist.Add(new Op("endswith", "Ends with","LIKE"));
            oplist.Add(new Op("has", "Has","LIKE"));
            //oplist.Add(new Op("Contains", "Contains", "Contains")); Full Text Search
            return oplist;
        }

    }


   public class Field
   {
       private string name;
       private string type;

       public Field(string Name, string Type)
       {
           name = Name;
           type = Type;
       }

       public string Name
       {
           get { return name; }
       }

       public string Type
       {
           get
           {
               return type;
           }
       }

   }

   public enum conjuction
    {
        and,
        or,
        nil
    }

    public  class Conjunction
    {
        public static conjuction Parse(string str)
        {
            if (str.ToUpper() == "AND")
            {
                return conjuction.and;
            }
            else if(str.ToUpper() == "OR")
            {
                return conjuction.or;
            }else
            {
                return conjuction.nil;
            }
        }
    }
}

