<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="AuditingSystem.Dashboard" %>

<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Admin Page</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
    window.onunload = function () { null };
  //  window.onload = function () { null};
    </script>
    
<%-------------------------------------------------------------------------------------------------------------------------------------------- --%>
<style type="text/css">
   .Panelst{

                height: 150px;
                border:5px solid #191970;
                top: 0px;
                left: 0px;
                background-color: #fefefe;
                box-shadow:3px 3px 3px 3px lightgrey;
                text-align:center;
                vertical-align:middle;
                width: 350px;
                border-radius: 10px;
                overflow:hidden;
            
   }

            
.top{
	width:100%;
    
	background-color:#f0f0f0;
	border-bottom:2px solid #f0f0f0;
    
}
.top div{
	width:1007px;
 
	color:black;
	background-color:#f0f0f0;
	font-family:calibri;
	padding:10px;
	text-align:right;
        height: 19px;
        margin-bottom: 0px;
    }



input[type="text"]{
	padding:12px;
	width:150px;
	height:5px;
}
select{
	padding:12px;
	width:250px;
}
textarea{
	padding:50px;
	width:550px;
}
    .button {
        width: 200px;
        padding: 10px;
        color: black;
        font-size: 15px;
        background-color:#f0f0f0;
        cursor:pointer;

    }
        .button:hover {
            background-color: blue;
            color: white;
        }
        body{
            background-image:url("/images/cq5dam.md.1200.795.jpg");
        }
        #message {
  display:none;
  background: #f1f1f1;
  color: #000;
  position: relative;
  padding: 5px;
  margin-top: 10px;

}

#message p {
  padding: 0px 35px;
  font-size: 16px;
}
  

/* Add a green text color and a checkmark when the requirements are right */
.valid {
  color: green;
}

.valid:before {
  position: relative;
  left: -35px;
  content: "T";
}

/* Add a red text color and an "x" when the requirements are wrong */
.invalid {
  color: red;
}

.invalid:before {
  position: relative;
  left: -35px;
  content: "F";
}

</style>
</head>
<%-----------------------------------------------------------------------------------------------------------------------------------------------------%>
<body > 
    <form runat="server">

	<div class="top">
      <div>
             <asp:Label ID="lblusrnm" runat="server"  Style="float:right; margin-left: 385px; margin-bottom: 0px;" Height="26px"></asp:Label>
          
	</div>
	</div>
<%------------------------------------------------------------------------------------------------------------------------------------------------------ --%>
	<table style="width:98%; margin-left:auto; margin-right:auto;"> 
<tr >
    <td >
        <table style="border-collapse:collapse">
<tr><td><asp:Button CssClass="button" id="btnaddcomp" runat="server"  onclick="btnaddcomp_Click" Text="Add Company"></asp:Button></td> </tr>
<tr><td><asp:Button CssClass="button" ID="btnaddintc" runat="server" OnClick="btnaddintc_Click" Text="Add Internal-control"></asp:Button></td></tr>
               <tr><td style="height:20px;"></td></tr>
<tr><td><asp:Button CssClass="button" id="btndelloc" runat="server"  onclick="btndelloc_Click" Text="Edit/Delete Location"></asp:Button></td> </tr>      
<tr><td><asp:Button CssClass="button" id="btndelun" runat="server"  onclick="btndelun_Click" Text="Edit/Delete Unit"></asp:Button></td> </tr>
<tr><td><asp:Button CssClass="button" id="btndelcp" runat="server"  onclick="btndelcp_Click" Text="Edit/Delete Type"></asp:Button></td> </tr>
<tr><td><asp:Button CssClass="button" id="btndelsv" runat="server"  onclick="btndelsv_Click" Text="Edit/Delete Severity"></asp:Button></td> </tr>
               <tr><td style="height:20px;"></td></tr>
            <tr><td><asp:Button CssClass="button" id="btnaddus" runat="server"  onclick="btnaddus_Click" Text="Add user"></asp:Button></td> </tr>
<tr><td><asp:Button CssClass="button" id="btneditus" runat="server"  onclick="btneditus_Click" Text="Edit user status"></asp:Button></td> </tr>
                           <tr><td style="height:20px;"></td></tr>
