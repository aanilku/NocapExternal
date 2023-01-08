
//For Checking valid date at Client Side

function ValidateDateOnClient(sender, args) {

    var dateString = document.getElementById(sender.controltovalidate).value;
    var regex = /(((0|1)[1-9]|1[0]|2[0]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$/;

    if (regex.test(dateString)) {
        var parts = dateString.split("/");
        var dt = new Date(parts[1] + "/" + parts[0] + "/" + parts[2]);

        args.IsValid = (dt.getDate() == parts[0] && dt.getMonth() + 1 == parts[1] && dt.getFullYear() == parts[2]);


    }

    else {
        args.IsValid = false;


    }
}
  