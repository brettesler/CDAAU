using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamUnicorn.CDAAU.Core
{
    public class CDAIGGenerator
    {
        static public string Generate(Type model)
        {
            string output = "<html><body>";

            output += ("<table style='padding-left:10px;border-top:solid black 1;width:100%' cellpadding=1 cellspacing=0 >");
            Annotations(ref output, model, " [ClinicalDocument]");
            output += "</table>";

            output += "</body></html>";
            return output;
        }

        static protected void Annotations(ref string output, Type model, string rimattrtext)
        {
            string name = model.Name;

            object o = Activator.CreateInstance(model);

            string classcode = o.GetAttributeValue<RIMActAttribute, string>(a => a.ClassCode);
            string classcode2 = o.GetAttributeValue<RIMRoleAttribute, string>(a => a.ClassCode);
            string moodcode = o.GetAttributeValue<RIMActAttribute, string>(a => a.MoodCode);
            string typecode = o.GetAttributeValue<RIMRelationshipAttribute, string>(a => a.TypeCode);
            List<string> items = new List<string>();
            if (!string.IsNullOrEmpty(classcode)) items.Add(classcode);
            if (!string.IsNullOrEmpty(classcode2)) items.Add(classcode2);
            if (!string.IsNullOrEmpty(moodcode)) items.Add(moodcode);
            if (!string.IsNullOrEmpty(typecode)) items.Add(typecode);

            string rimclasspath = "";
            if (!string.IsNullOrEmpty(classcode) || !string.IsNullOrEmpty(classcode2) || !string.IsNullOrEmpty(moodcode) || !string.IsNullOrEmpty(typecode))
                rimclasspath = " (" + string.Join(" ", items.ToArray()) + ")";
            output += ( rimattrtext + "<b>" + name + "</b>" + rimclasspath);
            output += ("<table style='padding-left:10px;border-top:solid black 1;width:100%' cellpadding=1 cellspacing=0 >");

            foreach (System.Reflection.PropertyInfo pinfo in model.GetProperties())
            {
                Type ptype = pinfo.PropertyType;
                if (ptype.HasElementType)
                    ptype = ptype.GetElementType();

                if (ptype.IsGenericType)
                    ptype = ptype.GetGenericArguments()[0];

                string pname = pinfo.Name;

                string actrelelementname = pinfo.GetAttributeValue<RIMRelationshipAttribute, string>(a => a.ElementName);
                string actreltargetelementname = pinfo.GetAttributeValue<RIMRelationshipAttribute, string>(a => a.TargetElementName);
                string actrel = (actrelelementname + "." + actreltargetelementname);
                if (actrel == ".") actrel = "";
                if (actrel.StartsWith(".")) actrel = actrel.Substring(1);

                string entityelement = pinfo.GetAttributeValue<RIMEntityAttribute, string>(a => a.ElementName);
                if (entityelement != null)
                    entityelement += ".";
                else
                    entityelement = "";

                RIMAttributeType rimattr = pinfo.GetAttributeValue<RIMAttributeAttribute, RIMAttributeType>(a => a.Name);
                string rimattrname = rimattr.ToString();
                if (rimattrname == "HPII")
                    rimattrname = "Id";
                if (rimattrname == "HPIO")
                    rimattrname = "Id";
                if (rimattrname == "IHI")
                    rimattrname = "Id";
                if (rimattrname == "MRN")
                    rimattrname = "Id";
                rimattrname = rimattrname.ToFirstLower();


                string rimpath = "";
                if (!string.IsNullOrEmpty(entityelement) || (!string.IsNullOrEmpty(rimattrname) && rimattrname != "ignore") || !string.IsNullOrEmpty(actrel))
                    rimpath = " [" + actrel + (entityelement + rimattrname).Replace(".ignore","").Replace("ignore","") + "] ";

                if (!ptype.ToString().Contains("System") && !ptype.ToString().Contains("TeamUnicorn.CDAAU.Core"))
                {
                    output += "<tr><td>" + pname + " : ";
                    Annotations(ref output, ptype, rimpath);
                    output += "</td></tr>";
                }
                else
                    output += ("<tr><td>" + pname + " : " + rimpath + ptype.Name + "</td></tr>");
            }

            output += ("</table>");
        }


    }
}