<tr><td><asp:Button CssClass="button" id="btnadddat" runat="server"  onClick="btnadddat_Click" Text="Add/View Data"></asp:Button></td> </tr>
<tr><td><asp:Button CssClass="button" id="btnresetp" runat="server"  onclick="btnresetp_Click" Text="Reset Password"></asp:Button></td> </tr>
<tr><td><asp:Button CssClass="button" id="btnlog" runat="server"  onclick="btnlog_Click" Text="Logout"></asp:Button></td> </tr>
            </table>
         </td>
<%------------------------------------------------------------------------------------------------------------------------------------------------------------ --%>
    <td>
    <asp:Panel CssClass="Panelst" ID="Panel1" runat="server" style="height:290px" visible="false">
        <div style="width:100%; background-color:#6d84b4; padding:10px;">ADD COMPANY</div><br />
		<table><tr><td><label>Company_id:</label></td><td>
            <asp:TextBox ID="ddaddcmp1" runat="server" pattern="(?=.*\d)(?=.*[A-Z]).{1,}" title="Must contain uppercase and numbers only"></asp:TextBox> </td></tr>
               <tr><td><label>Company name:</label></td><td><asp:TextBox ID="ddaddcmp2" runat="server"></asp:TextBox></td></tr>
               <tr><td><label>Type:</label></td><td><asp:TextBox ID="ddaddcmp3" runat="server"></asp:TextBox></td></tr>
             <tr><td><label>Headquarters:</label></td><td><asp:TextBox ID="ddaddcmp4" runat="server"></asp:TextBox></td></tr>
               <tr><td>            </td><td><asp:Button id="buttonadd1"  runat="server" onclick="buttonadd1_Click" Text="Add"></asp:Button></td></tr></table>
        <br />
          <div style="width:100%; background-color: #f0f0f0; padding:10px; text-align:left;" >
                <asp:Button id="buttonex1" runat="server" onclick="buttonex1_Click" Text="Exit"></asp:Button>
              </div><br />
        </asp:Panel>

<%----------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
    <asp:Panel CssClass="Panelst" ID="Panel2" runat="server" style="height:355px" visible="false">
				 <div style="width:100%; background-color: #6d84b4; padding:10px;">ADD INTERNAL-CONTROL</div><br />
        <table><tr><td><label>Company_id:</label></td><td>
            <asp:DropDownList ID="ddaddic1" runat="server" style="width:180px;"></asp:DropDownList></td></tr>
        <tr><td><label>Location:</label></td><td><asp:TextBox ID="ddaddic2" runat="server"></asp:TextBox></td></tr>
        <tr><td><label>Unit:</label></td><td><asp:TextBox ID="ddaddic3" runat="server"></asp:TextBox></td></tr>
        <tr><td><label>Type:</label></td><td>
            <asp:DropDownList ID="ddaddic4" Style="width: 180px;" runat="server"></asp:DropDownList></td></tr>
         <tr><td><label>Severity:</label></td><td>
             <asp:DropDownList ID="ddaddic5" Style="width: 180px;" runat="server"></asp:DropDownList></td></tr></table>
            <asp:Button id="buttonadd2" runat="server" onclick="buttonadd2_Click" Text="Add"></asp:Button>
        <br />
        <br />
          <div style="width:100%; background-color: #f0f0f0; padding:10px; text-align:left;" >
                <asp:Button id="buttonex2" runat="server" onclick="buttonex2_Click" Text="Exit"></asp:Button>
              </div><br />
        </asp:Panel>

