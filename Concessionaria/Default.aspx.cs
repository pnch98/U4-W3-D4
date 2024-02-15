using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Concessionaria
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Concessionaria"].ToString();
                SqlConnection conn = new SqlConnection(connectionString);

                try
                {
                    conn.Open();

                    string query = "SELECT idAuto, modello FROM Auto";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = reader["modello"].ToString();
                        listItem.Value = reader["idAuto"].ToString();

                        ddlExample.Items.Add(listItem);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally { conn.Close(); }
            }
        }


        protected void SelectedCar(object sender, EventArgs e)
        {
            divDettaglio.Attributes.Remove("class");
            string selectedCar = ddlExample.SelectedValue;

            if (selectedCar != "null")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Concessionaria"].ToString();
                SqlConnection conn = new SqlConnection(connectionString);

                try
                {
                    conn.Open();

                    string query = $"SELECT * FROM Auto WHERE idAuto = {selectedCar}";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        selectedImg.Src = reader["immagine"].ToString();
                        selectedModel.InnerText = reader["modello"].ToString();
                        selectedBasePrice.InnerText = "Prezzo base: " + reader["prezzoBase"].ToString();

                        double prezzototale = Convert.ToDouble(reader["prezzoBase"]) + (Convert.ToInt16(garanzia.SelectedValue) * 120);

                        string finalString = "Garanzia: " + Convert.ToInt16(garanzia.SelectedValue) * 120 + " | ";

                        if (ABS.Checked)
                        {
                            prezzototale += Convert.ToDouble(reader["ABS"]);
                            finalString += "ABS: " + reader["ABS"] + " | ";
                        }
                        if (cerchi.Checked)
                        {
                            prezzototale += Convert.ToDouble(reader["cerchiInLega"]);
                            finalString += "Cerchi: " + reader["cerchiInLega"] + " | ";
                        }
                        if (fariLED.Checked)
                        {
                            prezzototale += Convert.ToDouble(reader["fariLED"]);
                            finalString += "Fari LED: " + reader["fariLED"] + " | ";
                        }
                        if (climatizzatore.Checked)
                        {
                            prezzototale += Convert.ToDouble(reader["climatizzatore"]);
                            finalString += "Climatizzatore: " + reader["climatizzatore"] + " | ";
                        }
                        optionals.InnerHtml = finalString;
                        prezzoTotaleFinaleUAU.InnerText = "Prezzo totale: " + prezzototale.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally { conn.Close(); }
            }
        }
    }
}