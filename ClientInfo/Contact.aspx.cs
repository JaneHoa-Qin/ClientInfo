using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientInfo.DataLayer;
using ClientInfo.DTO;
using ClientInfo.Model;

namespace ClientInfo
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadClientsRepeater();
                LoadClientsDropdown();
            }
        }

        private void LoadClientsRepeater()
        {
            using (var context = new ClientAddressContext())
            {
                var data = context.Clients
                    .Include(c => c.Addresses)
                    .Select(c => new ClientWithAddresses
                    {
                        ClientId = c.ClientId,
                        FullName = c.FullName,
                        Addresses = c.Addresses.Select(a => new AddressDto
                        {
                            AddressId = a.AddressId,
                            Street = a.Street,
                            Email = a.Email
                        }).ToList()
                    }).ToList();

                rptClients.DataSource = data;
                rptClients.DataBind();
            }
        }

        private void LoadClientsDropdown()
        {
            using (var context = new ClientAddressContext())
            {
                var clients = context.Clients.ToList(); // Fetch clients from DB
                ddlClients.DataSource = clients;
                ddlClients.DataTextField = "FullName"; // Display the client's name
                ddlClients.DataValueField = "ClientId"; // Use the ClientId as the value
                ddlClients.DataBind();

                // Add a default item for selecting a client
                ddlClients.Items.Insert(0, new ListItem("Select Existing Client", "0"));
            }
        }

        protected void rptClients_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var client = (ClientWithAddresses)e.Item.DataItem;
                var rptAddresses = (Repeater)e.Item.FindControl("rptAddresses");
                rptAddresses.DataSource = client.Addresses;
                rptAddresses.DataBind();
            }
        }

        protected void btnAddAddress_Click(object sender, EventArgs e)
        {
            int clientId = int.Parse(ddlClients.SelectedValue);
            string street = txtStreet.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (!string.IsNullOrEmpty(street))
            {
                using (var context = new ClientAddressContext())
                {
                    var address = new Address
                    {
                        ClientId = clientId,
                        Street = street,
                        Email = email
                    };

                    context.Addresses.Add(address);
                    context.SaveChanges();
                }

                // Clear fields
                txtStreet.Text = "";
                txtEmail.Text = "";

                LoadClientsRepeater(); // Refresh display
            }
        }

        protected void btnAddClientWithAddress_Click(object sender, EventArgs e)
        {
            string clientName = txtClientName.Text.Trim();
            string street = txtStreet.Text.Trim();
            string email = txtEmail.Text.Trim();
            int clientId = 0;

            if (string.IsNullOrEmpty(clientName) && ddlClients.SelectedValue == "0")
            {
                lblError.Text = "Please enter a client name or select an existing client.";
                lblError.Visible = true;
                return;
            }

            using (var context = new ClientAddressContext())
            {
                if (!string.IsNullOrEmpty(clientName)) // Adding a new client
                {
                    var newClient = new Client
                    {
                        FullName = clientName
                    };
                    context.Clients.Add(newClient);
                    context.SaveChanges();
                    clientId = newClient.ClientId; // Get the new client ID
                }
                else if (ddlClients.SelectedValue != "0") // Adding address to existing client
                {
                    clientId = int.Parse(ddlClients.SelectedValue); // Get the selected client ID
                }

                if (clientId > 0) // Now, add the address
                {
                    var address = new Address
                    {
                        Street = street,
                        Email = email,
                        ClientId = clientId // Link the address to the client
                    };

                    context.Addresses.Add(address);
                    context.SaveChanges();
                }
            }

            // Reset fields after successful insertion
            txtClientName.Text = "";
            txtStreet.Text = "";
            txtEmail.Text = "";
            ddlClients.SelectedIndex = 0;

            lblError.Visible = false; // Hide any previous error
            LoadClientsRepeater();
            LoadClientsDropdown();
        }


        protected void rptAddresses_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int addressId = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "Edit")
            {
                // Toggle visibility for editing
                var txtStreet = (TextBox)e.Item.FindControl("txtEditStreet");
                var txtEmail = (TextBox)e.Item.FindControl("txtEditEmail");
                var btnSave = (Button)e.Item.FindControl("btnSave");
                var btnEdit = (Button)e.Item.FindControl("btnEdit");
                var btnCancel = (Button)e.Item.FindControl("btnCancel");

                txtStreet.Visible = txtEmail.Visible = btnSave.Visible = true;
                btnEdit.Visible = false;
                btnCancel.Visible = true;
                // Hide labels
                ((Label)e.Item.FindControl("lblStreet")).Visible = false;
                ((Label)e.Item.FindControl("lblEmail")).Visible = false;
            }
            else if (e.CommandName == "Save")
            {
                var txtStreet = (TextBox)e.Item.FindControl("txtEditStreet");
                var txtEmail = (TextBox)e.Item.FindControl("txtEditEmail");

                using (var context = new ClientAddressContext())
                {
                    var address = context.Addresses.Find(addressId);
                    if (address != null)
                    {
                        address.Street = txtStreet.Text.Trim();
                        address.Email = txtEmail.Text.Trim();
                        context.SaveChanges();
                    }
                }

                LoadClientsRepeater();
            }
            else if (e.CommandName == "Cancel")
            {
                // Cancel edit mode: toggle UI back to view mode
                var txtStreet = (TextBox)e.Item.FindControl("txtEditStreet");
                var txtEmail = (TextBox)e.Item.FindControl("txtEditEmail");
                var lblStreet = (Label)e.Item.FindControl("lblStreet");
                var lblEmail = (Label)e.Item.FindControl("lblEmail");
                var btnSave = (Button)e.Item.FindControl("btnSave");
                var btnCancel = (Button)e.Item.FindControl("btnCancel");
                var btnEdit = (Button)e.Item.FindControl("btnEdit");

                // Restore visibility
                txtStreet.Visible = false;
                txtEmail.Visible = false;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnEdit.Visible = true;
                lblStreet.Visible = true;
                lblEmail.Visible = true;

            }
            else if (e.CommandName == "Delete")
            {
                using (var context = new ClientAddressContext())
                {
                    var address = context.Addresses.Find(addressId);
                    if (address != null)
                    {
                        context.Addresses.Remove(address);
                        context.SaveChanges();
                    }
                }

                LoadClientsRepeater();
            }
        }

    }
}