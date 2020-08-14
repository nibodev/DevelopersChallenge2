using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Data;
using System.Xml.Linq;
using BankData.Business;
using System.IO;
using System.Runtime.InteropServices;

namespace BankData
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            panelAlert.Visible = false;

            //load bank data
            RenderGrid(string.Empty);
        }

        protected void ShowAlert(string text, bool isError = false)
        {
            literalAlert.Text = text;
            panelAlert.CssClass = (isError) ? "alert alert-danger" : "alert alert-success";
            panelAlert.Visible = true;
        }
        protected void RenderGrid(string strSorting)
        {
            Transaction trans = new Transaction();
            gv.DataSource = trans.List(strSorting);
            gv.DataBind();
            trans = null;
        }
        protected void RenderGrid(string startDate, string endDate, string strSorting)
        {
            Transaction trans = new Transaction();
            gv.DataSource = trans.List(startDate, endDate, strSorting);
            gv.DataBind();
            trans = null;
        }
        protected string DisplayAmount(object amount)
        {
            string cssClass = string.Empty;
            if (amount.ToString().IndexOf("-") != -1)
            {
                // displays negative amounts in red
                cssClass = "class='negative'";
            }
            return cssClass;
        }
        protected bool isValidDates()
        {
            string dateProblems = string.Empty;
            DateTime startDate;
            DateTime endDate;
            bool isDatesOK = true;

            try
            {
                // Date validations
                startDate = Convert.ToDateTime(txtStartDate.Text);
                endDate = Convert.ToDateTime(txtEndDate.Text);

                if (startDate > endDate)
                {
                    dateProblems = "Start Date must be before End Date.";
                }
            }
            catch
            {
                dateProblems = "Invalid date format (DD/MM/YYYY).";
            }

            if (!string.IsNullOrEmpty(dateProblems))
            {
                isDatesOK = false;
                ShowAlert(dateProblems, true);
            }
            return isDatesOK;
        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            panelAlert.Visible = false;
            if (FileUploadBank.HasFile && FileUploadBank.PostedFile.FileName.ToUpper().IndexOf("OFX") != -1)
            {
                string tempFile = System.AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\" + Path.GetFileName(FileUploadBank.PostedFile.FileName);
                FileUploadBank.SaveAs(tempFile);
                bool booIsOK = Import.ImportFile(tempFile);
                File.Delete(tempFile);

                if (booIsOK)
                {
                    Transaction trans = new Transaction();
                    DataTable dt = new DataTable();
                    dt = trans.List(string.Empty);
                    trans = null;
                    gv.DataSource = dt;
                    gv.DataBind();
                    ShowAlert("Upload Completed!");
                }
                else
                {
                    ShowAlert("Sorry, it was not possible to import your file. Please check your file and try again...", true);
                }
            }
            else
            {
                ShowAlert("File not found or invalid format (not .OFX file).", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
           if (string.IsNullOrEmpty(txtStartDate.Text) && string.IsNullOrEmpty(txtEndDate.Text))
            {
                RenderGrid(string.Empty);
            }
            else if (isValidDates())
            {
                RenderGrid(txtStartDate.Text, txtEndDate.Text, string.Empty);
            }
        }
        protected void gv_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
        {
            if ((hdSortText.Value.IndexOf(" ASC") == -1) && (hdSortText.Value.IndexOf(" DESC") == -1))
            {
                //first column sorting, no ASC or DESC
                hdSortText.Value = e.SortExpression + " ASC";
            }
            else if (hdSortText.Value == e.SortExpression + " ASC")
            {
                hdSortText.Value = e.SortExpression + " DESC";
            }
            else
            {
                hdSortText.Value = e.SortExpression + " ASC";
            }

            if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                if (isValidDates())
                {
                    RenderGrid(txtStartDate.Text, txtEndDate.Text, hdSortText.Value);
                }
            }
            else
            {
                RenderGrid(hdSortText.Value);
            }
        }

    }
}