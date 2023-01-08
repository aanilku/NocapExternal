<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AttachmentLimit.ascx.cs"
    Inherits="AscxControl_AttachmentLimit" %>
<div>
    <table>
        <tr>
            <td style="text-align: right">
                Maximum Number of Attachment Allowed-<asp:Label ID="lblAttachment" runat="server"
                    Text=""></asp:Label>
                <br />
                Maximum Size of each Attachment Allowed-
                <asp:Label ID="lblSize" runat="server" Text=""></asp:Label><span style="margin-left: 3px">MB</span><br />
            </td>
        </tr>
    </table>
</div>