<%-- --------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
    <asp:Panel CssClass="Panelst" ID="Panel5" style="height:415px" runat="server" visible="false">
        	 <div style="width:100%; background-color: #6d84b4; padding:10px;">EDIT USER STATUS</div><br />
        <table style="width:90%; margin-left:auto; margin-right:auto">
            <tr>
				<td><label>SSN:</label></td><td>
                    <asp:DropDownList ID="userdet1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="userdet1_SelectedIndexChanged"></asp:DropDownList></td></tr>
              <tr><td>First name:</td><td>
                  <asp:TextBox ID="userdet3" style="width:220px;" Enabled="false" runat="server"></asp:TextBox>
                </td></tr>
                <tr><td>Last name:</td><td>
                    <asp:TextBox ID="userdet4" style="width:220px;" Enabled="false" runat="server"></asp:TextBox>
                </td></tr>
            <tr><td>City:</td><td>
                <asp:TextBox ID="userdet5" Style="width: 220px;" Enabled="false" runat="server"></asp:TextBox></td></tr>
              <tr><td>Salary:</td><td>
                <asp:TextBox ID="userdet6" style="width:220px;" Enabled="false" runat="server"></asp:TextBox></td></tr>
            <tr><td><label>Status:</label></td><td>
                    <asp:DropDownList ID="userdet2" runat="server"><asp:ListItem Text="SELECT"></asp:ListItem><asp:ListItem Text="active" Value="0"></asp:ListItem><asp:ListItem Text="inactive" Value="1"></asp:ListItem><asp:ListItem Text="delete" Value="2"></asp:ListItem></asp:DropDownList></td></tr>
      <tr><td>    </td><td>  <asp:Button ID="buttonset2"  runat="server" OnClick="buttonset2_Click" Text="Edit" /><asp:Button ID="buttonset" onclick="buttonset_Click" runat="server" Text="Set" /></td></tr> 
       </table>
      
        <br />
        <br />
      
          <div style="width:100%; background-color: #f0f0f0; padding:10px; text-align:left;" >
                <asp:Button id="buttonex5" runat="server" onclick="buttonex5_Click" Text="Exit"></asp:Button>
              </div><br />
                
        </asp:Panel>

<%---------------------------------------------------------------------------------------------------------------------------------------------------------------------------%>

    <asp:Panel  CssClass="Panelst"  ID="Panel6" style="height:225px" runat="server" visible="false">
         <div style="width:100%; background-color: #6d84b4; padding:10px;">Edit/Delete LOCATION</div><br />
        <table>
            <tr><td> <label>Location:</label></td><td><asp:DropDownList ID="ddloc" runat="server"></asp:DropDownList></td></tr>
             <tr><td>                        </td><td><asp:TextBox style="width:220px;" ID="ddloctxt" runat="server" Enabled="false"></asp:TextBox></td></tr>
        </table>
              
        
        <asp:Button ID="buttonupdt1" runat="server" onclick="buttonupdt1_Click" Text="Edit" /> <asp:Button id="buttondel1" runat="server" onclick="buttondel1_Click" Text="Delete"></asp:Button>
        <br />
        <br />
          <div style="width:100%; background-color: #f0f0f0; padding:10px; text-align:left;" >
                <asp:Button id="buttonex6" runat="server" onclick="buttonex6_Click" Text="Exit"></asp:Button>
              </div><br />
        </asp:Panel>

<%----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------%>

    <asp:Panel CssClass="Panelst" ID="Panel7" runat="server" style="height:225px" visible="false">
        <div style="width:100%; background-color: #6d84b4; padding:10px;">Edit/Delete UNIT</div><br />
        <table>
         <tr><td><label>Company:</label></td><td><asp:DropDownList ID="ddun" runat="server"></asp:DropDownList></td></tr>
          <tr><td>                    </td><td> <asp:TextBox ID="dduntxt" style="width:220px;" runat="server" Enabled="false"></asp:TextBox></td></tr></table>
        
        <asp:Button ID="buttonupdt2" runat="server" onclick="buttonupdt2_Click" Text="Edit" /> <asp:Button id="buttondel2" runat="server" onclick="buttondel2_Click" Text="Delete"></asp:Button>
        <br />
        <br />
          <div style="width:100%; background-color: #f0f0f0; padding:10px; text-align:left;" >
                <asp:Button id="buttonex7" runat="server" onclick="buttonex7_Click" Text="Exit"></asp:Button>
              </div><br />
        </asp:Panel>

<%----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
    <asp:Panel CssClass="Panelst" ID="Panel8" style="height:225px;width:360px;" runat="server" visible="false">
        
          <div style="width:100%; background-color: #6d84b4; padding:10px;">Edit/Delete TYPE</div><br />
        <table><tr><td>
                <label>Control Points:</label></td><td><asp:DropDownList ID="ddcp" runat="server"></asp:DropDownList></td></tr>
            <tr><td>                    </td><td>
                <asp:TextBox ID="ddcptxt" style="width:220px;" Enabled="false" runat="server"></asp:TextBox></td></tr></table>
        <asp:Button ID="buttonupdt4" OnClick="buttonupdt4_Click" runat="server" Text="Edit" /> <asp:Button id="buttondel3" runat="server" onclick="buttondel3_Click" Text="Delete"></asp:Button>
        <br />
        <br />
          <div style="width:100%; background-color: #f0f0f0; padding:10px; text-align:left;" >
                <asp:Button id="buttonex8" runat="server" onclick="buttonex8_Click" Text="Exit"></asp:Button>
              </div><br />
        </asp:Panel>

