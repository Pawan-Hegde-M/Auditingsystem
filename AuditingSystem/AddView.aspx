<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddView.aspx.cs" Inherits="AuditingSystem.AddView" %>
<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 226px;
        }
        .auto-style2 {
            width: 13%;
        }
        .auto-style3 {
            width: 32%;
        }
        .auto-style4 {
            width: 19%;
        }
    </style>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
    window.onunload = function () { null };
    window.onload = function () { preventBack() };
    </script>
    
</head>
<%-------------------------------------------------------------------------------------------------------------------------------------------------------%>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div>
        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" style="width:85%; margin-left:auto; margin-right:auto; border-radius:10px">
                             
        <asp:TabPanel ID="TabPanel0" runat="server" HeaderText="ADD DATA"><ContentTemplate><br />
        <asp:Panel ID="Paneladd" runat="server">

            <table style="width: 95%; margin-left: auto; margin-right: auto">
                <tr>
                    <td colspan="4">
                        <div style="width: 100%; background-color: #6d84b4; padding: 10px; text-align: center">ADD DATA</div>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2"><label>Date:</label></td><td><asp:TextBox ID="ddadddt" runat="server" style="width:248px; border:1px solid #262626"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="ddadddt" BehaviorID="ddadddt" Enabled="True"></asp:CalendarExtender></td>
                    <td class="auto-style4"><label>Remarks:</label></td><td class="auto-style3"><asp:TextBox ID="ddaddrm" runat="server" style="width:250px; border:1px solid #262626"></asp:TextBox></td>
                </tr>
             
                <tr>
                     <td class="auto-style2"><label>Company_id:</label></td><td><asp:DropDownList ID="ddaddcid" OnSelectedIndexChanged="ddaddcid_SelectedIndexChanged" AutoPostBack="true" runat="server" style="width:250px; height:24px; border:1px solid #262626"></asp:DropDownList></td>
                    <td class="auto-style4"><label>Recommendations:</label></td><td class="auto-style3"><asp:TextBox ID="ddaddrec" runat="server" TextMode="MultiLine" style="width:248px; border:1px solid #262626"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="auto-style2"><label>Location:</label></td><td><asp:DropDownList ID="ddaddloc" OnSelectedIndexChanged="ddaddloc_SelectedIndexChanged" AutoPostBack="true" runat="server" style="width:250px; height:24px; border:1px solid #262626 "></asp:DropDownList></td>
                    <td class="auto-style4"><label>Observations:</label></td><td class="auto-style3"> <asp:TextBox ID="ddaddob" runat="server" TextMode="MultiLine" style="width:247px; border:1px solid #262626"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="auto-style2"><label>Unit:</label></td><td><asp:DropDownList ID="ddaddun" OnSelectedIndexChanged="ddaddun_SelectedIndexChanged" AutoPostBack="true" runat="server" style="width:250px; height:24px; border:1px solid #262626"></asp:DropDownList></td>
                    <td class="auto-style4"><label>Closure status:</label></td><td class="auto-style3"><asp:DropDownList ID="ddaddcs" runat="server" style="width:250px; border:1px solid #262626"><asp:ListItem Text="SELECT"></asp:ListItem><asp:ListItem Text="Open" ></asp:ListItem><asp:ListItem Text="Inprogress" ></asp:ListItem><asp:ListItem Text="Closed" ></asp:ListItem></asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="auto-style2"><label>Type:</label></td><td> <asp:DropDownList ID="ddaddty" OnSelectedIndexChanged="ddaddty_SelectedIndexChanged1" AutoPostBack="true" runat="server" style="width:250px; height:24px; border:1px solid #262626"></asp:DropDownList></td>
                    <td class="auto-style4"><label>Closure date:</label></td><td class="auto-style3"> <asp:TextBox ID="ddaddcd" runat="server" style="width:250px; border:1px solid #262626"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="ddaddcd">
                            </asp:CalendarExtender></td>
                </tr>
                <tr>
                    <td class="auto-style2"><label>Severity:</label></td><td><asp:DropDownList ID="ddaddsv" runat="server" style="width:250px; border:1px solid #262626"></asp:DropDownList></td>
                  
                    <tr>
                        <td colspan="4">
                            <div style="width: 100%; background-color: #6d84b4; padding: 10px; text-align: center">
                                <asp:Button ID="btnsac" runat="server" OnClick="btnsac_Click" Text="Submit and Continue" />
                                <asp:Button ID="btnsar" runat="server" OnClick="btnsar_Click" Text="Submit and Reset" />
                                <asp:Button ID="BtnCancel" runat="server" OnClick="BtnCancel_Click" Text="Cancel Update" Visible="False" />
                                <asp:Button ID="btnclear" runat="server" OnClick="btnclear_Click" Text="Clear" />
                            </div>
                        </td>
                    </tr>
                </tr>
                
                </table>
                <br />

            <asp:Panel ID="PanelGrid" runat="server" style="width:90%; text-align:center; margin-left:auto; margin-right:auto">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" style="font-size:12px; width:95%; margin-left:auto; margin-right:auto">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" />
                        <asp:BoundField DataField="remarks" HeaderText="Remarks" />
                        <asp:BoundField DataField="recommendation" HeaderText="Recommendations"/>
                        <asp:BoundField DataField="observation" HeaderText="Observations"/>
                        <asp:BoundField DataField="closure_status" HeaderText="Closurestatus"/>
                        <asp:BoundField DataField="closure_date" HeaderText="Closuredate" DataFormatString="{0:M/dd/yyyy}"/>
                        <asp:TemplateField HeaderText="Update"><ItemTemplate><asp:ImageButton ID="BtnUpdate" runat="server" ImageUrl="~/images/update.png" CausesValidation="false" OnClick="BtnUpdate_Click" style="width:27px"/></ItemTemplate></asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete"><ItemTemplate><asp:ImageButton ID="BtnDelete" runat="server" ImageUrl="~/images/discard.png" CausesValidation="false" OnClick="BtnDelete_Click" style="width:30px"/></ItemTemplate></asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
            </asp:Panel>
      
                            
            </asp:Panel>             
        </ContentTemplate></asp:TabPanel>
