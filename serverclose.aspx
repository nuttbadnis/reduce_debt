<%@ Page Language="VB" AutoEventWireup="false" CodeFile="serverclose.aspx.vb" Inherits="SpacialPayin_serverclose" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>อยู่ในระหว่างจัดเตรียมระบบ</title>
</head>
<body>
    <form id="form1" runat="server">
        <center>
                <table width="500">
                    <tr>
                        <td>
                            <br />
                            <br />
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Cordia New" Font-Size="16pt"
                                ForeColor="Red" Text="ระบบ Feasibility จะเปิดใช้งานจริง ในวันจันทร์ที่ 16/11/2020 เวลา 14.00 น."></asp:Label><br />
                            <br />
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Cordia New" Font-Size="16pt"
                                ForeColor="Red" Text="ขออภัยในความไม่สะดวก"></asp:Label><br />
                            <br />
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/under-construction.png" /></td>
                    </tr>
                </table>
        </center>
        <input id="rbtApprove" value="อนุมัติ" type="radio" checked="checked"/>อนุมัติ &nbsp;
        &nbsp;<input id="Radio2" type="radio" />
    </form>
</body>
</html>
