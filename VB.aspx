<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VB.aspx.vb" Inherits="VB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
            </br>
    </br>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:DropDownList ID="ddlFruits" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged">
                <asp:ListItem Text="Mango" Value="1" />
                <asp:ListItem Text="Apple" Value="2" />
                <asp:ListItem Text="Banana" Value="3" />
                <asp:ListItem Text="Guava" Value="4" />
                <asp:ListItem Text="Orange" Value="5" />
            </asp:DropDownList>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlFruits" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