<%--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
    <asp:Panel  CssClass="Panelst" ID="Panel9" style="height:225px" runat="server" visible="false">
             <div style="width:100%; background-color: #6d84b4; padding:10px;">Edit/Delete SEVERITY</div><br />
        <table>
               <tr><td> <label>Severity:</label></td><td><asp:DropDownList ID="ddsv" runat="server"></asp:DropDownList></td></tr>
            <tr><td>                             </td><td><asp:TextBox ID="ddsvtxt" style="width:220px;" runat="server" Enabled="false"></asp:TextBox></td></tr></table>
       
            <asp:Button ID="buttonupdt3" onclick="buttonupdt3_Click" runat="server" Text="Edit" />  <asp:Button id="buttondel4" runat="server" onclick="buttondel4_Click" Text="Delete"></asp:Button>
        <br />
        <br />
          <div style="width:100%; background-color: #f0f0f0; padding:10px; text-align:left;" >
                <asp:Button id="buttonex9" runat="server" onclick="buttonex9_Click" Text="Exit"></asp:Button>
            
              </div><br />
        </asp:Panel>

<%--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------%>

          <asp:Panel  CssClass="Panelst" ID="Panel10" style="height:335px; width:600px;" runat="server" visible="false">
             <div style="width:100%; background-color: #6d84b4; padding:10px;">ADD USER</div><br />
              <table>
               <tr><td><label> First Name:</label></td><td> <asp:TextBox ID="txtregfname" runat="server"></asp:TextBox></td>
                  <td><label>Last Name:</label></td><td><asp:TextBox ID="txtreglname" runat="server"></asp:TextBox></td></tr>
                 <tr><td><label>SSN:</label></td><td> <asp:TextBox ID="txtreguser" runat="server" pattern="(?=.*\d)(?=.*[A-Z]).{1,}" title="Must contain uppercase and numbers only"></asp:TextBox></td>
                   <td><label>City:</label></td><td> <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td></tr>
                   <tr><td><label>Salary:</label></td><td> <asp:TextBox ID="TextBox2" runat="server" pattern="(?=.*\d).{1,}" title="Must contain integer values only"></asp:TextBox></td>
                  <td><label>Gender:</label></td><td><asp:DropDownList ID="txtreggen"  style="padding:8px; width:180px;" runat="server"><asp:ListItem Text="SELECT" Value="0"></asp:ListItem><asp:ListItem Text="Male" Value="1"></asp:ListItem><asp:ListItem Text="Female" Value="2"></asp:ListItem></asp:DropDownList></td></tr>
                   <tr><td><label>Privilege:</label></td><td><asp:DropDownList ID="txtregprev"  style="padding:8px; width:180px;" runat="server"><asp:ListItem Text="SELECT" Value="0"></asp:ListItem><asp:ListItem Text="Auditor" Value="1"></asp:ListItem><asp:ListItem Text="Audities" Value="2"></asp:ListItem></asp:DropDownList></td>
                   <td><label>Password:</label></td><td><asp:TextBox ID="txtregpass" style="height:28px; width:175px;" TextMode="Password" runat="server" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@$*^!#&]).{8,}" title="Must contain at least one number and one uppercase and lowercase letter and a special character, and at least 8 or more characters" ></asp:TextBox></td></tr>
                  <tr><td></td><td></td><td><label>Confirm Password:</label></td><td><asp:TextBox ID="txtregconpass" style="height:28px; width:175px;" TextMode="Password" runat="server" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@$*^!#&]).{8,}" title="Must contain at least one number and one uppercase and lowercase letter and a special character, and at least 8 or more characters"  ></asp:TextBox></td></tr>
                   <tr><td></td><td></td><td>
                       <asp:Button ID="btnregsub" onclick="btnregsub_Click" runat="server" Text="Submit" CausesValidation="false" /></td></tr>
                  </table>
              <br />
          <div style="width:100%; background-color: #f0f0f0; padding:10px; text-align:left;" >
                <asp:Button id="buttonex10" runat="server" onclick="buttonex10_Click" Text="Exit"></asp:Button>
            
              </div><br />
        </asp:Panel>

