function showError(divid) {
    if (divid) {
        $("#"+divid).html("Some error occurred! Please try again.");
        $("#"+divid).attr("class", "alert alert-danger");
    }
    else {
        alert("Some error occurred! Please try again.");
    }
}
var parseBool = function (str) {
    if (str == null)
        return false;

    if (typeof str === 'boolean') {
        if (str === true)
            return true;

        return false;
    }

    if (typeof str === 'string') {
        if (str == "")
            return false;

        str = str.replace(/^\s+|\s+$/g, '');
        if (str.toLowerCase() == 'true' || str.toLowerCase() == 'yes')
            return true;

        str = str.replace(/,/g, '.');
        str = str.replace(/^\s*\-\s*/g, '-');
    }

    // var isNum = string.match(/^[0-9]+$/) != null;
    // var isNum = /^\d+$/.test(str);
    if (!isNaN(str))
        return (parseFloat(str) != 0);

    return false;
}
function showMessage(divid, message, mode) {
    /*success
    info
    warning
    danger*/
    if ($("#"+divid)) {
        $("#"+divid).attr("class", "alert alert-" + mode);
        $("#"+divid).html(message)
    }
    else {
        alert(message);
    }
}
function showException(exceptionObject, status, headers, config)
{
    // To be implemented 
    //if(status=="404") invalid URL, Page not found
    //if(status=="401") Unauthorized, Session Expired
    //if(status=="500") Internal Error
    if (status == 401) // session expired
        location.href = "/account/login?ReturnUrl=" + location.pathname;
    else {
        var message = "<div class='text-danger'>";
        message += "<h1>Oops some thing went wrong</h1>";
        message += "<p>Sorry, we are not able to process your reqiest due to some unexpected error...</p>";
        message += "<p>Please re-check if you can perform the same by doing the action again. Click here if you want to go to <a href='/'>Homepage</a>?</p>";
       
        // more details of exception
        if (exceptionObject.ExceptionMessage)
            message+="<div class='text-info'>Server returned : <br/>"+ exceptionObject.ExceptionMessage+"</div>";
        else if (exceptionObject.Message)
            message += "<div class='text-info'>" + exceptionObject.Message +"</div>";
        message += "</div>";
        bootbox.alert(message);
    }
    
}



function checkActiveDisplay(status)
{
    if (status == true)
        return "<span class='glyphicon glyphicon-ok'></span>";
    else
        return "<span class='glyphicon glyphicon-remove'></span>";
}

function setModalDialogData(modalContent)
{
    $("#panelModalContent").html(modalContent);
}
function nullToBlank(str)
{
    if (!str)
        return "";
    else
        return str;
}
function showModalDialog(title) {
    $("#modalTitle").html(title);
    $('#panelModal').modal({
        keyboard: false,
        backdrop: 'static'

    })
}
function bindEnterEventOnTable(tableId,tableObject)
{
    $('#' + tableId +'_filter input').unbind();
    $('#'+ tableId + '_filter input').bind('keyup', function (e) {
        if (e.keyCode == 13)
            tableObject.fnFilter(this.value);
    });
}