<%------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ --%>
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="VIEW/PULL REPORT"><ContentTemplate>
        <asp:Panel ID="Panelupdate" runat="server">
                <asp:Panel ID="PanelFilter" runat="server">
                      <table style="width: 70%; margin-left: auto; margin-right: auto">
                    <tr>
                        <td colspan="4">
                           <div style="width: 98%; background-color: #6d84b4; padding: 10px; text-align: center">FILTER</div>
                        </td>
                    </tr>
                    
                  <tr>
                         <td><label>Company_id:</label></td><td><asp:DropDownList ID="ddunit_search" OnSelectedIndexChanged="ddunit_search_SelectedIndexChanged" AutoPostBack="true" runat="server" style="width:180px; height:24px; border:1px solid #262626 "></asp:DropDownList></td>
                      <td>Hide Columns</td><td><asp:DropDownCheckBoxes ID="ddChkBoxPending" runat="server" Width="180px" CssClass="dropstylehead" AddJQueryReference="True" RepeatDirection="Horizontal" UseButtons="False" UseSelectAllNode="True">
                                            <Style SelectBoxWidth="180px" DropDownBoxBoxWidth="180px" DropDownBoxBoxHeight="" SelectBoxCssClass="dropstylehead" />
                                            <Style2 DropDownBoxBoxHeight="" DropDownBoxBoxWidth="180px" SelectBoxCssClass="dropstylehead" SelectBoxWidth="180px" />
                                            <Texts SelectAllNode="Select all" />
                                            <Items>
                                                  <asp:ListItem Text="Sl.No" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Date" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Location" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Unit" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Type" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="Severity" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="Remarks" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="Recommendation" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="Observation" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="Closurestatus" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="Closuredate" Value="10"></asp:ListItem>
                                            </Items>
                                        </asp:DropDownCheckBoxes></td>
                  </tr>                
                  <tr>         
                       <td><label>Location:</label></td><td><asp:DropDownCheckBoxes ID="ddloc_search" runat="server" Width="180px" CssClass="dropstylehead" AddJQueryReference="True" RepeatDirection="Horizontal" UseButtons="False" UseSelectAllNode="True">
                                            <Style SelectBoxWidth="180px" DropDownBoxBoxWidth="180px" DropDownBoxBoxHeight="" SelectBoxCssClass="dropstylehead" />
                                            <Style2 DropDownBoxBoxHeight="" DropDownBoxBoxWidth="180px" SelectBoxCssClass="dropstylehead" SelectBoxWidth="180px" /><Texts SelectAllNode="Select all" /></asp:DropDownCheckBoxes></td>
                  </tr>
                  <tr>
                      <td>Closure Date From: </td><td><asp:TextBox ID="txtfrom" runat="server" CssClass="textboxstyle" style="width:180px; cursor:pointer; border:1px solid #262626" placeholder="Select Date"></asp:TextBox><asp:CalendarExtender runat="server" Enabled="True" TargetControlID="txtfrom" ID="CalendarExtender_txtfrom"></asp:CalendarExtender></td>
                      <td>Closure Date To: </td><td><asp:TextBox ID="txtto" runat="server" CssClass="textboxstyle" style="width:180px; cursor:pointer; border:1px solid #262626" placeholder="Select Date"></asp:TextBox><asp:CalendarExtender runat="server" Enabled="True" TargetControlID="txtto" ID="CalendarExtender_txtto"></asp:CalendarExtender></td> 
                  </tr>
                    <tr>
                    <td colspan="4">
                            <div style="width: 98%; background-color: #6d84b4; padding: 10px; text-align: center">
                             <asp:Button ID="BtnSearch" runat="server" OnClick="BtnSearch_Click" Text="Search" />
                              <asp:Button ID="BtnClearSearch" runat="server" OnClick="BtnClearSearch_Click" Text="Clear" />
                                <asp:Button ID="Btncsvexprt" runat="server" OnClick="Btncsvexprt_Click" Text="CSV" />
                            </div>
                        </td>
                   </tr>
                  
                  </table>
                </asp:Panel>
