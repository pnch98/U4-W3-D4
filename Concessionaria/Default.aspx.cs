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


        double prezzoBaseVar, absVar, cerchiLegaVar, fariVar, climatizzatoreVar;

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

                        prezzoBaseVar = Convert.ToDouble(reader["prezzoBase"]);
                        absVar = Convert.ToDouble(reader["ABS"]);
                        cerchiLegaVar = Convert.ToDouble(reader["cerchiInLega"]);
                        fariVar = Convert.ToDouble(reader["fariLED"]);
                        climatizzatoreVar = Convert.ToDouble(reader["climatizzatore"]);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally { conn.Close(); }
            }
        }

        protected void CalcolaPrezzo(object sender, EventArgs e)
        {
            int anniGaranzia = Convert.ToInt16(garanzia.SelectedValue);
            bool checkABS = ABS.Checked;
            bool checkCerchi = cerchi.Checked;
            bool checkfariLED = fariLED.Checked;
            bool checkClimatizzatore = climatizzatore.Checked;

            double prezzototale = prezzoBaseVar + (anniGaranzia * 120);
            if (checkABS)
            {
                prezzototale += absVar;
            }

            if (checkCerchi)
            {
                prezzototale += cerchiLegaVar;
            }

            if (checkfariLED)
            {
                prezzototale += fariVar;
            }

            if (checkClimatizzatore)
            {
                prezzototale += climatizzatoreVar;
            }
            Response.Write(prezzoBaseVar);
            prezzoTotaleFinaleUAU.InnerText = prezzototale.ToString();
        }
    }
}