<%------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------%>

         <asp:Panel  CssClass="Panelst" ID="Panel11" style="height:275px; width:350px;" runat="server" visible="false">
             <div style="width:100%; background-color: #6d84b4; padding:10px;">RESET PASSWORD</div><br />
              <table>
                  <tr><td><label>Username:</label></td><td> <asp:TextBox ID="rstusername" runat="server"></asp:TextBox></td></tr>
                   <tr><td><label>Old Password:</label></td><td><asp:TextBox ID="rstoldpass" style="height:28px; width:175px;" TextMode="Password" runat="server" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@$*^!#&]).{8,}" title="Must contain at least one number and one uppercase and lowercase letter and a special character, and at least 8 or more characters" ></asp:TextBox></td></tr>
                  <tr><td><label>New Password:</label></td><td><asp:TextBox ID="rstnewpass" style="height:28px; width:175px;" TextMode="Password" runat="server" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@$*^!#&]).{8,}" title="Must contain at least one number and one uppercase and lowercase letter and a special character, and at least 8 or more characters"  ></asp:TextBox></td></tr>
                  <tr><td><label>Confirm Password:</label></td><td><asp:TextBox ID="rstconpass" style="height:28px; width:175px;" TextMode="Password" runat="server" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@$*^!#&]).{8,}" title="Must contain at least one number and one uppercase and lowercase letter and a special character, and at least 8 or more characters"  ></asp:TextBox></td></tr>

               <tr><td>                            </td><td><asp:Button id="resetbtn" runat="server" onclick="resetbtn_Click" Text="Submit"></asp:Button></td></tr> 
              </table>
          <div style="width:100%; background-color: #f0f0f0; padding:10px; text-align:left;" >
                <asp:Button id="buttonex11" runat="server" onclick="buttonex11_Click" Text="Exit"></asp:Button>
            
              </div><br />
        </asp:Panel>
<%----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------%>           
        </td>
  <td>
         <div id="message" style="position:absolute; margin-left:-500px; margin-top:-45px;">
                    <h3>Password must contain the following:</h3>
                      <p id="letter" class="invalid">A <b>lowercase</b> letter</p>
                       <p id="capital" class="invalid">A <b>capital (uppercase)</b> letter</p>
                       <p id="number" class="invalid">A <b>number</b></p>
                       <p id="characters" class="invalid">A <b>special characters</b></p>
                        <p id="length" class="invalid">Minimum <b>8 characters</b></p>
                    </div>
    </td>
    </tr>
        </table>
        </form>
	</body>
<%-- --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
 <script>
     var myInput = document.getElementById("txtregpass");
       
     var letter = document.getElementById("letter");
var capital = document.getElementById("capital");
var number = document.getElementById("number");
var characters=document.getElementById("characters");
     var length = document.getElementById("length");
      
         myInput.onfocus = function () {
             document.getElementById("message").style.display = "block";
         }

         // When the user clicks outside of the password field, hide the message box
         myInput.onblur = function () {
             document.getElementById("message").style.display = "none";
         }
         // When the user starts to type something inside the password field
         myInput.onkeyup = function () {
             // Validate lowercase letters
             var lowerCaseLetters = /[a-z]/g;
             if (myInput.value.match(lowerCaseLetters)) {
                 letter.classList.remove("invalid");
                 letter.classList.add("valid");
             } else {
                 letter.classList.remove("valid");
                 letter.classList.add("invalid");
             }

             // Validate capital letters
             var upperCaseLetters = /[A-Z]/g;
             if (myInput.value.match(upperCaseLetters)) {
                 capital.classList.remove("invalid");
                 capital.classList.add("valid");
             } else {
                 capital.classList.remove("valid");
                 capital.classList.add("invalid");
             }

             // Validate numbers
             var numbers = /[0-9]/g;
             if (myInput.value.match(numbers)) {
                 number.classList.remove("invalid");
                 number.classList.add("valid");
             } else {
                 number.classList.remove("valid");
                 number.classList.add("invalid");
             }
             var cs = /[@$*^!#&]/g;
             if (myInput.value.match(cs)) {
                 characters.classList.remove("invalid");
                 characters.classList.add("valid");
             } else {
                 characters.classList.remove("valid");
                 characters.classList.add("invalid");
             }

             // Validate length
             if (myInput.value.length >= 8) {
                 length.classList.remove("invalid");
                 length.classList.add("valid");
             } else {
                 length.classList.remove("valid");
                 length.classList.add("invalid");
             }
         }
</script> 
</html>
 

