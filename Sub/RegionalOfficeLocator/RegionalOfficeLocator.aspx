<%@ Page Title="" Language="C#" MasterPageFile="~/Sub/ApplicantRegi/ApplicantRegistrationMaster.master"
    AutoEventWireup="true" CodeFile="RegionalOfficeLocator.aspx.cs" Inherits="Sub_RegionalOfficeLocator_RegionalOfficeLocator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidCSRF" runat="server" value=""/>
    <table align="center" class="SubFormWOBG" style="line-height:25px" width="100%">
        <tr>
            <th colspan="7">
                <div class="div_IndAppHeading" style="padding-left:10px;font-size:18px;">
                    Regional Office Locator
                </div>
            </th>
        </tr>
        <tr>
            <td colspan="2">
                State:
            </td>
            <td colspan="5">
                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" Width="350px"
                    OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" InitialValue=" "
                    Display="Dynamic" ControlToValidate="ddlState" ForeColor="Red" ValidationGroup="DistrictOfficeLocator">Please select State</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Address
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtAddress" TextMode="MultiLine" runat="server" Height="164px" ReadOnly="true" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="7">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th style="font-size:15px" colspan="7">
                Regional Office List
            </th>
        </tr>
        <tr>
            <th>S.No</th>
            <th>State/UT</th>
            <th>Authorised Officer</th>
            <th>Address</th>
            <th>Contact No.</th>
            <th>Email </th>
            <th>Alternate Email</th>
        </tr>
        <tr>
            <td>1</td>
            <td>Punjab & Haryana</td>
            <td>Regional Director</td>
            <td>Central Ground Water Board, North Western Region, Bhujal Bhawan, Plot No. 3B, Sector 27-A, Chandigarh  160019</td>
            <td>Tel: 0172-5021960, 5021961 <br /> Fax: 0172-2639500</td>
            <td>rdnwr-cgwb[at]nic[dot]in</td>
            <td>cgwanwr-cgwb[at]nic[dot]in</td>
        </tr>
        <tr>
            <td>2</td>
            <td>Rajasthan</td>
            <td>Regional Director</td>
            <td>Central Ground Water Board, Western Region,6-A, Jhalana Doongri, Jaipur 302004, Rajasthan.</td>
            <td>Tel: 0141-2706338, <br />Fax: 0141-2706991</td>
            <td>rdwr-cgwb[at]nic[dot]in</td>
            <td>cgwawr-cgwb[at]nic[dot]in</td>
        </tr>
        <tr>
            <td>3</td>
            <td>Uttar Pradesh</td>
            <td>Regional Director </td>
            <td>Central Ground Water Board, Northern Region, Bhujal Bhavan, Sector-B. Sitapur Road Yojna, Ram Ram Bank Chauraha, Lucknow - 226021.</td>
            <td> Tel: 0522-2363812, 2360494-98<br /> Fax: 0522-2363820</td>
            <td> rdnr-cgwb[at]nic[dot]in</td>
            <td>cgwanr-cgwb[at]nic[dot]in</td>
        </tr>
        <tr>
            <td>4</td>
            <td>Uttrakhand</td>
            <td>Regional Director</td>	
            <td> Central Ground Water Board, Uttarakhand Region, 419-A, Kanwali Road, Baluwala , Near Urja Bhawan, Dehradun - 248001, Uttarakhand</td>	
            <td>Tel: 0135-2761675, 2769533, 2621298 <br />Fax: 0135-2769525</td>
            <td> rdur-cgwb[at]nic[dot]in</td>
            <td>cgwaur-cgwb[at]nic[dot]in</td>
        </tr>
        <tr>
            <td>5</td>
            <td>Gujarat,<br />Daman and Diu</td>
            <td>Regional Director</td>
            <td>Central Ground Water Board, West Central Region, Swami Narayan College Building, Shah Alam Tolnaka, Ahmedabad - 380 022 Gujarat</td>	
            <td>Tel: 079-25320476, 25396007,  25394464 <br />Fax: 079- 25329379</td>
            <td>rdwcr-cgwb[at]nic[dot]in</td>
            <td>cgwawcr-cgwb[at]nic[dot]in</td>
        </tr>
        <tr>
            <td>6</td>
            <td>Maharashtra & UT of Dadra & Nagar Haveli</td>
            <td> Regional Director</td>
            <td>Central Ground Water Board, Central Region, N.S. Building, Civil Lines, Nagpur  440001 Maharashtra</td>
            <td>Tel: 0712-2550646 <br />Fax: 0712-2564390</td>
            <td>rdcr-cgwb[at]nic[dot]in</td>
            <td>cgwacr-cgwb[at]nic[dot]in</td>
        </tr>
        <tr>
            <td>7</td>
            <td>Madhya Pradesh</td>
            <td>Regional Director</td>
            <td>Central Ground Water Board, North Central Region, Block-1, 4th Floor, Paryawas Bhawan Area Hills, Jail Road, Bhopal - 462011, Madhya Pradesh</td>
            <td>Tel: 0755-2557639, <br />Fax: 0755-2760090</td>
            <td>rdncr-cgwb[at]nic[dot]in</td>
            <td>cgwancr-cgwb[at]nic[dot]in</td>
        </tr>
        <tr>
            <td>8</td>
            <td>Chhattisgarh</td>
            <td>Regional Director</td>
            <td>Central Ground Water Board, North Central Chhattisgarh Region, 
                <br />
                2nd Floor, LK Corporate and Logistic Park Dumartarai, Raipur-492015&nbsp; Chattisgarh.</td>
            <td>Tel: 0771-2413903, <br />Fax: 0771-2413689</td>
            <td> rdnccr-cgwb[at]nic[dot]in </td>
            <td>cgwanccr-cgwb[at]nic[dot]in</td>
        </tr>
        <tr>
            <td>9</td>
            <td>Odisha</td>	
            <td>Regional Director</td>
            <td>Central Ground Water Board, South Eastern Region, Bhujal Bhawan, Khandagiri Square, NH-5,Bhubaneshwar  751001, Odisha</td>
            <td>Tel: 0674-2350342<br />Fax: 0674-2350332</td>
            <td>rdser-cgwb[at]nic[dot]in</td>
            <td>cgwaser-cgwb[at]nic[dot]in</td>
        </tr>
        <tr>
            <td>10</td>
            <td>Sikkim & UT of Andaman & Nicobar Islands</td>
            <td>Regional Director</td>
            <td>Central Ground Water Board, Eastern Region, Bhujalika, C P Block-6, Sector-V, Bidhan Nagar Kolkata - 700 091, West Bengal</td>	
            <td>Tel: 033-23673080, 23673081 <br />Fax: 033-23673080</td>
            <td>rder-cgwb[at]nic[dot]in </td>
            <td>cgwaer-cgwb[at]nic[dot]in</td>
        </tr>
        <tr>
            <td>11</td>
            <td>Assam, Arunachal Pradesh,Manipur,<br />Meghalaya Mizoram, Nagaland &amp;Tripura</td>
            <td>Regional Director</td>
            <td>Central Ground Water Board North Eastern Region, Guwahati, Betkuchi, Opposite ISBT, NH-37, Guwahati, Assam. 781035.</td>
            <td>Tel: 0361-2456298, 2134452<br /> Fax: 0361-2455497 </td>
            <td>rdner-cgwb[at]nic[dot]in</td>
            <td>cgwaner-cgwb[at]nic[dot]in</td>
        </tr>
        <tr>
            <td>12</td>
            <td>Bihar & Jharkhand</td>	
            <td>Regional Director</td>
            <td>Central Ground Water Board, Mid Eastern Region, 6th& 7th Floor, Lok Nayak Jai Prakash Bhawan, Frazer Road, Dak Banglow, Patna- 800011, Bihar.</td>	
            <td>Tel: 0612-2231785, 2201076, 2205435, 2231020</td>
            <td> rdmer-cgwb[at]nic[dot]in</td>
            <td>cgwamer-cgwb[at]nic[dot]in</td>
        </tr>

        <tr>
            <td>13</td>
            <td>Andhra Pradesh & Telangana</td>	
            <td>Regional Director</td>
            <td>Central Ground Water Board, Southern Region, 3-6-291, GSI Post, Bandlaguda, Hyderabad, Telangana. <br /> PIN Code- 
                500068.</td>	
            <td>Tel: 040-24225200 <br /> Fax: 040-24225202</td>
            <td> </td>
            <td> </td>
        </tr>


    </table>
</asp:Content>
