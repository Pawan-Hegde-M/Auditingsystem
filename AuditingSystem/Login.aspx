<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AuditingSystem.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
     <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>

<%-----------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
  <style>
  <meta charset="utf-8">
  * { margin: 0px; padding: 0px; }

body {
  font-size: 120%;
            background-image:url("/images/cq5dam.md.1200.795.jpg");
}
.header {
  width: 25%;
  margin: 150px auto 0px;
  /*color*/: white;
  background:blue;
  text-align: center;
  border: 1px solid #B0C4DE;
  border-bottom: none;
  border-radius: 10px 10px 0px 0px;
  padding: 20px;
  padding-top:2px;
  padding-bottom:2px;
}
form, .content {
  width: 25%;
  margin: 0px auto;
  padding: 20px;
  border: 1px solid #B0C4DE;
  background: white;
  opacity:.8;
  border-radius: 0px 0px 10px 10px;
}
.input-group {
  margin: 10px 5px 10px 5px;
}
.input-group label {
  display: block;
  text-align: center;
  margin: 3px;
}
.input-group input {
  height: 30px;
  width: 93%;
  padding: 5px 10px;
  font-size: 16px;
  border-radius: 5px;
  border: 1px solid gray;
}
#user_type {
  height: 40px;
  width: 98%;
  padding: 5px 10px;
  background: white;
  font-size: 16px;
  border-radius: 5px;
  border: 1px solid gray;
}
.btn {
  padding: 10px;
  font-size: 15px;
  color: white;
  background: blue;
  border: none;
  border-radius: 5px;
}
.btn:hover{
    background: grey;
	color: black;
}
</style>
</head>
<%--------------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
<body>
  <div class="header">
			
				<h1 style="height:20px;">AUDITING SYSTEM</h1>
			
			</div>
		<form runat="server">
  <div class="input-group">
    <label>Username</label>
      <asp:TextBox ID="txtusrname" runat="server" ></asp:TextBox>
  </div>
  
     <div class="input-group">
    <label> Password</label><asp:TextBox ID="txtpsw" TextMode="Password" runat="server" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@$*^!#&]).{8,}" title="Must contain at least one number and one uppercase and lowercase letter and a special character, and at least 8 or more characters" ></asp:TextBox>
   
  </div>

  <div class="loginBox" align="center">
      <asp:Button ID="loginbtn" CssClass="btn" runat="server" onclick="loginbtn_Click" Text="Login" />
  </div>

            <asp:LinkButton ID="frgtpassbtn" style="float:right;" runat="server" Text="Forgot-Password" OnClick="frgtpassbtn_Click" />
            <br />
        </form>
</body>
</html>
    
