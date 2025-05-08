<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ClientInfo.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h5>Client List</h5>
    <asp:Repeater ID="rptClients" runat="server" OnItemDataBound="rptClients_ItemDataBound">
    <ItemTemplate>
        <div class="card mb-4">
            <div class="card-header">
                <strong><%# Eval("FullName") %></strong>
            </div>
            <div class="card-body">
                <asp:Repeater ID="rptAddresses" runat="server" OnItemCommand="rptAddresses_ItemCommand">
                    <ItemTemplate>
                        <div class="address-item mb-3 p-3 border rounded">
                            <asp:Label ID="lblStreet" runat="server" Text='<%# Eval("Street") %>' class="d-block mb-2" />
                            <asp:TextBox ID="txtEditStreet" runat="server" Text='<%# Eval("Street") %>' Visible="false" class="form-control mb-2" />
                            
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>' class="d-block mb-2" />
                            <asp:TextBox ID="txtEditEmail" runat="server" Text='<%# Eval("Email") %>' Visible="false" class="form-control mb-2" />

                            <div class="btn-group">
                                <asp:Button ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("AddressId") %>' Text="Edit" CssClass="btn btn-info btn-sm" />
                                <asp:Button ID="btnSave" runat="server" CommandName="Save" CommandArgument='<%# Eval("AddressId") %>' Text="Save" Visible="false" CssClass="btn btn-success btn-sm" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" CommandArgument='<%# Eval("AddressId") %>' Text="Cancel" Visible="false" CssClass="btn btn-secondary btn-sm" />
                                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("AddressId") %>' Text="Delete" CssClass="btn btn-danger btn-sm" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>

   <div class="add-address-card card mb-4">
    <div class="card-header text-center">
        <strong>Add New Client with Address</strong>
    </div>
    <div class="card-body">
        <!-- New Client Fields -->
        <asp:TextBox ID="txtClientName" runat="server" CssClass="form-control mb-3" Placeholder="Enter Client Full Name" />
        
        <!-- Dropdown to select existing client, for adding address -->
        <asp:DropDownList ID="ddlClients" runat="server" CssClass="form-control mb-3">
            <asp:ListItem Text="Select Existing Client" Value="0" />
           
        </asp:DropDownList>

        <!-- Address Fields -->
        <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control mb-3" Placeholder="Enter Street" />
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mb-3" Placeholder="Enter Email" />

        <!-- Button to add client with address -->
        <div class="text-center">
            <asp:Button ID="btnAddClientWithAddress" runat="server" Text="Add" CssClass="btn btn-success btn-sm" OnClick="btnAddClientWithAddress_Click" />
        </div>

        <!-- Error Message -->
       <asp:Label ID="lblError" runat="server" CssClass="lbl-error" Visible="false"></asp:Label>
    </div>
</div>

</asp:Content>
