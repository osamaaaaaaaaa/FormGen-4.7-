using FormGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormGenerator.Models
{
    public class FormGenComponent
    {
        public static string GenerateField(long fieldId)
        {
            FormGeneratorEntities db = new FormGeneratorEntities();


            FormField field = db.FormFields.Find(fieldId);
           

            string htmlTag = "";

            switch (field.FieldTypeId)
            {
                case (byte)FieldType.Integer:

                    htmlTag = "<h6>{5}</h6>" + 
                                "<input name=\"Name\" class=\"form-control item\" type='number' id='{0}'   value='{1}' max='{2}' min='{3}' placeholder='{4}' />"
                                + "<p>{6}</p>";
                    break;
                case (byte)FieldType.Decimal:
                    htmlTag = "<h6>{5}</h6>" + 
                            "<input asp-for=\"Name\" class=\"form-control item\" type='number' step=\".01 id='{0}'  value='{1}' max='{2}' min='{3}' placeholder='{4}'/>"
                            + "<p>{6}</p>" ;
                    break;
                case (byte)FieldType.Text:

                    htmlTag = "<h6>{5}</h6>" +
                              "<input name=\"Name\" class=\"form-control item\" type='text' id='{0}'  value='{1}' max='{2}' min='{3}' placeholder='{4}'/>"
                              + "<p>{6}</p>"; 
                    break;
                case (byte)FieldType.Date:
                    htmlTag = "<h6>{5}</h6>" + "<input asp-for=\"Name\" class=\"form-control item\" type='date' id='{0}'  value='{1}' max='{2}' min='{3}' placeholder='{4}'/>"+"<p>{6}</p>" ;
                    break;

                case (byte)FieldType.Selection:

                  var selectedListItem= db.SelectedListItems.Where(x=>x.ListId==field.SelectListId);
                    htmlTag = "<h6>{5}</h6>" + "<select class=\"form-select\" style=\"border-radius:20px ;margin: 15px 0px !important \" name=\"Name\" id='{0}' placeholder='{4}' >\n";

                    foreach (var item in selectedListItem)
                    {
                        htmlTag += "<option";
                        if (field.DefaultValue == item.Name && item.IsActive==true)
                            htmlTag += " Selected";
                        htmlTag += ">";
                        htmlTag += item.Name;
                        htmlTag += "</option>\n";
                    }
                    htmlTag += "</select>\n"+ "<p>{6}</p>";
                    break;
         default: return "";
            }

            return string.Format(htmlTag, field.Id, field.DefaultValue, field.MaxValue, field.MinValue,field.Caption,field.TextBefore,field.TextAfter);
        }

        public enum FieldType : byte
        {
            Integer = 1,
            Decimal = 2,
            Text = 3,
            Date = 4,
            Selection = 5
        }
    }
}