using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DiagnosticSystemWeb.BLL;
using DiagnosticSystemWeb.Models;

namespace DiagnosticSystemWeb.view
{
    public partial class TestSetupUI : System.Web.UI.Page
    {
        TestSetupManager testSetupManager = new TestSetupManager();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadAllTestType();
            }

            LoadAllTestSetup();

        }
        private void LoadAllTestSetup()
        {
            List<TestSetupViewModel> allTestSetupList = testSetupManager.GetAllTestSetup();
            testSetupGridView.DataSource = allTestSetupList;
            testSetupGridView.DataBind();
        }
        public void LoadAllTestType()
        {
            List<TestType> allTestTypes = testSetupManager.GetAllTestType();
            TestTypeDropDownList.DataSource = allTestTypes;
            TestTypeDropDownList.DataTextField = "TestTypeName";
            TestTypeDropDownList.DataValueField = "Id";
            TestTypeDropDownList.DataBind();

            TestTypeDropDownList.Items.Insert(0, "---SELECT---");
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            TestSetup testSetup = new TestSetup();

            if (testNameTextBox.Text == string.Empty
                || feeTextBox.Text == string.Empty
                || TestTypeDropDownList.SelectedIndex == 0
            )
            {
                DisplayInfoMessage("Empty! Fields are Required.", Color.DarkRed);
                return;
            }

            testSetup.TestName = testNameTextBox.Text;
            try
            {
                testSetup.Fee = Convert.ToDecimal(feeTextBox.Text);
            }
            catch (Exception)
            {

                DisplayInfoMessage("Please Enter A Valid Amount", Color.DarkRed);
                CleadrDisplay();
                return;
            }

            testSetup.TestTypeId = Convert.ToInt32(TestTypeDropDownList.SelectedValue);

            string message = testSetupManager.SaveTestSetup(testSetup);

            if (message == "success")
            {
                DisplayInfoMessage("Successlly! Test Setup Created.", Color.Blue);

            }
            else if (message == "failed")
            {
                DisplayInfoMessage("Failed! Test Setup Problem", Color.DarkRed);

            }
            else if (message == "exists")
            {
                DisplayInfoMessage("Error! Test Name Already Exists.", Color.DarkRed);

            }
            CleadrDisplay();
            LoadAllTestSetup();
        }
        private void DisplayInfoMessage(string text, Color color)
        {

            InfoMessageLabel.Text = text;
            InfoMessageLabel.Visible = true;
            InfoMessageLabel.BackColor = color;
        }

        protected void testSetupGridView_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            testSetupManager.GetAllTestSetup();
            testSetupGridView.PageIndex = e.NewPageIndex;
            testSetupGridView.DataBind();

        }

        private void CleadrDisplay()
        {
            testNameTextBox.Text = string.Empty;
            feeTextBox.Text = string.Empty;
            TestTypeDropDownList.SelectedIndex = 0;

        }
    }
}