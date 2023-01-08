<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderOnly.ascx.cs" Inherits="AscxControl_HeaderOnly" %>
<link type="text/css" rel="Stylesheet" href="../css/ExternalUserCss.css" />
<style type="text/css">
    .style2
    {
        width: 100%;
    }
</style>
<table align="center" cellpadding="0" cellspacing="0" class="style2" bgcolor="#F0F0F0">
    <tr>
        <td valign="bottom">
            <div class="div_header">
                <div class="div_header_left">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/satyamevjayate_Logo.png"
                        Width="81px" Height="110px" />
                </div>
            </div>
        </td>
        <td>
            <div class="logo_txt">
                <div class="div_header_line1">
                    Government of India<br />
                    Ministry of Jal Shakti<br />
                    Department of Water Resources, River Development and Ganga Rejuvenation</div>
                <div class="div_header_line2">
                    Central Ground Water Authority (CGWA)
                </div>
                <br />
                <div class="div_header_line3">
                    <strong>Application for Issue of NOC to Abstract Ground Water (NOCAP)</strong></div>
            </div>
        </td>
        <td align="right">
            <div class="div_header_right" align="right">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/logocgwb.png" />
            </div>
        </td>
    </tr>
</table>
