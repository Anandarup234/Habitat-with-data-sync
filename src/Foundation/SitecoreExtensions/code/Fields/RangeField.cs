using System;

using Sitecore.Web;

using Sitecore.Web.UI.Sheer;

using System.Web.UI.HtmlControls;

using Sitecore.Web.UI.HtmlControls;

using Sitecore;

using System.Text.RegularExpressions;

namespace Sitecore.Foundation.SitecoreExtensions.Fields
{
    public class RangeField : Edit
    {
        public RangeField()
        {

            this.Class = "scContentControl";

        }



        public override void HandleMessage(Message message)

        {

            base.HandleMessage(message);



            switch (message.Name)

            {

                case "rangefield:validate":

                    this.EmailValidate();

                    break;

            }

        }



        protected void EmailValidate()

        {

            string currentvalue = WebUtil.GetFormValue(ID);

            if(string.IsNullOrWhiteSpace(currentvalue))
            {
                SheerResponse.SetInnerHtml("validator_" + ID, "");
                return;
            }

            if (Regex.IsMatch(currentvalue, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))

                SheerResponse.SetInnerHtml("validator_" + ID, "Valid email entered!");

            else

                SheerResponse.SetInnerHtml("validator_" + ID, "Invalid");

        }



        protected override void OnPreRender(EventArgs e)

        {

            base.OnPreRender(e);

            this.ServerProperties["Value"] = this.ServerProperties["Value"];

        }



        protected override void Render(System.Web.UI.HtmlTextWriter output)

        {

            base.Render(output);

            RenderValidatorOutput(output);

        }



        protected void RenderValidatorOutput(System.Web.UI.HtmlTextWriter output)

        {

            HtmlGenericControl validatortext = new HtmlGenericControl("div");



            validatortext.Attributes.Add("ID", "validator_" + ID);

            validatortext.Attributes.Add("style", "color:grey");

            validatortext.InnerHtml = "";



            validatortext.RenderControl(output);

        }



        protected override bool LoadPostData(string value)

        {

            value = StringUtil.GetString(new string[1]

            {

                value

            });



            if (!(this.Value != value))

                return false;



            this.Value = value;

            base.SetModified();



            return true;

        }

    }
}