<%------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
               <asp:Panel ID="PanelView" runat="server" style="width:96%; margin-left:auto; margin-right:auto; overflow-y:scroll; height:300px">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" style="margin-left:auto; margin-top:auto; width:95%; font-size:10px">
                     <AlternatingRowStyle BackColor="White" />
                     <Columns>
                         <asp:TemplateField HeaderText="Sl No">
    <ItemTemplate>
        <%# Container.DataItemIndex + 1 %>
    </ItemTemplate>
    <ItemStyle Width="2%" />
</asp:TemplateField>
                         <asp:BoundField DataField="date" HeaderText="Date" SortExpression="date"  DataFormatString="{0:dd/MM/yyyy}"/>
                         <asp:BoundField DataField="location" HeaderText="Location" SortExpression="location" />
                        <asp:BoundField DataField="unit" HeaderText="unit" SortExpression="unit" />
                         <asp:BoundField DataField="type" HeaderText="Type" SortExpression="type" />
                         <asp:BoundField DataField="severity" HeaderText="Severity" SortExpression="severity" />
                         <asp:BoundField DataField="remarks" HeaderText="Remarks" SortExpression="remarks" />
                         <asp:BoundField DataField="recommendation" HeaderText="Recommendation" SortExpression="recommendation" />
                          <asp:BoundField DataField="observation" HeaderText="Observation" SortExpression="observation" />
                         <asp:BoundField DataField="closure_status" HeaderText="Closurestatus" SortExpression="closurestatus" />
                         <asp:BoundField DataField="closure_date" HeaderText="Closuredate" SortExpression="closuredate"  DataFormatString="{0:dd/MM/yyyy}"/>
                     </Columns>
                     <EditRowStyle BackColor="#7C6F57" />
                     <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                     <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                     <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                     <RowStyle BackColor="#E3EAEB" />
                     <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                     <SortedAscendingCellStyle BackColor="#F8FAFA" />
                     <SortedAscendingHeaderStyle BackColor="#246B61" />
                     <SortedDescendingCellStyle BackColor="#D4DFE1" />
                     <SortedDescendingHeaderStyle BackColor="#15524A" />
                 </asp:GridView>
               </asp:Panel>
            </asp:Panel>
        </ContentTemplate></asp:TabPanel>
        </asp:TabContainer>
        </div>

   <div style="width:100%; background-color: #f0f0f0; padding:8px; text-align:center;" >
            <asp:Button id="BtnBack" runat="server" onclick="BtnBack_Click" Text="Back"></asp:Button>
            </div>
              
    </form>
    </body>
    
</html>
